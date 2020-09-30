using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusMasterFramework
{
    public partial class WriteSelectionForm : Form
    {
        Form1 form1;
        WriteRequestForm writeRequestForm;
        ushort writeMultiReqStartAdd = 0, writeMultiReqTotalReg = 0, writeMultiReqFuncCode = 15;
       
        string previousString,previousFunctionCode = "";
        Task writeTask = null;
        public WriteSelectionForm(Form1 f,ConnectForm cform)
        {
            
            InitializeComponent();

            form1 = f;
            
            writeMultiReqStartAdd = (ushort)f.formBaseDataTable.baseStartAddress;
            writeMultiReqTotalReg= (ushort)f.formBaseDataTable.baseTotalRegister;
            textBoxWriteStartAddress.Text = (writeMultiReqStartAdd+1).ToString();
            textBoxWriteTotalRegister.Text = writeMultiReqTotalReg.ToString();
            


        }
        
        private void writeFunctionCodeChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                var rb = (RadioButton)sender;
                if (rb==radioButtonMultipleCoils)                
                    writeMultiReqFuncCode = 15;
                else
                    writeMultiReqFuncCode = 16;

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

        public void TxtUInt16DataLeave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            
            ushort checkTotalReg= 127;
            ushort checkStartAdd = 0;
            ushort checkEndAdd =  65535;
            byte[] b = new byte[2];
            ushort res;
           
            if (ushort.TryParse(textBox.Text, out res))
            {
               
                if (textBox.Name.Equals("textBoxWriteStartAddress") )
                {
                    if ((  res > checkStartAdd) && (res < checkEndAdd))
                    {
                        writeMultiReqStartAdd=res;
                        writeMultiReqStartAdd--;
                    }
                    else
                    {
                        MessageBox.Show("Start Address must be between 0 To 65535", "Input Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox.Text = previousString;
                    }                    
                }
                else
                {
                    if ((res > 0) && (res < checkTotalReg))
                    {
                        writeMultiReqTotalReg = res;
                    }
                    else
                    {
                        MessageBox.Show("Total Register must be between 0 To 127" , "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox.Text = previousString;
                    }
                }                       
            }
            else
            {
                MessageBox.Show("Enter value between 0-65535", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Text = previousString;
            }
        }
       
        public void openWriteRequestWindow()
        {
            int totalRegister=writeMultiReqTotalReg;
            byte[] writeRegData = null,buffer;
            string datatype = "";

            var backgroundScheduler = TaskScheduler.Default;
            var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            try
            {
                //=====================================================================================

                writeTask = Task.Factory.StartNew(delegate
                {
                    if (form1.buttonStop.Enabled)
                    {
                        if (form1._connectType.Equals("TCP"))
                            form1.tcpclient.stopTransfer();
                        else
                            form1.serialDataTable.StopTransfer();
                    }
                    Task.Delay(form1.formBaseDataTable.baseReceiveDelay).Wait();
                    previousFunctionCode = form1.formBaseDataTable.baseFunctionCode;
                }, backgroundScheduler).
                       ContinueWith(delegate
                       {
                           writeRegData = getRegisterDataToChange();
                           
                       
                       if (writeRegData == null)
                       {
                           form1.AppendLog("Starting Address or total register is wrong. Please change it and try Again.");
                           return;
                       }

                       if (writeMultiReqFuncCode == 15)
                       {
                           datatype = "LED";
                       }
                       else
                       {
                               form1.Invoke(new MethodInvoker(delegate { datatype = form1.comboBoxDatatype.SelectedItem.ToString(); }));
                           }

                           form1.formBaseDataTable.baseWriteFunctionCode = writeMultiReqFuncCode;

                           writeRequestForm = new WriteRequestForm("");
                           writeRequestForm.buttonSendWriteRequest.Click += writeRequestForm_buttonSubmitWrite_Click;
                           writeRequestForm.setWriteRequestForm(form1.formBaseDataTable, writeMultiReqStartAdd, totalRegister, writeMultiReqFuncCode, datatype, writeRegData);
                           writeRequestForm.ShowDialog();
                       }).ContinueWith(delegate{

                           form1.formBaseDataTable.baseFunctionCode = previousFunctionCode;//setting previous read function code
                           if (form1.buttonStop.Enabled)
                           {
                               if (form1._connectType.Equals("TCP"))
                                   form1.tcpclient.startTransfer();
                               else
                                   form1.serialDataTable.StartTransfer();
                           }
                           Task.Delay(500).Wait();
                       });
                
                }
            catch (Exception ae)
            {
                MessageBox.Show("Please change function Code for read request.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void writeRequestForm_buttonSubmitWrite_Click(object sender, EventArgs e)
        {
            byte[] writeReqData = null;
            //=====================================================================================
            var backgroundScheduler = TaskScheduler.Default;
            
            writeReqData = writeRequestForm.getWriteRequestData();
            if(form1._connectType.Equals("TCP"))
                form1.tcpclient.writeSendReceive(writeReqData, writeRequestForm.writeFunctionCode);
            else
                form1.serialDataTable.WriteSendReceive(writeReqData, writeRequestForm.writeFunctionCode);

            this.Close();
            writeRequestForm.Close();

                        

            



            //writeReqData = writeRequestForm.getWriteRequestData();
            //if (form1._connectType.Equals("TCP"))
            //{
            //    if (form1.buttonStop.Enabled)
            //    {
            //        Task.Factory.StartNew(delegate { form1.tcpclient.writeSendReceive(writeReqData, writeRequestForm.writeFunctionCode); }, backgroundScheduler).
            //            ContinueWith(delegate { form1.tcpclient.startTransfer(); }, backgroundScheduler);
            //        //Task.Factory.StartNew(delegate { form1.tcpclient.stopTransfer(); }, backgroundScheduler).
            //        //ContinueWith(delegate { form1.tcpclient.writeSendReceive(writeReqData, writeRequestForm.writeFunctionCode); }, backgroundScheduler).
            //        //ContinueWith(delegate { form1.tcpclient.startTransfer(); }, uiScheduler);
            //    }
            //    else
            //    {

            //        Task.Factory.StartNew(delegate { form1.tcpclient.writeSendReceive(writeReqData, writeRequestForm.writeFunctionCode); });
            //    }
            //}
            //else
            //{
            //    if (form1.buttonStop.Enabled)
            //    {
            //        Task.Factory.StartNew(delegate { form1.serialDataTable.stopTransfer(); }, backgroundScheduler).
            //    ContinueWith(delegate { form1.serialDataTable.writeSendReceive(writeReqData, writeRequestForm.writeFunctionCode); }, backgroundScheduler).
            //    ContinueWith(delegate { form1.serialDataTable.startTransfer(); }, backgroundScheduler);
            //    }
            //    else
            //    {
            //        Task.Factory.StartNew(delegate { form1.serialDataTable.writeSendReceive(writeReqData, writeRequestForm.writeFunctionCode); });
            //    }
            //}


            //writeRequestForm.Close();
            //if (form1._connectType.Equals("TCP"))
            //    form1.tcpclient.writeSendReceive(writeReqData, writeMultiReqFuncCode);
            //else
            //    form1.serialDataTable.writeSendReceive(writeReqData, writeMultiReqFuncCode);
            //this.Close();
            //writeRequestForm.Close();
            //=====================================================================================

            //if (form1._connectType.Equals("TCP"))
            //    form1.tcpclient.writeSendReceive(writeReqData, writeMultiReqFuncCode);
            //else
            //    form1.serialDataTable.writeSendReceive(writeReqData, writeMultiReqFuncCode);
            //this.Close();
            //writeRequestForm.Close();

        }
        public byte[] getRegisterDataToChange()
        {
            //sends read request(03 or 01) to get register data for showing on textboxes and also to change.
            byte[] sendReq, requestedData = null, totalRegister, startAddress,temp = null;
            int sendIndex = 0, readReqFunctionCode,tR;
            string tempFC = "";

            if (writeMultiReqFuncCode == 15)
            {
                readReqFunctionCode = 1;
            }
            else
            {
                readReqFunctionCode = 3;
            }

            
            tR = writeMultiReqTotalReg;
            tempFC = form1.formBaseDataTable.baseFunctionCode; //temporarly storing previous read request so read request for writing multiple data can be compared
            form1.formBaseDataTable.baseFunctionCode = "0" + readReqFunctionCode.ToString(); //multiple write read request function code
            if (form1._connectType.Equals("TCP"))
            {
                sendReq = new byte[12];
                Array.Copy(new byte[] { 0, 0, 0, 0, 0, 6 }, 0, sendReq, sendIndex, 6);
                sendIndex += 6;
                
            }                
            else
            {
                sendReq = new byte[8];
            }

            try
            {
                sendReq[sendIndex++] = byte.Parse(form1.textBoxMasterId.Text);

                sendReq[sendIndex++] = (byte)readReqFunctionCode;  //for reading multiple holding register

               
                startAddress = BitConverter.GetBytes((ushort)(writeMultiReqStartAdd));
                Array.Reverse(startAddress);
                Array.Copy(startAddress, 0, sendReq, sendIndex, 2);
                sendIndex += 2;

                totalRegister = BitConverter.GetBytes((ushort)tR);
                Array.Reverse(totalRegister);
                Array.Copy(totalRegister, 0, sendReq, sendIndex, 2);
                sendIndex += 2;

                //==============================================================================================================================================
                var backgroundScheduler = TaskScheduler.Default;
                if (form1._connectType.Equals("TCP"))
                {
                    temp = form1.tcpclient.sendReadReqForWriteData(sendReq);
                    requestedData = new byte[temp[8]];
                    Array.Copy(temp, 9, requestedData, 0, temp[8]);
                }
                else
                {
                    Thread.Sleep(100);
                    Array.Copy(CRC16.getCrcByte(sendReq.Take(sendReq.Length - 2).ToArray()), 0, sendReq, sendIndex, 2);
                    temp = form1.serialDataTable.SendReadReqForWriteData(sendReq);
                    requestedData = new byte[temp[2]];
                    Array.Copy(temp, 3, requestedData, 0, temp[2]);
                }
                

                //if (form1._connectType.Equals("TCP"))
                //{
                //    temp = form1.tcpclient.sendReadReqForWriteData(sendReq);
                //    requestedData = new byte[temp[8]];
                //    Array.Copy(temp, 9, requestedData, 0, temp[8]);
                //    //Task.Factory.StartNew(delegate { temp = form1.tcpclient.sendReadReqForWriteData(sendReq);
                //    //    Task.Delay(10000).Wait();
                //    //    requestedData = new byte[temp[8]];
                //    //})
                //    //    .ContinueWith(delegate
                //    //    {


                //    //        Array.Copy(temp, 9, requestedData, 0, temp[8]);
                //    //    }, backgroundScheduler);

                //    //or |
                //    //   V
                //    //if (form1.buttonStop.Enabled)
                //    //{
                //    //    Task.Factory.StartNew(delegate { form1.tcpclient.stopTransfer(); }, backgroundScheduler).
                //    //        ContinueWith(delegate { temp = form1.tcpclient.sendReadReqForWriteData(sendReq); }, backgroundScheduler);
                //    //        //ContinueWith(delegate { form1.tcpclient.startTransfer(); }, uiScheduler);
                //    //}
                //    //else
                //    //{

                //    //    Task.Factory.StartNew(delegate { temp = form1.tcpclient.sendReadReqForWriteData(sendReq); });
                //    //}

                //}
                //else
                //{
                //    Array.Copy(CRC16.getCrcByte(sendReq.Take(sendReq.Length - 2).ToArray()), 0, sendReq, sendIndex, 2);


                //    if (form1.buttonStop.Enabled)
                //    {
                //        Task.Factory.StartNew(delegate { form1.serialDataTable.stopTransfer(); }, backgroundScheduler).
                //            ContinueWith(delegate { temp = form1.serialDataTable.sendReadReqForWriteData(sendReq); }, backgroundScheduler);
                //            //ContinueWith(delegate { form1.serialDataTable.startTransfer(); }, backgroundScheduler);
                //    }
                //    else
                //    {
                //        Task.Factory.StartNew(delegate { temp = form1.serialDataTable.sendReadReqForWriteData(sendReq); });
                //    }
                //    requestedData = new byte[temp[2]];
                //    Array.Copy(temp, 3, requestedData, 0, temp[2]);
                //}
                //==============================================================================================================================================
                //if (form1._connectType.Equals("TCP"))
                //{

                //    temp = form1.tcpclient.sendReadReqForWriteData(sendReq);
                //    requestedData = new byte[temp[8]];
                //    Array.Copy(temp,9,requestedData,0, temp[8]);
                //}
                //else
                //{
                //    Array.Copy(CRC16.getCrcByte(sendReq.Take(sendReq.Length - 2).ToArray()), 0, sendReq, sendIndex, 2);
                //    temp = form1.serialDataTable.sendReadReqForWriteData(sendReq);
                //    requestedData = new byte[temp[2]];
                //    Array.Copy(temp, 3, requestedData, 0, temp[2]);
                //}   
            }
            catch (Exception ae)
            {
                form1.AppendLog("Cannot read data for write request");
                requestedData = new byte[2 * (int)tR];
            }
            
            return requestedData;
        }
    }
}
