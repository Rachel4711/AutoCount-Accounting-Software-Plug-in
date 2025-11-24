using DbfDataReader;
using DevExpress.XtraRichEdit.Model;
using PlugIn_1.Entity;
using PlugIn_1.Entity.General_Maintainance;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Creditor = PlugIn_1.Entity.Account.Creditor;
using Debtor = PlugIn_1.Entity.Debtor;

namespace PlugIn_1.Forms
{
    public partial class UBSDataMigrateForm : Form
    {
        private string exception { get; set; }

        private string successes { get; set; }

        private bool syncing_range = false;

        private bool isPathChanging = false;

        public UBSDataMigrateForm()
        {
            InitializeComponent();
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Use 7zip software to extract .ACC file to a new folder, and .STK file to another new folder.\r\n" +
                "Name the folders accordingly and copy the folder paths to the corresponding field below.",
                "Data Import Help Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                "Import Range Setting Help Guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                btn_listModule.Enabled =
                txt_path_AccFolder.Text.Equals("") ? false : true;

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
            if (!syncing_range)
            {
                SetRecordRange("Start From Record", nud_recRangeStart);
            }
        }

        private void nud_recRangeEnd_ValueChanged(object sender, EventArgs e)
        {
            if (!syncing_range)
            {
                SetRecordRange("End To Record", nud_recRangeEnd);
                CalibrateRecordRange();

                if (nud_recRangeStart.Value > nud_recRangeEnd.Value)
                {
                    nud_recRangeStart.Value = nud_recRangeEnd.Value;
                }
            }
            else
            {
                nud_recRangeStart.Minimum = nud_recRangeEnd.Value > 0 ? 1 : 0;
            }
        }

        private void dgv_selModImport_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SyncRecordRange();
        }

        private void dgv_selModImport_SelectionChanged(object sender, EventArgs e)
        {
            SyncRecordRange();
        }

        private void btn_resetRange_Click(object sender, EventArgs e)
        {
            syncing_range = true;
            
            foreach (DataGridViewRow row in dgv_selModImport.SelectedRows)
            {
                ResetRecordRange(row);
            }

            syncing_range = false;

            nud_recRangeStart.Value = nud_recRangeEnd.Value = 0;
        }

        private void btn_resetAllRange_Click(object sender, EventArgs e)
        {
            syncing_range = true;
            
            foreach (DataGridViewRow row in dgv_selModImport.Rows)
            {
                ResetRecordRange(row);
            }

            syncing_range = false;

            nud_recRangeStart.Value = nud_recRangeEnd.Value = 0;
        }

        private void btn_listModule_Click(object sender, EventArgs e)
        {
            if (txt_path_AccFolder.Text.Equals(""))
            {
                MessageBox.Show(
                    "UBS Account Folder Path is required.",
                    "Missing Required File Path",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (isPathChanging)
            {
                List<string> list = new List<string>();

                exception = null;

                if (dgv_selModImport.RowCount > 0)
                {
                    dataSet1.Tables.Remove("Import_Tables");
                }

                Text = "Migrate UBS Account";
                lbl_ImportTitle.Text = "Select and Import Table";

                DGVTable_Load();

                DataTable dataTable_show = dataSet1.Tables["Import_Tables"];
                DataTable dataTable_back = dataSet2.Tables["Stock_Tables"];

                UBSData_Load(dataTable_show, txt_path_AccFolder, "gldata", "Chart of Account");
                UBSData_Load(dataTable_show, txt_path_AccFolder, "icagent", "Sales Agent");

                if (txt_path_StkFolder.Text != "") 
                    UBSData_Load(dataTable_show, txt_path_StkFolder, "icagent", "Purchase Agent");

                UBSData_Load(dataTable_show, txt_path_AccFolder, "icarea", "Area");
                UBSData_Load(dataTable_show, txt_path_AccFolder, "project", "Project");
                UBSData_Load(dataTable_show, txt_path_AccFolder, "accmem", "Terms");
                UBSData_Load(dataTable_show, txt_path_AccFolder, "currency", "Currency");
                UBSData_Load(dataTable_show, txt_path_AccFolder, "arcust", "Customer");
                UBSData_Load(dataTable_show, txt_path_AccFolder, "apvend", "Supplier");

                if (txt_path_StkFolder.Text != "")
                {
                    UBSData_Load(dataTable_show, txt_path_StkFolder, "icitem", "Item");
                    UBSData_Load(dataTable_show, txt_path_StkFolder, "iccate", "Category");
                    UBSData_Load(dataTable_show, txt_path_StkFolder, "icgroup", "Group");
                    UBSData_Load(dataTable_show, txt_path_StkFolder, "iclocate", "Location");
                }

                if (exception != null)
                {
                    string printing_text = GenerateMessageList(exception);

                    MessageBox.Show(
                        "The table cannot be listed because of the errors below:\n" +
                        printing_text,
                        "External error occur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    gb_dataMigrate.Enabled = txt_path_AccFolder.Equals("") ? false : true;

                    try
                    {
                        string company_name = ExtractComName();

                        Text = $"Migrate UBS Account ({company_name})";
                        lbl_ImportTitle.Text = $"Import backup data from: {company_name}";
                    }
                    catch (IOException) { }
                }

                isPathChanging = false;
            }
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            DataTable dataTable_show = dataSet1.Tables["Import_Tables"];
            DataTable dataTable_back = dataSet2.Tables["Stock_Tables"];

            rtxt_importStusLog.Text = "";
            lb_copied.Visible = false;

            Dictionary<string, string[]> table_rows = new Dictionary<string, string[]>();
            Dictionary<string, string> name_to_file = new Dictionary<string, string>()
            {
                {"Chart of Account" , "gldata"},
                {"Sales Agent"      , "icagent"},
                {"Purchase Agent"   , "icagent"},
                {"Area"             , "icarea"},
                {"Project"          , "project"},
                {"Terms"            , ""},
                {"Currency"         , "currency"},
                {"Customer"         , "arcust"},
                {"Supplier"         , "apvend"},
                {"Item"             , "icitem"},
                {"Category"         , "iccate"},
                {"Group"            , "icgroup"},
                {"Location"         , "iclocate"}
            };

            string[] stk_table_name =
            {
                "Purchase Agent",
                "Category",
                "Group",
                "Item",
                "Location",
                "icl3p"
            };

            foreach (DataRow row in dataTable_show.Rows)
            {
                if ((bool)row["Select"])
                {
                    table_rows.Add(
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

            if (dataTable_back.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable_back.Rows)
                {
                    table_rows.Add(
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

            if (table_rows.Count > 0)
            {
                exception = null;

                foreach (KeyValuePair<string, string[]> table in table_rows)
                {
                    string dbf_file_path = stk_table_name.Contains(table.Key) ? // Take from stock folder if is stock table, else take from account folder.
                        txt_path_StkFolder.Text : txt_path_AccFolder.Text;

                    UBSData_Import(dbf_file_path, table.Key, table.Value);
                }

                if (exception != null)
                {
                    exception = GenerateMessageList(exception);

                    MessageBox.Show(
                        "Exceptions has occur when migrating the table below:\n" +
                        $"{exception}\n" +
                        "Please review the status log for more information.",
                        $"Data migration contains error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "All selected table has successfully imported.\n" +
                        "Please check the data migration information below:\n" +
                        $"{successes}\n",
                        "Data migration successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Clipboard.SetText(rtxt_importStusLog.Text);

            lb_copied.Visible = true;
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
                dgv_selModImport.Rows.Count > 0
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

        //----------------------------------------------------------------------------------------------------//

        private void DGVTable_Load()
        {
            DataTable dataTable_display = new DataTable("Import_Tables");
            DataTable dataTable_backing = new DataTable("Stock_Tables");

            dataTable_display.Columns.Add("Select", typeof(bool));
            dataTable_display.Columns.Add("Table Name", typeof(string));
            dataTable_display.Columns.Add("Start From Record", typeof(object));
            dataTable_display.Columns.Add("End To Record", typeof(object));
            dataTable_display.Columns.Add("Total Records", typeof(int));

            dataTable_backing.Columns.Add("Select", typeof(bool));
            dataTable_backing.Columns.Add("Table Name", typeof(string));
            dataTable_backing.Columns.Add("Start From Record", typeof(object));
            dataTable_backing.Columns.Add("End To Record", typeof(object));
            dataTable_backing.Columns.Add("Total Records", typeof(int));

            DataColumn[] column = { dataTable_display.Columns[1] };
            dataTable_display.PrimaryKey = column;

            if (dataSet1.Tables["Import_Tables"] == null) dataSet1.Tables.Add(dataTable_display);
            if (dataSet2.Tables["Stock_Tables"] == null) dataSet2.Tables.Add(dataTable_backing);

            dgv_selModImport.DataSource = dataSet1.Tables["Import_Tables"];

            dgv_selModImport.Columns[0].Width = 50;
            dgv_selModImport.Columns[1].Width = 200;
            dgv_selModImport.Columns[2].Width = 100;
            dgv_selModImport.Columns[3].Width = 100;
            dgv_selModImport.Columns[4].Width = 95;

            dgv_selModImport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_selModImport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_selModImport.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_selModImport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_selModImport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void UBSData_Load(DataTable target_table, TextBox textBox, string dbf_file_name, string table_name)
        {
            DataTable dataTable = target_table;

            string file_path = $"{textBox.Text}\\{dbf_file_name}.dbf";
            int total_rec = 0;

            var options = new DbfDataReaderOptions
            {
                SkipDeletedRecords = true
            };

            try
            {
                using (var dbfDataReader = new DbfDataReader.DbfDataReader(file_path, options))
                {
                    while (dbfDataReader.Read()) total_rec++;
                }

                if (dataTable.Rows.Find(table_name) == null)
                {
                    dataTable.Rows.Add(false, table_name, "-", "-", total_rec);
                }
            }
            catch (Exception ex)
            {
                exception += $"{dbf_file_name} ({table_name})|{ex.Message}\n";
            }
        }

        private void UBSData_Import(string dbf_file_path, string table_name, string[] table_details)
        {
            string file_path = $"{dbf_file_path}\\{table_details[0]}.dbf";

            int start_from_rec = table_details[1].Equals("-") ? 1 : int.Parse(table_details[1]);
            int end_to_rec = table_details[2].Equals("-") ? 0 : int.Parse(table_details[2]);
            int total_rec = int.Parse(table_details[3]);

            end_to_rec = end_to_rec == 0 ? total_rec : end_to_rec;

            int current_record = 0, success = 0, failure = 0;

            var options = new DbfDataReaderOptions
            {
                SkipDeletedRecords = true
            };

            using (var dbfDataReader = new DbfDataReader.DbfDataReader(file_path, options))
            {
                rtxt_importStusLog.AppendText($" \u25B6 Start import {table_name} data from record number {start_from_rec} to {end_to_rec}.\n");

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
                            exception += $"\nTable: {table_name}\n|- {ex.GetType().Name}\n>";

                            string exc_msg;

                            try
                            {
                                exc_msg = $"{ex.InnerException.Message}: {ex.Message}";
                            }
                            catch (NullReferenceException)
                            {
                                exc_msg = ex.Message;
                            }

                            rtxt_importStusLog.AppendText($"\n \u274C (Rec {current_record}) {exc_msg}");
                            failure++;
                        }
                    }

                    rtxt_importStusLog.ScrollToCaret();
                }
            }
            successes += $"\n- {table_name} |(total {total_rec} records, {success} imported (No. {start_from_rec} to {end_to_rec})) \n>";

            rtxt_importStusLog.AppendText($"\n\n \u24BE Data migration of {table_name} has been completed. ({success} success, {failure} failed)\n\n");
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

            string acc_no = dbfDataReader.GetString(1);
            string acc_name = dbfDataReader.GetString(2);
            string display_term = dbfDataReader.GetString(25);

            string sts_word = DefStatus();

            debtor.CreateOrUpdate_Debtor(chk_overwriteExistData.Checked, dbfDataReader);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} debtor : {acc_no} - {acc_name}");
        }

        private void ProcessImport_Creditor(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            Creditor creditor = new Creditor(Program.session);
            DisplayTerms terms = new DisplayTerms(Program.session);

            string acc_no = dbfDataReader.GetString(1);
            string acc_name = dbfDataReader.GetString(2);

            string sts_word = DefStatus();

            creditor.CreateOrUpdate_Creditor(chk_overwriteExistData.Checked, dbfDataReader);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} creditor : {acc_no} - {acc_name}");
        }

        private void ProcessImport_ItemGroup(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            StockItem item = new StockItem(Program.session);

            string short_code = dbfDataReader.GetString(0);
            string desc = dbfDataReader.GetString(1);

            string sts_word = DefStatus();

            item.CreateOrUpdate_ItemGroup(chk_overwriteExistData.Checked, dbfDataReader);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} item category : {short_code} ({desc})");
        }

        private void ProcessImport_ItemCategory(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            StockItem item = new StockItem(Program.session);

            string short_code = dbfDataReader.GetString(0);
            string desc = dbfDataReader.GetString(1);

            string sts_word = DefStatus();

            item.CreateOrUpdate_ItemCategory(chk_overwriteExistData.Checked, dbfDataReader);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} item category : {short_code} ({desc})");
        }

        private void ProcessImport_Item(DbfDataReader.DbfDataReader dbfDataReader, int rec_no)
        {
            StockItem item = new StockItem(Program.session);

            string sts_word = DefStatus();

            item.CreateOrUpdate_Item(chk_overwriteExistData.Checked, dbfDataReader);

            rtxt_importStusLog.AppendText($"\n \u2705 (Rec {rec_no}) {sts_word} stock item : {dbfDataReader.GetString(1)}");
        }

        private void ReformatAccNo(string acc_no)
        {
            if (chk_reformatAccNo.Checked)
            {

            }
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

        private string ExtractComName()
        {
            var mmf = MemoryMappedFile.CreateFromFile($"{txt_path_AccFolder.Text}\\ACCOUNT.MEM");
            
            var accessor = mmf.CreateViewAccessor(offset: 0, size: 0);

            byte[] stringBytes = new byte[5000];
            accessor.ReadArray(0, stringBytes, 0, 5000);

            string text = Encoding.ASCII.GetString(stringBytes).Replace("\0", "").Replace("\u0001", "");

            while (!text.StartsWith("COMPANY")) text = text.Remove(0, 1);

            char[] new_text = new char[85];
            text.CopyTo(0, new_text, 0, 85);
            text = "";

            foreach (char c in new_text)
            {
                text += c;
            }

            return text.Trim().Remove(0, 10);
        }

        private void SetRecordRange(string column, NumericUpDown nud)
        {
            if (dgv_selModImport.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_selModImport.SelectedRows)
                {
                    if (nud.Value > 0)
                    {
                        row.Cells[column].Value = nud.Value;
                    }
                    else
                    {
                        row.Cells[column].Value = "-";
                    }
                }
            }

            dgv_selModImport.Refresh();
        }

        private void CalibrateRecordRange()
        {
            if (dgv_selModImport.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_selModImport.SelectedRows)
                {
                    if (nud_recRangeEnd.Value > 0 && row.Cells["Start From Record"].Value.Equals("-"))
                    {
                        row.Cells["Start From Record"].Value = 1;

                        nud_recRangeStart.Maximum = 1;
                        nud_recRangeStart.Value = 1;
                    }
                    else if (nud_recRangeEnd.Value == 0)
                    {
                        row.Cells["Start From Record"].Value = "-";

                        nud_recRangeStart.Maximum = 0;
                    }

                    nud_recRangeStart.Minimum = nud_recRangeEnd.Value > 0 ? 1 : 0;
                }
            }

            dgv_selModImport.Refresh();
        }

        private void SyncRecordRange()
        {
            syncing_range = true;
            
            foreach (DataGridViewRow row in dgv_selModImport.SelectedRows)
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
            {
                row.Cells["Start From Record"].Value = row.Cells["End To Record"].Value = "-";
            }
        }

        private string GenerateMessageList(string msg_strean)
        {
            List<string[]> exc_list = new List<string[]>();
            
            string[] exc = msg_strean.Split('>');
            string printing_text = "";
            string previous_table = "";
            string previous_ex = "";

            foreach (string exc_list_arr in exc)
            {
                exc_list.Add(exc_list_arr.Split('|'));
            }

            exc_list.Sort((prev, cur) => prev[0].CompareTo(cur[0]));

            foreach (string[] exc_list_item in exc_list)
            {
                if (previous_table != exc_list_item[0])
                {
                    previous_ex = "";
                    previous_table = exc_list_item[0];
                    printing_text += exc_list_item[0].Equals("") ? "" : $"\n{exc_list_item[0]}";
                }

                if (exc_list_item.Length > 1 && !exc_list_item[1].Equals(previous_ex))
                {
                    previous_ex = exc_list_item[1];
                    printing_text += exc_list_item[0].Equals("") ? "" : $"{exc_list_item[1]}";
                }
            }

            return printing_text;
        }

        private string DefStatus()
        {
            return chk_overwriteExistData.Checked ? "Updated" : "Added new";
        }
    }
}
