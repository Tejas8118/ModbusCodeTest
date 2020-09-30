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
    public partial class TCPDataTable 
    {
        #region Global Variables
        private delegate void SafeCallDelegate();
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        UInt16 tcpRequestHeaderCount = 0;
        #endregion

        #region Reference
        BaseDataTable tcpBaseDataTable;
        Form1 f;
        #endregion

        public TCPDataTable(Form1 form1)
        {
            f = form1;
            tcpBaseDataTable = form1.formBaseDataTable;
        }

        public byte[] sendData(byte[] reqByte,int sendReqFunctionCode)
        {
            ushort i = 0 ,sendDataStartAddress=0,sendDataTotalRegister=0;
            //sendDataFunctionCode = UInt16.Parse(tcpBaseDataTable.baseFunctionCode);
            
            byte[] b = new byte[2];
            byte[] sendByte = new byte[12];

            if(reqByte!=null)
                sendByte = new byte[8+reqByte.Length];


            b =BitConverter.GetBytes(tcpRequestHeaderCount++);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            Array.Copy(b, 0, sendByte, i, 2);
            i+=2;
            Array.Copy(new byte[4] { 0, 0, 0, (byte)(2 + reqByte.Length) }, 0, sendByte, i, 4); //2 (Master Id+Function Code)+length of databytes
            i += 4;
            sendByte[i++] = (byte)int.Parse(f.textBoxMasterId.Text);
            sendByte[i++] = (byte)sendReqFunctionCode;




            if (sendReqFunctionCode < 5)
            {
                

                sendDataStartAddress = (ushort)(tcpBaseDataTable.baseStartAddress);
                sendDataTotalRegister = (ushort)tcpBaseDataTable.baseTotalRegister;
                b = BitConverter.GetBytes(sendDataStartAddress);
                
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(b);
                Array.Copy(b, 0, sendByte, i, 2);
                i += 2;
                b = BitConverter.GetBytes(sendDataTotalRegister);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(b);
                Array.Copy(b, 0, sendByte, i, 2);
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
                        Array.Copy(new byte[6] { 0,0,0,0,0,3}, 0, sendByte, 0, 6);
                        sendByte[7] = byte.Parse(f.textBoxMasterId.Text);
                        sendByte[7] = 131;
                        sendByte[8] = (byte)1;
                        break;
                }
            }
            
            return sendByte;
        }
        public bool receivedData(byte[] recBuf)
        {
            try
            {
                ushort receivedStartAddress, receivedTotalRegister, receivedFunctionCode;
                int temp, receiveFrameSize = 0,recBufFunctionCode = 0;

                var result = checkError(recBuf);
                if (result) 
                    return false; //if exception is received.

                temp = tcpBaseDataTable.baseTotalRegister;

                recBufFunctionCode = (int)recBuf[7];
                
                //comparing send and receive function code
                if(recBufFunctionCode > 4 && recBufFunctionCode == tcpBaseDataTable.baseWriteFunctionCode)
                {
                    receiveFrameSize = 3;
                }
                else if(recBufFunctionCode < 5 && recBufFunctionCode == int.Parse(tcpBaseDataTable.baseFunctionCode))
                {
                    if (tcpBaseDataTable.baseFunctionCode.Contains("1") || tcpBaseDataTable.baseFunctionCode.Contains("2"))
                    {
                        receiveFrameSize = temp / 8;
                        if (temp % 8 != 0)
                        {
                            receiveFrameSize += 1;
                        }

                    }
                    else if (tcpBaseDataTable.baseFunctionCode.Contains("3") || tcpBaseDataTable.baseFunctionCode.Contains("4"))
                    {
                        receiveFrameSize = temp * 2;
                    }
                }
                else
                {
                    f.AppendLog("Exception: Requested Function code does note match with received function code.");
                    return false;
                }
               
                receiveFrameSize += 9;
                if (receiveFrameSize != recBuf.Length)
                {
                    f.AppendLog("Incomplete Data");
                    return false;
                }
                receivedFunctionCode = recBuf[7]; //this is wrong
                receivedStartAddress = (ushort)tcpBaseDataTable.baseStartAddress;
                receivedTotalRegister = (ushort)tcpBaseDataTable.baseTotalRegister;

                //if ((receivedStartAddress < tcpBaseDataTable.baseStartAddress) || (receivedStartAddress > (tcpBaseDataTable.baseStartAddress + tcpBaseDataTable.baseTotalRegister)))
                //{
                //    MessageBox.Show(pa)
                //}


                //var controls = f.panel1.Controls.OfType<TextBox>().AsEnumerable<TextBox>();
                //IEnumerable<TextBox> textBoxes =
                //    from control in controls
                //    where ((int.Parse(control.Name) > receivedStartAddress) && (int.Parse(control.Name) <= (receivedStartAddress + receivedTotalRegister)))
                //    select control;

                switch (receivedFunctionCode)
                {
                    case 1:
                        readMultipleCoilStatus(recBuf);
                        break;
                    case 2:
                        readMultipleCoilStatus(recBuf);
                        break;
                    case 3:
                        readMultipleRegister(recBuf);

                        break;
                    case 4:
                        readMultipleRegister(recBuf);
                        break;
                    case 5:
                        //MessageBox.Show("Write Single Coil");
                        break;

                    case 6:
                        //MessageBox.Show("Write Single Register");

                        break;
                    case 15:
                        //MessageBox.Show("Write Multi Coil");
                        break;
                    case 16:
                        //MessageBox.Show("Write Multi Register");
                        break;

                        //default:
                        //    sendByte = new byte[9];
                        //    Array.Copy(recBuf, 0, sendByte, 0, 7);
                        //    sendByte[7] = 131;
                        //    sendByte[8] = (byte)1;
                        //    break
                }
                tcpBaseDataTable.UpdateData();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception while receiving data");
                return false; // not received correct data .
            }
            return true;
        }

        public bool checkError(byte[] recBuf)
        {
            bool error = false;
            
            byte[] b = new byte[2];
            byte[] sendByte = new byte[9];
            
            int[] functionCodes = { 1, 2, 3, 4, 5, 6, 15, 16 };
            if(functionCodes.AsEnumerable().Contains(recBuf[7]))// at 7th index Exception Function Code is received which is = Requested function code + 128 (0x80) 
                return error;


            error = true;
            switch (recBuf[8]) // at 8th index Exception Code is received
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
        public void readMultipleRegister(byte[] recBuf)
        {
            
            
            try
            {
                int readIndex = 9;

                int totalDataBytes = (int)recBuf[8];
                tcpBaseDataTable.RegisterData = new byte[totalDataBytes];


                Array.Copy(recBuf, readIndex, tcpBaseDataTable.RegisterData, 0, totalDataBytes);

                tcpBaseDataTable.UpdateData();
            }
            catch(Exception e)
            {
                MessageBox.Show("Exception inside while reading Multiple Register");
            }

        }

        //This function called for Function code 01 and 02.
        public void readMultipleCoilStatus( byte[] recBuf)
        {
            int localTotalRegister = tcpBaseDataTable.baseTotalRegister;
            byte[] sendByte = null;
            byte b;
            int[] temp = new int[8];
            int i = 0, totalBytes = 0, readByte = 8,recBufDataIndex=9;
            string binaryString;

            totalBytes = (int)recBuf[8];

            tcpBaseDataTable.CoilStatus = new int[tcpBaseDataTable.baseTotalRegister];

            for (int j = 0; j < totalBytes; j++)
            {
                if (localTotalRegister < 8)
                {
                    readByte = localTotalRegister;
                }
                temp = new int[readByte];
                b = recBuf[recBufDataIndex++];
                binaryString = Convert.ToString(b, 2);
                temp = (new String('0', 8 - binaryString.Length) + binaryString ).ToArray().Select(p => (int)Char.GetNumericValue(p)).ToArray();
                Array.Reverse(temp);
                Array.Copy(temp, 0, tcpBaseDataTable.CoilStatus, i, readByte);
                i += readByte;
                localTotalRegister -= 8;
            }

            tcpBaseDataTable.UpdateData();
        }

    }
}
