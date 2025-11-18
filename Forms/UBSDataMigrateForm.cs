using DbfDataReader;
using PlugIn_1.Entity.General_Maintainance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Creditor = PlugIn_1.Entity.Account.Creditor;
using Debtor = PlugIn_1.Entity.Debtor;

namespace PlugIn_1.Forms
{
    public partial class UBSDataMigrateForm : Form
    {
        private string exception { get; set; }

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
                "Here are the guides to set record importing range:\n" +
                "1. Select the entire row by click on most left side of the row.\n" +
                "2. Set both value to 0 to import all records in the table.\n" +
                "3. Set both value to the same number to import single record.\n" +
                "4. The value of \"Start from record\" should less than \"End to record\".\n" +
                "5. The value of both \"Start from record\" and \"End to record\" should less than total number of records in selected table.\n",
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
            if (nud_recRangeStart.Value != 0)
            {
                SetRecordRange("Start From Record", nud_recRangeStart);
            }
            CalibrateRecordRange();
        }

        private void nud_recRangeEnd_ValueChanged(object sender, EventArgs e)
        {
            SetRecordRange("End To Record", nud_recRangeEnd);
            CalibrateRecordRange();

            if (nud_recRangeStart.Value > nud_recRangeEnd.Value)
            {
                nud_recRangeStart.Value = nud_recRangeEnd.Value;
            }
        }

        private void dgv_selModImport_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
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
            else
            {
                List<string> list = new List<string>();

                exception = null;

                DGVTable_Load();

                UBSData_Load(txt_path_AccFolder, "gldata", "Chart of Account");
                UBSData_Load(txt_path_AccFolder, "icagent", "Agent");
                UBSData_Load(txt_path_AccFolder, "icarea", "Area");
                UBSData_Load(txt_path_AccFolder, "project", "Project");
                UBSData_Load(txt_path_AccFolder, "accmem", "Terms");
                UBSData_Load(txt_path_AccFolder, "currency", "Currency");
                UBSData_Load(txt_path_AccFolder, "arcust", "Customer");
                UBSData_Load(txt_path_AccFolder, "apvend", "Supplier");

                if (exception != null)
                {
                    string printing_text = GenerateExceptionList(exception);

                    MessageBox.Show(
                        "The table cannot be listed because of the errors below:\n" +
                        printing_text,
                        "External error occur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    gb_dataMigrate.Enabled = txt_path_AccFolder.Equals("") ? false : true;
                }
            }
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            DataTable dataTable = dataSet1.Tables["Table"];

            rtxt_importStusLog.Text = "";

            Dictionary<string, string[]> module_rows = new Dictionary<string, string[]>();
            Dictionary<string, string> name_to_file = new Dictionary<string, string>()
            {
                {"Chart of Account" , "gldata"},
                {"Agent"            , "icagent"},
                {"Area"             , "icarea"},
                {"Project"          , "project"},
                {"Terms"            , "accmem"},
                {"Currency"         , "currency"},
                {"Customer"         , "arcust"},
                {"Supplier"         , "apvend"},
            };

            string[] stk_file_name =
            {
                "iccate",
                "icgroup",
                "icitem",
                "iclocate",
                "icl3p"
            };

            foreach (DataRow row in dataTable.Rows)
            {
                if ((bool)row["Select"])
                {
                    module_rows.Add(
                        name_to_file[row["Module Name"].ToString()],
                        new string[]
                        {
                            row["Module Name"].ToString(),
                            row["Start From Record"].ToString(),
                            row["End To Record"].ToString(),
                            row["Total Records"].ToString()
                        }
                    );
                }
            }

            if (module_rows.Count > 0)
            {
                exception = null;

                foreach (KeyValuePair<string, string[]> module in module_rows)
                {
                    string dbf_file_path = stk_file_name.Contains(module.Key) ?
                        txt_path_StkFolder.Text : txt_path_AccFolder.Text;

                    UBSData_Import(dbf_file_path, module.Key, module.Value);
                }

                if (exception != null)
                {
                    exception = GenerateExceptionList(exception);

                    MessageBox.Show(
                        "Exception has occur when migrating the table below:\n" +
                        exception + "\n" +
                        "Please review the status log for more information.",
                        $"Data migration failure",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(
                        "",
                        "Data migration successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btn_copyImportStus_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtxt_importStusLog.Text);

            lb_copied.Visible = true;
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UBSDataMigrateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool changeMade = (
                (txt_path_AccFolder.Text.Equals("") && txt_path_StkFolder.Text.Equals("")) ||
                dgv_selModImport.Rows.Count == 0
                ) ? false : true;

            if (changeMade)
            {
                DialogResult result = MessageBox.Show(
                "Any information given here wil be LOST. Confirm to quit from data migration?",
                "Exit from Data Migration",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                e.Cancel = result.ToString().Equals("No") ? true : false;
            }
        }

        private void DGVTable_Load()
        {
            DataTable dataTable = new DataTable("Table");

            dataTable.Columns.Add("Select", typeof(bool));
            dataTable.Columns.Add("Module Name", typeof(string));
            dataTable.Columns.Add("Start From Record", typeof(object));
            dataTable.Columns.Add("End To Record", typeof(object));
            dataTable.Columns.Add("Total Records", typeof(int));

            DataColumn[] column = {dataTable.Columns[1]};
            dataTable.PrimaryKey = column;

            if (dataSet1.Tables["Table"] == null)
            {
                dataSet1.Tables.Add(dataTable);
            }

            dgv_selModImport.DataSource = dataSet1.Tables["Table"];

            dgv_selModImport.Columns[0].Width = 50;
            dgv_selModImport.Columns[1].Width = 200;
            dgv_selModImport.Columns[2].Width = 100;
            dgv_selModImport.Columns[3].Width = 100;
            dgv_selModImport.Columns[4].Width = 95;

            dgv_selModImport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_selModImport.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_selModImport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_selModImport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_selModImport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void UBSData_Load(TextBox textBox, string dbf_file_name, string module_name)
        {
            DataTable dataTable = dataSet1.Tables["Table"];

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

                if (dataTable.Rows.Find(module_name) == null)
                {
                    dataTable.Rows.Add(false, module_name, "-", "-", total_rec);
                }
            }
            catch (Exception ex)
            {
                exception += $"{dbf_file_name} ({module_name})|{ex.Message}\n";
            }
        }

        private void UBSData_Import(string dbf_file_path, string dbf_file_name, string[] module_details)
        {
            string file_path = $"{dbf_file_path}\\{dbf_file_name}.dbf";
            string module_name = module_details[0] ;
            
            int start_from_rec   = module_details[1].Equals("-") ? 1 : int.Parse(module_details[1]);
            int end_to_rec       = module_details[2].Equals("-") ? 0 : int.Parse(module_details[2]);
            int total_rec = int.Parse(module_details[3]);

            int current_record = 0, success = 0, failure = 0;

            end_to_rec = end_to_rec == 0 ? total_rec : end_to_rec;
            
            var options = new DbfDataReaderOptions
            {
                SkipDeletedRecords = true
            };

            using (var dbfDataReader = new DbfDataReader.DbfDataReader(file_path, options))
            {
                rtxt_importStusLog.Text += $" \u25B6 Start import {module_name} data from record number {start_from_rec} to {end_to_rec}.\n";

                while (dbfDataReader.Read())
                {
                    current_record++;

                    if ((start_from_rec <= current_record && current_record <= end_to_rec))
                    {
                        txt_currentRecNo.Text = current_record.ToString();
                        try
                        {
                            switch (module_name)
                            {
                                case "Agent":
                                    ProcessImport_Agent(dbfDataReader);
                                    break;
                                case "Area":
                                    ProcessImport_Area(dbfDataReader);
                                    break;
                                case "Project":
                                    break;
                                case "Terms":
                                    break;
                                case "Currency":
                                    ProcessImport_Currency(dbfDataReader);
                                    break;
                                case "Customer":
                                    ProcessImport_Debtor(dbfDataReader);
                                    break;
                                case "Supplier":
                                    ProcessImport_Creditor(dbfDataReader);
                                    break;
                                default:
                                    throw new Exception($"{dbf_file_name}({module_name}) does not belong to the backup data table.");
                            }
                            success++;
                        }
                        catch (Exception ex)
                        {
                            exception += $"{module_name}|{ex.GetType().ToString()}\n";

                            string msg;

                            try
                            { 
                                msg = ex.InnerException.Message; 
                            }
                            catch (NullReferenceException)
                            { 
                                msg = ex.Message;  
                            }

                            rtxt_importStusLog.AppendText($"\n \u274C (Rec no. {current_record}) {msg}");
                            failure++;
                        }
                    }

                    rtxt_importStusLog.ScrollToCaret();
                }
            }

            rtxt_importStusLog.AppendText($"\n\n \u24BE Data migration of {module_name} has been completed. ({success} success, {failure} failed)\n\n");
            rtxt_importStusLog.ScrollToCaret();
        }

        private void ProcessImport_Agent(DbfDataReader.DbfDataReader dbfDataReader)
        {
            Agents agent = new Agents(Program.session);

            string agent_name = dbfDataReader.GetString(0);
            string agent_desc = dbfDataReader.GetString(1);

            agent.NewSalesAgent(agent_name, agent_desc);

            rtxt_importStusLog.AppendText($"\n \u2705 Added agent : {agent_name} --- {agent_desc}");
        }

        private void ProcessImport_Area(DbfDataReader.DbfDataReader dbfDataReader)
        {
            Areas area = new Areas(Program.session);

            string area_code = dbfDataReader.GetString(0);
            string area_desc = dbfDataReader.GetString(1);

            area.NewArea(area_code, area_desc);

            rtxt_importStusLog.AppendText($"\n \u2705 Added area : {area_code} --- {area_desc}");
        }

        private void ProcessImport_Currency(DbfDataReader.DbfDataReader dbfDataReader)
        {
            Currencies currencies = new Currencies(Program.session);

            string currency_code = dbfDataReader.GetString(0);
            string currency_word = dbfDataReader.GetString(1);

            currencies.NewCurrency(currency_code, currency_word);

            rtxt_importStusLog.AppendText($"\n \u2705 Created currency : {currency_code}");
        }

        private void ProcessImport_Debtor(DbfDataReader.DbfDataReader dbfDataReader)
        {
            Debtor debtor = new Debtor(Program.session);

            string acc_no = dbfDataReader.GetString(1);
            string acc_name = dbfDataReader.GetString(2);

            debtor.CreateNewDebtor(dbfDataReader);

            rtxt_importStusLog.AppendText($"\n \u2705 Added debtor : {acc_no} --- {acc_name}");
        }

        private void ProcessImport_Creditor(DbfDataReader.DbfDataReader dbfDataReader)
        {
            Creditor creditor = new Creditor(Program.session);

            string acc_no = dbfDataReader.GetString(1);
            string acc_name = dbfDataReader.GetString(2);

            creditor.CreateNewCreditor(dbfDataReader);

            rtxt_importStusLog.AppendText($"\n \u2705 Added creditor : {acc_no} --- {acc_name}");
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
                            $"External error from {ex.GetBaseException().GetType().BaseType.ToString()}",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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
                    }

                    nud_recRangeStart.Minimum = nud_recRangeEnd.Value > 0 ? 1 : 0;
                }
            }

            dgv_selModImport.Refresh();
        }

        private string GenerateExceptionList(string exception)
        {
            string[] prime_arr = exception.Split('\n');
            string printing_text = "";
            string previous_ex = "";
            string previous_table = "";

            foreach (string prime in prime_arr)
            {
                string[] arr = prime.Split('|');

                try
                {
                    if (arr[1] != previous_ex)
                    {
                        previous_ex = arr[1];
                        printing_text += "\nError: " + arr[1] + "\n";
                    }
                }
                catch (IndexOutOfRangeException) { }

                if (arr[0] != previous_table)
                {
                    previous_table = arr[0];
                    printing_text += prime.Equals("") ? "" : "- " + arr[0] + "\n";
                }
            }

            return printing_text;
        }

        //private string GenerateSuccessList(string success)
        //{

        //}
    }
}
