using AutoCount.Data.EntityFramework;
using DbfDataReader;
using PlugIn_1.Entity.Accounts;
using PlugIn_1.Entity.General_Maintainance;
using System;
using System.Windows.Forms;

using Creditor = PlugIn_1.Entity.Creditor;
using Debtor = PlugIn_1.Entity.Debtor;

namespace PlugIn_1.Forms
{
    [AutoCount.PlugIn.MenuItem(
        "UBS Migrate", 10, 
        OpenAccessRight = "", 
        VisibleAccessRight = "")]
        
    public partial class MainForm : Form
    {
        LoginForm loginForm = new LoginForm();

        private string file_path;

        public MainForm()
        {
            InitializeComponent();

            if (Program.session == null)
            {
                loginForm.ShowDialog();
            }

            group_LogIn.Visible = Program.session != null ? false : true;
            
            Text = Program.session != null ? 
                $"AutoCount AC - UBS Data Migration ({Program.session.DBSetting.DBName.ToString()})" : "User Login";

            file_path = "C:\\Users\\user1\\Downloads\\030524BACKUPACC\\";
        }

        private void btn_LogIn_Click(object sender, EventArgs e)
        {
            var previous_form = Program.main_form.ToString();
            Program.main_form = new LoginForm();

            if (Program.main_form.ShowDialog() == DialogResult.Cancel && Program.session == null)
            {
                Close();
            }
            else
            {
                group_LogIn.Visible = false;

                string dbName = Program.session.DBSetting.DBName.ToString();

                Text = $"AutoCount AC - UBS Data Migration ({dbName})";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.session != null)
            {
                DialogResult result = MessageBox.Show(
                "Confirm to close the application?",
                "Closing application",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                e.Cancel = result.ToString().Equals("No") ? true : false;
            }
        }

        private void btn_UBS_migrate_Click(object sender, EventArgs e)
        {
            UBSDataMigrateForm form = new UBSDataMigrateForm();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Accounts accounts = new Accounts(Program.session);

            var options = new DbfDataReaderOptions { SkipDeletedRecords = true };

            try
            {
                using (var dbfDataReader = new DbfDataReader.DbfDataReader(file_path + "gldata.dbf", options))
                {
                    while (dbfDataReader.Read())
                    {
                        accounts.DeleteAccount(dbfDataReader.GetString(2));
                    }
                }

                MessageBox.Show("Delete Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteData("project");

                MessageBox.Show("Delete Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        private void DeleteData(string file_name) // Testing purpose only
        {
            var options = new DbfDataReaderOptions { SkipDeletedRecords = true };

            using (var dbfDataReader = new DbfDataReader.DbfDataReader(file_path + file_name + ".dbf", options))
            {
                while (dbfDataReader.Read())
                {
                    new Projects(Program.session).DeleteProject(dbfDataReader.GetString(0));
                }
            }
        }
    }
}
