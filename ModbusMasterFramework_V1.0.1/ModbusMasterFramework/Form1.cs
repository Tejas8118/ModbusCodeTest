using ModbusMasterFramework;
using ModbusMasterFramework.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModbusMasterFramework
{
    public partial class Form1 : Form
    {
        #region Global variables
        public DateTime TrialTime { get; private set; }
        private delegate void SafeCallDelegate();

        // Form References
        public TCPClient tcpclient = null;
        public SerialDataTable serialDataTable = null;
        public BaseDataTable formBaseDataTable = null;
        public LogFile logFile_Form1 = null;
        public ConnectForm connectForm = null;
        public FormSaveSetting formSaveSetting_form1 = null;
        public WriteSelectionForm writeSelectionForm = null;

        // Variables
        public String _connectType = "TCP", _formatType = "Little Endian", _datatype = "Float", _functionCode = "01", csvFilePath = "";
        static int requestCount = 0, responseCount = 0;
        string _oldStartingAddress = "", _oldTotalRegister = "", _oldMasterId = "", _oldRequestDelay = "", _oldReceiveDelay = "";
        public bool dataLoging = false, commLogColorFlag = false;
        private bool _logPaused = false;
        public System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        #endregion

        public Form1()
        {
            if (!Settings1.Default.Activated)
            {
                new ClientSideLicense().ShowDialog();
            }
            InitializeComponent();

            listBoxCommLog.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxCommLog.DrawItem += new DrawItemEventHandler(listBoxCommLog_DrawItem);

            groupBoxDataTable.Controls.Add(listBoxCommLog);
            logFile_Form1 = new LogFile(this);

            this.datalogONToolStripMenuItem.Click += new System.EventHandler(logFile_Form1.DatalogOnAction_Click);
            this.datalogOFFToolStripMenuItem.Click += new System.EventHandler(logFile_Form1.DatalogOffAction_Click);
            connectForm = new ConnectForm(this);   // Initialize connectform
            formSaveSetting_form1 = new FormSaveSetting(); // initialize formsaveseeting

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            TrialTime = DateTime.Now;
            //Check Software Activation
            if (!Settings1.Default.Activated) 
            {
                if (!Settings1.Default.Checked)
                {

                    Settings1.Default.TrialTime = TrialTime;
                    Settings1.Default.Checked = true;
                    Settings1.Default.Save();
                    MessageBox.Show("Trial Version valid till " + TrialTime.Add(new TimeSpan(31, 0, 0, 0)).ToString("dd/MM/yyyy"));
                    
                }

                if ((Settings1.Default.TrialTime.Add(new TimeSpan(31, 0, 0, 0)) > DateTime.Now))
                {


                    int daysRemBeforeExpire = Settings1.Default.TrialTime.Add(new TimeSpan(31, 0, 0, 0)).Subtract(DateTime.Now).Days;
                    MessageBox.Show(daysRemBeforeExpire.ToString() + " days Remaining. Activate before it expires.\n\tDemo will run for 10 minutes only.");


                    myTimer.Interval = 10 * 60000;
                    myTimer.Tick += new EventHandler(myTimer_Tick);
                    myTimer.Start();                  

                }
                else
                {
                    MessageBox.Show("Trial Expired...");
                    this.Close();
                }

            }
            formBaseDataTable = new BaseDataTable(this, _datatype);
            
            // Form loadtime setup
            comboBoxFunctionCode.SelectedIndex = 2;
            comboBoxDatatype.SelectedIndex = 4;
            comboBoxFormat.SelectedIndex = 1;
            enableComponentsAfterDisconnection();


          //  SetValuesOnSubItems(this.menuStrip1.Items.OfType<ToolStripMenuItem>().ToList());
            //baseDataTable.generateTextBox();

        }
        
        #region Console Logging

        public delegate void AppendLogDelegate(String log);
        public delegate void AppendLogColorDelegate(String log,Color color);
        
        //For color change of Request, Receive and error in showlog. 
        private void listBoxCommLog_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 ) return;
            try
            {
                Brush brush;

                // Draw the background of the ListBox control for each item.
                e.DrawBackground();
                // Draw the current item text
                string listboxItemStr = listBoxCommLog.Items[e.Index].ToString();
                if (listboxItemStr.ToLower().Contains("send"))
                    brush = Brushes.GreenYellow;
                else if (listboxItemStr.ToLower().Contains("received"))
                    brush = Brushes.LightGoldenrodYellow;
                else if (listboxItemStr.ToLower().Contains("exception"))
                    brush = Brushes.Red;
                else
                    brush = Brushes.LimeGreen;
                e.Graphics.DrawString(listboxItemStr, e.Font, brush, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
            }catch(Exception)
            {
                ;
            }
           
        }

        //Write evry log in logbox
        public void AppendLog(String log )
        {
            if (_logPaused)
                return;
            if (InvokeRequired)
            {
                BeginInvoke(new AppendLogDelegate(AppendLog), new object[] { log });
                return;
            }
            commLogColorFlag = false;
            
            var now = DateTime.Now;
            var tmpStr = ">" + now.ToLongTimeString() + ": " + log;
            listBoxCommLog.Items.Add(tmpStr);
            listBoxCommLog.SelectedIndex = listBoxCommLog.Items.Count - 1;
            listBoxCommLog.SelectedIndex = -1;
        }

        #endregion

        #region 10 Minute demo time
        //Timer for demo time
        private void myTimer_Tick(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                /* Not on UI thread, reenter there... */
                this.BeginInvoke(new EventHandler(myTimer_Tick), sender, e);
            }
            else
            {
                lock (myTimer)
                {
                    /* only work when this is no reentry while we are already working */
                    if (this.myTimer.Enabled)
                    {
                        this.myTimer.Stop();
                        this.buttonStop.PerformClick();
                        logFile_Form1.SetDataLogFlagsOff();
                        Thread.Sleep(500);
                        this.buttonDisconnect.PerformClick();
                        Thread.Sleep(2000);
                        MessageBox.Show("Demo time expired please close and reopen application.");
                        this.Close();

                    }
                }
            }
        }
        #endregion

        #region Form1 Buttons events

        private void ImportExportMenuSettings_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
                //Check connection setting are availabe or not if not then Emoport it
                if (formSaveSetting_form1 == null)
                    formSaveSetting_form1 = new FormSaveSetting();
                if (menuItem.Text.ToLower().Contains("import"))
                {
                    AppendLog("Importing Settings");
                    formSaveSetting_form1.getValuesFromRegistry();
                    formSaveSetting_form1.saveValuesIntoForm(this);
                }
                else
                {
                    AppendLog("Exporting Settings");
                    formSaveSetting_form1.getValuesFromForm(this);
                    formSaveSetting_form1.saveValuesIntoRegistry();
                }
            }
            catch (Exception ae)
            {
                AppendLog("Exception! " + ae.Message);
            }

        }
        
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (connectForm == null)
                connectForm = new ConnectForm(this);  //if incase not intialize on Above
            //connectForm.ShowDialog();

            if (sender is ToolStripMenuItem)
            {
                connectForm.ShowDialog(); // in setuptolmenuiteml
            }
            else
            {
                //connectForm.Show();
                connectForm.buttonOk_Click(null, null); //for direct connection
            }

        }
        public void enableComponentsAfterConnection()
        {
            if (InvokeRequired)
            {
                Invoke(new SafeCallDelegate(enableComponentsAfterConnection));
            }
            //Enabling other components on successful connection
            if (_functionCode.Equals("01") || _functionCode.Equals("02"))
            {
                groupBoxFormat.Enabled = false;
                groupBoxDatatype.Enabled = false;
            }
            else
            {
                groupBoxFormat.Enabled = true;
                groupBoxDatatype.Enabled = true;
            }
            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = true;
            buttonConnect.Enabled = false;
            buttonDisconnect.Enabled = true;

            groupBoxMasterIdDelay.Enabled = true;
            groupBoxInput.Enabled = true;

            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            buttonWriteMultiRegister.Enabled = true;
        }
        public void enableComponentsAfterDisconnection()
        {
            //Enabling components required for connection
            if (InvokeRequired)
            {
                Invoke(new SafeCallDelegate(enableComponentsAfterDisconnection));

            }
            disconnectToolStripMenuItem.Enabled = false;
            connectToolStripMenuItem.Enabled = true;
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;

            groupBoxFormat.Enabled = false;
            groupBoxDatatype.Enabled = false;
            groupBoxMasterIdDelay.Enabled = false;
            groupBoxInput.Enabled = false;

            buttonStart.Enabled = false;
            buttonStop.Enabled = false;
            buttonWriteMultiRegister.Enabled = false;
        }

        private void beforeClosing(object sender, FormClosingEventArgs e)
        {
            // give optin to Save connection settings
            DialogResult result = MessageBox.Show("Do you want to save settings ?", "Save Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                formSaveSetting_form1.getValuesFromForm(this);
                formSaveSetting_form1.saveValuesIntoRegistry();
            }

        }

        private void buttonShowLog_Click(object sender, EventArgs e)
        {
            //Show log and show data event.
            Panel panel = groupBoxDataTable.Controls.Cast<Panel>().Where(panel1 => panel1.Name.Equals("panel1")).First();
            if (panel.Visible)
            {
                buttonShowLog.Text = "Show Data";
                groupBoxDataTable.Text = "Data Log";
                panel.Hide();
                listBoxCommLog.Show();
            }
            else
            {
                buttonShowLog.Text = "Show Log";
                groupBoxDataTable.Text = "Register Data Table";
                listBoxCommLog.Hide();
                panel.Show();
            }
        }

        private void buttonWriteMultiRegister_Click(object sender, EventArgs e)
        {
            // Write data for function code 01 and 03.
            writeSelectionForm = new WriteSelectionForm(this, connectForm);
            writeSelectionForm.buttonSubmitWrite.Click += new EventHandler(writeSelectionForm_buttonSubmitWrite_Click);
            writeSelectionForm.ShowDialog();

        }
        private void writeSelectionForm_buttonSubmitWrite_Click(object sender, EventArgs e)
        { //for stoping transfer once request for old data to be updated is require

            writeSelectionForm.openWriteRequestWindow();
            writeSelectionForm.Close();

        }

        public void buttonDisconnect_Click(object sender, EventArgs e)
        {
           // Disconnect connection when  
            try
            {
                if (connectForm._connectType.Equals("TCP"))
                {

                    if (!tcpclient.disconnect())
                    {
                        MessageBox.Show("First stop send request after then Disconnect");
                        return;
                    }
                    AppendLog(" TCP Disconnected");
                    labeldisplay.Text = "Disconnected";
                    labeldisplay.ForeColor = Color.Red;
                    labelConnectionType.Text = "None";
                    labelConnectionType.ForeColor = Color.Red;
                }
                else
                {
                    serialDataTable.DisconnectSerialConnection();
                }

            }
            catch (Exception)
            {
                AppendLog("There was no client connected");
            }
            enableComponentsAfterDisconnection();

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listBoxCommLog.Items.Clear();
            clearRequestCount();
            clearResponseCount();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!(_oldStartingAddress.Equals(this.textBoxStartAdress.Text)) || !(_oldTotalRegister.Equals(this.textBoxTotalRegister.Text))) && (this.panel1.Controls.Count != 0))
                {
                    this.panel1.Controls.Clear();

                }
                formBaseDataTable.generateTextBox();
                if (connectForm._connectType.Equals("TCP"))
                {
                    tcpclient.startTransfer();

                }
                else
                {
                    serialDataTable.StartTransfer();
                }

                _oldStartingAddress = this.textBoxStartAdress.Text;
                _oldTotalRegister = this.textBoxTotalRegister.Text;
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = false;
                groupBoxInput.Enabled = false;
                groupBoxMasterIdDelay.Enabled = false;

                if (logFile_Form1.dataLogOnFlag)
                {
                    logFile_Form1.Datalog(true);
                }
            }
            catch (Exception ae)
            {
                MessageBox.Show(ae.Message);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {

                formBaseDataTable.savePreviousData(int.Parse(textBoxTotalRegister.Text.ToString()), int.Parse(textBoxStartAdress.Text.ToString()) - 1);
                groupBoxInput.Enabled = true;

                if (connectForm._connectType.Equals("TCP"))
                {
                    tcpclient.stopTransfer();
                }
                else
                {
                    serialDataTable.StopTransfer();
                }
                if (_functionCode.Equals("01") || _functionCode.Equals("02"))
                {
                    groupBoxDatatype.Enabled = false;
                    groupBoxFormat.Enabled = false;
                }
                else
                {
                    groupBoxDatatype.Enabled = true;
                    groupBoxFormat.Enabled = true;
                }

                groupBoxMasterIdDelay.Enabled = true;
                buttonStart.Enabled = true;
                buttonStop.Enabled = false;
                buttonDisconnect.Enabled = true;

            }
            catch (Exception ae)
            {
                MessageBox.Show(ae.Message);
            }
        }

        #endregion

        #region Form1 Combobox Events

        // Function code changed event
        public void SelectindechangedfunctionCode(object sender, EventArgs e)
        {
            _functionCode = comboBoxFunctionCode.SelectedItem.ToString().Split(' ')[0];
            switch (comboBoxFunctionCode.SelectedIndex)
            {
                case 0:

                    _datatype = "LED";
                    groupBoxDatatype.Enabled = false;
                    groupBoxFormat.Enabled = false;
                    buttonWriteMultiRegister.Enabled = true;
                    buttonWriteMultiRegister.Enabled = true;
                    break;
                case 1:
                    _functionCode = "2";
                    _datatype = "LED";
                    groupBoxDatatype.Enabled = false;
                    groupBoxFormat.Enabled = false;
                    buttonWriteMultiRegister.Enabled = false;

                    break;

                case 2:
                    groupBoxDatatype.Enabled = true;
                    groupBoxFormat.Enabled = true;

                    if (_datatype.Equals("LED"))
                    {
                        _datatype = "Float";
                        groupBoxDatatype.Enabled = true;
                        comboBoxFormat.SelectedIndex = 1;
                    }
                    buttonWriteMultiRegister.Enabled = true;
                    break;

                case 3:
                    groupBoxDatatype.Enabled = true;
                    groupBoxFormat.Enabled = true;
                    buttonWriteMultiRegister.Enabled = false;

                    if (_datatype.Equals("LED"))
                    {
                        _datatype = "Float";
                        comboBoxDatatype.SelectedIndex = 4;
                        comboBoxFormat.SelectedIndex = 1;

                    }
                    break;
            }
            formBaseDataTable.changeFunctionCode(_functionCode);
            formBaseDataTable.changeDataType(_datatype);

            formBaseDataTable.UpdateData();

        } 

        // Data type change event
        private void DatatypeChanged(object sender, EventArgs e)
        {
            _datatype = comboBoxDatatype.SelectedItem.ToString();
            string[] Formatarray = new string[4] { "Big Endian(ABCD)", "Little Endian(DCBA)", "Mid-Big Endian(BADC)", "Mid-Little Endian(CDAB)" };
            if (comboBoxFormat.Items != null || comboBoxFormat.SelectedIndex == -1)
                comboBoxFormat.Items.Clear();
            groupBoxFormat.Enabled = true;
            if (_datatype.Contains("Int16"))
            {

                comboBoxFormat.Items.AddRange(Formatarray.Take(2).ToArray());
                comboBoxFormat.SelectedIndex = 0;
            }
            else if (_datatype.Contains("Hex") || _datatype.Contains("Binary"))
            {
                groupBoxFormat.Enabled = false;
            }
            else
            {
                comboBoxFormat.Items.AddRange(Formatarray);
                comboBoxFormat.SelectedIndex = 0;

            }
            formBaseDataTable.changeDataType(_datatype);
            formBaseDataTable.generateTextBox();

        }

        // Display formate changed event
        private void DisplayFormatModeChanged(object sender, EventArgs e)
        {

            _formatType = comboBoxFormat.SelectedItem.ToString();
            formBaseDataTable.changeFormatType(_formatType);
            //baseDataTable.generateTextBox();                                

        }

        private void aboutSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutSoftwareForm.OpenUserManual();
        }

        #endregion

        #region Fom1 Status
        public void setDataRreadingLabel(bool Status)
        {//show status on form1 
            if (Status)
            {
                labelDataReadingStatus.Text = "Success";
                labelDataReadingStatus.ForeColor = Color.LimeGreen;
            }
            else
            {
                labelDataReadingStatus.Text = "Not Success";
                labelDataReadingStatus.ForeColor = Color.Red;
            }
        }

        #endregion

        #region Validation
        private void Registration(object sender, EventArgs e)
        {
            new AboutSoftwareForm();
        }

        void txtDataIntegerKeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        void txtData_Enter(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            switch (textBox.Name)
            {
                case "textBoxStartAdress":
                    _oldStartingAddress = textBox.Text;
                    break;
                case "textBoxTotalRegister":
                    _oldTotalRegister = textBox.Text;
                    break;
                case "textBoxMasterId":
                    _oldMasterId = textBox.Text;
                    break;
                case "textBoxRequestDelay":

                    _oldRequestDelay = textBox.Text;
                    break;
                case "textBoxReceiveDelay":

                    _oldReceiveDelay = textBox.Text;
                    break;
            }
            if (!String.IsNullOrEmpty(textBox.Text))
            {
                textBox.Clear();
            }
        }

        void TxtDataLeave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;


            UInt64 res;


            if (UInt64.TryParse(textBox.Text, out res))
            {
                switch (textBox.Name)
                {
                    case "textBoxStartAdress":
                        if (res < 0 || res > 65535)
                        {
                            textBox.Text = _oldStartingAddress;
                            MessageBox.Show("Start Address must be betweeon 0 to 65535.");
                            return;
                        }
                        _oldStartingAddress = textBox.Text;
                        formBaseDataTable.changeStartAddress(textBox.Text);
                        break;
                    case "textBoxTotalRegister":
                        if ((res < 0 || res > 124) && (_functionCode.Equals("03") || _functionCode.Equals("04")))
                        {
                            textBox.Text = _oldTotalRegister;
                            MessageBox.Show("Total Register must be betweeon 1 to 123.");
                            return;
                        }
                        else if ((res < 1 || res > 2000) && (_functionCode.Equals("01") || _functionCode.Equals("02")))
                        {
                            textBox.Text = _oldTotalRegister;
                            MessageBox.Show("Total Coils must be between 1 to 2000.");
                            return;
                        }
                        _oldTotalRegister = textBox.Text;
                        formBaseDataTable.changeTotalRegister(textBox.Text);
                        break;
                    case "textBoxMasterId":
                        if (res < 0 || res > 256)
                        {
                            textBox.Text = _oldTotalRegister;
                            MessageBox.Show("Slave Id must be betweeon 1 to 255.");
                            return;
                        }
                        _oldMasterId = textBox.Text;
                        break;
                    case "textBoxRequestDelay":

                        if (res < 500)
                        {
                            textBox.Text = _oldRequestDelay;
                            MessageBox.Show("Delay must be more than 500 millis ");
                            return;
                        }

                        _oldRequestDelay = textBox.Text;
                        formBaseDataTable.changeRequestDelay(textBox.Text);
                        break;
                    case "textBoxReceiveDelay":

                        checkReceiveDelay(textBox, res, sender);

                        formBaseDataTable.changeReceiveDelay(textBox.Text);
                        break;
                }

            }
            else
            {
                switch (textBox.Name)
                {
                    case "textBoxStartAdress":
                        textBox.Text = _oldStartingAddress;
                        break;
                    case "textBoxTotalRegister":
                        textBox.Text = _oldTotalRegister;
                        break;
                    case "textBoxMasterId":
                        textBox.Text = _oldMasterId;
                        break;
                    case "textBoxRequestDelay":
                        textBox.Text = _oldRequestDelay;
                        break;
                    case "textBoxReceiveDelay":
                        textBox.Text = _oldReceiveDelay;
                        break;
                }
            }
        }
        public string checkReceiveDelay(TextBox textBox, UInt64 res, object sender)
        {

            switch (connectForm.comboBoxBaudRate.SelectedItem.ToString())
            {
                case "7200":
                    if (res < 400)
                    {
                        textBox.Text = "400";
                    }
                    break;
                case "4800":
                    if (res < 600)
                    {
                        textBox.Text = "600";
                    }
                    break;
                case "2400":
                    if (res < 1100)
                    {
                        textBox.Text = "1100";
                    }
                    break;
                case "1800":
                    if (res < 1500)
                    {
                        textBox.Text = "1500";
                    }
                    break;
                case "1200":
                    if (res < 2200)
                    {
                        textBox.Text = "2200";
                    }
                    break;
                case "600":
                    if (res < 4700)
                    {
                        textBox.Text = "4700";
                    }
                    break;
                case "300":
                    if (res < 10000)
                    {
                        textBox.Text = "10000";
                    }
                    break;
                case "150":
                    if (res < 19200)
                    {
                        textBox.Text = "19200";
                    }

                    break;
                default:
                    if (res != 300)
                    {
                        if (sender is ComboBox)
                        {
                            textBox.Text = "300";
                            _oldReceiveDelay = "300";
                        }
                        else
                            _oldReceiveDelay = textBox.Text;
                    }
                    break;
            }
            return textBox.Text;
        }

        #endregion

        #region Request/Response Count.
        public void updateRequestCount()
        {
            requestCount++;
            requestCountLabel.Text = requestCount.ToString();
        }

        public void updateResponseCount()
        {
            responseCount++;
            responseCountLabel.Text = responseCount.ToString();
        }

        public void clearRequestCount()
        {
            requestCount = 0;
            requestCountLabel.Text = requestCount.ToString();
        }

        public void clearResponseCount()
        {
            responseCount = 0;
            responseCountLabel.Text = responseCount.ToString();
        }

        #endregion

    }
}
