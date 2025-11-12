using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlugIn_1.Forms
{
    public partial class beforeLoadPopUp : Form
    {
        public beforeLoadPopUp()
        {
            InitializeComponent();
        }

        private void loadingGif_Load(object sender, EventArgs e)
        {
            loadingGif.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
