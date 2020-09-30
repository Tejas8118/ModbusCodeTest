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
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ModbusMasterFramework
{
    public partial class SerialDataTable
    {
        #region Global Variables
        public SerialPort _uart;
        private delegate void SafeCallDelegate();
        public BackgroundWorker serialbackgroundWorker1 = new BackgroundWorker();
        public byte[] recBuf = null, sendBuf = null;
        public bool stopTransferFlag = false;
        ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
        Task serialTask = null;
        CancellationTokenSource serialTaskCancelSource = null;
        ConnectForm cf;
        Form1 f;

        #endregion

        BaseDataTable serialBaseDataTable = null;
        LogFile logFile_SerialDataTable = null;

        public SerialDataTable(ConnectForm connectForm)
        {
             cf = connectForm;
            f = connectForm.form1;
            try
            {
                _uart = new SerialPort(cf.comboBoxSerialPorts.Text, Int16.Parse(cf.comboBoxBaudRate.Text), cf.Parity, cf.DataBits, cf.StopBits);

                _uart.Open();
                _uart.DataReceived += SerialPort_DataReceived;
                _uart.ReadTimeout = 50;
                f.AppendLog(cf.comboBoxSerialPorts.Text + " is opened");
                serialBaseDataTable = f.formBaseDataTable;
                
                this.serialbackgroundWorker1 = new BackgroundWorker();
                this.serialbackgroundWorker1.WorkerSupportsCancellation = true;
                this.serialbackgroundWorker1.DoWork += new DoWorkEventHandler(this.BackgroundWorker1_DoWork);
                this.serialbackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);

                logFile_SerialDataTable = f.logFile_Form1;
            }
            catch (Exception e)
            {
                f.AppendLog(e.Message);
                f.AppendLog("Please try connecting other port.");
                throw new Exception();
            }
        }

        public void DisconnectSerialConnection()
        {
            if (f.buttonStop.Enabled)
                StopTransfer();
            if (_uart.IsOpen)
            {
                _uart.Close();
                f.AppendLog(cf.comboBoxSerialPorts.Text + "is Closed");
                f.labeldisplay.Text = "Disconnected";
                f.labeldisplay.ForeColor = Color.Red;
                f.labelConnectionType.Text = "None";
                f.labelConnectionType.ForeColor = Color.Red;
            }
        }
        public void  SerialPort_DataReceived(object s, SerialDataReceivedEventArgs e)
        { //function is called whenever data is available
            try
            {
                Thread.Sleep(serialBaseDataTable.baseReceiveDelay);

                recBuf = new byte[_uart.BytesToRead];
                _uart.Read(recBuf, 0, recBuf.Length);
                
                f.AppendLog("Received Data:" + SendRecivePrint(recBuf));

                if (ReceivedData(recBuf))
                {
                    if (f.InvokeRequired)
                    {
                        f.Invoke(new MethodInvoker(delegate
                        {
                            f.updateResponseCount();
                            f.setDataRreadingLabel(true);
                        }));
                    }
                    else
                    {
                        f.updateResponseCount();
                        f.setDataRreadingLabel(true);
                    }
                    //call datalogging function
                    if (logFile_SerialDataTable.dataLogOnFlag && !logFile_SerialDataTable.dataLogTimer.Enabled)
                        logFile_SerialDataTable.AddToDataLogFile();
                }
                else
                {
                    if (f.InvokeRequired)
                    {
                        f.Invoke(new MethodInvoker(delegate
                        {
                            
                            f.setDataRreadingLabel(false);
                        }));

                    }
                }


                //below is working so if backgroundworker method fails  for sendReceive then uncomment it
                //Thread.Sleep(serialBaseDataTable.baseRequestDelay - receiveDelay);
                //if (!stopTransferFlag)
                //    sendSingleData();                
                //else
                //{
                //    //_uart.DiscardOutBuffer();
                //    //_uart.DiscardInBuffer();
                //}      
                _uart.DiscardOutBuffer();
                _uart.DiscardInBuffer();

            }
            catch (Exception es)
            {
                f.AppendLog(es.Message);
                f.AppendLog("Error Receiving Message");
            }
        }

        public void WriteSendReceive(byte[] sendWriteReqData, int functionCode)
        {
            try
            {
                _uart.DiscardOutBuffer();
                
                recBuf = null;
                byte[] sendReq = SendData(sendWriteReqData, functionCode);
                f.AppendLog("*Write Request*");
                //Thread.Sleep(100)
                _uart.Write(sendReq, 0, sendReq.Length);
                if (f.InvokeRequired)
                {
                    f.Invoke(new MethodInvoker(delegate
                    {
                        f.updateRequestCount();
                    }));
                }
                else
                {
                    f.updateRequestCount();
                }
                Thread.Sleep(CheckReceiveDelayForWrite());

                f.AppendLog("Write Request Send to Server " + SendRecivePrint(sendReq));
                
            }
            catch (Exception e)
            {
                f.AppendLog("Exception inside writeSendReceive() function " + e.Message);
            }
            
        }

       //Send read request before multiple Register/Coil write request.
        public byte[] SendReadReqForWriteData(byte[] sendReq)
        {
            byte[] receivedData = null; 

            try
            {
                _uart.DiscardOutBuffer();
                _uart.DiscardInBuffer();

                _uart.Write(sendReq, 0, sendReq.Length);

                f.AppendLog("Sending Data:" + SendRecivePrint(sendReq));
                if (f.InvokeRequired)
                {
                    f.Invoke(new MethodInvoker(delegate
                    {
                        f.updateRequestCount();
                    }));
                }
                else
                {
                    f.updateRequestCount();
                }

                Thread.Sleep(serialBaseDataTable.baseReceiveDelay);
                receivedData = new byte[_uart.BytesToRead];
                _uart.Read(receivedData, 0, receivedData.Length);
                
                if (CheckError(receivedData) )
                {
                    receivedData = null;                    
                }
            }

            catch (Exception ae)
            {
                f.AppendLog( ae.ToString());
            }

           

            return receivedData;

        }

        #region serial Background worker methods
        public async Task StartTransfer()
        {
            stopTransferFlag = false;
            serialTaskCancelSource = new CancellationTokenSource();
            serialTask = Task.Factory.StartNew(DoWork);
            stopTransferFlag = false;
            
            f.AppendLog("Starting tranfer ");
        }
        public async Task StopTransfer()
        {
            stopTransferFlag = true;
            if (serialTaskCancelSource != null)
                serialTaskCancelSource.Cancel();


            f.AppendLog("Stopping tranfer ");
            f.labelDataReadingStatus.Text = "Stoped Reading";
            f.labelDataReadingStatus.ForeColor = Color.Red;

        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //try
            //{
            //    DoWork();

            //    if (serialbackgroundWorker1.CancellationPending)
            //    {
            //        // Set Cancel property of DoWorkEventArgs object to true
            //        e.Cancel = true;

            //    }

            //}
            //catch (Exception ae)
            //{
            //    e.Cancel = true;            
            //    this.serialbackgroundWorker1.CancelAsync();
            //    disconnectSerialConnection();
            //    f.AppendLog("Cannot Send Data." + ae.ToString());
            //    stopTransferFlag = true;

            //}

            //=======================================================original code=======================================================

            try
            {
                while (true)
                {
                    sendBuf = SendData(null, int.Parse(serialBaseDataTable.baseFunctionCode));

                    f.AppendLog("Sending Data:" + SendRecivePrint(sendBuf));

                    _uart.Write(sendBuf, 0, sendBuf.Length);

                    if (f.InvokeRequired)
                    {
                        f.Invoke(new MethodInvoker(delegate
                        {
                            f.updateRequestCount();
                        }));
                    }
                    else
                    {
                        f.updateRequestCount();
                    }
                    Thread.Sleep(serialBaseDataTable.baseReceiveDelay);

                    if (serialbackgroundWorker1.CancellationPending)
                    {
                        // Set Cancel property of DoWorkEventArgs object to true
                        e.Cancel = true;
                        return;
                    }
                }
            }
            catch (Exception ae)
            {
                e.Cancel = true;
                this.serialbackgroundWorker1.CancelAsync();
                DisconnectSerialConnection();
                f.AppendLog(  ae.ToString());
            }
        }
        public void DoWork()    
        {
            CancellationToken taskToken = serialTaskCancelSource.Token;
            try
            {
                while (!taskToken.IsCancellationRequested)
                {

                    sendBuf = SendData(null, int.Parse(serialBaseDataTable.baseFunctionCode));

                    if (!_uart.IsOpen)
                    {
                        f.AppendLog(_uart.PortName +" is Closed.Can't send data");
                        return;
                    }
                        _uart.Write(sendBuf, 0, sendBuf.Length);

                    f.AppendLog("Sending Data:" + SendRecivePrint(sendBuf));
                    if (f.InvokeRequired)
                    {
                        f.Invoke(new MethodInvoker(delegate
                        {
                            f.updateRequestCount();
                        }));
                    }
                    else
                    {
                        f.updateRequestCount();
                    }
                    if(serialBaseDataTable.baseReceiveDelay >=serialBaseDataTable.baseRequestDelay)
                        Thread.Sleep(serialBaseDataTable.baseReceiveDelay+200);
                    else
                        Thread.Sleep(serialBaseDataTable.baseRequestDelay);
                    if (taskToken.IsCancellationRequested)
                        break;

                }
            }
            catch(Exception e)
            {
                if (_uart.IsOpen)
                {
                    if(f.buttonStop.Enabled)
                    {
                        f.Invoke(new MethodInvoker(delegate { f.buttonStop.PerformClick(); }));

                    }

                    f.Invoke(new MethodInvoker(delegate { f.buttonDisconnect.PerformClick(); }));
                }
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Cancelled)
            {
                f.AppendLog("Stoping transfer ");
            }
            else if (e.Error != null)
            {
                f.AppendLog(e.Error.Message);
            }
            else
            {
                f.AppendLog("");
            }

        }
        #endregion

        public byte[] SendData(byte[] reqByte, int sendReqFunctionCode)
        {
            ushort sendDataStartAddress = 0, sendDataTotalRegister = 0;
            int i = 0;

            byte[] b = new byte[2];
            byte[] sendByte = new byte[8];

            if (reqByte != null)
                sendByte = new byte[4 + reqByte.Length];

            sendByte[i++] = (byte)int.Parse(f.textBoxMasterId.Text);
            sendByte[i++] = (byte)sendReqFunctionCode;

            if (sendReqFunctionCode < 5)
            {
                sendDataStartAddress = (ushort)(serialBaseDataTable.baseStartAddress);
                sendDataTotalRegister = (ushort)serialBaseDataTable.baseTotalRegister;
                b = BitConverter.GetBytes(sendDataStartAddress);

                if (BitConverter.IsLittleEndian)
                    Array.Reverse(b);
                Array.Copy(b, 0, sendByte, i, 2);
                i += 2;
                b = BitConverter.GetBytes(sendDataTotalRegister);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(b);
                Array.Copy(b, 0, sendByte, i, 2);
                i+=2;
            }
            else
            {
                if (reqByte == null)
                {
                    MessageBox.Show("write data is null.Cannot send WriteRequest inside sendData. ");
                    return null;
                }
                switch (sendReqFunctionCode)
                {
                    case 5:
                        Array.Copy(reqByte, 0, sendByte, i, reqByte.Length);
                        break;

                    case 6:
                        Array.Copy(reqByte, 0, sendByte, i, reqByte.Length);
                        break;


                    case 15:
                        Array.Copy(reqByte, 0, sendByte, i, reqByte.Length);

                        break;
                    case 16:
                        Array.Copy(reqByte, 0, sendByte, i, reqByte.Length);
                        break;

                    default:
                        sendByte = new byte[9];
                        Array.Copy(new byte[6] { 0, 0, 0, 0, 0, 3 }, 0, sendByte, 0, 6);
                        sendByte[7] = byte.Parse(f.textBoxMasterId.Text);
                        sendByte[7] = 131;
                        sendByte[8] = (byte)1;
                        break;
                }
                i += reqByte.Length;
            }
            
            Array.Copy(CRC16.getCrcByte(sendByte.Take(sendByte.Length-2).ToArray()), 0, sendByte, i, 2);
            return sendByte;
        }
        public bool ReceivedData(byte[] recBuf)
        {
            try
            {
                ushort receivedStartAddress, receivedTotalRegister, receivedFunctionCode;
                int temp, receiveFrameSize=0, recBufFunctionCode = 0; ;

                var result = CheckError(recBuf);
                if (result)
                    return false; //if exception is received.

                temp = serialBaseDataTable.baseTotalRegister;

                recBufFunctionCode = (int)recBuf[1];

                // Checking received bytes frame size
                if (recBufFunctionCode > 4 && recBufFunctionCode == serialBaseDataTable.baseWriteFunctionCode)
                {
                    receiveFrameSize = 3;
                }
                else if (recBufFunctionCode < 5 && recBufFunctionCode == int.Parse(serialBaseDataTable.baseFunctionCode))
                {
                    if (serialBaseDataTable.baseFunctionCode.Equals("01") || serialBaseDataTable.baseFunctionCode.Equals("02"))
                    {
                        receiveFrameSize = temp / 8;
                        if (temp % 8 != 0)
                        {
                            receiveFrameSize += 1;
                        }
                    }
                    else if (serialBaseDataTable.baseFunctionCode.Equals("03") || serialBaseDataTable.baseFunctionCode.Equals("04"))
                    {
                        receiveFrameSize = temp * 2;
                    }
                    else
                    {
                        f.AppendLog("Exception: Requested Function code does note match with received function code.");
                        return false;
                    }
                }
                receiveFrameSize += 5;
                if (receiveFrameSize != recBuf.Length)
                {
                    f.AppendLog("Incomplete Data");
                    return false;
                }

                receivedFunctionCode = recBuf[1]; //this is wrong
                receivedStartAddress = (ushort)serialBaseDataTable.baseStartAddress;
                receivedTotalRegister = (ushort)serialBaseDataTable.baseTotalRegister;

                switch (receivedFunctionCode)
                {
                    case 1:
                        ReadMultipleCoilStatus(recBuf);
                        break;
                    case 2:
                        ReadMultipleCoilStatus(recBuf);
                        break;
                    case 3:
                        ReadMultipleRegister(recBuf);

                        break;
                    case 4:
                        ReadMultipleRegister(recBuf);
                        break;
                    case 5:

                        break;

                    case 6:

                        break;
                    case 15:

                        break;
                    case 16:

                        break;

                }
                serialBaseDataTable.UpdateData();
            }
            catch (Exception e)
            {
                f.AppendLog("Exception while receiving data");
                return false; // not received correct data .
            }
            return true;
        }

        public bool CheckError(byte[] recBuf)
        {
            bool error = false;


            int[] functionCodes = { 1, 2, 3, 4, 5, 6, 15, 16 };
            if (functionCodes.AsEnumerable().Contains(recBuf[1]))// at 7th index Exception Function Code is received which is = Requested function code + 128 (0x80) 
                return error;


            error = true;
            switch (recBuf[2]) // at 8th index Exception Code is received
            {
                case 1:
                    f.AppendLog("Illegal Function (Exception Code : 01)");
                    break;

                case 2:
                    f.AppendLog("Illegal Data Address  (Exception Code : 02)");
                    break;

                case 3:
                    f.AppendLog("Illegal Data Value  (Exception Code : 03)");
                    break;

                case 4:
                    f.AppendLog("Master Device Failure  (Exception Code : 04)");
                    break;

                case 5:
                    f.AppendLog("Master device required more time to process  (Exception Code : 05)");
                    break;

                case 6:
                    f.AppendLog("Master Device Busy  (Exception Code : 06)");
                    break;

                case 7:
                    f.AppendLog("Negative Acknowledge  (Exception Code : 07)");
                    break;

                case 8:
                    f.AppendLog("Memory Parity Error  (Exception Code : 08)");
                    break;

                case 10:
                    f.AppendLog("Gateway Path Unavailable  (Exception Code : 0A)");
                    break;

                case 11:
                    f.AppendLog("Gateway Target Device Failed to Respond  (Exception Code : 0B)");
                    break;
            }
            return error;
        }
     
        //This function called for Function code 03 and 04.
        public void ReadMultipleRegister(byte[] recBuf)
        {
            int readIndex = 3;
            try
            {

                int totalDataBytes = (int)recBuf[2];
                serialBaseDataTable.RegisterData = new byte[totalDataBytes];

                Array.Copy(recBuf, readIndex, serialBaseDataTable.RegisterData, 0, totalDataBytes);

            }
            catch (Exception e)
            {
                f.AppendLog("Exception inside while reading Multiple Register");
            }

        }

        //This function called for Function code 01 and 02.
        public void ReadMultipleCoilStatus(byte[] recBuf)
        {
            int localTotalRegister = serialBaseDataTable.baseTotalRegister;
            byte b;
            int[] temp = new int[8];
            int i = 0, totalBytes = 0, readByte = 8, recBufDataIndex = 3;
            string binaryString;

            totalBytes = (int)recBuf[2];

            serialBaseDataTable.CoilStatus = new int[serialBaseDataTable.baseTotalRegister];

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
                Array.Copy(temp, 0, serialBaseDataTable.CoilStatus, i, readByte);
                i += readByte;
                localTotalRegister -= 8;
            }
        }
      
        public string SendRecivePrint(byte[] buffer)
        {
            int datactr = 0;
            string str = "";
            for (datactr = 0; datactr < buffer.Length; datactr++)
            {
                str += "" + buffer[datactr].ToString("X2") + " ";
            }
            //Array.Copy(RegisterData, datactr, b, 0, 2);
            //Array.Reverse(b);
            //temp = BitConverter.ToUInt16(b, 0);

            //toHex = (temp & 65535).ToString("X4"); //65535(i.e. 0000ffff) for removing ff e.g. ffff41c2 & 0000ffff = 41c2
            //txt.Text = String.Format("0x{0:x4}", toHex);
            return str;
        }
        //Receive delay for different Baudrate.
        public int CheckReceiveDelayForWrite()
        {
            int delay = 0;
            switch (cf.comboBoxBaudRate.SelectedItem.ToString())
            {
                case "7200":

                    delay = 400;
                    
                    break;
                case "4800":
                    delay = 600;
                    
                    break;
                case "2400":
                    delay = 1100;
                    break;
                case "1800":
                    delay = 1500;
                    break;
                case "1200":
                    delay = 2200;
                    break;
                case "600":
                    delay = 4700;
                    break;
                case "300":
                    delay = 10000;
                    break;
                case "150":
                    delay = 19200;

                    break;
                default:
                    delay = 300;
                    break;
            }
            return delay;
        }

    }
}
