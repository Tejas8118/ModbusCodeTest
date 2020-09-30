
using ModbusMasterFramework.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusMasterFramework
{
    public partial class Validate : Form
    {
        const int productCode=1;
        public Validate()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            if (Settings1.Default.ProductKey.Equals(productCodeTextBox.Text) )
            {
                MessageBox.Show("Product Activated.");

                Settings1.Default.Activated = true;
                Settings1.Default.Save();
            }
            else
            {
                MessageBox.Show("Your Product Key is Invalid.Please contact at info@digitalreach.com");
                Settings1.Default.Activated = false;
                Settings1.Default.Save();
            }
            this.Close();
        }

        private void activateLaterButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
