using AutoCount.Data.EntityFramework;
using AutoCount.Tax.TaxEntityMaintenance;
using System;
using System.Drawing;
using System.Windows.Forms;
using static PlugIn_1.PlugInMain;

namespace PlugIn_1.Forms
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        XtraForm_LogIn form = new XtraForm_LogIn();
        PlugInMain obj = new PlugInMain();

        public XtraForm1()
        {
            InitializeComponent();

            if (Program.user == null)
            {
                form.ShowDialog();
            }

            if (Program.user != null)
            {
                label_title.Text = "Welcome, " + Program.user.GetSession().LoginUserID;
                navigation_Main.Visible = true;
            }
            else
            {
                label_title.Text = "You have not yet signed in.";
                button_SignIn.Visible = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button_SignIn_Click(object sender, EventArgs e)
        {
            Program.main_form = new XtraForm_LogIn();

            try
            {
                if (Program.main_form.ShowDialog() == DialogResult.Cancel)
                {
                    Close();
                }
            }
            catch (ObjectDisposedException)
            {
                Close();
            }
        }

        private void simpleButton_Submit_Click(object sender, EventArgs e)
        {
            label_exception.ForeColor = Color.Black;
            label_exception.Text = "Loading Data...";

            try
            {
                TaxEntityData taxEntityData = obj.GetTaxEntityData(textBox_TIN.Text, textBox_BRN.Text, textBox_Name.Text);

                AutoCount.Tax.TaxEntityMaintenance.TaxEntity taxEntity = obj.GetTaxEntity(Program.user.GetSession(), taxEntityData);

                using (FormCustomTaxEntityView fm = new FormCustomTaxEntityView(taxEntity))
                {
                    fm.ShowDialog();
                }

                label_exception.ForeColor = Color.Black;
                label_exception.Text = "";
            }
            catch (Exception ex)
            {
                label_exception.ForeColor = Color.Red;
                label_exception.Text = ex.Message;
            }
        }

        private void simpleButton_Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (obj.CreateNewTaxEntity(Program.user.GetSession()))
                {
                    navigationPage1.Show();
                }
                else
                {
                    lb_exception.ForeColor = Color.Red;
                    lb_exception.Text = "!";
                } 
            }
            catch (Exception ex)
            {
                lb_exception.ForeColor = Color.Red;
                lb_exception.Text = ex.Message;
            }
        }

        private void lb_exception_Click(object sender, EventArgs e)
        {

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                groupControl_Num.Visible = true;
                groupControl_Name.Visible = false;
            }
            else
            {
                groupControl_Num.Visible = false;
                groupControl_Name.Visible = true;
            }
        }

        private void textBox_ServName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox_TIN_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_BRN_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DebtorForm debtorForm = new DebtorForm();
            debtorForm.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            StockItemForm stockItemForm = new StockItemForm();
            stockItemForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SalesOrderForm salesOrderForm = new SalesOrderForm();
            salesOrderForm.Show();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {

        }

    }
}