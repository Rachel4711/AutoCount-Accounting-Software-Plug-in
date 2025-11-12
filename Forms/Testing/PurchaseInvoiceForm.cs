using AutoCount.Invoicing.Purchase.PurchaseInvoice;
using PlugIn_1.Entity;
using System;
using System.Windows.Forms;


namespace PlugIn_1.Forms
{
    public partial class PurchaseInvoiceForm : Form
    {
        public PurchaseInvoiceForm()
        {
            InitializeComponent();
        }

        private void button_Create_Click(object sender, EventArgs e)
        {
            PurchaseInvoices purchaseInvoice = new PurchaseInvoices(Program.session);

            try
            {
                string doc_no = purchaseInvoice.CreatePurchaseInvoice();

                MessageBox.Show($"Purchase Invoice {doc_no} has been created.", "Operation Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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
