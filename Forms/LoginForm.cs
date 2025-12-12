using AutoCount.Data;
using PlugIn_1.Entity;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlugIn_1.Forms
{
    public partial class LoginForm : Form
    {
        PlugInMain plugInMain = new PlugInMain();
        
        public LoginForm()
        {
            InitializeComponent();
            AutoCount.AutoCountServer.CommonServiceHelper.ShowDetailErrorMessages = true;

            if (!Program.appHasLoaded)
            {
                Log_In();

                Select();
                Program.appHasLoaded = true;
            }
        }

        private async void Log_In()
        {
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
                    //"AED_Blank",
                    "AED_Format",
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
        }
    }
}
