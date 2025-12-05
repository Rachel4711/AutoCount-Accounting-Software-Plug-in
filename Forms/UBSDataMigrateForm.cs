using AutoCount.BackupService;
using AutoCount.Data.EntityFramework;
using DbfDataReader;
using Microsoft.Extensions.Options;
using PlugIn_1.Entity;
using PlugIn_1.Entity.Accounts;
using PlugIn_1.Entity.General_Maintainance;
using PlugIn_1.Entity.Stock;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Creditor   = PlugIn_1.Entity.Creditor;
using DbfData = DbfDataReader.DbfDataReader;
using Debtor     = PlugIn_1.Entity.Debtor;
using ItemGroups = PlugIn_1.Entity.Stock.ItemGroups;
using Locations  = PlugIn_1.Entity.Stock.Locations;

namespace PlugIn_1.Forms
{
    public partial class UBSDataMigrateForm : Form
    {
        private string exception { get; set; }

        private string successes { get; set; }

        private readonly string db_name = Program.session.DBSetting.DBName.ToString();

        private bool isSyncingRange = false;

        private bool isPathChanging = false;

        // Dictionary to relate the table name to the correspond file name
        private readonly Dictionary<string, string> name_to_file = new Dictionary<string, string>()
        {
            {"Chart of Account" , "gldata"},
            {"Sales Agent"      , "icagent"},
            {"Purchase Agent"   , "icagent"},
            {"Area"             , "icarea"},
            {"Project"          , "project"},
            {"Currency"         , "currency"},
            {"Customer"         , "arcust"},
            {"Supplier"         , "apvend"},
            {"Item"             , "icitem"},
            {"Category"         , "iccate"},
            {"Group"            , "icgroup"},
            {"Location"         , "iclocate"}
        };

        // Array to declare table name classified within stock
        private readonly string[] stk_table_name =
        {
            "Purchase Agent",
            "Category",
            "Group",
            "Item",
            "Location",
            "icl3p"
        };

        public UBSDataMigrateForm()
        {
            InitializeComponent();

            btn_import.Cursor = Cursors.Hand;           // Indicate that "IMPORT" button is important

            Text = $"Migrate UBS Account ({db_name})";  // Show the DB name
        }

        //UI Elements and controls----------------------------------------------------------------------------//

        private void btn_folderHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Guides to select backup folders:\n\n" +
                "1. Use 7zip or any other file compresser software to extract .ACC file to a new folder, and .STK file to another new folder.\n" +
                "2. Choose the extracted folders for the corresponding folder path entry.",
                "Data Import Help Guide",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_rangeHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Guides to set record importing range:\n\n" +
                "1. Click on the checkbox on the left to select a table to import.\n" +
                "2. Select the entire row by click on most left side of the row.\n" +
                "3. Leave both value to 0 to import all records in the table.\n" +
                "4. Set both value to the same number to import single record.\n" +
                "5. The valid value: start <= end <= total records in table.\n\n" +
                "Tips: Setting record range for multiple tables is allowed.",
                "Import Range Setting Help Guide", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_browseAccFolder_Click(object sender, EventArgs e)
        {
            OpenFolderDialog("Select UBS Account Folder", txt_path_AccFolder);
        }

        private void btn_browseStkFolder_Click(object sender, EventArgs e)
        {
            OpenFolderDialog("Select UBS Stock Folder", txt_path_StkFolder);
        }

        private void txt_path_AccFolder_TextChanged(object sender, EventArgs e)
        {
            // Enable or stock folder selection only if UBS account folder path is given
            btn_browseStkFolder.Enabled =
            txt_path_StkFolder.Enabled =
            btn_listTable.Enabled = txt_path_AccFolder.Text.Equals("") ? false : true;

            isPathChanging = txt_path_AccFolder.Text.Equals("") ? false : true; // Indicate path changes if not left empty

            txt_path_AccFolder.BackColor = SystemColors.Window;

            if (dgv_selTblImport.RowCount > 0)
            {
                btn_import.BackColor = Color.NavajoWhite;
                idc_warning.Text = "\u26A0";
            }
        }

        private void txt_path_StkFolder_TextChanged(object sender, EventArgs e)
        {
            isPathChanging = true; // Indicate path changes

            txt_path_StkFolder.BackColor = SystemColors.Window;

            if (dgv_selTblImport.RowCount > 0)
            {
                btn_import.BackColor = Color.NavajoWhite;
                idc_warning.Text = "\u26A0";
            }
        }

        private void nud_recRangeStart_Click(object sender, EventArgs e)
        {
            nud_recRangeStart.Maximum = nud_recRangeEnd.Value; // Record start <= Record End
        }

        private void nud_recRangeEnd_Click(object sender, EventArgs e)
        {
            nud_recRangeStart.Maximum = nud_recRangeEnd.Value; // Record start <= Record End
        }

        private void nud_recRangeStart_ValueChanged(object sender, EventArgs e)
        {
            // Set the data record range through controls only if not syncing range to the control
            if (!isSyncingRange) SetRecordRange("Start From Record", nud_recRangeStart);
        }

        private void nud_recRangeEnd_ValueChanged(object sender, EventArgs e)
        {
            // Set the data record range through controls only if not syncing range to the control
            if (!isSyncingRange)
            {
                SetRecordRange("End To Record", nud_recRangeEnd);
                CalibrateRecordRange(); // Calibrate the value of Record start

                // Make sure Record start is always <= Record End
                if (nud_recRangeStart.Value > nud_recRangeEnd.Value)
                    nud_recRangeStart.Value = nud_recRangeEnd.Value;
            }
            else
            {
                // Make sure the min value of Record start set correctly when select through records
                nud_recRangeStart.Minimum = nud_recRangeEnd.Value > 0 ? 1 : 0;
            }
        }

        private void dgv_selTblImport_Click(object sender, EventArgs e)
        {
            // Enable record range setting when at least 1 row selected and checkbox of master row is checked
            gb_recRangeSet.Enabled = 
                dgv_selTblImport.SelectedRows.Count > 0 && !(bool)dgv_selTblImport[0, 0].Value ? 
                true : false;
        }

        private void dgv_selTblImport_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SyncRecordRange();
        }

        private void dgv_selTblImport_SelectionChanged(object sender, EventArgs e)
        {
            SyncRecordRange();
        }

        private void dgv_selTblImport_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Make sure data grid view reacts immediately with checkbox selection changes
            if (dgv_selTblImport.IsCurrentCellDirty)
                dgv_selTblImport.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgv_selTblImport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Only apply checkbox selection rule when the first row is the master row
            if (dgv_selTblImport[1, 0].Value.Equals("Chart of Account"))
                SetCheckBoxSelection(e);

            // Enable record range setting when at least 1 row selected and checkbox of master row is checked
            gb_recRangeSet.Enabled =
                dgv_selTblImport.SelectedRows.Count > 0 && !(bool)dgv_selTblImport[0, 0].Value ?
                true : false;

            // If checkbox of master row is checked
            if ((bool)dgv_selTblImport[0, 0].Value)
            {
                isSyncingRange = true;  // Indicate record range sync

                // Reset data record range of other rows in data grid view
                foreach (DataGridViewRow row in dgv_selTblImport.Rows)
                {
                    ResetRecordRange(row);
                }

                nud_recRangeStart.Value = nud_recRangeEnd.Value = 0; // Reset value of range setting controls

                isSyncingRange = false; // Close record range sync indication
            }
        }

        private void btn_resetRange_Click(object sender, EventArgs e)
        {
            isSyncingRange = true;  // Indicate record range sync

            // Reset data record range of each selected row(s) in data grid view
            foreach (DataGridViewRow row in dgv_selTblImport.SelectedRows)
            {
                ResetRecordRange(row);
            }

            isSyncingRange = false; // Close record range sync indication

            nud_recRangeStart.Value = nud_recRangeEnd.Value = 0; // Reset value of range setting controls
        }

        private void btn_resetAllRange_Click(object sender, EventArgs e)
        {
            isSyncingRange = true;  // Indicate record range sync

            // Reset data record range of all rows in data grid view
            foreach (DataGridViewRow row in dgv_selTblImport.Rows)
            {
                ResetRecordRange(row, true);
            }

            nud_recRangeStart.Value = nud_recRangeEnd.Value = 0; // Reset value of range setting controls

            isSyncingRange = false; // Close record range sync indication
        }

        private void btn_listTable_Click(object sender, EventArgs e)
        {
            // Check if is the correct folder(s), dismiss the stock text field unless stock folder path has given
            bool isValidAccFolder = ValidateFolders(txt_path_AccFolder.Text, "ACCOUNT");
            bool isValidStkFolder = txt_path_StkFolder.Text.Equals("") ?
                true : (ValidateFolders(txt_path_StkFolder.Text, "STOCK") && 
                !ValidateFolders(txt_path_StkFolder.Text, "ACCOUNT"));

            // Reject if is not the correct folder(s) and show error message
            if (!isValidAccFolder || !isValidStkFolder)
            {
                // Show message based on which folder(s) is incorrect
                string invFileErr = (!isValidAccFolder && !isValidStkFolder) ?
                    "UBS account folder and UBS stock folder" : !isValidAccFolder ?
                    "UBS account folder" : "UBS stock folder";

                string invFileNoun = (!isValidAccFolder && !isValidStkFolder) ? "are" : "is";

                MessageBox.Show(
                    $"Folder(s) from given path(s) {invFileNoun} not a correct \n{invFileErr}.",
                    "Incorrect Backup Folder",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                txt_path_AccFolder.BackColor = !isValidAccFolder ? Color.LightCoral : SystemColors.Window;
                txt_path_StkFolder.BackColor = !isValidStkFolder ? Color.LightCoral : SystemColors.Window;

                return;
            }

            // If the folder path(s) has changed
            if (isPathChanging)
            {
                // Reset the titles
                Text = $"Migrate UBS Account ({db_name})";
                lbl_ImportTitle.Text = "Select and Import Table";

                gb_recRangeSet.Enabled = false; // Reset disabled record range setting

                // Extract company name from the memory file in the folder(s) from given path(s)
                string company_name = ExtractComName(txt_path_AccFolder.Text, "ACCOUNT");
                string compare_name = txt_path_StkFolder.Text.Equals("") ?
                    company_name : ExtractComName(txt_path_StkFolder.Text, "STOCK");

                // If folders from the same company
                if (company_name == compare_name)
                {
                    // Update the title text to show the name of the company
                    Text = $"Migrate UBS Account ({company_name} \u25B6 {db_name})";
                    lbl_ImportTitle.Text = $"Import backup data from: {company_name}";
                }
                else
                {
                    // Prompt user to decide either to mess up the backup data folders or not
                    DialogResult result = MessageBox.Show(
                    $"Backup data of UBS Account and UBS Stock are likely different: \n\n" +
                    $"UBS Account: \n{company_name}\n\n" +
                    $"UBS Stock: \n{compare_name}\n\n" +
                    $"Continue with the selected folders?",
                    "Backup Data Mismatch",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result.ToString().Equals("No")) return;
                }

                exception = null; // Reset exception message text

                // Refresh the tables shown in data grid view by removing the existing tables
                if (dgv_selTblImport.RowCount > 0)
                {
                    dataSet_ImportTables.Tables.Remove("Import_Tables");
                    dataSet_ImportTables.Tables.Remove("Stock_Tables");
                }

                DGVTable_Load();

                // Retrieve display table and background table from dataSet_ImportTables
                DataTable dataTable_show = dataSet_ImportTables.Tables["Import_Tables"];
                DataTable dataTable_back = dataSet_ImportTables.Tables["Stock_Tables"];

                // ----- Load the data tables in a specified order ----- //

                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Chart of Account"], "Chart of Account");
                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Sales Agent"]     , "Sales Agent");

                if (txt_path_StkFolder.Text != "")
                    UBSData_Load(dataTable_show, txt_path_StkFolder, name_to_file["Purchase Agent"], "Purchase Agent");

                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Area"]   , "Area");
                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Project"], "Project");
                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Currency"], "Currency");
                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Customer"], "Customer");
                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Supplier"], "Supplier");

                // Load the stock data table to background table if the stock folder path has given
                if (txt_path_StkFolder.Text != "")
                {
                    UBSData_Load(dataTable_show, txt_path_StkFolder, name_to_file["Category"], "Category");
                    UBSData_Load(dataTable_show, txt_path_StkFolder, name_to_file["Group"]   , "Group");
                    UBSData_Load(dataTable_show, txt_path_StkFolder, name_to_file["Location"], "Location");
                    UBSData_Load(dataTable_show, txt_path_StkFolder, name_to_file["Item"]    , "Item");
                }

                // Generate exception message list if any exception has thrown
                if (exception != null)
                {
                    exception = ConvertMessageToList(exception, 1, true);

                    MessageBox.Show(
                        "The table cannot be listed because of the errors below:" +
                        exception,
                        "External error occur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Enable the data migration settings if no exception thrown
                    gb_dataMigrate.Enabled = txt_path_AccFolder.Equals("") ? false : true;
                }

                isPathChanging = false; // Reset folder path change indication

                btn_import.BackColor = Color.White;
                idc_warning.Text = string.Empty;
            }
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            // Retrieve display table and background table
            DataTable dataTable_show = dataSet_ImportTables.Tables["Import_Tables"];
            DataTable dataTable_back = dataSet_ImportTables.Tables["Stock_Tables"];

            lb_copied.Visible = false; // Reset copy success message to hidden

            // Indicate master row checkbox selection
            bool isMasterSelected = (bool)dataTable_show.Rows.Find("Chart of Account")["Select"];

            // Dictionary to store data tables to import
            Dictionary<string, string[]> import_table_rows = new Dictionary<string, string[]>();

            foreach (DataRow row in dataTable_show.Rows)
            {
                // Add to import_table_rows dictionary if is selected rows or has master row selected
                if ((bool)row["Select"] || isMasterSelected) // 
                {
                    import_table_rows.Add(
                        row["Table Name"].ToString(),                      // Table name as the KEY
                        new string[]
                        {
                            name_to_file[row["Table Name"].ToString()],    // File name and record number details
                            row["Start From Record"].ToString(),
                            row["End To Record"].ToString(),
                            row["Total Records"].ToString()
                        }
                    );
                }
            }

            // Add to import_table_rows dictionary if has master row selected
            if (dataTable_back.Rows.Count > 0 && isMasterSelected) // 
            {
                foreach (DataRow row in dataTable_back.Rows)
                {
                    import_table_rows.Add(
                        row["Table Name"].ToString(),                       // Table name as the KEY
                        new string[]
                        {
                            name_to_file[row["Table Name"].ToString()],     // File name and record number details
                            row["Start From Record"].ToString(),
                            row["End To Record"].ToString(),
                            row["Total Records"].ToString()
                        }
                    );
                }
            }

            // if at least 1 data table has selected
            if (import_table_rows.Count > 0)
            {
                // ----- Validate folder path 1 more time ----- //

                if (isPathChanging)
                {
                    MessageBox.Show(
                        "The given folder path(s) has changed, please reload the tables before import.",
                        "Folder Path Changed",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                // ----- Display import table list ----- //

                string import_list = ""; // Reset import listing message

                foreach (KeyValuePair<string, string[]> table in import_table_rows)
                {
                    string record_range;

                    // If record range set and is not from 1 to total records
                    if (
                        (table.Value[1] != "-" && table.Value[2] != "-") && 
                        !(table.Value[1] == "1" && table.Value[2] == table.Value[3]))
                    {
                        // Show "Record No.1" if same start and end, else show "No.1 - No.10"
                        record_range = table.Value[1] == table.Value[2] ?
                            $"Record No.{table.Value[1]}" : $"Record No.{table.Value[1]} - No.{table.Value[2]}";
                    }
                    else
                    {
                        // Show total number of record if have else show "No records"
                        record_range = table.Value[3] != "0" ?
                            $"All {table.Value[3]} record(s)" : "No records";
                    }

                    import_list +=
                        $"{table.Key}|".PadRight(15) +
                        $" \t- ({record_range})\n>";
                }

                import_list = ConvertMessageToList(import_list); // Convert import list message to list

                // ----- Display import settings ----- //

                string settings = "";

                // If one of the setting has been selected
                if (chk_reformatAccNo.Checked || chk_overwriteExistData.Checked)
                {
                    if (chk_reformatAccNo.Checked)      settings += "\n\u25B6 Apply AutoCount account number formatting";
                    if (chk_overwriteExistData.Checked) settings += "\n\u25B6 Overwrite existing data";
                }
                else
                {
                    settings = "\n\u274C NONE";
                }

                DialogResult result = MessageBox.Show(
                    $"Please check the information below before proceed:\n\n" +
                    $"Table to import :\n\n" +
                    $"{import_list}\n\n" +
                    $"Data import settings :\n{settings}\n\n" +
                    $"Confirm to start data migration?",
                    "Confirm Data Migration Action",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result.ToString().Equals("No")) return;
                
                exception = successes = null;   // Reset exception and success message text
                rtxt_importStusLog.Text = "";   // Reset import status log

                // ----- Show messages based on the chosen options ----- //

                if (chk_reformatAccNo.Checked)
                    rtxt_importStusLog.AppendText("\n \u24D8 Account number reformat has enabled.\n");

                string nl = rtxt_importStusLog.Text.Equals("") ? "\n" : ""; // Move 1 line downward if is the 1st line
                if (chk_overwriteExistData.Checked)
                    rtxt_importStusLog.AppendText($"{nl} \u24D8 Data overwrite has enabled.\n");

                string stock_source = txt_path_StkFolder.Text != "" ?
                    $"\u25B6 Stock source : {txt_path_StkFolder.Text}\n" : "";

                rtxt_importStusLog.AppendText(
                    $"\n " +
                    $"\u24D8 {Text}\n" +
                    $"\u25B6 Account source : {txt_path_AccFolder.Text}\n" +
                    $"{stock_source}" +
                    $"\n\u25B6 Operation Start\n");

                // ----- Perform data migration for all selected data tables ----- //

                foreach (KeyValuePair<string, string[]> table in import_table_rows)
                {
                    // Take from stock folder if is stock table, else take from account folder.
                    string dbf_file_path = stk_table_name.Contains(table.Key) ? 
                        txt_path_StkFolder.Text : txt_path_AccFolder.Text;

                    // If the data table has any records
                    if (!table.Value[3].Equals("0"))
                    {
                        UBSData_Import(dbf_file_path, table.Key, table.Value); // Perform data migration
                    }
                    else
                    {
                        // Show the message to indicate empty table in the status log and append to exception message text
                        rtxt_importStusLog.AppendText($"\n \u26A0 No data found in {table.Key} table, operation skipped.");
                        successes += 
                            $"{table.Key}|".PadRight(15) +
                            $"\t- No records.\n>";
                    }
                }

                // Count success and failure based on how many table issue the message,
                // -1 to remove extra empty element
                int success = successes == null ? 0 : successes.Split('>').Count() - 1,
                    failure = exception == null ? 0 : exception.Split('>').Distinct().Count() - 1; // Dismiss repeative message

                rtxt_importStusLog.AppendText($"\n\n \u24D8 UBS Data Migration Operation Completed ({success} completed, {failure} failure)\n");
                rtxt_importStusLog.ScrollToCaret();

                // If no exception thrown during the migration process
                if (exception == null)
                {
                    successes = ConvertMessageToList(successes, 1, true); // Convert the success message to sorted list form
                    
                    MessageBox.Show(
                        "All selected table has successfully imported.\n\n" +
                        "Please review the migration information below:\n\n" +
                        $"{successes}\n",
                        "Data migration successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    exception = ConvertMessageToList(exception, 1, true); // Convert the exception message to sorted list form

                    MessageBox.Show(
                        "Some error has occur when migrating the table below:\n" +
                        $"{exception}\n" +
                        "Please review the status log for more information.",
                        $"Data migration contains error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else // If no data table has selected
            {
                MessageBox.Show(
                    "Please select at least 1 table to import.",
                    $"No selected table(s)",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_copyImportStus_Click(object sender, EventArgs e)
        {
            // Copy the import status log if is not empty
            if (rtxt_importStusLog.Text != "")
            {
                Clipboard.SetText(rtxt_importStusLog.Text);

                lb_copied.Visible = true; // Show the copy success message
            }
        }

        private void btn_copyImportStus_Leave(object sender, EventArgs e)
        {
            lb_copied.Visible = false; // Reset copy success message to hidden
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UBSDataMigrateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Indicate changes if folder path changed or has data table loaded
            bool changeMade = (isPathChanging || dgv_selTblImport.Rows.Count > 0) ? true : false;

            // Prompt the user to left the selected folder path or loaded table discarded before leave
            if (changeMade)
            {
                DialogResult result = MessageBox.Show(
                "Any information given here wil be LOST. Confirm to quit from data migration?",
                "Exit from Data Migration",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                e.Cancel = result.ToString().Equals("No") ? true : false;
            }
        }

        //UBS Data Migration----------------------------------------------------------------------------------//

        private void UBSData_Load(DataTable target_table, TextBox text_box, string dbf_file_name, string table_name)
        {
            // Determine to load the data table in either display table or background table
            DataTable dataTable = target_table;

            string file_path = $"{text_box.Text}\\{dbf_file_name}.dbf";
            int total_rec = 0;

            var options = new DbfDataReaderOptions { SkipDeletedRecords = true };

            try
            {
                // Count the total number of records in the given data file
                using (var dbfData = new DbfData(file_path, options))
                    while (dbfData.Read()) total_rec++;

                // Add new data table row to target_table if is not in target_table
                if (dataTable.Rows.Find(table_name) == null)
                    dataTable.Rows.Add(false, table_name, "-", "-", total_rec);
                // dataTable.Rows.Add(checkbox select, table name, record start, record end, total record);
            }
            catch (Exception ex)
            {
                exception += 
                    $"\n" +
                    $"{ex.Message}|\n" +
                    $"\u25AA {dbf_file_name} ({table_name})\n>";
            }
        }

        private void UBSData_Import(string dbf_file_path, string table_name, string[] table_details)
        {
            string file_path = $"{dbf_file_path}\\{table_details[0]}.dbf";
            string exc = null; // Declare local exception message text, and reset the text once every data table import

            /*
             table_details[0] = file name
             table_details[1] = record start
             table_details[2] = record end
             table_details[3] = total record
             */

            int start_from_rec = table_details[1].Equals("-") ? 1 : int.Parse(table_details[1]); // if the value = '-' then set to 1
            int end_to_rec = table_details[2].Equals("-") ? 0 : int.Parse(table_details[2]);     // if the value = '-' then set to 0
            int total_rec = int.Parse(table_details[3]);

            if (end_to_rec == 0) end_to_rec = total_rec;         // Set record end = total record if the value is 0

            int current_record = 0, success = 0, failure = 0;    // Reset record counts to 0

            var options = new DbfDataReaderOptions { SkipDeletedRecords = true };

            using (var dbfData = new DbfData(file_path, options))
            {
                rtxt_importStusLog.AppendText(
                    $"\n" +
                    $"\n" +
                    $" \u25B6 Start import {table_name} data from record number {start_from_rec} to {end_to_rec}.\n");

                // Iterate through every row in the data file
                while (dbfData.Read())
                {
                    current_record++;

                    // If current_record count is in range of start_from_rec and end_to_rec
                    if ((start_from_rec <= current_record && current_record <= end_to_rec))
                    {
                        txt_currentRecNo.Text = current_record.ToString(); // Show the current processing record count

                        try
                        {
                            switch (table_name)
                            {
                                case "Chart of Account":
                                    ProcessImport_ChartOfAccount(dbfData, current_record);
                                    break;
                                case "Sales Agent":
                                    ProcessImport_SalesAgent(dbfData, current_record);
                                    break;
                                case "Purchase Agent":
                                    ProcessImport_PurchaseAgent(dbfData, current_record);
                                    break;
                                case "Area":
                                    ProcessImport_Area(dbfData, current_record);
                                    break;
                                case "Project":
                                    ProcessImport_Project(dbfData, current_record);
                                    break;
                                case "Currency":
                                    ProcessImport_Currency(dbfData, current_record);
                                    break;
                                case "Customer":
                                    ProcessImport_Debtor(dbfData, current_record);
                                    break;
                                case "Supplier":
                                    ProcessImport_Creditor(dbfData, current_record);
                                    break;
                                case "Category":
                                    ProcessImport_ItemCategory(dbfData, current_record);
                                    break;
                                case "Group":
                                    ProcessImport_ItemGroup(dbfData, current_record);
                                    break;
                                case "Location":
                                    ProcessImport_ItemLocation(dbfData, current_record);
                                    break;
                                case "Item":
                                    ProcessImport_Item(dbfData, current_record);
                                    break;
                                default:
                                    // If is other table
                                    exception += 
                                        $"\n" +
                                        $"Table: {table_name}\n|" +
                                        $"- {table_details[0]} is not a backup data table.\n";
                                    break;
                            }
                            success++;
                        }
                        catch (Exception ex)
                        {
                            // Set the local exception text
                            exc = $"\n" +
                                $"Table: {table_name}\n|" +
                                $"\u25AA {ex.GetType().Name}\n>";

                            exception += exc; // Append local exception text to global exception text

                            string exc_msg;

                            // Set exception message based on either has inner exception message or not
                            try 
                            { exc_msg = $"{ex.InnerException.Message}: {ex.Message}"; }
                            catch (NullReferenceException) 
                            { exc_msg = ex.Message; }

                            // Log the thrown exception
                            rtxt_importStusLog.AppendText($"\n \u274C (Rec {current_record}) {exc_msg}");
                            failure++;
                        }
                    }

                    rtxt_importStusLog.ScrollToCaret(); // Scroll to the bottom of the status log text box
                }
            }

            // If no exception thrown while import the data table
            // Cannot use 'exception' because it is global and may contain value until all tables are done
            if (exc == null)
            {
                // Append successful data import count to the message that will show in MessageBox pop-up
                successes += 
                    $"{table_name} |".PadRight(15) +
                    $"\t- {success} successful (No. {start_from_rec} to {end_to_rec})\n>";
            }

            // Log the import status of the table
            rtxt_importStusLog.AppendText(
                $"\n" +
                $"\n" +
                $" \u24D8 Data migration of {table_name} has been completed. ({success} success, {failure} failed)\n" +
                $"----------");

            rtxt_importStusLog.ScrollToCaret(); // Scroll to the bottom of the status log text box
        }

        private void ProcessImport_ChartOfAccount(DbfData dbfData, int rec_no)
        {
            Accounts accounts = new Accounts(Program.session);

            string src_acc_no = dbfData.GetString(2);
            string fmt_acc_no = ReformatAccNo(src_acc_no);

            // Make sure no duplicate account even with different account number format 
            string acc_no;
            string acc_name = dbfData.GetString(3);

            if (chk_reformatAccNo.Checked)
                // Use formatted account number when no source account number found in existing data
                acc_no = !accounts.hasAccount(src_acc_no) ? fmt_acc_no : src_acc_no;
            else
                // Use source account number when no formatted account number found in existing data
                acc_no = !accounts.hasAccount(fmt_acc_no) ? src_acc_no : fmt_acc_no;

            string sts_word = DefStatus(accounts.hasAccount(acc_no)); // Show if add new or update record

            accounts.CreateNormalAccount(chk_overwriteExistData.Checked, acc_no, dbfData);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} account : '{acc_name}'");
        }

        private void ProcessImport_SalesAgent(DbfData dbfData, int rec_no)
        {
            Agents agent = new Agents(Program.session);

            string agent_name = dbfData.GetString(0);
            string agent_desc = dbfData.GetString(1);

            string sts_word = DefStatus();

            agent.CreateOrUpdate_SalesAgent(chk_overwriteExistData.Checked, agent_name, agent_desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} sales agent : {agent_name} - {agent_desc}");
        }

        private void ProcessImport_PurchaseAgent(DbfData dbfData, int rec_no)
        {
            Agents agent = new Agents(Program.session);

            string agent_name = dbfData.GetString(0);
            string agent_desc = dbfData.GetString(1);

            string sts_word = DefStatus();

            agent.CreateOrUpdate_PurchaseAgent(chk_overwriteExistData.Checked, agent_name, agent_desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} purchase agent : {agent_name} - {agent_desc}");
        }

        private void ProcessImport_Area(DbfData dbfData, int rec_no)
        {
            Areas area = new Areas(Program.session);

            string area_code = dbfData.GetString(0);
            string area_desc = dbfData.GetString(1);

            string sts_word = DefStatus();

            area.CreateOrUpdate_Area(chk_overwriteExistData.Checked, area_code, area_desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} area : {area_code} - {area_desc}");
        }

        private void ProcessImport_Project(DbfData dbfData, int rec_no)
        {
            Projects projects = new Projects(Program.session);

            string project_no = dbfData.GetString(0);
            string project_desc = dbfData.GetString(1);
            string project_type = dbfData.GetString(2);

            string sts_word = DefStatus();

            projects.CreateOrUpdate_Project(chk_overwriteExistData.Checked, project_no, project_desc, project_type);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} project : {project_no} - {project_desc}");
        }

        private void ProcessImport_Currency(DbfData dbfData, int rec_no)
        {
            Currencies currencies = new Currencies(Program.session);

            string currency_code = dbfData.GetString(0);
            string currency_word = dbfData.GetString(1);

            string sts_word = DefStatus();

            currencies.CreateOrUpdate_Currency(chk_overwriteExistData.Checked, currency_code, currency_word);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} currency : {currency_code}");
        }

        private void ProcessImport_Debtor(DbfData dbfData, int rec_no)
        {
            Debtor debtor = new Debtor(Program.session);

            string src_acc_no = dbfData.GetString(1);
            string fmt_acc_no = ReformatAccNo(src_acc_no);

            // Make sure no duplicate debtor even with different account number format 
            string acc_no;
            string com_name = dbfData.GetString(2);

            if (chk_reformatAccNo.Checked)
                // Use formatted account number when no source account number found in existing data
                acc_no = !debtor.hasDebtor(src_acc_no, com_name) ? fmt_acc_no : src_acc_no;
            else
                // Use source account number when no formatted account number found in existing data
                acc_no = !debtor.hasDebtor(fmt_acc_no, com_name) ? src_acc_no : fmt_acc_no;

            string sts_word = DefStatus(debtor.hasDebtor(acc_no)); // Show if add new or update record

            debtor.CreateOrUpdate_Debtor(chk_overwriteExistData.Checked, acc_no, dbfData);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} debtor : {acc_no} - {com_name}");
        }

        private void ProcessImport_Creditor(DbfData dbfData, int rec_no)
        {
            Creditor creditor = new Creditor(Program.session);

            string src_acc_no = dbfData.GetString(1);
            string fmt_acc_no = ReformatAccNo(src_acc_no);

            // Make sure no duplicate creditor even with different account number format
            string acc_no;
            string com_name = dbfData.GetString(2);

            if (chk_reformatAccNo.Checked)
                // Use formatted account number when no source account number found in existing data
                acc_no = !creditor.hasCreditor(src_acc_no, com_name) ? fmt_acc_no : src_acc_no;
            else
                // Use source account number when no formatted account number found in existing data
                acc_no = !creditor.hasCreditor(fmt_acc_no, com_name) ? src_acc_no : fmt_acc_no;

            string sts_word = DefStatus(creditor.hasCreditor(acc_no)); // Show if add new or update record

            creditor.CreateOrUpdate_Creditor(chk_overwriteExistData.Checked, acc_no, dbfData);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} creditor : {acc_no} - {com_name}");
        }

        private void ProcessImport_ItemGroup(DbfData dbfData, int rec_no)
        {
            ItemGroups groups = new ItemGroups(Program.session);

            string item_group = dbfData.GetString(0);
            string desc = dbfData.GetString(1);

            string sts_word = DefStatus();

            groups.CreateOrUpdate_ItemGroup(chk_overwriteExistData.Checked, item_group, desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} item group : {item_group} ({desc})");
        }

        private void ProcessImport_ItemCategory(DbfData dbfData, int rec_no)
        {
            ItemCategories categories = new ItemCategories(Program.session);

            string short_code = dbfData.GetString(0);
            string desc = dbfData.GetString(1);

            string sts_word = DefStatus();

            categories.CreateOrUpdate_ItemCategory(chk_overwriteExistData.Checked, short_code, desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} item category : {short_code} ({desc})");
        }

        private void ProcessImport_ItemLocation(DbfData dbfData, int rec_no)
        {
            Locations locations = new Locations(Program.session);

            string location = dbfData.GetString(0);
            string desc = dbfData.GetString(1);

            string sts_word = DefStatus();

            locations.CreateOrUpdate_Location(chk_overwriteExistData.Checked, location, desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} item location : {location} ({desc})");
        }

        private void ProcessImport_Item(DbfData dbfData, int rec_no)
        {
            Items item = new Items(Program.session);

            string sts_word = DefStatus();

            item.CreateOrUpdate_Item(chk_overwriteExistData.Checked, dbfData);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} stock item : {dbfData.GetString(1)}");
        }

        //Utilities-------------------------------------------------------------------------------------------//

        private void DGVTable_Load()
        {
            // Declare display table and background table
            DataTable dataTable_display = new DataTable("Import_Tables");
            DataTable dataTable_backing = new DataTable("Stock_Tables");

            dataTable_display.Columns.Add("Select"              , typeof(bool));    // Selection checkbox
            dataTable_display.Columns.Add("Table Name"          , typeof(string));
            dataTable_display.Columns.Add("Start From Record"   , typeof(object));
            dataTable_display.Columns.Add("End To Record"       , typeof(object)); 
            dataTable_display.Columns.Add("Total Records"       , typeof(int));

            dataTable_backing.Columns.Add("Select"              , typeof(bool));    // Selection checkbox
            dataTable_backing.Columns.Add("Table Name"          , typeof(string));
            dataTable_backing.Columns.Add("Start From Record"   , typeof(object));
            dataTable_backing.Columns.Add("End To Record"       , typeof(object));
            dataTable_backing.Columns.Add("Total Records"       , typeof(int));

            dataTable_display.PrimaryKey = new DataColumn[] { dataTable_display.Columns[1] };
            dataTable_backing.PrimaryKey = new DataColumn[] { dataTable_backing.Columns[1] };

            // Add tables to dataSet_ImportTables
            dataSet_ImportTables.Tables.Add(dataTable_display);
            dataSet_ImportTables.Tables.Add(dataTable_backing);

            dgv_selTblImport.DataSource = dataSet_ImportTables.Tables["Import_Tables"]; // Binding the data grid view with "Import_Tables" in dataSet_ImportTables

            dgv_selTblImport.Columns[0].Width = 45;
            dgv_selTblImport.Columns[1].Width = 200;
            dgv_selTblImport.Columns[2].Width = 100;
            dgv_selTblImport.Columns[3].Width = 100;
            dgv_selTblImport.Columns[4].Width = 88;

            dgv_selTblImport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void OpenFolderDialog(string desc, TextBox textBox)
        {
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                ofd.Description = desc;

                // If folder has selected in the dialog
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBox.Text = ofd.SelectedPath; // Set the text of given text field to the selected folder path
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            $"External error from {ex.GetBaseException().GetType().Name}",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool ValidateFolders(string file_path, string file_name)
        {
            return new FileInfo($"{file_path}\\{file_name}.MEM").Exists; // Check if file exist
        }

        private void SetRecordRange(string column, NumericUpDown nud)
        {
            foreach (DataGridViewRow row in dgv_selTblImport.SelectedRows)
            {
                if (0 < nud.Value && (int)nud.Value <= (int)row.Cells["Total Records"].Value)
                    row.Cells[column].Value = nud.Value; // If nud control value > 0 and <= total records: column value = nud control value   
                else if (nud.Value == 0)
                    row.Cells[column].Value = "-";       // If nud control value = 0: column value = '-'
            }

            dgv_selTblImport.Refresh();
        }

        private void CalibrateRecordRange()
        {
            foreach (DataGridViewRow row in dgv_selTblImport.SelectedRows)
            {
                // If value of nud_recRangeEnd control > 0 and "Start From Record" value of the row = '-'
                if (nud_recRangeEnd.Value > 0 && row.Cells["Start From Record"].Value.Equals("-"))
                {
                    // If the row shows that the data table has any records
                    if ((int)row.Cells["Total Records"].Value > 0)
                    {
                        // Set the values to 1 so that the range start with no.1 instead of no.0 //

                        row.Cells["Start From Record"].Value = 1;

                        nud_recRangeStart.Minimum = 1;
                    }
                }
                else if (nud_recRangeEnd.Value == 0)
                {
                    // Reset the values if value of nud_recRangeEnd control > 0 //

                    row.Cells["Start From Record"].Value = "-";

                    nud_recRangeStart.Minimum = 0;
                }
            }

            dgv_selTblImport.Refresh();
        }

        private void SyncRecordRange()
        {
            isSyncingRange = true; // Indicate record range sync
            
            foreach (DataGridViewRow row in dgv_selTblImport.SelectedRows)
            {
                var start = row.Cells["Start From Record"].Value.Equals("-") ?  // start = 0 if column value is a '-' else = column value
                    0 : row.Cells["Start From Record"].Value;

                var end = row.Cells["End To Record"].Value.Equals("-") ?        // end = 0 if column value is a '-' else = column value
                    0 : row.Cells["End To Record"].Value;

                var total = row.Cells["Total Records"].Value.Equals(0) ?        // total = 0 if column value = 0 else = column value
                    0 : row.Cells["Total Records"].Value;

                // Make sure Record start and Record end <= total records
                nud_recRangeStart.Maximum = nud_recRangeEnd.Maximum = decimal.Parse(total.ToString());
                
                // Set the current value of controls
                nud_recRangeEnd.Value = decimal.Parse(end.ToString());
                nud_recRangeStart.Value = decimal.Parse(start.ToString());
            }

            isSyncingRange = false; // Close record range sync indication
        }

        private void ResetRecordRange(DataGridViewRow row, bool resetCheckbox = false)
        {
            // Reset both value of "Start From Record" and "End To Record" column = '-' only if column value != '-'
            if (!row.Cells["Start From Record"].Value.Equals("-") || !row.Cells["End To Record"].Value.Equals("-"))
                row.Cells["Start From Record"].Value = row.Cells["End To Record"].Value = "-";

            // Reset the selection checkbox if is checked
            if (resetCheckbox && (bool)row.Cells["Select"].Value) row.Cells["Select"].Value = false;
        }

        private void SetCheckBoxSelection(DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.RowIndex < 0) return; // If is not clicking on the checkbox column

            // If the master row is selected
            if (e.RowIndex == 0)
            {
                for (int i = 1; i < dgv_selTblImport.Rows.Count; i++)   // Iterate from 2nd row
                {
                    dgv_selTblImport[0, i].Value = false;               // Deselect the other rows
                }

                return;
            }

            for (int i = 1; i < dgv_selTblImport.Rows.Count; i++)       // Iterate from 2nd row
            {
                if ((bool)dgv_selTblImport[0, i].Value)
                {
                    dgv_selTblImport[0, 0].Value = false;               // Deselect the master row if any other row is selected
                    break;
                }
            }
        }

        private string ConvertMessageToList(string msg_stream, int groupBy = 0, bool sortList = false)
        {
            List<string[]> msg_list = new List<string[]>();
            
            string[] msg_row_arr = msg_stream.Split('>'); // Split the stream of exception text into ["table 1|msg 1", "table 2|msg 2"]
            string[] msg_for_table = new string[10];      // Message buffer to make distinct message within a table
            int count = 0;
            
            string printing_text = "";  // Output text
            string previous_table = "";
            string previous_msg = "";

            // Split chunks into ["table 1", "msg 1"], ["table 2", "msg 2"] and add them into the list
            foreach (string msg_chunk in msg_row_arr) { msg_list.Add(msg_chunk.Split('|')); }

            // Sort the list in ascending order if choose to sort the output list
            if (sortList) msg_list.Sort((prev, crnt) => prev[0].CompareTo(crnt[0]));

            foreach (string[] msg_list_row in msg_list)
            {
                // If to either list by table name or print distinct
                if (groupBy > 0)
                {
                    // List by table name
                    if (previous_table != msg_list_row[0])
                    {
                        msg_for_table = new string[10];     // Reset the message buffer when is another table
                        count = 0;
                        
                        previous_table = msg_list_row[0];                                   // Set to new table name
                        printing_text += msg_list_row[0].Equals("") ? "" : msg_list_row[0]; // Append the new table name if is not empty

                        if (groupBy == 1) // If choose to only list by table name
                        {
                            printing_text += msg_list_row[0].Equals("") ? "" : msg_list_row[1];
                            
                            msg_for_table[count] = msg_list_row[0].Equals("") ? "" : msg_list_row[1]; // Add first new message to buffer
                            count++;
                        }
                    }
                    else if (groupBy == 1 && msg_list_row.Length > 1 && !msg_for_table.Contains(msg_list_row[1])) // If same table have different message
                    {
                        printing_text += msg_list_row[0].Equals("") ? "" : msg_list_row[1]; // Append the new message if no same message in buffer

                        msg_for_table[count] = msg_list_row[1];                             // Add new message to buffer
                        count++;
                    }

                    // Print distinct for both table name and messages
                    if (groupBy == 2 && msg_list_row.Length > 1 && !msg_list_row[1].Equals(previous_msg))
                    {
                        previous_msg = msg_list_row[1];
                        printing_text += msg_list_row[0].Equals("") ? "" : msg_list_row[1];
                    }
                }
                else
                {
                    printing_text += msg_list_row[0].Equals("") ? "" : msg_list_row[0]; // Append the new table name if is not empty
                    printing_text += msg_list_row[0].Equals("") ? "" : msg_list_row[1]; // Append the new message if the table name is not empty
                }
            }

            return printing_text;
        }

        private string ExtractComName(string file_path, string file_name)
        {
            // Construct access to file in given path
            var mmf = MemoryMappedFile.CreateFromFile($"{file_path}\\{file_name}.MEM");
            var accessor = mmf.CreateViewAccessor();

            byte[] stringBytes = new byte[5000];
            accessor.ReadArray(0, stringBytes, 0, 5000); // Read the first 5000 bytes from given memory file

            // Convert the bytes to string and remove miscellaneous characters
            string text = Encoding.ASCII.GetString(stringBytes).Replace("\0", "").Replace("\u0001", "");

            while (!text.StartsWith("COMPANY")) { text = text.Remove(0, 1); } // Remove all characters before "COMPANY"

            char[] new_text = new char[85];
            text.CopyTo(0, new_text, 0, 85); // Dismiss the characters after 85 characters
            text = "";

            foreach (char c in new_text) { text += c; } // Restore the text

            // Close the file
            mmf.Dispose();
            accessor.Dispose();

            return text.Trim().Remove(0, 10); // Remove the "COMPANY" identifier in front of the company name
        }

        private string ReformatAccNo(string acc_no)
        {
            string[] parts = acc_no.Split('/'); // Split account number to ["3000", "A01"]

            if (parts.Length != 2) return acc_no; // fallback if is ambigous account number format

            string mainPart = parts[0]; // "3000"
            string subPart = parts[1];  // "A01"

            // Format mainPart to 3 digits
            if (mainPart.Length > 3) mainPart = mainPart.Substring(0, 3);

            // Format subPart in the form of letters + numbers with leading zeros
            string alpha = new string(subPart.Where(c => char.IsLetter(c)).ToArray()); // "A"
            string digit = new string(subPart.Where(c => char.IsDigit(c)).ToArray());  // "01"

            string subFormatted = alpha + digit.PadLeft(digit.Length + 1, '0'); // "A001"

            return $"{mainPart}-{subFormatted}"; // "300-A001"
        }

        private string DefStatus()
        {
            return chk_overwriteExistData.Checked ? "Updated" : "Added new"; // Indicate data overwrite
        }

        private string DefStatus(bool hasExistData)
        {
            return hasExistData ? "Updated" : "Added new"; // Indicate data overwrite based on existing data
        }
    }
}
