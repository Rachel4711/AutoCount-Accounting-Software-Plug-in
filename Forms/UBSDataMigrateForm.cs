using AutoCount.Utils;
using DbfDataReader;
using DevExpress.Utils.Extensions;
using DevExpress.XtraRichEdit.Model;
using PlugIn_1.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace PlugIn_1.Forms
{
    public partial class UBSDataMigrateForm : Form
    {
        private string exception { get; set; }

        private bool isClicked = false;
        
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

        private void btn_browseAccFolder_Click(object sender, EventArgs e)
        {
            string desc = "Select UBS Account Folder";

            OpenFolderDialog(desc, txt_path_AccFolder);
        }

        private void btn_browseStkFolder_Click(object sender, EventArgs e)
        {
            string desc = "Select UBS Stock Folder";

            OpenFolderDialog(desc, txt_path_StkFolder);
        }

        private void nud_recRangeStart_Click(object sender, EventArgs e)
        {
            SetRecordRange("Start From Record", nud_recRangeStart);

            nud_recRangeStart.Maximum = nud_recRangeEnd.Value;
        }

        private void nud_recRangeStart_ValueChanged(object sender, EventArgs e)
        {
            SetRecordRange("Start From Record", nud_recRangeStart);

            nud_recRangeStart.Maximum = nud_recRangeEnd.Value;
        }

        private void nud_recRangeEnd_Click(object sender, EventArgs e)
        {
            SetRecordRange("End To Record", nud_recRangeEnd);
        }

        private void nud_recRangeEnd_ValueChanged(object sender, EventArgs e)
        {
            SetRecordRange("End To Record", nud_recRangeEnd);

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

                var total = row.Cells["Total Records"].Value.Equals("-") ?
                    0 : row.Cells["Total Records"].Value;

                nud_recRangeStart.Maximum = nud_recRangeEnd.Maximum = decimal.Parse(total.ToString());

                nud_recRangeEnd.Value = decimal.Parse(end.ToString());
                nud_recRangeStart.Value = decimal.Parse(start.ToString());
            }
        }

        private void btn_listModule_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();

            exception = null;

            DGVTable_Load();

            UBSData_Load(txt_path_AccFolder, "gldata", "Chart of Account");

            UBSData_Load(txt_path_StkFolder, "icagent", "Agent");
            UBSData_Load(txt_path_StkFolder, "icarea", "Area");
            UBSData_Load(txt_path_AccFolder, "project", "Project");
            UBSData_Load(txt_path_AccFolder, "accmem", "Terms");
            UBSData_Load(txt_path_AccFolder, "currency", "Currency");

            UBSData_Load(txt_path_AccFolder, "arcust", "Customer");
            UBSData_Load(txt_path_AccFolder, "apvend", "Supplier");

            if (exception != null)
            {
                string[] prime_arr = exception.Split('\n');
                string printing_text = "";
                string previous_ex = "";

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

                    printing_text += prime.Equals("") ? "" : "- " + arr[0] + "\n";
                }

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

        private void btn_import_Click(object sender, EventArgs e)
        {
            rtxt_importStusLog.Text = "";
            
            DataTable dataTable = dataSet1.Tables["Table"];

            string strTrue = null;
            string strFalse = null;

            foreach (DataRow row in dataTable.Rows)
            {
                if ((bool)row["Select"])
                {
                    strTrue += $"- {row["Module Name"]} | {row["Start From Record"]} | {row["End To Record"]}\n";
                }
                else
                {
                    strFalse += $"- {row["Module Name"]} | {row["Start From Record"]} | {row["End To Record"]}\n";
                }
            }

            MessageBox.Show(
                $"Selected Module:\n" +
                $"{strTrue}\n" +
                $"Remaining Module:\n" +
                $"{strFalse}");

            if (strTrue != null)
            {
                UBSAccData_Import("arcust");
            }
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Close();
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
            dgv_selModImport.Columns[4].Width = 100;

            dgv_selModImport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_selModImport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_selModImport.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_selModImport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_selModImport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
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

        private void UBSAccData_Import(string dbf_file_name)
        {
            string file_path = $"{txt_path_AccFolder.Text}\\{dbf_file_name}.dbf";
            int current_record = 0, success = 0, failure = 0;

            Exception exception = null;

            var options = new DbfDataReaderOptions
            {
                SkipDeletedRecords = true
            };

            using (var dbfDataReader = new DbfDataReader.DbfDataReader(file_path, options))
            {
                rtxt_importStusLog.Text = " \u25B6 Start import Customer data.";

                while (dbfDataReader.Read())
                {
                    current_record++;
                    txt_currentRecNo.Text = current_record.ToString();
                        
                    Debtor debtor = new Debtor(Program.session);

                    string acc_name = dbfDataReader.GetString(2);
                    string acc_no = dbfDataReader.GetString(1);

                    try
                    {
                        debtor.CreateNewDebtor("300-0000", acc_no, acc_name, RtfUtils.ToRichText("Tahoma", 12, $"Record no. {current_record}"));
                        
                        rtxt_importStusLog.AppendText($"\n \u2705 Success : {acc_no} --- {acc_name}");
                        success++;
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                        
                        rtxt_importStusLog.AppendText($"\n \u274C {ex.Message} : {acc_no} --- {acc_name}");
                        failure++;
                    }

                    rtxt_importStusLog.ScrollToCaret();
                }
            }

            rtxt_importStusLog.AppendText($"\n\n \u24BE Data migration of Debtor has been completed. ({success} success, {failure} failed)");
            rtxt_importStusLog.ScrollToCaret();

            if (exception != null)
            {
                MessageBox.Show(
                    exception.Message,
                    $"Exception occur: {exception.GetBaseException().GetType().BaseType.ToString()}",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                MessageBox.Show(
                    "",
                    "Data migration successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
