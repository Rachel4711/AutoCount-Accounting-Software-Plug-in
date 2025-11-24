using System;
using System.Drawing;
using PlugIn_1.Entity;

namespace PlugIn_1.Forms
{
    public partial class XtraForm_LogIn : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_LogIn()
        {
            InitializeComponent();
            AutoCount.AutoCountServer.CommonServiceHelper.ShowDetailErrorMessages = true;
        }

        private void label_title_Click(object sender, EventArgs e)
        {

        }

        private void textBox_ServName_TextChanged(object sender, EventArgs e)
        {
            lb_ServName_inv.Text = ReturnNullValueErrMsg(label_ServName.Text, textBox_ServName.Text);
            if (textBox_ServName.Text.Equals("c"))
            {
                textBox_ServName.Text = @"(local)\SQLEXPRESS07";
            }
        }

        private void textBox_DBName_TextChanged(object sender, EventArgs e)
        {
            lb_DBName_inv.Text = ReturnNullValueErrMsg(label_DBName.Text, textBox_DBName.Text);
            if (textBox_DBName.Text.Equals("x"))
            {
                textBox_DBName.Text = "AED_MOBILE";
            }
        }

        private void textBox_Username_TextChanged(object sender, EventArgs e)
        {
            lb_Username_inv.Text = ReturnNullValueErrMsg(label_Username.Text, textBox_Username.Text);
            if (textBox_Username.Text.Equals("k"))
            {
                textBox_Username.Text = "ADMIN";
            }
        }

        private void textBox_Secret_TextChanged(object sender, EventArgs e)
        {
            lb_Secret_inv.Text = ReturnNullValueErrMsg(label_Password.Text, textBox_Secret.Text);
        }

        private void simpleButton_Clear_Click(object sender, EventArgs e)
        {
            textBox_ServName.ResetText();
            textBox_DBName.ResetText();
            textBox_Username.ResetText();
            textBox_Secret.ResetText();

            lb_ServName_inv.Text = lb_DBName_inv.Text = lb_Username_inv.Text = lb_Secret_inv.Text = null;
        }

        private void simpleButton_Login_Click(object sender, EventArgs e)
        {
            User user = new User(
                textBox_ServName.Text, 
                textBox_DBName.Text, 
                textBox_Username.Text, 
                textBox_Secret.Text
                );

            lb_ServName_inv.Text = ReturnNullValueErrMsg(label_ServName.Text, textBox_ServName.Text);
            lb_DBName_inv.Text = ReturnNullValueErrMsg(label_DBName.Text, textBox_DBName.Text);
            lb_Username_inv.Text = ReturnNullValueErrMsg(label_Username.Text, textBox_Username.Text);
            lb_Secret_inv.Text = ReturnNullValueErrMsg(label_Password.Text, textBox_Secret.Text);

            lb_exception.ForeColor = Color.Black;
            lb_exception.Text = "Signing you in, just a moment.";

            try
            {
                user.ConnectToSession();

                if (user.GetSession().IsLogin)
                {
                    Program.user = user;
                    Program.main_form = new XtraForm1();

                    Close();

                    lb_exception.ForeColor = Color.Black;
                    lb_exception.Text = "Status: " + user.GetSession().IsLogin;
                }
                else
                {
                    lb_exception.ForeColor = Color.Red;
                    lb_exception.Text = "Please ensure you have correct username and password provided here.";
                }
            }
            catch (Exception ex)
            {
                lb_exception.ForeColor = Color.Red;
                lb_exception.Text = ex.InnerException.Message;
            }
        }

        private string ReturnNullValueErrMsg(string control_name, string text)
        {
            if (text.Equals(""))
            {
                return control_name + " is required.";
            }

            return null;
        }

        private void XtraForm_LogIn_Load(object sender, EventArgs e)
        {

        }
    }
}