using AutoCount.Authentication;
using PlugIn_1.Entity;
using PlugIn_1.Forms.Testing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                e.Cancel = result.ToString().Equals("No") ? true : false;
            }
        }

        private void btn_UBS_migrate_Click(object sender, EventArgs e)
        {
            UBSDataMigrateForm form = new UBSDataMigrateForm();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
