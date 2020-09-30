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
    partial class AboutSoftwareForm : Form
    {
        public AboutSoftwareForm()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        private void AboutSoftwareForm_Load(object sender, EventArgs e)
        {
            if (Settings1.Default.Activated)
            {
                registrationLabel.Text = "Modbus Master is registered.";
                enterProductKeyLabel.Text = "";
                buttonEnterProductKey.Visible = false;
                this.Size = new Size(187, 51);
            }
            else
            {
                registrationLabel.Text = "This is an unregistered copy of Modbus Master.";
                enterProductKeyLabel.Text = "Click below to enter Product Key.";
                buttonEnterProductKey.Visible = true;
                this.Size = new Size(187, 129);
            }
        }

        private void buttonEnterProductKey_Click(object sender, EventArgs e)
        {
            if (!Settings1.Default.Activated)
            {
                new ClientSideLicense().ShowDialog();

                if (Settings1.Default.Activated)
                {
                    registrationLabel.Text = "Modbus Master is registered.";
                    enterProductKeyLabel.Visible = false;
                    buttonEnterProductKey.Visible = false;
                    this.Size = new Size(187, 51);
                }
                else
                {
                    registrationLabel.Text = "This is an unregistered copy of Modbus Master.";
                    enterProductKeyLabel.Text = "Click below to activate software.";
                    buttonEnterProductKey.Visible = true;
                    this.Size = new Size(187, 129);
                }
            }
        }
        public static void OpenUserManual()
        {
            string userManualPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            userManualPath = userManualPath.Replace(userManualPath.Split('\\').ToArray().Last(), "ModbusMaster_UserGuide.pdf");
            System.Diagnostics.Process.Start(userManualPath);
        }

    }
}
