using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ModbusMasterFramework
{
    public class StateObject
    {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
        
    }

    public class TCPClient
    {
        private delegate void SafeCallDelegate();
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);
        const int BUFFER_SIZE = 2048;

        byte[] buffer = new byte[BUFFER_SIZE];
        public byte[] recBuf = null, sendBuf = null;
        Form1 f = null;
        TCPDataTable TCPDataTable = null;
        TcpClient tcp = new TcpClient();

        ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
        public Task tcpTask = null;
        CancellationTokenSource tcpTaskCancelSource = null;

        public BaseDataTable tcpClientBaseDataTable;

        LogFile logFile_TcpDataTable = null;
        public ConnectForm connectForm = null;
        public LogFile logFile_Form1 = null;
        
        public TCPClient(ConnectForm connectForm, string address, int port)
        {
            this.f = connectForm.form1;
            logFile_TcpDataTable = f.logFile_Form1;

            TCPDataTable = new TCPDataTable(f);
            tcpClientBaseDataTable = f.formBaseDataTable;
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
                        
            SetupTCPConnection(address, port);                        
            
        }
        public TCPClient(Form1 f, string address, int port, string functionCode, string datatype, string formattype)
        {
            this.f = f;
            TCPDataTable = new TCPDataTable(f);
            tcpClientBaseDataTable = f.formBaseDataTable;
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);

            tcpClientBaseDataTable.baseFunctionCode = functionCode;
            tcpClientBaseDataTable.baseDatatype = datatype;
            tcpClientBaseDataTable.baseFormatType = formattype;
            SetupTCPConnection(address, port);

        }

        public TCPClient(Form1 f, string address, int port)
        {
            this.f = f;
            TCPDataTable = new TCPDataTable(f);
            tcpClientBaseDataTable = f.formBaseDataTable;
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);

            SetupTCPConnection(address, port);
        }
        public void SetupTCPConnection(string address, int listenPort)
        {
            f.AppendLog("Setting up Client...");

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           
            client.BeginConnect(new IPEndPoint(IPAddress.Parse(address), listenPort), new AsyncCallback(ConnectCallback), client);
            
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket current = (Socket)ar.AsyncState;

                // Complete the connection.  
                current.EndConnect(ar);
                f.AppendLog("Client setup complete");
                f.AppendLog("Socket connected to " + current.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
                //Send(current);
            }
            catch (Exception e)
            {

                f.AppendLog("Unable to connect.Please check IP/Port and retry.");

            }
        }

        public byte[] sendReadReqForWriteData(byte[] sendReq)
        {
            
            //TcpClient local = new TcpClient();
            //local.Client = client;
            //Stream streamlocal.GetStream();
            
            try
            {
                buffer = new byte[BUFFER_SIZE];

                

                client.BeginSend(sendReq, 0, sendReq.Length, 0, new AsyncCallback(SendCallback), client);
                f.AppendLog("Before Write Multiple Request Send to Server " + SendRecivePrint(sendReq));
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


                Receive(client);
                Thread.Sleep(100);
                if (TCPDataTable.checkError(recBuf))
                {
                    recBuf = null;
                    MessageBox.Show("TCPClient Write Read request data error inside WriteReceiveCallback");
                }
                else
                {
                    if (f.InvokeRequired)
                    {
                        f.Invoke(new MethodInvoker(delegate
                        {
                            f.updateResponseCount();
                        }));
                    }
                    else
                    {
                        f.updateResponseCount();
                    }
                }
                
            }
            catch (Exception e)
            {
                f.enableComponentsAfterDisconnection();
                f.AppendLog("Unable to connect. " + e.Message);
            }
            
            return recBuf;

        }

        public void beforeWriteReceiveCallback(IAsyncResult ar)
        {



            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;

            // Read data from the remote device.  


            int bytesRead = client.EndReceive(ar);
            recBuf = new byte[bytesRead];
            Array.Copy(buffer, recBuf, bytesRead);
            f.AppendLog("Bytes received from server " + SendRecivePrint(recBuf));

            if (TCPDataTable.checkError(recBuf))
            {
                recBuf = null;
                MessageBox.Show("TCPClient Write Read request data error inside WriteReceiveCallback");
            }


            receiveDone.Set();
        }
        public void writeSendReceive(byte[] sendWriteReqData, int functionCode)
        {
            try
            {
                
                //Thread.Sleep(1000);
                //readReqFunctionCode = int.Parse(tcpClientBaseDataTable.baseFunctionCode);
                //tcpClientBaseDataTable.baseFunctionCode = functionCode.ToString();
                
                byte[] sendReq = TCPDataTable.sendData(sendWriteReqData, functionCode);

                // Begin sending the data to the remote device.  
                f.AppendLog("*Write Request*");
                client.BeginSend(sendReq, 0, sendReq.Length, 0, new AsyncCallback(SendCallback), client);
                f.AppendLog("Bytes Send to server " + SendRecivePrint(sendReq));


                //client.EndReceive(client.BeginReceive(buffer, 0, buffer.Length, 0,
                //    new AsyncCallback(ReceiveCallback), client));
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

                // Receive the response from the remote device.  
                Receive(client);
                
                if (f.InvokeRequired)
                {
                    f.Invoke(new MethodInvoker(delegate
                    {
                        f.updateResponseCount();
                    }));
                }
                else
                {
                    f.updateResponseCount();
                }


                // Receive the response from the remote device.  


            }
            catch (Exception e)
            {
                f.AppendLog("Exception inside writeSendReceive() function " + e.Message);
            }
            
           
        }
        public void sendDataOnceOnly()
        {
            try
            {
                Send(client);


                // Receive the response from the remote device.  
                Receive(client);

            }
            catch (Exception e)
            {

                //f.enableComponentsAfterDisconnection();
                f.AppendLog( e.Message);
            }
        }
        public void Send(Socket current)
        {
            //try
            //{
            // Convert the string data to byte data using ASCII encoding.  
            sendBuf = TCPDataTable.sendData(new byte[4] { 0, 0, 0, 0 }, int.Parse(tcpClientBaseDataTable.baseFunctionCode));
            f.AppendLog("Bytes Send to server " + SendRecivePrint(sendBuf));
            // Begin sending the data to the remote device.  
            current.BeginSend(sendBuf, 0, sendBuf.Length, 0, new AsyncCallback(SendCallback), current);

            //}
            //catch( Exception e)
            //{
            //    f.enableComponentsAfterDisconnection();
            //    f.AppendLog("Cannot Send Data. "+e.Message);
            //}
        }

        public void SendCallback(IAsyncResult ar)
        {
            //try
            //{
            // Retrieve the socket from the state object.  
            Socket client = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.  
            int bytesSent = client.EndSend(ar);
            

            // Signal that all bytes have been sent.  

            connectDone.Set();
           

            //Receive(client);
            //}
            //catch (Exception e)
            //{
            //    f.enableComponentsAfterDisconnection();
            //    f.AppendLog(e.ToString());
            //}
        } 

        public void Receive(Socket client)
        {
            //try
            //{

                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;
            
                // Begin receiving the data from the remote device.  
                client.BeginReceive(buffer, 0, buffer.Length, 0,
                    new AsyncCallback(ReceiveCallback), state);
            
                
            //}
            //catch (Exception e)
            //{
            //    f.enableComponentsAfterDisconnection();
            //    f.AppendLog(e.ToString());
            //}
        }
        public void ReceiveCallback(IAsyncResult ar)
        {

            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  


                StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;

            // Read data from the remote device.  
                
                
            int bytesRead = client.EndReceive(ar);
            recBuf = new byte[bytesRead];
            Array.Copy(buffer, recBuf, bytesRead);
            f.AppendLog("Bytes received from server " + SendRecivePrint(recBuf));

            if (!TCPDataTable.receivedData(recBuf))
            {
                if (f.InvokeRequired)
                {
                    f.Invoke(new MethodInvoker(delegate
                    {
                        f.buttonStop.PerformClick();
                        f.setDataRreadingLabel(false);
                    }));
                    
                }

            }
            else
            {
                if (logFile_TcpDataTable.dataLogOnFlag && !logFile_TcpDataTable.dataLogTimer.Enabled)
                    logFile_TcpDataTable.AddToDataLogFile();

                f.Invoke(new MethodInvoker(delegate
                {
                    f.setDataRreadingLabel(true);
                }));
            }
        

            

        //if (bytesRead > 0)
        //{
        //    // There might be more data, so store the data received so far.  
        //    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

        //    // Get the rest of the data.  
        //    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //        new AsyncCallback(ReceiveCallback), state);
        //}
        //else
        //{
        //    // All the data has arrived; put it in response.  
        //    if (state.sb.Length > 1)
        //    {
        //        recBuf = new byte[state.buffer.Length]  ;
        //    }
        //    // Signal that all bytes have been received.  

        //}
        receiveDone.Set();

                //f.AppendLog("Slee|Oing for 2 seconds ");

                //Thread.Sleep(2000);
                //Send(client);
            }
            catch (Exception e)
            {
                f.enableComponentsAfterDisconnection();
                f.AppendLog(e.ToString());
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
        public async Task startTransfer()
        {
            //try
            //{
            if (tcpTask != null)
                await tcpTask;
            tcpTaskCancelSource = new CancellationTokenSource();
            tcpTask = Task.Factory.StartNew(DoWork);

            //f.AppendLog("inside start transfer if condition= thread busy= " + this.backgroundWorker1.IsBusy);
            f.AppendLog("Starting tranfer ");
                
                // This method will start the execution asynchronously in the background
                //this.backgroundWorker1.RunWorkerAsync();
                
                


            //}
            //catch (Exception e)
            //{
            //    f.enableComponentsAfterDisconnection();
            //    f.AppendLog(e.ToString());
            //}


        }
        public async Task stopTransfer()
        {
            if (tcpTaskCancelSource != null)
                tcpTaskCancelSource.Cancel();
            
            //await tcpTask;
            
            f.AppendLog("Stopping tranfer ");
            f.labelDataReadingStatus.Text = "Stopped Reading";
            f.labelDataReadingStatus.ForeColor = Color.Red;
            
            //try {

            //if (backgroundWorker1.IsBusy)
            //{
            //    // Cancel the asynchronous operation if still in progress
            //    this.backgroundWorker1.CancelAsync();
            //}                
            //}
            //catch (Exception e)
            //{
            //    f.AppendLog(e.ToString());
            //}


        }
        public void DoWork()
        {
            CancellationToken taskToken = tcpTaskCancelSource.Token;
            try
            {
                while (!taskToken.IsCancellationRequested)
                {
                    Send(client);
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

                    // Receive the response from the remote device.  
                    Receive(client);
                    if (f.InvokeRequired)
                    {
                        f.Invoke(new MethodInvoker(delegate
                        {
                            f.updateResponseCount();
                        }));
                    }
                    else
                    {
                        f.updateResponseCount();
                    }
                    Thread.Sleep(tcpClientBaseDataTable.baseRequestDelay);

                    if (taskToken.IsCancellationRequested)
                        break;
                }
            }
            catch (SocketException ae)
            {

                f.buttonDisconnect_Click(f,null);
                
                //disconnect();
                f.AppendLog( ae.ToString());
            }
            catch (Exception ae)
            {
                //disconnect();
                f.AppendLog(ae.ToString());
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                while (true)
                {
                    Send(client);
                    

                    // Receive the response from the remote device.  
                    Receive(client);
                    
                    Thread.Sleep(tcpClientBaseDataTable.baseRequestDelay);
                    f.AppendLog("background thread inside backgroundWorker1_DoWork busy= " + this.backgroundWorker1.IsBusy);
                    if (backgroundWorker1.CancellationPending)
                    {
                        f.AppendLog("inside backgroundWorker1_DoWork if condition= thread busy= " + this.backgroundWorker1.IsBusy);
                        // Set Cancel property of DoWorkEventArgs object to true
                        e.Cancel = true;
                        
                        return;
                    }
                }


            }
            catch (Exception ae)
            {
                this.backgroundWorker1.CancelAsync();
                disconnect();
                f.AppendLog(ae.ToString());
                                
                
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
            f.AppendLog("background thread inside runworkercom|oleted busy= "+this.backgroundWorker1.IsBusy);
        }




        public bool disconnect()
        {
            try
            {
                if (f.buttonStop.Enabled)
                    stopTransfer();

                client.Shutdown(SocketShutdown.Both);
                client.Close();
                return true;
            }
            catch (Exception e)
            {
                f.AppendLog("Exception: Server Disconnected");
                return false;
               
            }
        }
    }

}


