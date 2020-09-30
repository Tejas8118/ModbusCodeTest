using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;

namespace ModbusMasterFramework
{
    public partial class ConnectForm : Form
    {
        #region Global variables
        public String _connectType = "TCP";
        public Form1 form1 = null;
        private CommunicationMode _communicationMode;
        #endregion

        public ConnectForm(Form1 f)
        {
            InitializeComponent();
            form1 = f;
            ConnectForm_Load(this, null);
        }

        #region Form_Loading event
        public void ConnectForm_Load(object sender, EventArgs e)
        {
            FillRTUDropDownLists();
            comboBoxBaudRate.SelectedIndex = 6;
            comboBoxParity.SelectedIndex = 0;
            comboBoxDataBits.SelectedIndex = 1;
            comboBoxStopBits.SelectedIndex = 1;
            if (_connectType.Equals("TCP"))
            {
                groupBoxTCP.Enabled = true;
                groupBoxRTU.Enabled = false;
            }
            else
            {
                groupBoxRTU.Enabled = true;
                groupBoxTCP.Enabled = false;
            }
        }
        private void FillRTUDropDownLists()
        {
            comboBoxSerialPorts.Items.Clear();
            foreach (var port in SerialPort.GetPortNames())
            {
                comboBoxSerialPorts.Items.Add(port);
            }
            if (comboBoxSerialPorts.Items.Count > 0)
                comboBoxSerialPorts.SelectedIndex = 0;
            comboBoxParity.Items.Clear();
            comboBoxParity.Items.Add(Parity.None.ToString());
            comboBoxParity.Items.Add(Parity.Odd.ToString());
            comboBoxParity.Items.Add(Parity.Even.ToString());
            comboBoxParity.Items.Add(Parity.Mark.ToString());
            comboBoxParity.Items.Add(Parity.Space.ToString());
        }
        #endregion

        #region Form Button Events
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {

                form1.buttonConnect.Enabled = false;
                form1.buttonDisconnect.Enabled = true;
                groupBoxRTU.Enabled = false;
                switch (_connectType)
                {
                    case "TCP":
                        form1.tcpclient = new TCPClient(this, textBoxIP.Text, Int32.Parse(textBoxPort.Text));
                        Thread.Sleep(500);
                        if (!form1.tcpclient.client.Connected)
                        { throw new Exception(); }
                        form1.labelConnectionType.Text = ("TCP " + "(" + "IP: " + textBoxIP.Text + " , Port: " + Int32.Parse(textBoxPort.Text) + ")");
                        form1.labelConnectionType.ForeColor = Color.LimeGreen;
                        break;

                    case "RTU":
                        form1.serialDataTable = new SerialDataTable(this);
                        form1.AppendLog("Serial Connected");
                        form1.labelConnectionType.Text = ("Serial " + "(" + "COMPort: " + comboBoxSerialPorts.Text) + ")";
                        form1.labelConnectionType.ForeColor = Color.LimeGreen;
                        break;
                }

                if (_connectType.Equals("TCP"))
                {
                    groupBoxTCP.Enabled = false;
                    form1.enableComponentsAfterConnection();
                }
                else
                {
                    groupBoxRTU.Enabled = false;
                    form1.enableComponentsAfterConnection();
                    form1.connectForm = this;
                }

                this.Hide();
                form1.labeldisplay.Text = "Connected";
                form1.labeldisplay.ForeColor = Color.LimeGreen;
            }

            catch (Exception ex)
            {
                form1.labelConnectionType.Text = ("None");
                form1.labelConnectionType.ForeColor = Color.Red;
                form1.enableComponentsAfterDisconnection();
                form1.labeldisplay.ForeColor = Color.Red;
                if (_connectType.Equals("TCP"))
                {
                    groupBoxTCP.Enabled = true;
                    MessageBox.Show("Error in TCP Connection", "TCP Connection Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    form1.labeldisplay.Text = "Error in TCP Connection";
                }
                else
                {
                    groupBoxRTU.Enabled = true;
                    form1.enableComponentsAfterDisconnection();
                    MessageBox.Show("Error in Serial Connection", "Serial Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    form1.labeldisplay.Text = "Error in Serial Connection";
                }
            }
        }

        private void RadioButtonModeChanged(object sender, EventArgs e)
        {
            var rb = (RadioButton)sender;
            _connectType = rb.Text;
            form1._connectType = rb.Text;
            SetMode();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            comboBoxSerialPorts.Items.Clear();
            foreach (var port in SerialPort.GetPortNames())
            {
                comboBoxSerialPorts.Items.Add(port);
            }
            if (comboBoxSerialPorts.Items.Count > 0)
                comboBoxSerialPorts.SelectedIndex = 0;
        }
        #endregion

        #region Set Parameters
        public void SetMode()
        {

            if (radioButtonTCP.Checked)
            {
                _communicationMode = CommunicationMode.TCP;
                groupBoxTCP.Enabled = true;
                groupBoxRTU.Enabled = false;
            }

            if (radioButtonRTU.Checked)
            {
                _communicationMode = CommunicationMode.RTU;
                groupBoxTCP.Enabled = false;
                groupBoxRTU.Enabled = true;
            }
        }

        public int DataBits
        {
            get
            {
                int bits = 0;
                switch (comboBoxDataBits.SelectedIndex)
                {
                    case 0:
                        bits = 7;
                        break;
                    case 1:
                        bits = 8;
                        break;
                }
                return bits;

            }
            set
            {
                switch (value)
                {
                    case 7:
                        comboBoxDataBits.SelectedIndex = 0;
                        break;
                    case 8:
                        comboBoxDataBits.SelectedIndex = 1;
                        break;
                }

            }
        }

        public Parity Parity
        {
            get
            {
                var parity = Parity.None;
                if (comboBoxParity.SelectedItem.Equals(Parity.None.ToString()))
                {
                    parity = Parity.None;
                }
                else if (comboBoxParity.SelectedItem.Equals(Parity.Odd.ToString()))
                {
                    parity = Parity.Odd;
                }
                else if (comboBoxParity.SelectedItem.Equals(Parity.Even.ToString()))
                {
                    parity = Parity.Even;
                }
                else if (comboBoxParity.SelectedItem.Equals(Parity.Mark.ToString()))
                {
                    parity = Parity.Mark;
                }
                else if (comboBoxParity.SelectedItem.Equals(Parity.Space.ToString()))
                {
                    parity = Parity.Space;
                }
                return parity;
            }
            set
            {
                comboBoxParity.SelectedItem = Convert.ToString(value);
            }
        }
        public StopBits StopBits
        {
            get
            {
                StopBits bits = StopBits.None;
                switch (comboBoxStopBits.SelectedIndex)
                {
                    case 0:
                        bits = StopBits.None;
                        break;
                    case 1:
                        bits = StopBits.One;
                        break;
                    case 2:
                        bits = StopBits.OnePointFive;
                        break;
                    case 3:
                        bits = StopBits.Two;
                        break;
                }
                return bits;
            }
            set
            {
                switch (value)
                {
                    case StopBits.None:
                        comboBoxStopBits.SelectedIndex = 0;
                        break;
                    case StopBits.One:
                        comboBoxStopBits.SelectedIndex = 1;
                        break;
                    case StopBits.OnePointFive:
                        comboBoxStopBits.SelectedIndex = 2;
                        break;
                    case StopBits.Two:
                        comboBoxStopBits.SelectedIndex = 3;
                        break;
                }
            }
        }
        #endregion

    }
}
