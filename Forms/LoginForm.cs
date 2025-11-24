using AutoCount.Data;
using DevExpress.Emf;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Microsoft.Identity.Client;
using PlugIn_1.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlugIn_1.Forms
{
    public partial class LoginForm : Form
    {
        PlugInMain plugInMain = new PlugInMain();
        beforeLoadPopUp popUp = new beforeLoadPopUp();
        
        public LoginForm()
        {
            InitializeComponent();
            AutoCount.AutoCountServer.CommonServiceHelper.ShowDetailErrorMessages = true;

            //if (!Program.appHasLoaded)
            //{
            //    Thread thread = new Thread(() =>
            //    {
            //        Application.Run(popUp);
            //    });

            //    thread.Start();

            //    LoadAvailableServers();

            //    thread.Abort();

            //    Select();
            //    Program.appHasLoaded = true;
            //}
        }

        private void comboBox_ServName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox_DBName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox_ServName_TextChanged(object sender, EventArgs e)
        {
            lb_blank_SN.Text = BlankFieldAlertLabel(comboBox_ServName.Text);

            if (comboBox_ServName.Text != "")
            {
                comboBox_DBName.Enabled = btn_search_DBN.Enabled = true;
            }
            else
            {
                comboBox_DBName.Text = "";
                comboBox_DBName.Enabled = btn_search_DBN.Enabled = false;
            }
        }

        private void comboBox_DBName_TextChanged(object sender, EventArgs e)
        {
            lb_blank_DBN.Text = BlankFieldAlertLabel(comboBox_DBName.Text);
        }

        private void textBox_Username_TextChanged(object sender, EventArgs e)
        {
            lb_blank_username.Text = BlankFieldAlertLabel(textBox_Username.Text);
        }

        private void textBox_Secret_TextChanged(object sender, EventArgs e)
        {
            lb_blank_secret.Text = BlankFieldAlertLabel(textBox_Secret.Text);
        }

        private async void btn_LogIn_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            
            //lb_blank_SN.Text = BlankFieldAlertLabel(comboBox_ServName.Text);
            //lb_blank_DBN.Text = BlankFieldAlertLabel(comboBox_DBName.Text);
            //lb_blank_username.Text = BlankFieldAlertLabel(textBox_Username.Text);
            //lb_blank_secret.Text = BlankFieldAlertLabel(textBox_Secret.Text);

            try
            {
                //User user = new User(
                //    comboBox_ServName.Text, 
                //    comboBox_DBName.Text,
                //    textBox_Username.Text, 
                //    textBox_Secret.Text
                //    );

                User user = new User(
                    "INTERN-MINIPC\\SQLEXPRESS08",
                    "AED_Blank",
                    "ADMIN",
                    "ADMIN"
                    );

                loadingGif.Visible = true;
                loadingGif.Start();

                await Task.Run(() =>
                {
                    user.ConnectToSession();
                });

                loadingGif.Visible = false;
                loadingGif.Stop();

                if (user.GetSession().IsLogin)
                {
                    Program.session = user.GetSession();
                    Program.main_form = new MainForm();

                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Please ensure you have correct credential provided here.", 
                        "Failure log in", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            } 
            catch (DataAccessException dex)
            {
                MessageBox.Show(
                    dex.Message,
                    $"Exception occur: {dex.GetBaseException().GetType().Name}",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                loadingGif.Visible = false;
                loadingGif.Stop();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message, 
                    $"Exception occur: {ex.GetBaseException().GetType().Name}", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                loadingGif.Visible = false;
                loadingGif.Stop();
            }

            Cursor = Cursors.Default;
        }

        private void btn_search_SN_Click(object sender, EventArgs e)
        {
            comboBox_ServName.Text = "Searching...";
            Cursor = Cursors.WaitCursor;

            Refresh();

            try
            {
                LoadAvailableServers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    $"External error from {ex.GetBaseException().GetType().Name}",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor = Cursors.Default;
            comboBox_ServName.Text = "";
        }

        private void btn_search_DBN_Click(object sender, EventArgs e)
        {
            comboBox_DBName.Text = "Searching...";
            Cursor = Cursors.WaitCursor;

            try
            {
                FindDatabases();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    $"External error from {ex.GetBaseException().GetType().Name}",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor = Cursors.Default;
            comboBox_DBName.Text = "";
        }

        private string BlankFieldAlertLabel(string text)
        {
            return text == "" ? "Required" : "*";
        }

        public void LoadAvailableServers()
        {
            string[] server_names;
            DialogResult result = new DialogResult();

            if (comboBox_ServName.Items.Count > 0)
            {
                result = MessageBox.Show(
                    "The server list has already loaded. \n" +
                    "Reload the list of server name could take longer time, proceed to reload?",
                    "Reload server name list",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result.ToString().Equals("Yes"))
                {
                    server_names = plugInMain.GetAvailableServer().ToArray();

                    comboBox_ServName.Items.Clear();
                    comboBox_ServName.Items.AddRange(server_names);
                }
            }
            else
            {
                server_names = plugInMain.GetAvailableServer().ToArray();
                comboBox_ServName.Items.AddRange(server_names);
            }
        }

        public void FindDatabases()
        {
            string[] databases = plugInMain.GetAvailableDatabase(comboBox_ServName.Text).ToArray();

            if (comboBox_DBName.Items.Count == 0)
            {
                comboBox_DBName.Items.AddRange(databases);
            }
            else
            {
                comboBox_DBName.Items.Clear();
                comboBox_DBName.Items.AddRange(databases);
            }
        }

    }
}
