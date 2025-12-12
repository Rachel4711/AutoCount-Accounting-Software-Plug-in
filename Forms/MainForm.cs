using DbfDataReader;
using PlugIn_1.Entity.Accounts;
using PlugIn_1.Entity.General_Maintainance;
using System;
using System.Windows.Forms;

namespace PlugIn_1.Forms
{
    [AutoCount.PlugIn.MenuItem(
        "UBS Migrate", 10, 
        OpenAccessRight = "", 
        VisibleAccessRight = "")]
        
    public partial class MainForm : Form
    {
        LoginForm loginForm = new LoginForm();

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

                e.Cancel = result.ToString().Equals("No");
            }
        }

        private void btn_UBS_migrate_Click(object sender, EventArgs e)
        {
            if (Program.pro_form == null)
            {
                // Create a new form if no same form opened
                Program.pro_form = new UBSDataMigrateForm();
                
                // Display the form
                Program.pro_form.Show();
            }
            else
            {
                IntPtr hWnd = Program.pro_form.Handle; // Get the handle of the current form

                // Restore the window if it's minimized
                WindowActivator.ShowWindow(hWnd, WindowActivator.SW_RESTORE);

                // Bring the window to the foreground
                WindowActivator.SetForegroundWindow(hWnd);
            }
        }
    }
}
