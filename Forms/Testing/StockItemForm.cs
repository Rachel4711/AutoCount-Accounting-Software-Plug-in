using AutoCount;
using AutoCount.ActivityStream;
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
    public partial class StockItemForm : Form
    {
        
        
        public StockItemForm()
        {
            InitializeComponent();
        }

        private void button_Create_Click(object sender, EventArgs e)
        {
            Items stockItem = new Items(Program.session);

            string item_code = textBox_ItemCode.Text;
            string description = textBox_ItemDesc.Text;
            string item_group = textBox_ItemGroup.Text;
            string UOM = textBox_UOM.Text;
            decimal price = Converter.ToDecimal(textBox_Price.Text);
            decimal cost = Converter.ToDecimal(textBox_Cost.Text);

            try
            {
                //stockItem.CreateUpdateItem();

                MessageBox.Show("Added new stock item: {item_code}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_CreateLoc_Click(object sender, EventArgs e)
        {
            Items stockItem = new Items(Program.session);

            try
            {
                //stockItem.CreateUpdateLocation();

                MessageBox.Show("Added new location: {item_code}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
