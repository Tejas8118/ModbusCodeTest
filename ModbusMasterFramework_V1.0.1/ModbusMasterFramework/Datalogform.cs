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
    public partial class Datalogform : Form
    {
        #region Global Variable
        Form1 form1_DatalogForm = null;
        public int datalogTimeInterval = 0;
        #endregion

        public Datalogform(Form1 form1)
        {
            InitializeComponent();
            form1_DatalogForm = form1;
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            this.AcceptButton = buttonOk;
            this.CancelButton = buttonCancel;
            
        }

        #region All Form Controlls
        private void Datalogform_Load(object sender, EventArgs e)
        {
            comboBoxDatalogDelay.SelectedIndex = 2;
            datalogTimeInterval = 0;
        }

        private void DatalogRadiobutton_Change(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                var rb = (RadioButton)sender;
                if (rb.Text.Contains("Every"))
                {

                    comboBoxDatalogDelay.Enabled = false;
                    datalogTimeInterval = 0;
                }
                else
                {
                    comboBoxDatalogDelay.Enabled = true;
                    ChangeTimeInterval(comboBoxDatalogDelay, new EventArgs());
                }

            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            form1_DatalogForm.datalogONToolStripMenuItem.Enabled = false;
            form1_DatalogForm.datalogOFFToolStripMenuItem.Enabled = true;

            this.Close();
        }

        private void ChangeTimeInterval(object sender, EventArgs e)
        {
            datalogTimeInterval = int.Parse(comboBoxDatalogDelay.SelectedItem.ToString().Split(' ')[0]) * 1000 * 60;
        }
        #endregion

    }
}
