using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;

namespace ModbusMasterFramework
{
    public partial class BaseDataTable
    {
        #region Global Variables
        private delegate void SafeCallDelegate();
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        WriteRequestForm writeRequestForm;
        public byte[] RegisterData = new byte[5];
        public int[] CoilStatus = new int[4];

        public int _displayCtrlCount;
        public float f1;
        public int previoustype = 0;
        public int currentytpe = 0;
        public string previousString = "";
        public Form1 f = null;
        public ConnectForm cf = null;

        public Label[] labels;
        public TextBox[] textBoxes;
        public int baseTotalRegister { get; set; }
        public int baseStartAddress { get; set; }
        public string baseDatatype { get; set; }
        public string baseFormatType { get; set; }
        public string baseFunctionCode { get; set; }
        public int baseRequestDelay { get; set; }
        public int baseReceiveDelay { get; set; }
        public int baseWriteFunctionCode { get; set; }

        #endregion

        #region  Constructor
        public BaseDataTable() { }

        public BaseDataTable(Form1 form1, string dataType)
        {

            f = form1;
            baseReceiveDelay = int.Parse(f.textBoxReceiveDelay.Text.ToString());
            baseRequestDelay = int.Parse(f.textBoxRequestDelay.Text.ToString());
            baseTotalRegister = int.Parse(f.textBoxTotalRegister.Text.ToString());
            baseStartAddress = int.Parse(f.textBoxStartAdress.Text.ToString()) - 1;
            baseFunctionCode = f._functionCode;
            baseDatatype = f._datatype;
            baseFormatType = f._formatType;

            RegisterData = new byte[baseTotalRegister * 2];
            CoilStatus = new int[baseTotalRegister];
            baseDatatype = dataType;

        }

        #endregion

        #region Basedatatble Parameter changes
        public void changeDataType(string _datatype)
        {
            baseDatatype = _datatype;
        }
        public void changeFormatType(string _formatType)
        {
            baseFormatType = _formatType;
        }
        public void changeFunctionCode(string _functionCode)
        {
            baseFunctionCode = _functionCode;
        }

        public void changeStartAddress(string _startAddress)
        {
            baseStartAddress = int.Parse(_startAddress) - 1;
        }
        public void changeTotalRegister(string _totalRegister)
        {
            baseTotalRegister = int.Parse(_totalRegister);
        }
        public void changeRequestDelay(string _requestDelay)
        {
            baseRequestDelay = int.Parse(_requestDelay);
        }

        public void changeReceiveDelay(string _receiveDelay)
        {
            baseReceiveDelay = int.Parse(_receiveDelay);
        }

        #endregion

        #region Basedatatable Writedata
        public void writeRequestWindow_OnDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                dynamic control, data = null;
                if (sender is TextBox)
                    control = (TextBox)sender;
                else
                    control = (LedBulb)sender;

                int startAddress = int.Parse(control.Name);
                int totalReg = 1;
                int writeFunctionCode = int.Parse(baseFunctionCode);
                if (baseFunctionCode.Equals("01"))
                {
                    writeFunctionCode = 5;
                    data = CoilStatus;
                }
                else if (baseFunctionCode.Equals("03"))
                {
                    if (baseDatatype.Contains("Int16") || baseDatatype.Equals("Hex") || baseDatatype.Equals("Binary"))
                        writeFunctionCode = 6;
                    else
                    {
                        totalReg = 2;
                        writeFunctionCode = 16;
                        startAddress -= 1;
                    }
                    data = new byte[2 * totalReg];
                    Array.Copy(RegisterData, (startAddress - baseStartAddress) * 2, data, 0, 2 * totalReg);
                }
                else
                {

                    throw new Exception();
                }


                writeRequestForm = new WriteRequestForm("");
                writeRequestForm.FormClosed += writeRequestForm_FormClosed;
                writeRequestForm.buttonSendWriteRequest.MouseClick += writeRequestForm_buttonSubmitWrite_Click;
                writeRequestForm.setWriteRequestForm(this, startAddress, totalReg, writeFunctionCode, baseDatatype, data);
                writeRequestForm.ShowDialog();

            }
            catch (Exception ae)
            {
                MessageBox.Show("Write Request is possible only for Function Code 01 and 03.Change function code", "Wrong Function Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void writeRequestForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void writeRequestForm_buttonSubmitWrite_Click(object sender, EventArgs e)
        {

            writeRequestForm.Close();
            byte[] writeReqData = writeRequestForm.getWriteRequestData();

            var backgroundScheduler = TaskScheduler.Default;
            var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            f.formBaseDataTable.baseWriteFunctionCode = writeRequestForm.writeFunctionCode;


            if (f._connectType.Equals("TCP"))
            {
                if (f.buttonStop.Enabled)
                {
                    Task.Factory.StartNew(delegate { f.tcpclient.stopTransfer(); }, backgroundScheduler).
                    ContinueWith(delegate { f.tcpclient.writeSendReceive(writeReqData, writeRequestForm.writeFunctionCode); }, backgroundScheduler).
                    ContinueWith(delegate { f.tcpclient.startTransfer(); }, uiScheduler);
                }
                else
                {

                    Task.Factory.StartNew(delegate { f.tcpclient.writeSendReceive(writeReqData, writeRequestForm.writeFunctionCode); });
                }
            }
            else
            {
                if (f.buttonStop.Enabled)
                {
                    Task.Factory.StartNew(delegate { f.serialDataTable.StopTransfer(); }, backgroundScheduler).
                ContinueWith(delegate { f.serialDataTable.WriteSendReceive(writeReqData, writeRequestForm.writeFunctionCode); }, backgroundScheduler).
                ContinueWith(delegate { f.serialDataTable.StartTransfer(); }, backgroundScheduler);
                }
                else
                {
                    Task.Factory.StartNew(delegate { f.serialDataTable.WriteSendReceive(writeReqData, writeRequestForm.writeFunctionCode); });
                }
            }
        }
        #endregion

        #region Basedatatable validation
        public void BulbClick(object sender, EventArgs e)
        {
            var bulb = (LedBulb)sender;
            bulb.On = !bulb.On;
            var localStartAddress = Int32.Parse(bulb.Name.ToString());

            if (bulb.On)
            {
                CoilStatus[localStartAddress - baseStartAddress] = 1;

            }
            else
            {
                CoilStatus[localStartAddress - baseStartAddress] = 0;

            }
        }
        public void txtDataBinaryKeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            // this will only allow valid binary values [0-1] [delete] to be entered. 
            char c = e.KeyChar;
            if (c != '\b' && c != 0x30 && c != 0x31 && c != Delete)
            {
                e.Handled = true;
            }
        }

        public void txtDataFloatKeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            // this will only allow valid hex values [0-9][a-f][A-F] [delete] to be entered. 
            char c = e.KeyChar;
            if (c != '\b' && !((c == 0x46) || (c >= 0x30 && c <= 0x39)) && c != Delete && e.KeyChar != '.')
            {
                e.Handled = true;
            }

        }

        public void txtDataHexKeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            // this will only allow valid hex values [0-9][a-f][A-F] [delete] to be entered. 
            char c = e.KeyChar;
            if (c != '\b' && !((c <= 0x66 && c >= 61) || (c <= 0x46 && c >= 0x41) || (c >= 0x30 && c <= 0x39)) && c != Delete)
            {
                e.Handled = true;
            }
        }

        public void txtDataIntegerKeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        public void txtData_Enter(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            previousString = textBox.Text;

        }

        public void TxtInt16DataLeave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int diff = 0;
            int textBoxAddress = int.Parse(textBox.Name);
            byte[] b = new byte[2];
            UInt16 res;
            diff = textBoxAddress - baseStartAddress;
            if (UInt16.TryParse(textBox.Text, out res))
            {
                b = BitConverter.GetBytes(res);
                Array.Reverse(b);
                Array.Copy(b, 0, RegisterData, (diff) * 2, 2);

            }
            else
            {
                MessageBox.Show("Enter value between 0-65535", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Text = previousString;
            }
        }

        public void TxtUInt16DataLeave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int diff = 0;
            int textBoxAddress = int.Parse(textBox.Name);
            byte[] b = new byte[2];
            UInt16 res;
            diff = textBoxAddress - baseStartAddress;
            if (UInt16.TryParse(textBox.Text, out res))
            {
                b = BitConverter.GetBytes(res);
                Array.Reverse(b);
                Array.Copy(b, 0, RegisterData, (diff) * 2, 2);

            }
            else
            {
                MessageBox.Show("Enter value between 0-65535", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Text = previousString;
            }
        }

        public void TxtInt32DataLeave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int diff = 0;
            int textBoxAddress = int.Parse(textBox.Name);
            byte[] b = new byte[2];
            UInt16 res;
            diff = textBoxAddress - baseStartAddress;
            if (UInt16.TryParse(textBox.Text, out res))
            {
                b = BitConverter.GetBytes(res);
                Array.Reverse(b);
                Array.Copy(b, 0, RegisterData, (diff) * 2, 2);

            }
            else
            {
                MessageBox.Show("Enter value between 0-65535", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Text = previousString;
            }
        }
        public void TxtUInt32DataLeave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int diff = 0;
            int textBoxAddress = int.Parse(textBox.Name);
            byte[] b = new byte[2];
            UInt16 res;
            diff = textBoxAddress - baseStartAddress;
            if (UInt16.TryParse(textBox.Text, out res))
            {
                b = BitConverter.GetBytes(res);
                Array.Reverse(b);
                Array.Copy(b, 0, RegisterData, (diff) * 2, 2);

            }
            else
            {
                MessageBox.Show("Enter value between 0-65535", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Text = previousString;
            }
        }
        public void TxtDataFloatLeave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int currentRegAddress = int.Parse(textBox.Name);
            byte[] b = new byte[4];
            byte[] c = new byte[4];
            
            try
            {
                b = BitConverter.GetBytes(float.Parse(textBox.Text));

                switch (baseFormatType.Split(' ')[0])
                {

                    case "Big":

                        Array.Reverse(b);
                        Array.Copy(b, 0, RegisterData, ((currentRegAddress - baseStartAddress) * 2) - 2, 4);
                        break;

                    case "Little":
                        Array.Copy(b, 0, RegisterData, ((currentRegAddress - baseStartAddress) * 2) - 2, 4);
                        break;

                    case "Mid-Big":

                        c[0] = b[2];
                        c[1] = b[3];
                        c[2] = b[0];
                        c[3] = b[1];
                        Array.Copy(c, 0, RegisterData, ((currentRegAddress - baseStartAddress) * 2) - 2, 4);//BADC
                        break;

                    case "Mid-Little":
                        c[0] = b[1];
                        c[1] = b[0];
                        c[2] = b[3];
                        c[3] = b[2];

                        Array.Copy(c, 0, RegisterData, ((currentRegAddress - baseStartAddress) * 2) - 2, 4);//CDAB                                               
                        break;

                }
            }
            catch (Exception)
            {

                textBox.Text = previousString;
            }
        }

        public void TxtDataHexLeave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            ushort res;
            int textBoxAddress = int.Parse(textBox.Name);
            byte[] b = new byte[2];
            if (UInt16.TryParse(textBox.Text, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out res))
            {
                Int16 intAgain = Int16.Parse(textBox.Text.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                b = BitConverter.GetBytes(intAgain);
                Array.Reverse(b);
                Array.Copy(b, 0, RegisterData, (textBoxAddress - baseStartAddress) * 2, 2);
            }
            else
            {
                textBox.Text = previousString;
            }
            textBox.Text = string.Format("{0}", textBox.Text.ToLower().PadLeft(4, '0'));
        }

        public void TxtDataBinaryLeave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int textBoxAddress = int.Parse(textBox.Name);
            byte[] b = new byte[2];
            try
            {
                b = BitConverter.GetBytes(Convert.ToUInt16(textBox.Text, 2));
                Array.Reverse(b);
                Array.Copy(b, 0, RegisterData, (textBoxAddress - baseStartAddress) * 2, 2);

            }
            catch (Exception)
            {
                textBox.Text = "0000000000000000";
            }
        }

        #endregion

        #region Basedatatable visualization
        public void generateTextBox()
        {
            try
            {

                f.panel1.Controls.Clear();
                var x = 0;
                var y = 10;
                var z = 20;
                var gap = 30;

                //int a = 0;
                Label labData = null;
                TextBox txtData = null;
                int txtStartAddress = baseStartAddress;
                int labelStartAddress = baseStartAddress;

                for (int i = 0; i < baseTotalRegister; i++)
                {
                    labData = new Label();
                    f.panel1.Controls.Add(labData);
                    labData.Size = new Size(50, 20);
                    labData.Location = new Point(y, z);
                    labData.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);

                    switch (baseDatatype)
                    {
                        case "LED":
                            var bulb = new LedBulb();
                            f.panel1.Controls.Add(bulb);
                            bulb.Size = new Size(25, 25);
                            bulb.Location = new Point(y + 50, z - 5);
                            bulb.Padding = new Padding(3);
                            bulb.Color = Color.Red;
                            bulb.On = false;
                            bulb.Tag = x;
                            //if(!baseFunctionCode.Contains("2"))
                            //{
                            bulb.MouseDoubleClick += writeRequestWindow_OnDoubleClick;
                            //}

                            bulb.Name = Convert.ToString(txtStartAddress + x);
                            z = z + bulb.Size.Height + 10;
                            labData.Text = Convert.ToString(labelStartAddress + x);
                            break;
                        case "Binary":

                            labData.Location = new Point(gap, z);
                            txtData = new TextBox();
                            f.panel1.Controls.Add(txtData);
                            txtData.Size = new Size(110, 5);
                            txtData.Location = new Point(gap + 50, z - 2);
                            txtData.TextAlign = HorizontalAlignment.Right;
                            txtData.Tag = x;
                            txtData.MaxLength = 16;
                            txtData.TextAlign = HorizontalAlignment.Right;
                            txtData.MouseDoubleClick += writeRequestWindow_OnDoubleClick;
                            z = z + txtData.Size.Height + 5;
                            labData.Text = Convert.ToString(labelStartAddress + x);
                            txtData.Name = Convert.ToString(txtStartAddress + x);

                            break;
                        case "Hex":
                            txtData = new TextBox();
                            f.panel1.Controls.Add(txtData);
                            txtData.Size = new Size(55, 20);
                            txtData.Location = new Point(y + 50, z - 2);
                            txtData.TextAlign = HorizontalAlignment.Right;
                            txtData.Tag = x;
                            txtData.MaxLength = 6;
                            z = z + txtData.Size.Height + 5;
                            txtData.MouseDoubleClick += writeRequestWindow_OnDoubleClick;
                            labData.Text = Convert.ToString(labelStartAddress + x);
                            txtData.Name = Convert.ToString(txtStartAddress + x);
                            break;
                        case "Int16":

                            txtData = new TextBox();
                            f.panel1.Controls.Add(txtData);
                            txtData.Size = new Size(55, 20);
                            txtData.Location = new Point(y + 50, z - 2);
                            txtData.TextAlign = HorizontalAlignment.Right;
                            txtData.Tag = x;
                            txtData.MaxLength = 5;
                            txtData.MouseDoubleClick += writeRequestWindow_OnDoubleClick;
                            z = z + txtData.Size.Height + 5;
                            labData.Text = Convert.ToString(labelStartAddress + x);
                            txtData.Name = Convert.ToString(txtStartAddress + x);


                            break;
                        case "Unsigned Int16":

                            txtData = new TextBox();
                            f.panel1.Controls.Add(txtData);
                            txtData.Size = new Size(55, 20);
                            txtData.Location = new Point(y + 50, z - 2);
                            txtData.TextAlign = HorizontalAlignment.Right;
                            txtData.Tag = x;
                            txtData.MaxLength = 5;
                            txtData.MouseDoubleClick += writeRequestWindow_OnDoubleClick;
                            z = z + txtData.Size.Height + 5;
                            labData.Text = Convert.ToString(labelStartAddress + x);
                            txtData.Name = Convert.ToString(txtStartAddress + x);


                            break;
                        case "Int32":
                            x++;
                            i++;

                            txtData = new TextBox();
                            f.panel1.Controls.Add(txtData);
                            txtData.Size = new Size(55, 20);
                            txtData.Location = new Point(y + 50, z - 2);
                            txtData.TextAlign = HorizontalAlignment.Right;
                            txtData.Tag = x;
                            txtData.MaxLength = 5;
                            txtData.MouseDoubleClick += writeRequestWindow_OnDoubleClick;
                            z = z + txtData.Size.Height + 5;
                            labData.Text = Convert.ToString(labelStartAddress + x);
                            txtData.Name = Convert.ToString(txtStartAddress + x);


                            break;
                        case "Unsigned Int32":
                            x++;
                            i++;

                            txtData = new TextBox();
                            f.panel1.Controls.Add(txtData);
                            txtData.Size = new Size(55, 20);
                            txtData.Location = new Point(y + 50, z - 2);
                            txtData.TextAlign = HorizontalAlignment.Right;
                            txtData.Tag = x;
                            txtData.MaxLength = 5;
                            txtData.MouseDoubleClick += writeRequestWindow_OnDoubleClick;
                            z = z + txtData.Size.Height + 5;
                            labData.Text = Convert.ToString(labelStartAddress + x);
                            txtData.Name = Convert.ToString(txtStartAddress + x);


                            break;
                        case "Float":
                            x++;
                            i++;

                            txtData = new TextBox();
                            f.panel1.Controls.Add(txtData);
                            txtData.Size = new Size(70, 20);
                            txtData.Location = new Point(y + 50, z - 2);
                            txtData.TextAlign = HorizontalAlignment.Right;
                            txtData.Tag = x;
                            txtData.MaxLength = 9;
                            z = z + txtData.Size.Height + 5;
                            txtData.MouseDoubleClick += writeRequestWindow_OnDoubleClick;
                            txtData.Name = Convert.ToString(txtStartAddress + x);
                            if (baseFunctionCode.Contains("3"))
                            {
                                labData.Text = Convert.ToString(labelStartAddress + x + 400000);
                            }
                            else
                            { labData.Text = Convert.ToString(labelStartAddress + x + 300000); }

                            break;

                    } // End of Switch Case
                    if (!baseDatatype.Equals("LED"))
                        txtData.ReadOnly = true;

                    x++;
                    if (z > f.panel1.Size.Height - 30)
                    {
                        var inc = 130;
                        y = y + inc;
                        z = 20;
                        gap += 190;
                    }

                }  // End For loop                     
                _displayCtrlCount = x;
                //UpdateData();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception while generating box");
            }
        }

        public void UpdateData()
        {
            try
            {
                if (baseDatatype.Equals("Float") || baseDatatype.Contains("Int32"))
                {
                    if (baseTotalRegister < 2)
                    {
                        f.AppendLog("Add more registers or change datatype." + baseDatatype + " contains 4 byte of data.");
                        f.Invoke(new MethodInvoker(delegate { f.buttonStop.PerformClick(); }));
                        return;
                    }

                }
                ushort localStartAddress = (ushort)baseStartAddress;
                ushort localTotalRegister = (ushort)(baseTotalRegister * 2);

                if (f.InvokeRequired)
                {
                    f.Invoke(new SafeCallDelegate(UpdateData));
                }

                var controls = f.panel1.Controls.OfType<TextBox>().AsEnumerable<TextBox>();

                int i = 0, datactr = 0;
                ushort temp;
                float tofloat;
                string toHex = "0000";
                var data = new ushort[localTotalRegister];
                byte[] b = null;

                if (baseDatatype.Equals("LED"))
                {
                    foreach (var bulb in f.panel1.Controls.OfType<LedBulb>())
                    {
                        if (CoilStatus[i] == 1)
                        {
                            bulb.On = true;
                        }
                        else
                        {
                            bulb.On = false;
                        }
                        i++;
                    }
                }
                else
                {

                    foreach (var txt in f.panel1.Controls.OfType<TextBox>())
                    {
                        if (baseDatatype.Equals("Float") || baseDatatype.Contains("Int32"))
                        {
                            if (datactr % 4 != 0)
                            {
                                f.AppendLog("Add more registers or change datatype." + baseDatatype + " contains 4 byte of data.");
                                f.Invoke(new MethodInvoker(delegate { f.buttonStop.PerformClick(); }));
                                break;
                            }

                        }
                        switch (baseDatatype)
                        {
                            case "Binary":
                                b = new byte[2];
                                Array.Copy(RegisterData, datactr, b, 0, 2);
                                Array.Reverse(b);
                                temp = BitConverter.ToUInt16(b, 0);
                                txt.Text = Convert.ToString(temp, 2).PadLeft(16, '0');
                                break;
                            case "Hex":

                                b = new byte[2];
                                Array.Copy(RegisterData, datactr, b, 0, 2);
                                Array.Reverse(b);
                                temp = BitConverter.ToUInt16(b, 0);

                                toHex = (temp & 65535).ToString("X4"); //65535(i.e. 0000ffff) for removing ff e.g. ffff41c2 & 0000ffff = 41c2
                                txt.Text = String.Format("0x{0:x4}", toHex);

                                break;
                            case "Float":
                                txt.Size = new Size(70, 20);
                                b = formatConverter(datactr, 4);
                                tofloat = BitConverter.ToSingle(b, 0);

                                txt.Text = string.Format("{0}", tofloat);
                                datactr += 2;
                                break;
                            case "Int32":
                                txt.Size = new Size(70, 20);
                                b = formatConverter(datactr, 4);
                                Int32 toInt32 = BitConverter.ToInt32(b, 0);
                                txt.Text = toInt32.ToString(CultureInfo.InvariantCulture);
                                datactr += 2;
                                break;
                            case "Unsigned Int32":
                                txt.Size = new Size(70, 20);
                                b = formatConverter(datactr, 4);
                                UInt32 toUInt32 = BitConverter.ToUInt32(b, 0);

                                txt.Text = toUInt32.ToString(CultureInfo.InvariantCulture);
                                datactr += 2;
                                break;
                            case "Int16":
                                b = formatConverter(datactr, 2);

                                Int16 toInt16 = BitConverter.ToInt16(b, 0);
                                txt.Text = toInt16.ToString(CultureInfo.InvariantCulture);
                                break;
                            case "Unsigned Int16":
                                b = formatConverter(datactr, 2);
                                UInt16 toUInt16 = BitConverter.ToUInt16(b, 0);
                                txt.Text = toUInt16.ToString(CultureInfo.InvariantCulture);
                                break;


                        }
                        datactr += 2; //for two bytes read
                        i++;

                    }
                }
            }
            catch (Exception e)
            {

                f.Invoke(new MethodInvoker(delegate { f.buttonStop.PerformClick(); }));

                return;
            }
        }

        public byte[] formatConverter(int datactr, int bytesToRead)
        {

            byte[] dataByte = new byte[bytesToRead];
            byte[] convertedByte = new byte[bytesToRead];
            try
            {
                Array.Copy(RegisterData, datactr, dataByte, 0, bytesToRead);

                switch (baseFormatType.Split()[0])
                {
                    case "Big":

                        Array.Reverse(dataByte);
                        convertedByte = dataByte;
                        break;

                    case "Little":
                        convertedByte = dataByte;
                        break;

                    case "Mid-Big":
                        Array.Reverse(dataByte);
                        convertedByte[0] = dataByte[1];
                        convertedByte[1] = dataByte[0];
                        convertedByte[2] = dataByte[3];
                        convertedByte[3] = dataByte[2];
                        break;

                    case "Mid-Little":
                        Array.Reverse(dataByte);
                        convertedByte[0] = dataByte[2];
                        convertedByte[1] = dataByte[3];
                        convertedByte[2] = dataByte[0];
                        convertedByte[3] = dataByte[1];
                        break;
                }
            }

            catch (Exception e)
            {
                f.AppendLog("Error converting to " + baseDatatype + ".Change Format/datatype.");
                throw new Exception();
            }

            return convertedByte;
        }

        public void savePreviousData(int txtTotalRegister, int txtStartAdress)
        {
            if (baseDatatype.Equals("LED"))
            {
                int[] prevData = new int[CoilStatus.Length];
                int readLength = prevData.Length;

                Array.Copy(CoilStatus, 0, prevData, 0, readLength);

                CoilStatus = new int[txtTotalRegister];
                if (prevData.Length > CoilStatus.Length)
                {
                    readLength = CoilStatus.Length;
                }
                Array.Copy(prevData, 0, CoilStatus, 0, readLength);
            }
            else
            {

                byte[] prevData = new byte[RegisterData.Length];
                int readLength = prevData.Length;

                Array.Copy(RegisterData, 0, prevData, 0, readLength);

                RegisterData = new byte[txtTotalRegister * 2];
                if (prevData.Length > RegisterData.Length)
                {
                    readLength = RegisterData.Length;
                }
                Array.Copy(prevData, 0, RegisterData, 0, readLength);

            }
            baseTotalRegister = txtTotalRegister;
            baseStartAddress = txtStartAdress;
        }
        #endregion

    }
}
