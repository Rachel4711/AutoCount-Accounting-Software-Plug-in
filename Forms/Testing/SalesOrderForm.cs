using AutoCount.Invoicing.Sales.SalesOrder;
using PlugIn_1.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PlugIn_1.Forms
{
    public partial class SalesOrderForm : Form
    {
        private Dictionary<string, string> outstandSO_DocNo;
        
        public SalesOrderForm()
        {
            InitializeComponent();
            LoadSalesOrderInComboBox();
        }

        public void LoadSalesOrderInComboBox()
        {
            lb_exception.Text = string.Empty;
            
            SalesOrders so = new SalesOrders(Program.session);
            outstandSO_DocNo = so.GetOutstandingSalesOrderDocNo();

            string[] so_DocNo_arr = outstandSO_DocNo.Select(x => x.Key).ToArray();
            comboBox_SO.Items.AddRange(so_DocNo_arr);
        }

        private void button_Create_Click(object sender, EventArgs e)
        {
            SalesOrders salesOrders = new SalesOrders(Program.session);

            try
            {
                salesOrders.CreateUpdateSalesOrder();
                MessageBox.Show("Sales Order has been created.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_CreateRN_Click(object sender, EventArgs e)
        {
            SalesOrders salesOrders = new SalesOrders(Program.session);

            try
            {
                salesOrders.CreateSalesOrderWithRunNum();
                MessageBox.Show("Sales Order has been created with running number.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox_SO_SelectedIndexChanged(object sender, EventArgs e)
        {
            string doc_no = comboBox_SO.Text;
            string debtor_code = outstandSO_DocNo[doc_no];

            lb_exception.ForeColor = Color.Black;
            lb_exception.Text = debtor_code;
        }

        private void button_TransIvc_Click(object sender, EventArgs e)
        {
            SalesInvoice salesInvoice = new SalesInvoice(Program.session);

            try
            {
                string doc_no = comboBox_SO.Text;
                string debtor_code = outstandSO_DocNo[doc_no];

                if (salesInvoice.CreateUpdateSalesInvoice(debtor_code, doc_no))
                {
                    MessageBox.Show("Sales Invoice has been created.", "Operation Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Cannot save the sales invoice.",
                        "Sales Invoice",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Thrown",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_TransPartIvc_Click(object sender, EventArgs e)
        {
            SalesInvoice salesInvoice = new SalesInvoice(Program.session);

            try
            {
                string doc_no = comboBox_SO.Text;
                string debtor_code = outstandSO_DocNo[doc_no];

                salesInvoice.CreateUpdateSalesInvoice_PartialSalesOrder(debtor_code, doc_no, GenerateTransferDetail());

                MessageBox.Show("Partial Sales Invoice has been created.", "Operation Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Thrown",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_CreatePack_Click(object sender, EventArgs e)
        {
            SalesOrders salesOrders = new SalesOrders(Program.session);

            try
            {
                string doc_no = salesOrders.CreateUpdateSalesOrderWithItemPackage();
                MessageBox.Show($"Sales Order {doc_no} has been created with packaged items.", "Operation Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Thrown",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<TransferDetailInfo> GenerateTransferDetail()
        {
            return new List<TransferDetailInfo>
            {
                new TransferDetailInfo
                {
                    item_code = "Item101",
                    Uom = "UNIT",
                    Qty = 1
                },
                new TransferDetailInfo
                {
                    item_code = "Item102",
                    Uom = "UNIT",
                    Qty = 1
                }
            };
        }
    }
}
