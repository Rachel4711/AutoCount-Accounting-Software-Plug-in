using AutoCount.Controls;
using PlugIn_1.Entity;
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
    public partial class DebtorForm : Form
    {
        public DebtorForm()
        {
            InitializeComponent();
        }

        private void dropdownButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void button_Create_Click(object sender, EventArgs e)
        {
            try
            {
                Debtor debtor = new Debtor(Program.session);

                string ctrl_acc = textBox_CtrlAcc.Text;
                string com_name = textBox_ComName.Text;
                string deb_note = richTextBox_DebNote.Text;

                debtor.CreateNewDebtor(ctrl_acc, com_name, "", deb_note);

                MessageBox.Show($"Added new debtor: {com_name}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox_ComName.Text.Equals(""))
            {
                lb_name_inv.Text = "Company name is required.";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
