using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ModbusMasterFramework
{
    public class LogFile
    {
        #region Global Variable
        public Form1 form1_LogFile = null;
        public FileStream dataLogFileStream, errorLogFileStream;
        public System.Timers.Timer dataLogTimer = new System.Timers.Timer(1000);
        string csvFilePath = "";
        public bool dataLogOnFlag = false;
        public Datalogform datalogForm = null;

        #endregion

        public LogFile(Form1 f)
        {
            form1_LogFile = f;
            GenerateErrorLogFile();
            this.dataLogTimer.Elapsed += this.DataLogTimer_OnTimedEvent ;
            this.dataLogTimer.AutoReset = true;            
            this.dataLogTimer.Start();
        }
        public void DatalogOnAction_Click(object sender, EventArgs e)
        {            
            Generatecsvfile();            
        }
        public void DatalogOffAction_Click(object sender, EventArgs e)
        {
            SetDataLogFlagsOff();

            if (this.dataLogTimer.Enabled)
            {
                this.dataLogTimer.Stop();
            }
            if(dataLogFileStream!=null )
                dataLogFileStream.Close();
            
        }
        private void GenerateErrorLogFile()
        {
            string applicationPath = string.Empty, errorLogFilePath = string.Empty;
            bool errorLogCreated = false;
            do
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(applicationPath))
                    {
                        applicationPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        applicationPath = applicationPath.Replace(applicationPath.Split('\\').ToArray().Last(), "");
                    }
                    errorLogFilePath = applicationPath + "errorLog.txt";
                    errorLogFileStream = new FileStream(errorLogFilePath, FileMode.Append, FileAccess.Write);
                    if (errorLogFileStream.Length <= 0)
                    {
                        AddText(errorLogFileStream, "\r\nThis file is for error tracing only.\r\n===========================================================");
                        AddText(errorLogFileStream, "\r\n\tTimestamp\t\tErrorMessages\r\n");
                    }
                    errorLogCreated = true;
                }
                catch (Exception)
                {
                    applicationPath = "C:\\" + Application.ProductName + "ErrorLog\\";
                }
            } while (!errorLogCreated);

        }

        //Function for generate csv file.
        public void Generatecsvfile()
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.FileName = "modbusData";
                saveFileDialog1.Title = "Save csv File";
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.DefaultExt = "csv";
                saveFileDialog1.Filter = "Csv files (*.csv)|*.csv";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.CheckFileExists = false;
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.OverwritePrompt = false;

                //Get path where user want to generate request file
                DialogResult result = saveFileDialog1.ShowDialog();


                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
                {
                    csvFilePath = saveFileDialog1.FileName;
                    dynamic userChoice = DialogResult.Yes;
                    if (File.Exists(csvFilePath))
                    {
                        userChoice = MessageBox.Show("File already exists.Do you want to replace or Overwrite new file.", "File Exists", MessageBoxButtons.YesNoCancel);
                    }

                    if (userChoice == DialogResult.Yes || !File.Exists(csvFilePath))
                    {
                        File.Delete(csvFilePath);
                        FileStream fs = File.Create(csvFilePath);
                        fs.Close();
                        datalogForm = new Datalogform(form1_LogFile);
                        DialogResult result1 = datalogForm.ShowDialog();
                        if (result1 == DialogResult.OK)
                        {
                           
                            if (datalogForm.datalogTimeInterval == 0) //For every request
                                this.dataLogTimer.Interval = form1_LogFile.formBaseDataTable.baseRequestDelay;
                            else //for particular time interval
                                this.dataLogTimer.Interval = datalogForm.datalogTimeInterval;
                            Datalog();
                            SetDataLogFlagsOn();
                        }
                    }
                    else if (userChoice == DialogResult.No)
                    {
                        
                        datalogForm = new Datalogform(form1_LogFile);
                        DialogResult result1 = datalogForm.ShowDialog();
                        if (result1 == DialogResult.OK)
                        {
                            
                            
                            if (datalogForm.datalogTimeInterval == 0) //For every request
                                this.dataLogTimer.Interval = form1_LogFile.formBaseDataTable.baseRequestDelay;
                            else //for particular time interval
                                this.dataLogTimer.Interval = datalogForm.datalogTimeInterval;
                            Datalog(true);
                            SetDataLogFlagsOn();
                        }
                    }
                    else
                    {

                        SetDataLogFlagsOff();
                    }                  

                }
                else if (result == DialogResult.Cancel)
                {
                    SetDataLogFlagsOff();                 
                    MessageBox.Show("No folder was selected.", "Folder error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                SetDataLogFlagsOff();
                MessageBox.Show(e.ToString());
            }
        }
        
        //Function for put Register address in csv file.
        public void Datalog()
        {
            string strForDataLog = "";
        dataLogFileStream = new FileStream(csvFilePath, FileMode.Append, FileAccess.Write);
            Task.Factory.StartNew(() =>
            {

                byte[] bytes = new byte[dataLogFileStream.Length];
                if (dataLogFileStream.Length <= 0)               
                {
                    strForDataLog = "Timestamp";
                    foreach (var label in form1_LogFile.panel1.Controls.OfType<Label>())
                    {
                        strForDataLog += "," + label.Text;
                    }
                }
                AddText(dataLogFileStream, strForDataLog);
                dataLogOnFlag = true;
            });
        }
        //Function for put register address in csv file when start reading.
        public void Datalog(bool functionCodeChanged)
        {
            string str = "";
            Task.Factory.StartNew(() =>
            {
                dataLogOnFlag = false;
                Task.Delay(500).Wait();
                if(dataLogFileStream == null || !dataLogFileStream.CanWrite)
                    dataLogFileStream = new FileStream(csvFilePath, FileMode.Append, FileAccess.Write);
                byte[] bytes = new byte[dataLogFileStream.Length];

                    str = "\r\nTimestamp";
                    foreach (var label in form1_LogFile.panel1.Controls.OfType<Label>())
                    {
                        str += "," + label.Text;
                    }
                
                AddText(dataLogFileStream, str);
                Task.Delay(500).Wait();
                dataLogOnFlag = true;
            });
        }

        //Function for write reading data in csv file.
        public void AddToDataLogFile()
        {
            string strForDataLog = "";
            

            var now = DateTime.Now;
            
            Task.Factory.StartNew(() =>
            {
                strForDataLog += "\r\n" + now.ToString("MM/dd/yyyy HH:mm:ss");
                if (form1_LogFile._functionCode.Contains("3") || form1_LogFile._functionCode.Contains("4"))
                {
                    foreach (var txt in form1_LogFile.panel1.Controls.OfType<TextBox>())
                    {
                        if (String.IsNullOrWhiteSpace(txt.Text))
                        {
                            strForDataLog += ",0";
                        }
                        else
                        {
                            strForDataLog += "," + txt.Text;
                        }
                    }
                }
                else
                {
                    foreach (var txt in form1_LogFile.panel1.Controls.OfType<LedBulb>())
                    {
                        
                            if(txt.On)
                                strForDataLog += "," + "1";
                            else
                                strForDataLog += "," + "0";
                      
                    }
                }
                AddText(dataLogFileStream,strForDataLog);
                if (datalogForm.datalogTimeInterval == 0) //For every request
                    dataLogTimer.Interval = form1_LogFile.formBaseDataTable.baseRequestDelay;
                else //for particular time interval
                    dataLogTimer.Interval = datalogForm.datalogTimeInterval;                
            },TaskCreationOptions.LongRunning);
            dataLogTimer.Start();

        }

        //Function for write Errorlog in logfile 
        public void AddToErrorLogFile(string errorText)
        {
            string strForErrorLog = "";
            var now = DateTime.Now;
            strForErrorLog += "\r\n" + now.ToString() + errorText;
            AddText(errorLogFileStream, strForErrorLog);
        }

        public void AddText(FileStream fs, string value)
        
        {

            byte[] info = Encoding.UTF8.GetBytes(value);

            fs.Write(info, 0, info.Length);
        }

        //Changing in visualization when dataloging ON.
        private void SetDataLogFlagsOn()
        {
            dataLogOnFlag = true;
            form1_LogFile.labelDataLoggingFlag.Text = "ON";
            form1_LogFile.labelDataLoggingFlag.ForeColor = Color.Green;
            form1_LogFile.datalogONToolStripMenuItem.Enabled = false;
            form1_LogFile.datalogOFFToolStripMenuItem.Enabled = true;
        }

        //Changing in visualization when dataloging OFF.
        public void SetDataLogFlagsOff()
        {
            dataLogOnFlag = false;
            form1_LogFile.labelDataLoggingFlag.Text = "OFF";
            form1_LogFile.labelDataLoggingFlag.ForeColor = Color.IndianRed;
            form1_LogFile.datalogONToolStripMenuItem.Enabled = true;
            form1_LogFile.datalogOFFToolStripMenuItem.Enabled = false;
        }

        //Time interval for datalog on logfile.
        private void DataLogTimer_OnTimedEvent(Object source, ElapsedEventArgs e)
        {

            ///* only work when this is no reentry while we are already working */
            if (dataLogTimer.Enabled)
            {
                this.dataLogTimer.Stop();
            }
        }

    }
}
