using AutoCount.BackupService;
using DbfDataReader;
using PlugIn_1.Entity;
using PlugIn_1.Entity.General_Maintainance;
using PlugIn_1.Entity.Stock;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Creditor = PlugIn_1.Entity.Account.Creditor;
using Debtor = PlugIn_1.Entity.Debtor;

namespace PlugIn_1.Forms
{
    public partial class UBSDataMigrateForm : Form
    {
        private string exception { get; set; }

        private string successes { get; set; }

        private string db_name = Program.session.DBSetting.DBName.ToString();

        private bool syncing_range = false;

        private bool isPathChanging = false;

        private Dictionary<string, string> name_to_file = new Dictionary<string, string>()
        {
            {"Chart of Account" , "gldata"},
            {"Sales Agent"      , "icagent"},
            {"Purchase Agent"   , "icagent"},
            {"Area"             , "icarea"},
            {"Project"          , "project"},
            //{"Terms"            , ""},
            {"Currency"         , "currency"},
            {"Customer"         , "arcust"},
            {"Supplier"         , "apvend"},
            {"Item"             , "icitem"},
            {"Category"         , "iccate"},
            {"Group"            , "icgroup"},
            {"Location"         , "iclocate"}
        };

        private string[] stk_table_name =
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

            btn_import.Cursor = Cursors.Hand;

            Text = $"Migrate UBS Account ({db_name})";
        }

        //Controls--------------------------------------------------------------------------------------------//

        private void btn_help_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Guides to select backup folders:\n\n" +
                "1. Use 7zip software to extract .ACC file to a new folder, and .STK file to another new folder.\n" +
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
                "3. Set both value to 0 to import all records in the table.\n" +
                "4. Set both value to the same number to import single record.\n" +
                "5. The value of start < end < total records in table.\n\n" +
                "Tips: Setting record range for multiple tables is supported.",
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
            btn_browseStkFolder.Enabled =
                txt_path_StkFolder.Enabled =
                btn_listTable.Enabled =
                txt_path_AccFolder.Text.Equals("") ? false : true;

            isPathChanging = true;
        }

        private void txt_path_StkFolder_TextChanged(object sender, EventArgs e)
        {
            isPathChanging = true;
        }

        private void nud_recRangeStart_Click(object sender, EventArgs e)
        {
            nud_recRangeStart.Maximum = nud_recRangeEnd.Value;
        }

        private void nud_recRangeEnd_Click(object sender, EventArgs e)
        {
            nud_recRangeStart.Maximum = nud_recRangeEnd.Value;
        }

        private void nud_recRangeStart_ValueChanged(object sender, EventArgs e)
        {
            if (!syncing_range) SetRecordRange("Start From Record", nud_recRangeStart);
        }

        private void nud_recRangeEnd_ValueChanged(object sender, EventArgs e)
        {
            if (!syncing_range && dgv_selTblImport.SelectedRows.Count > 0)
            {
                SetRecordRange("End To Record", nud_recRangeEnd);
                CalibrateRecordRange();

                if (nud_recRangeStart.Value > nud_recRangeEnd.Value)
                    nud_recRangeStart.Value = nud_recRangeEnd.Value;
            }
            else
            {
                nud_recRangeStart.Minimum = nud_recRangeEnd.Value > 0 ? 1 : 0;
            }
        }

        private void dgv_selTblImport_Click(object sender, EventArgs e)
        {
            gb_recRangeSet.Enabled = 
                dgv_selTblImport.SelectedRows.Count > 0 && !(bool)dgv_selTblImport[0, 0].Value ? 
                true : false;
        }

        private void dgv_selTblImport_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            gb_recRangeSet.Enabled = (bool)dgv_selTblImport[0, 0].Value ? false : true;
            SyncRecordRange();
        }

        private void dgv_selTblImport_SelectionChanged(object sender, EventArgs e)
        {
            SyncRecordRange();
        }

        private void dgv_selTblImport_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_selTblImport.IsCurrentCellDirty)
                dgv_selTblImport.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgv_selTblImport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_selTblImport[1, 0].Value.Equals("Chart of Account"))
                SetCheckBoxSelection(e);

            gb_recRangeSet.Enabled =
                dgv_selTblImport.SelectedRows.Count > 0 && !(bool)dgv_selTblImport[0, 0].Value ?
                true : false;

            if ((bool)dgv_selTblImport[0, 0].Value)
            {
                syncing_range = true;

                foreach (DataGridViewRow row in dgv_selTblImport.Rows)
                {
                    ResetRecordRange(row);
                }

                nud_recRangeStart.Value = nud_recRangeEnd.Value = 0;

                syncing_range = false;
            }
        }

        private void btn_resetRange_Click(object sender, EventArgs e)
        {
            syncing_range = true;
            
            foreach (DataGridViewRow row in dgv_selTblImport.SelectedRows)
            {
                ResetRecordRange(row);
            }

            syncing_range = false;

            nud_recRangeStart.Value = nud_recRangeEnd.Value = 0;
        }

        private void btn_resetAllRange_Click(object sender, EventArgs e)
        {
            syncing_range = true;
            
            foreach (DataGridViewRow row in dgv_selTblImport.Rows)
            {
                ResetRecordRange(row);
            }

            syncing_range = false;

            nud_recRangeStart.Value = nud_recRangeEnd.Value = 0;
        }

        private void btn_listTable_Click(object sender, EventArgs e)
        {
            bool isValidAccFolder = ValidateFolders(txt_path_AccFolder.Text, "ACCOUNT");
            bool isValidStkFolder = txt_path_StkFolder.Text.Equals("") ?
                true : (ValidateFolders(txt_path_StkFolder.Text, "STOCK") && 
                !ValidateFolders(txt_path_StkFolder.Text, "ACCOUNT"));

            if (!isValidAccFolder || !isValidStkFolder)
            {
                string invFileErr = (!isValidAccFolder && !isValidStkFolder) ?
                    "UBS account folder and UBS stock folder" : !isValidAccFolder ?
                    "UBS account folder" : "UBS stock folder";

                string invFileVerb = (!isValidAccFolder && !isValidStkFolder) ? "are" : "is";

                MessageBox.Show(
                    $"Folder(s) from given path(s) {invFileVerb} not a correct \n{invFileErr}.",
                    "Incorrect Backup Folder",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (isPathChanging)
            {
                Text = $"Migrate UBS Account ({db_name})";
                lbl_ImportTitle.Text = "Select and Import Table";

                string company_name = ExtractComName(txt_path_AccFolder.Text, "ACCOUNT");
                string compare_name = txt_path_StkFolder.Text.Equals("") ?
                    company_name : ExtractComName(txt_path_StkFolder.Text, "STOCK");

                if (company_name == compare_name)
                {
                    Text = $"Migrate UBS Account ({company_name} \u25B6 {db_name})";
                    lbl_ImportTitle.Text = $"Import backup data from: {company_name}";
                }
                else
                {
                    MessageBox.Show(
                    $"Backup data of UBS Account and UBS Stock are likely different: \n\n" +
                    $"UBS Account: \n{company_name}\n\n" +
                    $"UBS Stock: \n{compare_name}\n\n" +
                    $"Please review the file path and try again.",
                    "Backup Data Mismatch",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                List<string> list = new List<string>();

                exception = null;

                if (dgv_selTblImport.RowCount > 0)
                {
                    dataSet1.Tables.Remove("Import_Tables");
                    dataSet1.Tables.Remove("Stock_Tables");
                }

                DGVTable_Load();

                DataTable dataTable_show = dataSet1.Tables["Import_Tables"];
                DataTable dataTable_back = dataSet1.Tables["Stock_Tables"];

                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Chart of Account"], "Chart of Account");
                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Sales Agent"]     , "Sales Agent");

                if (txt_path_StkFolder.Text != "")
                    UBSData_Load(dataTable_show, txt_path_StkFolder, name_to_file["Purchase Agent"], "Purchase Agent");

                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Area"]   , "Area");
                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Project"], "Project");
                //UBSData_Load(dataTable_show, txt_path_AccFolder, , "Terms");
                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Currency"], "Currency");
                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Customer"], "Customer");
                UBSData_Load(dataTable_show, txt_path_AccFolder, name_to_file["Supplier"], "Supplier");

                if (txt_path_StkFolder.Text != "")
                {
                    UBSData_Load(dataTable_back, txt_path_StkFolder, name_to_file["Category"], "Category");
                    UBSData_Load(dataTable_back, txt_path_StkFolder, name_to_file["Group"]   , "Group");
                    UBSData_Load(dataTable_back, txt_path_StkFolder, name_to_file["Location"], "Location");
                    UBSData_Load(dataTable_back, txt_path_StkFolder, name_to_file["Item"]    , "Item");
                }

                if (exception != null)
                {
                    string printing_text = GenerateMessageList(exception);

                    MessageBox.Show(
                        "The table cannot be listed because of the errors below:" +
                        printing_text,
                        "External error occur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    gb_dataMigrate.Enabled = txt_path_AccFolder.Equals("") ? false : true;
                }

                isPathChanging = false;
            }
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            DataTable dataTable_show = dataSet1.Tables["Import_Tables"];
            DataTable dataTable_back = dataSet1.Tables["Stock_Tables"];

            rtxt_importStusLog.Text = "";
            lb_copied.Visible = false;

            bool isMasterSelected = (bool)dataTable_show.Rows.Find("Chart of Account")["Select"];

            Dictionary<string, string[]> import_table_rows = new Dictionary<string, string[]>();

            foreach (DataRow row in dataTable_show.Rows)
            {
                if ((bool)row["Select"])
                {
                    import_table_rows.Add(
                        row["Table Name"].ToString(),                      // Table name --> KEY
                        new string[]
                        {
                            name_to_file[row["Table Name"].ToString()],    // File name
                            row["Start From Record"].ToString(),
                            row["End To Record"].ToString(),
                            row["Total Records"].ToString()
                        }
                    );
                }
            }

            if (dataTable_back.Rows.Count > 0 && isMasterSelected)          // Only import stock files when select "Chart of Account"
            {
                foreach (DataRow row in dataTable_back.Rows)
                {
                    import_table_rows.Add(
                        row["Table Name"].ToString(),                       // Table name --> KEY
                        new string[]
                        {
                            name_to_file[row["Table Name"].ToString()],     // File name
                            row["Start From Record"].ToString(),
                            row["End To Record"].ToString(),
                            row["Total Records"].ToString()
                        }
                    );
                }
            }

            if (import_table_rows.Count > 0)
            {
                exception = successes = null;

                if (chk_reformatAccNo.Checked)
                    rtxt_importStusLog.AppendText(" \u24BE Account number reformatting has enabled.\n");

                if (chk_overwriteExistData.Checked)
                    rtxt_importStusLog.AppendText(" \u24BE Existing data overwrite has enabled.\n\n");

                rtxt_importStusLog.AppendText(" \u25B6 UBS Data Migration Operation Start\n");

                foreach (KeyValuePair<string, string[]> table in import_table_rows)
                {
                    string dbf_file_path = stk_table_name.Contains(table.Key) ? // Take from stock folder if is stock table, else take from account folder.
                        txt_path_StkFolder.Text : txt_path_AccFolder.Text;

                    if (!table.Value[3].Equals("0"))
                    {
                        UBSData_Import(dbf_file_path, table.Key, table.Value);
                    }
                    else
                    {
                        rtxt_importStusLog.AppendText($"\n \u26A0 No data found in {table.Key} table, operation skipped.");
                        successes += $"\n- {table.Key}\n|No data found in {table.Key} table.\n>";
                    }
                }

                int failure = exception == null ? 0 : exception.Split('>').Count() - 1,
                    success = successes == null ? 0 : successes.Split('>').Count() - 1;

                rtxt_importStusLog.AppendText($"\n\n\n \u24BE UBS Data Migration Operation Completed ({success} completed, {failure} failure)");
                rtxt_importStusLog.ScrollToCaret();

                if (exception == null)
                {
                    successes = GenerateMessageList(successes);
                    
                    MessageBox.Show(
                        "All selected table has successfully imported.\n" +
                        "Please review the migration information below:\n" +
                        $"{successes}\n",
                        "Data migration successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    exception = GenerateMessageList(exception);

                    MessageBox.Show(
                        "Exceptions has occur when migrating the table below:\n" +
                        $"{exception}\n" +
                        "Please review the status log for more information.",
                        $"Data migration contains error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(
                    "Please select at least 1 table to import.",
                    $"No selected table(s)",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_copyImportStus_Click(object sender, EventArgs e)
        {
            if (rtxt_importStusLog.Text != "")
            {
                Clipboard.SetText(rtxt_importStusLog.Text);

                lb_copied.Visible = true;
            }
        }

        private void btn_copyImportStus_Leave(object sender, EventArgs e)
        {
            lb_copied.Visible = false;
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UBSDataMigrateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool changeMade = (
                (!txt_path_AccFolder.Text.Equals("") && !txt_path_StkFolder.Text.Equals("")) ||
                dgv_selTblImport.Rows.Count > 0
                ) ? true : false;

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
            DataTable dataTable = target_table;

            string file_path = $"{text_box.Text}\\{dbf_file_name}.dbf";
            int total_rec = 0;

            var options = new DbfDataReaderOptions { SkipDeletedRecords = true };

            try
            {
                using (var dbfDataReader = new DbfDataReader.DbfDataReader(file_path, options))
                    while (dbfDataReader.Read()) total_rec++;

                if (dataTable.Rows.Find(table_name) == null)
                    dataTable.Rows.Add(false, table_name, "-", "-", total_rec);
            }
            catch (Exception ex)
            {
                exception += $"\n\n{ex.Message}|\n- {dbf_file_name} ({table_name})>";
            }
        }

        private void UBSData_Import(string dbf_file_path, string table_name, string[] table_details)
        {
            string file_path = $"{dbf_file_path}\\{table_details[0]}.dbf";
            string exc = null;

            int start_from_rec = table_details[1].Equals("-") ? 1 : int.Parse(table_details[1]);
            int end_to_rec = table_details[2].Equals("-") ? 0 : int.Parse(table_details[2]);
            int total_rec = int.Parse(table_details[3]);

            if (end_to_rec == 0) end_to_rec = total_rec;

            int current_record = 0, success = 0, failure = 0;

            var options = new DbfDataReaderOptions { SkipDeletedRecords = true };

            using (var dbfDataReader = new DbfDataReader.DbfDataReader(file_path, options))
            {
                rtxt_importStusLog.AppendText($"\n\n----------\n" +
                    $" \u25B6 Start import {table_name} data from record number {start_from_rec} to {end_to_rec}.\n");

                while (dbfDataReader.Read())
                {
                    current_record++;

                    if ((start_from_rec <= current_record && current_record <= end_to_rec))
                    {
                        txt_currentRecNo.Text = current_record.ToString();
                        try
                        {
                            switch (table_name)
                            {
                                case "Sales Agent":
                                    ProcessImport_SalesAgent(dbfDataReader, current_record);
                                    break;
                                case "Purchase Agent":
                                    ProcessImport_PurchaseAgent(dbfDataReader, current_record);
                                    break;
                                case "Area":
                                    ProcessImport_Area(dbfDataReader, current_record);
                                    break;
                                case "Project":
                                    ProcessImport_Project(dbfDataReader, current_record);
                                    break;
                                case "Currency":
                                    ProcessImport_Currency(dbfDataReader, current_record);
                                    break;
                                case "Customer":
                                    ProcessImport_Debtor(dbfDataReader, current_record);
                                    break;
                                case "Supplier":
                                    ProcessImport_Creditor(dbfDataReader, current_record);
                                    break;
                                case "Category":
                                    ProcessImport_ItemCategory(dbfDataReader, current_record);
                                    break;
                                case "Group":
                                    ProcessImport_ItemGroup(dbfDataReader, current_record);
                                    break;
                                case "Location":
                                    ProcessImport_ItemLocation(dbfDataReader, current_record);
                                    break;
                                case "Item":
                                    ProcessImport_Item(dbfDataReader, current_record);
                                    break;
                                default:
                                    exception += $"{table_details[0]}({table_name}) does not belong to the backup data table.\n";
                                    break;
                            }
                            success++;
                        }
                        catch (Exception ex)
                        {
                            exc = $"\nTable: {table_name}\n|- {ex.GetType().Name}\n>";
                            exception += exc;

                            string exc_msg;

                            try 
                            { exc_msg = $"{ex.InnerException.Message}: {ex.Message}"; }
                            catch (NullReferenceException) 
                            { exc_msg = ex.Message; }

                            rtxt_importStusLog.AppendText($"\n \u274C (Rec {current_record}) {exc_msg}");
                            failure++;
                        }
                    }

                    rtxt_importStusLog.ScrollToCaret();
                }
            }
            if (exc == null) 
                successes += $"\n- {table_name} \n|(total {total_rec} records, {success} imported (No. {start_from_rec} to {end_to_rec})) \n>";

            rtxt_importStusLog.AppendText($"\n\n \u24BE Data migration of {table_name} has been completed. ({success} success, {failure} failed)" +
                $"\n----------");
            rtxt_importStusLog.ScrollToCaret();
        }

        private void ProcessImport_SalesAgent(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            Agents agent = new Agents(Program.session);

            string agent_name = dbfDataReader.GetString(0);
            string agent_desc = dbfDataReader.GetString(1);

            string sts_word = DefStatus();

            agent.CreateOrUpdate_SalesAgent(chk_overwriteExistData.Checked, agent_name, agent_desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} sales agent : {agent_name} - {agent_desc}");
        }

        private void ProcessImport_PurchaseAgent(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            Agents agent = new Agents(Program.session);

            string agent_name = dbfDataReader.GetString(0);
            string agent_desc = dbfDataReader.GetString(1);

            string sts_word = DefStatus();

            agent.CreateOrUpdate_PurchaseAgent(chk_overwriteExistData.Checked, agent_name, agent_desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} purchase agent : {agent_name} - {agent_desc}");
        }

        private void ProcessImport_Area(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            Areas area = new Areas(Program.session);

            string area_code = dbfDataReader.GetString(0);
            string area_desc = dbfDataReader.GetString(1);

            string sts_word = DefStatus();

            area.CreateOrUpdate_Area(chk_overwriteExistData.Checked, area_code, area_desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} area : {area_code} - {area_desc}");
        }

        private void ProcessImport_Project(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            Projects projects = new Projects(Program.session);

            string project_no = dbfDataReader.GetString(0);
            string project_desc = dbfDataReader.GetString(1);
            string project_type = dbfDataReader.GetString(2);

            string sts_word = DefStatus();

            projects.CreateOrUpdate_Project(chk_overwriteExistData.Checked, project_no, project_desc, project_type);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} project : {project_no} - {project_desc}");
        }

        private void ProcessImport_Currency(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            Currencies currencies = new Currencies(Program.session);

            string currency_code = dbfDataReader.GetString(0);
            string currency_word = dbfDataReader.GetString(1);

            string sts_word = DefStatus();

            currencies.CreateOrUpdate_Currency(chk_overwriteExistData.Checked, currency_code, currency_word);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} currency : {currency_code}");
        }

        private void ProcessImport_Debtor(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            Debtor debtor = new Debtor(Program.session);
            DisplayTerms terms = new DisplayTerms(Program.session);

            string acc_no = chk_reformatAccNo.Checked ? 
                ReformatAccNo(dbfDataReader.GetString(1)) : dbfDataReader.GetString(1);

            string acc_name = dbfDataReader.GetString(2);
            string display_term = dbfDataReader.GetString(25);

            string sts_word = DefStatus();

            debtor.CreateOrUpdate_Debtor(chk_overwriteExistData.Checked, acc_no, dbfDataReader);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} debtor : {acc_no} - {acc_name}");
        }

        private void ProcessImport_Creditor(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            Creditor creditor = new Creditor(Program.session);
            DisplayTerms terms = new DisplayTerms(Program.session);

            string acc_no = chk_reformatAccNo.Checked ?
                ReformatAccNo(dbfDataReader.GetString(1)) : dbfDataReader.GetString(1);

            string acc_name = dbfDataReader.GetString(2);

            string sts_word = DefStatus();

            creditor.CreateOrUpdate_Creditor(chk_overwriteExistData.Checked, acc_no, dbfDataReader);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} creditor : {acc_no} - {acc_name}");
        }

        private void ProcessImport_ItemGroup(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            ItemGroups groups = new ItemGroups(Program.session);

            string item_group = dbfDataReader.GetString(0);
            string desc = dbfDataReader.GetString(1);

            string sts_word = DefStatus();

            groups.CreateOrUpdate_ItemGroup(chk_overwriteExistData.Checked, item_group, desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} item category : {item_group} ({desc})");
        }

        private void ProcessImport_ItemCategory(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            ItemCategories categories = new ItemCategories(Program.session);

            string short_code = dbfDataReader.GetString(0);
            string desc = dbfDataReader.GetString(1);

            string sts_word = DefStatus();

            categories.CreateOrUpdate_ItemCategory(chk_overwriteExistData.Checked, short_code, desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} item category : {short_code} ({desc})");
        }

        private void ProcessImport_ItemLocation(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            Locations locations = new Locations(Program.session);

            string location = dbfDataReader.GetString(0);
            string desc = dbfDataReader.GetString(1);

            string sts_word = DefStatus();

            locations.CreateOrUpdate_Location(chk_overwriteExistData.Checked, location, desc);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} item location : {location} ({desc})");
        }

        private void ProcessImport_Item(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            Items item = new Items(Program.session);

            string sts_word = DefStatus();

            item.CreateOrUpdate_Item(chk_overwriteExistData.Checked, dbfDataReader);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} stock item : {dbfDataReader.GetString(1)}");
        }

        //Utilities-------------------------------------------------------------------------------------------//

        private void DGVTable_Load()
        {
            DataTable dataTable_display = new DataTable("Import_Tables");
            DataTable dataTable_backing = new DataTable("Stock_Tables");

            dataTable_display.Columns.Add("Select"              , typeof(bool));
            dataTable_display.Columns.Add("Table Name"          , typeof(string));
            dataTable_display.Columns.Add("Start From Record"   , typeof(object));
            dataTable_display.Columns.Add("End To Record"       , typeof(object));
            dataTable_display.Columns.Add("Total Records"       , typeof(int));

            dataTable_backing.Columns.Add("Select"              , typeof(bool));
            dataTable_backing.Columns.Add("Table Name"          , typeof(string));
            dataTable_backing.Columns.Add("Start From Record"   , typeof(object));
            dataTable_backing.Columns.Add("End To Record"       , typeof(object));
            dataTable_backing.Columns.Add("Total Records"       , typeof(int));

            dataTable_display.PrimaryKey = new DataColumn[] { dataTable_display.Columns[1] };
            dataTable_backing.PrimaryKey = new DataColumn[] { dataTable_backing.Columns[1] };

            if (dataSet1.Tables["Import_Tables"] == null) dataSet1.Tables.Add(dataTable_display);
            if (dataSet1.Tables["Stock_Tables"] == null) dataSet1.Tables.Add(dataTable_backing);

            dgv_selTblImport.DataSource = dataSet1.Tables["Import_Tables"];

            dgv_selTblImport.Columns[0].Width = 50;
            dgv_selTblImport.Columns[1].Width = 200;
            dgv_selTblImport.Columns[2].Width = 100;
            dgv_selTblImport.Columns[3].Width = 100;
            dgv_selTblImport.Columns[4].Width = 95;

            dgv_selTblImport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void OpenFolderDialog(string desc, Control control)
        {
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                ofd.Description = desc;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        control.Text = ofd.SelectedPath;
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
            FileInfo file = new FileInfo($"{file_path}\\{file_name}.MEM");
            
            return file.Exists;
        }

        private void SetRecordRange(string column, NumericUpDown nud)
        {
            if (dgv_selTblImport.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_selTblImport.SelectedRows)
                {
                    if (0 < nud.Value && (int)nud.Value <= (int)row.Cells["Total Records"].Value) 
                        row.Cells[column].Value = nud.Value;
                    else if (nud.Value == 0)
                        row.Cells[column].Value = "-";
                }
            }

            dgv_selTblImport.Refresh();
        }

        private void CalibrateRecordRange()
        {
            if (dgv_selTblImport.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_selTblImport.SelectedRows)
                {
                    if (nud_recRangeEnd.Value > 0 && row.Cells["Start From Record"].Value.Equals("-"))
                    {
                        if ((int)row.Cells["Total Records"].Value > 0)
                        {
                            row.Cells["Start From Record"].Value = 1;

                            nud_recRangeStart.Maximum = 1;
                            nud_recRangeStart.Value = 1;
                        }
                    }
                    else if (nud_recRangeEnd.Value == 0)
                    {
                        row.Cells["Start From Record"].Value = "-";

                        nud_recRangeStart.Maximum = 0;
                    }

                    nud_recRangeStart.Minimum = nud_recRangeEnd.Value > 0 ? 1 : 0;
                }
            }

            dgv_selTblImport.Refresh();
        }

        private void SyncRecordRange()
        {
            syncing_range = true;
            
            foreach (DataGridViewRow row in dgv_selTblImport.SelectedRows)
            {
                var start = row.Cells["Start From Record"].Value.Equals("-") ?
                    0 : row.Cells["Start From Record"].Value;

                var end = row.Cells["End To Record"].Value.Equals("-") ?
                    0 : row.Cells["End To Record"].Value;

                var total = row.Cells["Total Records"].Value.Equals(0) ?
                    0 : row.Cells["Total Records"].Value;

                nud_recRangeStart.Maximum = nud_recRangeEnd.Maximum = decimal.Parse(total.ToString());
                
                nud_recRangeEnd.Value = decimal.Parse(end.ToString());
                nud_recRangeStart.Value = decimal.Parse(start.ToString());
            }

            syncing_range = false;
        }

        private void ResetRecordRange(DataGridViewRow row)
    {
        if (!row.Cells["Start From Record"].Value.Equals("-") || !row.Cells["End To Record"].Value.Equals("-"))
            row.Cells["Start From Record"].Value = row.Cells["End To Record"].Value = "-";
    }

        private void SetCheckBoxSelection(DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.RowIndex < 0) return;

            if (e.RowIndex == 0) // If table "Chart of Account" is selected
            {
                // Set selection of other tables to "Chart of Account"
                for (int i = 1; i < dgv_selTblImport.Rows.Count; i++)
                {
                    dgv_selTblImport[0, i].Value = false;
                }

                return;
            }

            // Else set selection of "Chart of Account"
            for (int i = 1; i < dgv_selTblImport.Rows.Count; i++)
            {
                if ((bool)dgv_selTblImport[0, i].Value)
                {
                    dgv_selTblImport[0, 0].Value = false;
                    break;
                }
            }
        }

        private string GenerateMessageList(string msg_strean)
        {
            List<string[]> msg_list = new List<string[]>();
            
            string[] msg_row_arr = msg_strean.Split('>');
            string printing_text = "";
            string previous_table = "";
            string previous_msg = "";

            foreach (string msg_row in msg_row_arr) { msg_list.Add(msg_row.Split('|')); }

            msg_list.Sort((prev, crnt) => prev[0].CompareTo(crnt[0]));

            foreach (string[] msg_list_row in msg_list)
            {
                if (previous_table != msg_list_row[0])
                {
                    previous_table = msg_list_row[0];
                    printing_text += msg_list_row[0].Equals("") ? "" : msg_list_row[0];
                }

                if (msg_list_row.Length > 1 && !msg_list_row[1].Equals(previous_msg))
                {
                    previous_msg = msg_list_row[1];
                    printing_text += msg_list_row[0].Equals("") ? "" : msg_list_row[1];
                }
            }

            return printing_text;
        }

        private string ExtractComName(string file_path, string file_name)
        {
            var mmf = MemoryMappedFile.CreateFromFile($"{file_path}\\{file_name}.MEM");
            
            var accessor = mmf.CreateViewAccessor(0, 0);

            byte[] stringBytes = new byte[5000];
            accessor.ReadArray(0, stringBytes, 0, 5000);

            string text = Encoding.ASCII.GetString(stringBytes).Replace("\0", "").Replace("\u0001", "");

            while (!text.StartsWith("COMPANY")) { text = text.Remove(0, 1); }

            char[] new_text = new char[85];
            text.CopyTo(0, new_text, 0, 85);
            text = "";

            foreach (char c in new_text) { text += c; }

            mmf.Dispose();
            accessor.Dispose();

            return text.Trim().Remove(0, 10);
        }

        private string ReformatAccNo(string acc_no)
        {
            string[] parts = acc_no.Split('/'); // ["3000", "A01"]

            if (parts.Length != 2) return acc_no; // fallback

            string mainPart = parts[0]; // "3000"
            string subPart = parts[1];  // "A01"

            // Format mainPart to 3 digits
            if (mainPart.Length > 3) mainPart = mainPart.Substring(0, 3);

            // Format subPart: letters + numbers with leading zeros
            string alpha = new string(subPart.Where(c => char.IsLetter(c)).ToArray()); // "A"
            string digit = new string(subPart.Where(c => char.IsDigit(c)).ToArray());  // "01"

            string subFormatted = alpha + digit.PadLeft(3, '0'); // "A001"

            return $"{mainPart}-{subFormatted}"; // "300-A001"
        }

        private string DefStatus()
        {
            return chk_overwriteExistData.Checked ? "Updated" : "Added new";
        }
    }
}
