using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusMasterFramework
{
    public partial class WriteRequestForm : Form
    {
        private delegate void SafeCallDelegate();
        public BaseDataTable WRFBaseDataTable =  new BaseDataTable();
        public int writeFunctionCode;
        public byte[] originalRegisterData;

        public WriteRequestForm(String bs)
        {
            InitializeComponent();
        }
         public void setWriteRequestForm(BaseDataTable bs, int writeReqStartAddress, int writeReqTotalRegister,int functionCode, string datatype ,dynamic writeRegisterData)
        {

            writeFunctionCode = functionCode;
            
            WRFBaseDataTable.baseTotalRegister = writeReqTotalRegister;
            WRFBaseDataTable.baseStartAddress = writeReqStartAddress;
            WRFBaseDataTable.baseDatatype = datatype;
            WRFBaseDataTable.baseFormatType = bs.baseFormatType;
            WRFBaseDataTable.f = bs.f;
            labelWriteStartAddress.Text = writeReqStartAddress.ToString();
            labelWriteTotalRegister.Text = writeReqTotalRegister.ToString();
            switch (writeFunctionCode)
            {
                case 5:
                    this.Text = "Write Single Coil Status";
                    break;

                case 6:
                    this.Text = "Write Single Holding Register";
                    break;

                case 15:
                    this.Text = "Write Multiple Coil Status";
                    break;
                case 16:
                    this.Text = "Write Multiple Holding Register";
                    break;
            }

            if (writeFunctionCode == 5)
            {
                WRFBaseDataTable.CoilStatus = new int[writeReqTotalRegister];

                Array.Copy(writeRegisterData, writeReqStartAddress - bs.baseStartAddress, WRFBaseDataTable.CoilStatus, 0, writeReqTotalRegister);
            }
            else if (writeFunctionCode == 6)
            {
                int readPreviousDataIndex = 0;

                if (bs.baseDatatype.Contains("Int16") || bs.baseDatatype.Equals("Hex") || bs.baseDatatype.Equals("Binary"))
                {
                    //WRFBaseDataTable.RegisterData = new byte[2 * writeReqTotalRegister];
                    readPreviousDataIndex = 2 * (writeReqStartAddress - bs.baseStartAddress);//for 2 bytes
                }
                else
                {
                    writeFunctionCode = 16;
                    //WRFBaseDataTable.RegisterData = new byte[2 * writeReqTotalRegister];
                    readPreviousDataIndex = 2 * (writeReqStartAddress - bs.baseStartAddress) - 2;  //for 4 bytes
                }
                WRFBaseDataTable.RegisterData = new byte[2 * writeReqTotalRegister];
                Array.Copy(writeRegisterData, readPreviousDataIndex, WRFBaseDataTable.RegisterData, 0, WRFBaseDataTable.RegisterData.Length);
            }
            else if (writeFunctionCode == 15)
            {
                WRFBaseDataTable.CoilStatus = readMultipleCoilStatus(writeRegisterData);
                //Array.Copy(writeRegisterData, 0, WRFBaseDataTable.RegisterData, 0, writeReqTotalRegister);
            }

            else
            { //function code ==16
                WRFBaseDataTable.RegisterData = writeRegisterData;

                Array.Copy(writeRegisterData, 0, WRFBaseDataTable.RegisterData, 0, writeReqTotalRegister);

            }

            this.generateTextBox();            
            this.ActiveControl = buttonSendWriteRequest;
        }
        
        // This function only  for Write data Functionality,
        //And validation is not put in this function otherwise basdatatable  generatetextbox() and this function are same.
        public void  generateTextBox()
        {
            
            this.panel1.Controls.Clear();
            var x = 0;
            var y = 10;
            var z = 20;
            var gap = 30;
           
            Label labData = null;
            TextBox txtData = null;
            int textboxStartAddress = WRFBaseDataTable.baseStartAddress  ;
            int labelStartAddress = WRFBaseDataTable.baseStartAddress;
            for (int i = 0; i < WRFBaseDataTable.baseTotalRegister; i++)
            {
                labData = new Label();
                this.panel1.Controls.Add(labData);
                labData.Size = new Size(40, 20);
                labData.Location = new Point(y, z);
                labData.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                
                switch (WRFBaseDataTable.baseDatatype)
                {
                    case "LED":
                        var bulb = new LedBulb();
                        this.panel1.Controls.Add(bulb);
                        bulb.Size = new Size(25, 25);
                        bulb.Location = new Point(y + 40, z - 5);
                        bulb.Padding = new Padding(3);
                        bulb.Color = Color.Red;
                        bulb.On = false;
                        bulb.Tag = x;
                        bulb.Click += WRFBaseDataTable.BulbClick;
                        bulb.Name = Convert.ToString(textboxStartAddress + x);
                        z = z + bulb.Size.Height + 10;
                        labData.Text = Convert.ToString(labelStartAddress + x);
                        break;
                    case "Binary":

                        labData.Location = new Point(gap, z);
                        txtData = new TextBox();
                        this.panel1.Controls.Add(txtData);
                        txtData.Size = new Size(110, 5);
                        txtData.Location = new Point(gap + 40, z - 2);
                        txtData.TextAlign = HorizontalAlignment.Right;
                        txtData.Tag = x;
                        txtData.MaxLength = 16;
                        txtData.TextAlign = HorizontalAlignment.Right;
                        txtData.Leave += WRFBaseDataTable.TxtDataBinaryLeave;
                        txtData.Enter += WRFBaseDataTable.txtData_Enter;
                        txtData.KeyPress += WRFBaseDataTable.txtDataBinaryKeyPress;
                        z = z + txtData.Size.Height + 5;
                        labData.Text = Convert.ToString(labelStartAddress + x);
                        txtData.Name = Convert.ToString(textboxStartAddress + x);

                        break;
                    case "Hex":
                        txtData = new TextBox();
                        this.panel1.Controls.Add(txtData);
                        txtData.Size = new Size(55, 20);
                        txtData.Location = new Point(y + 40, z - 2);
                        txtData.TextAlign = HorizontalAlignment.Right;
                        txtData.Tag = x;
                        txtData.MaxLength = 4;
                        z = z + txtData.Size.Height + 5;
                        txtData.Leave += WRFBaseDataTable.TxtDataHexLeave;
                        txtData.Enter += WRFBaseDataTable.txtData_Enter;
                        txtData.KeyPress += WRFBaseDataTable.txtDataHexKeyPress;
                        labData.Text = Convert.ToString(labelStartAddress + x);
                        txtData.Name = Convert.ToString(textboxStartAddress + x);
                        break;
                    case "Integer":

                        txtData = new TextBox();
                        this.panel1.Controls.Add(txtData);
                        txtData.Size = new Size(55, 20);
                        txtData.Location = new Point(y + 40, z - 2);
                        txtData.TextAlign = HorizontalAlignment.Right;
                        txtData.Tag = x;
                        txtData.MaxLength = 5;
                        txtData.Leave += WRFBaseDataTable.TxtInt16DataLeave;
                        txtData.Enter += WRFBaseDataTable.txtData_Enter;
                        txtData.KeyPress += WRFBaseDataTable.txtDataIntegerKeyPress;
                        z = z + txtData.Size.Height + 5;
                        labData.Text = Convert.ToString(labelStartAddress + x);
                        txtData.Name = Convert.ToString(textboxStartAddress + x);


                        break;
                    case "Int16":

                        txtData = new TextBox();
                        this.panel1.Controls.Add(txtData);
                        txtData.Size = new Size(55, 20);
                        txtData.Location = new Point(y + 40, z - 2);
                        txtData.TextAlign = HorizontalAlignment.Right;
                        txtData.Tag = x;
                        txtData.MaxLength = 5;
                        txtData.Leave += WRFBaseDataTable.TxtInt16DataLeave;
                        txtData.Enter += WRFBaseDataTable.txtData_Enter;
                        txtData.KeyPress += WRFBaseDataTable.txtDataIntegerKeyPress;
                        z = z + txtData.Size.Height + 5;
                        labData.Text = Convert.ToString(labelStartAddress + x);
                        txtData.Name = Convert.ToString(textboxStartAddress + x);


                        break;
                    case "Unsigned Int16":

                        txtData = new TextBox();
                        this.panel1.Controls.Add(txtData);
                        txtData.Size = new Size(55, 20);
                        txtData.Location = new Point(y + 40, z - 2);
                        txtData.TextAlign = HorizontalAlignment.Right;
                        txtData.Tag = x;
                        txtData.MaxLength = 5;
                        txtData.Leave += WRFBaseDataTable.TxtUInt16DataLeave;
                        txtData.Enter += WRFBaseDataTable.txtData_Enter;
                        txtData.KeyPress += WRFBaseDataTable.txtDataIntegerKeyPress;
                        z = z + txtData.Size.Height + 5;
                        labData.Text = Convert.ToString(labelStartAddress + x);
                        txtData.Name = Convert.ToString(textboxStartAddress + x);


                        break;
                    case "Int32":
                        x++;
                        i++;

                        txtData = new TextBox();
                        this.panel1.Controls.Add(txtData);
                        txtData.Size = new Size(55, 20);
                        txtData.Location = new Point(y + 40, z - 2);
                        txtData.TextAlign = HorizontalAlignment.Right;
                        txtData.Tag = x;
                        txtData.MaxLength = 5;
                        txtData.Leave += WRFBaseDataTable.TxtInt32DataLeave;
                        txtData.Enter += WRFBaseDataTable.txtData_Enter;
                        txtData.KeyPress += WRFBaseDataTable.txtDataIntegerKeyPress;
                        z = z + txtData.Size.Height + 5;
                        labData.Text = Convert.ToString(labelStartAddress + x);
                        txtData.Name = Convert.ToString(textboxStartAddress + x);


                        break;
                    case "Unsigned Int32":
                        x++;
                        i++;

                        txtData = new TextBox();
                        this.panel1.Controls.Add(txtData);
                        txtData.Size = new Size(55, 20);
                        txtData.Location = new Point(y + 40, z - 2);
                        txtData.TextAlign = HorizontalAlignment.Right;
                        txtData.Tag = x;
                        txtData.MaxLength = 5;
                        txtData.Leave += WRFBaseDataTable.TxtUInt32DataLeave;
                        txtData.Enter += WRFBaseDataTable.txtData_Enter;
                        txtData.KeyPress += WRFBaseDataTable.txtDataIntegerKeyPress;
                        z = z + txtData.Size.Height + 5;
                        labData.Text = Convert.ToString(labelStartAddress + x);
                        txtData.Name = Convert.ToString(textboxStartAddress + x);


                        break;
                    case "Float":
                        x++;
                        i++;

                        txtData = new TextBox();
                        this.panel1.Controls.Add(txtData);
                        txtData.Size = new Size(55, 20);
                        txtData.Location = new Point(y + 40, z - 2);
                        txtData.TextAlign = HorizontalAlignment.Right;
                        txtData.Tag = x;
                        txtData.MaxLength = 7;
                        z = z + txtData.Size.Height + 5;
                        txtData.Leave += WRFBaseDataTable.TxtDataFloatLeave;
                        txtData.Enter += WRFBaseDataTable.txtData_Enter;
                        txtData.KeyPress += WRFBaseDataTable.txtDataFloatKeyPress;
                        labData.Text = Convert.ToString(labelStartAddress + x);
                        txtData.Name = Convert.ToString(textboxStartAddress + x);

                        break;

                      
                } // End of Switch Case
               
                x++;
                if (z > this.panel1.Size.Height - 30)
                {
                    var inc = 130;
                    y = y + inc;
                    z = 20;
                    gap += 190;
                }

            }  // End For loop                     
            WRFBaseDataTable._displayCtrlCount = x;
            UpdateData();
        }

        // This function only  for Write data Functionality,
        //otherwise basdatatable  generatetextbox() and this function are same.
        public void UpdateData()
        {
            try
            {
                ushort localStartAddress = (ushort)WRFBaseDataTable.baseStartAddress;
                ushort localTotalRegister = (ushort)(WRFBaseDataTable.baseTotalRegister * 2);

                if (this.InvokeRequired)
                {
                    this.Invoke(new SafeCallDelegate(UpdateData));
                }

                var controls = this.panel1.Controls.OfType<TextBox>().AsEnumerable<TextBox>();
                //IEnumerable<TextBox> floatTextBoxes =
                //    from control in controls
                //    where ((int.Parse(control.Name) > localStartAddress) && (int.Parse(control.Name) <= (localStartAddress + localTotalRegister)))
                //    select control;
                //    var s=txtboxes.SelectMany(c => int.Parse(c.Text.ToString());
                //    f.panel1.Controls.OfType<TextBox>().Where() where tb.Tag == "AmountTextBox"
                //                                 select tb).FirstOrDefault();
                //}

                //foreach (TextBox txt in textBoxes)
                //{
                //    temp = int.Parse(txt.Text);
                //    if (temp > 255)
                //    {
                //        sendByte[i++] = BitConverter.GetBytes(temp)[1];
                //        sendByte[i++] = BitConverter.GetBytes(temp)[0];
                //    }
                //    else
                //    {
                //        sendByte[i++] = 0;
                //        sendByte[i++] = (byte)temp;
                //    }

                //}

                int i = 0, datactr = 0;
                ushort temp;
                float tofloat;
                string toHex = "0000";
                var data = new ushort[localTotalRegister];
                byte[] b = null;
                

                if (WRFBaseDataTable.baseDatatype.Equals("LED"))
                {
                    foreach (var bulb in this.panel1.Controls.OfType<LedBulb>())
                    {
                        if (WRFBaseDataTable.CoilStatus[i] == 1)
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

                    foreach (var txt in this.panel1.Controls.OfType<TextBox>())
                    {

                        //  MessageBox.Show(data.GetUpperBound(0).ToString());
                        switch (WRFBaseDataTable.baseDatatype)
                        {
                            case "Binary":
                                b = new byte[2];
                                Array.Copy(WRFBaseDataTable.RegisterData, datactr, b, 0, 2);
                                Array.Reverse(b);
                                temp = BitConverter.ToUInt16(b, 0);
                                //  MessageBox.Show(data[x].ToString());
                                txt.Text = Convert.ToString(temp, 2).PadLeft(16, '0');
                                //txt.Text = "00001111";
                                break;
                            case "Hex":

                                b = new byte[2];
                                Array.Copy(WRFBaseDataTable.RegisterData, datactr, b, 0, 2);
                                Array.Reverse(b);
                                temp = BitConverter.ToUInt16(b, 0);

                                toHex = (temp & 65535).ToString("X4"); //65535(i.e. 0000ffff) for removing ff e.g. ffff41c2 & 0000ffff = 41c2
                                txt.Text = String.Format("0x{0:x4}", toHex);

                                break;
                            case "Float":
                                //Array.Copy(RegisterData, datactr, b, 0, 4);
                                //Array.Reverse(b);
                                txt.Size = new Size(70, 20);
                                b = WRFBaseDataTable.formatConverter(datactr, 4);
                                tofloat = BitConverter.ToSingle(b, 0);

                                txt.Text = string.Format("{0}", tofloat);
                                datactr += 2;
                                break;
                            case "Int32":
                                //b = new byte[4];
                                //Array.Copy(RegisterData, datactr, b, 0, 2);
                                //Array.Reverse(b);
                                txt.Size = new Size(70, 20);
                                b = WRFBaseDataTable.formatConverter(datactr, 4);
                                Int32 toInt32 = BitConverter.ToInt32(b, 0);
                                txt.Text = toInt32.ToString(CultureInfo.InvariantCulture);
                                datactr += 2;
                                break;
                            case "Unsigned Int32":

                                //b = new byte[4];
                                //Array.Copy(RegisterData, datactr, b, 0, 2);
                                //Array.Reverse(b);
                                txt.Size = new Size(70, 20);
                                b = WRFBaseDataTable.formatConverter(datactr, 4);
                                UInt32 toUInt32 = BitConverter.ToUInt32(b, 0);

                                txt.Text = toUInt32.ToString(CultureInfo.InvariantCulture);
                                datactr += 2;
                                break;
                            case "Int16":
                                //b = new byte[2];
                                //Array.Copy(RegisterData, datactr, b, 0, 2);
                                //Array.Reverse(b);

                                b = WRFBaseDataTable.formatConverter(datactr, 2);

                                Int16 toInt16 = BitConverter.ToInt16(b, 0);
                                txt.Text = toInt16.ToString(CultureInfo.InvariantCulture);
                                break;
                            case "Unsigned Int16":
                                //b = new byte[2];
                                //Array.Copy(RegisterData, datactr, b, 0, 2);
                                //Array.Reverse(b);

                                b = WRFBaseDataTable.formatConverter(datactr, 2);
                                UInt16 toUInt16 = BitConverter.ToUInt16(b, 0);
                                txt.Text = toUInt16.ToString(CultureInfo.InvariantCulture);
                                break;


                        }
                        datactr += 2; //for two bytes read
                        i++;
                        //txt.Visible = true;               

                    }
                }
            }
            catch (Exception e)
            {
                 WRFBaseDataTable.f.AppendLog("Error While updating Data");
            }


        }


        public byte[] getWriteRequestData()
        {

            byte[] sendByte = null;
           
            switch (writeFunctionCode)
            {
                case 5:
                    sendByte = writeSingleCoil();
                    break;

                case 6:
                    sendByte = writeSingleRegister();

                    break;
                case 15:
                    sendByte = writeMultiCoil();

                    break;
                case 16:
                    sendByte = writeMultiRegister();

                    break;

                default:
                    sendByte = new byte[9];
                    Array.Copy(new byte[6] { 0,0,0,0,0,2}, 0, sendByte, 0, 7);
                    sendByte[7] = 131;
                    sendByte[8] = (byte)1;
                    break;
            }
            
            
            return sendByte;
            
        }
        public byte[] writeSingleRegister()
        {
            byte[] b = new byte[4];
            byte[] startAddress = BitConverter.GetBytes((ushort)(WRFBaseDataTable.baseStartAddress));
            Array.Reverse(startAddress);

            Array.Copy(startAddress, 0, b, 0, 2);
            Array.Copy(WRFBaseDataTable.RegisterData, 0, b, 2, 2);


            return b;
           
        }

        public byte[] writeMultiRegister()
        {
            int sendIndex=0;
            byte[]  b, startAddress, totalRegister;
            b = new byte[5+ (WRFBaseDataTable.RegisterData.Length)];
            startAddress = BitConverter.GetBytes((ushort)(WRFBaseDataTable.baseStartAddress));

            Array.Reverse(startAddress);
            Array.Copy(startAddress, 0, b, sendIndex, 2);
            sendIndex +=2;
            
            totalRegister = BitConverter.GetBytes((ushort)(WRFBaseDataTable.RegisterData.Length / 2));
            Array.Reverse(totalRegister);            
            Array.Copy(totalRegister, 0, b, sendIndex, 2);
            sendIndex += 2;
            b[sendIndex++] = (byte)(WRFBaseDataTable.RegisterData.Length);



            Array.Copy(WRFBaseDataTable.RegisterData, 0, b, sendIndex, WRFBaseDataTable.RegisterData.Length);
         
            return b;

        }
        public byte[] writeSingleCoil()
        {

            byte[] b = new byte[4];
            byte[] startAddress = BitConverter.GetBytes((ushort)(WRFBaseDataTable.baseStartAddress));
            Array.Reverse(startAddress);

            Array.Copy(startAddress, 0, b, 0, 2);
            

            if (WRFBaseDataTable.CoilStatus[0] == 0)
            {
                Array.Copy(new byte[] { 0, 0 }, 0, b, 2, 2);
            }
            else
            {
                Array.Copy(new byte[] { 255, 0 }, 0, b, 2, 2); 
            }
            return b;
        }

        public byte[] writeMultiCoil()
        {
            int readCoilIndex = 0;
            int localTotalRegister = WRFBaseDataTable.baseTotalRegister;
            byte[] sendByte = null, startAddress, totalRegister;
            int[] temp = new int[8];
            int totalBytes = 0, readByte = 8;
            string binaryString;
            int sendIndex = 0;

            totalBytes = localTotalRegister / 8;
            if ((localTotalRegister % 8) != 0)
            {
                totalBytes += 1;
            }

            sendByte = new byte[5 + totalBytes];

            startAddress = BitConverter.GetBytes((ushort)(WRFBaseDataTable.baseStartAddress));
            Array.Reverse(startAddress);
            Array.Copy(startAddress, 0, sendByte, sendIndex, 2);
            sendIndex += 2;

            totalRegister = BitConverter.GetBytes((ushort)(WRFBaseDataTable.baseTotalRegister));
            Array.Reverse(totalRegister);
            Array.Copy(totalRegister, 0, sendByte, sendIndex, 2);
            sendIndex += 2;
           

           
            sendByte[sendIndex++] = (byte)totalBytes;

            
            
            for (int j = 0; j < totalBytes; j++)
            {

                if (localTotalRegister < 8)
                {
                    readByte = localTotalRegister;

                }
                temp = new int[readByte];
                Array.Copy(WRFBaseDataTable.CoilStatus, readCoilIndex, temp, 0, readByte);
                Array.Reverse(temp);
                binaryString = String.Join("", temp.Select(p => p.ToString()).ToArray());

                binaryString = new String('0', 8 - readByte) + binaryString;
                sendByte[sendIndex++] = Convert.ToByte(binaryString.Substring(8 * 0, 8), 2);
                readCoilIndex += 8;
                localTotalRegister -= 8;
            }

            return sendByte;

        }

        //This function called for Function code 03 and 04.
        public int[] readMultipleCoilStatus(byte[] recBuf)
        {
            int localTotalRegister =WRFBaseDataTable.baseTotalRegister;
            byte b;
            int[] temp = new int[8];
            int i = 0, totalBytes = 0, readByte = 8, recBufDataIndex = 0;
            string binaryString;

            totalBytes = recBuf.Length;

            int[] CoilStatus = new int[localTotalRegister];

            for (int j = 0; j < totalBytes; j++)
            {
                if (localTotalRegister < 8)
                {
                    readByte = localTotalRegister;
                }
                temp = new int[readByte];
                b = recBuf[recBufDataIndex++];
                binaryString = Convert.ToString(b, 2);
                temp = (new String('0', 8 - binaryString.Length) + binaryString).ToArray().Select(p => (int)Char.GetNumericValue(p)).ToArray();
                Array.Reverse(temp);
                Array.Copy(temp, 0, CoilStatus, i, readByte);
                i += readByte;
                localTotalRegister -= 8;
            }

            return CoilStatus;
        }

    }
}
