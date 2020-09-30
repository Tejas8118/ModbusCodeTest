using ModbusMasterFramework.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusMasterFramework
{
    
    public partial class ClientSideLicense : Form
    {
        #region Global Variables
        IDictionary<string, string> serverActivationData = new Dictionary<string, string>();
        string filePath = "";
        #endregion

        #region Constructor
        public ClientSideLicense()
        {
            InitializeComponent();
            saveFileDialog1.FileName = "license";
            saveFileDialog1.Title = "Save Request File";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = "req";
            saveFileDialog1.Filter = "Request files (*.req)|*.req";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;

            openFileDialog1.Title = "Open Activation File";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.DefaultExt = "act";
            openFileDialog1.Filter = "Activation files (*.act)|*.act";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Multiselect = false;
            textboxMACAddress.ReadOnly = true;
            textboxMACAddress.Text = GetMacAddress();
            EncryptAesManaged("0016B65C54B3"); //MAC Address for encryption

        }
        #endregion

        #region MAC-Address 
        public string GetMacAddress()
        {
            string addr = "";
            foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (n.Name.Equals("Ethernet"))
                {
                    addr = n.GetPhysicalAddress().ToString();
                    break;
                }
                else if (n.OperationalStatus == OperationalStatus.Up)
                {
                    addr += n.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return addr;
        }
        #endregion

        #region AES Methods
        public byte[] getAESKey()
        {
            byte[] key = new byte[32];
            int indexOfKey = 0;
            string str1 = "digitalreachmodbusmaster12";  //NOTE:- KEY must contain 26 CHARACTERS ONLY.So take care before setting key.
            string str2 = textboxMACAddress.Text;

            foreach (char c in str1)
            {
                key[indexOfKey++] = Convert.ToByte(c);
            }

            for (int j = 0; j < str2.Length; j++)
            {
                key[indexOfKey++] = Convert.ToByte(str2.Substring(j, 2), 16);
                j++;
            }
            return key;
        }
        public void EncryptAesManaged(string raw)
        {
            byte[] aeskey = getAESKey();
            try
            {
                // Create Aes that generates a new key and initialization vector (IV).    
                // Same key must be used in encryption and decryption    
                using (AesManaged aes = new AesManaged())
                {
                    // Encrypt string    
                    byte[] encrypted = Encrypt(raw, aeskey , aes.IV);                    
                    // Decrypt the bytes to a string.    
                    string decrypted = Decrypt(encrypted, aeskey, aes.IV);
                    //// Print decrypted string. It should be same as raw data    
                    
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error!Something went wrong.");
                this.Close();
            }
           
        }
        public byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                aes.Padding = PaddingMode.PKCS7;
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }
        public string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {

            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                aes.Padding = PaddingMode.PKCS7;
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        #endregion

        #region Licence Form Textbox Eventhandlers
        private void textboxName_Validating(object sender, CancelEventArgs e)
        {
            Regex mRegxExpression;
            var textbox = (TextBox)sender;
            if (textbox.Text.Trim() != string.Empty)
            {
                mRegxExpression = new Regex(@"^[A-Za-z | \s]+$");

                if (!mRegxExpression.IsMatch(textbox.Text.Trim()))
                {
                    errorProvider1.SetError((TextBox)sender, "Name format is not correct.");
                    textbox.Focus();
                }
                else
                {
                    errorProvider1.Clear();
                }
            }
        }
       
        private void textboxEmailid_Validating(object sender, CancelEventArgs e)
        {

            Regex mRegxExpression;
            var textbox = (TextBox)sender;
            if (textbox.Text.Trim() != string.Empty)
            {
                //mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                mRegxExpression = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                if (!mRegxExpression.IsMatch(textbox.Text.Trim()))
                {
                    errorProvider1.SetError(textbox, "E-mail address format is not correct.");
                    textbox.Focus();
                }
                else
                {
                    errorProvider1.Clear();
                }

            }
        }

        public bool validateTextbox()
        {
            string name = textboxName.Text;
            string emailId = textboxEmailid.Text;

            Regex nameRegex = new Regex(@"^[A-Za-z | \s]+$");
            Regex emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase); 

            if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name) || !nameRegex.IsMatch(name))
            {
                errorProvider1.SetError(textboxName, "Name format is not correct.");
                return false;
            }

            if (String.IsNullOrEmpty(emailId) || String.IsNullOrWhiteSpace(emailId) || !emailRegex.IsMatch(emailId))
            {
                errorProvider1.SetError(textboxEmailid, "E-mail address format is not correct.");
                return false;
            }
            return true;
        }


        #endregion

        #region Request File Methods
        private void buttonGenerateRequestFile_Click(object sender, EventArgs e)
        {
            if(!validateTextbox())
                return;
            
            generateRequestFile();
        }
        public void generateRequestFile()
        {
            string reqFilePath = "";
            //Get path where user want to generate request file

            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
            {
                //string[] files = Directory.GetFiles(fbd.SelectedPath);
                reqFilePath = saveFileDialog1.FileName;

                if (File.Exists(reqFilePath))
                    File.Delete(reqFilePath);

                using (FileStream fs = File.Create(reqFilePath))
                {
                    AddText(fs, "Name:" + textboxName.Text);
                    AddText(fs, "\r\nEmailid:" + textboxEmailid.Text);
                    AddText(fs, "\r\nMAC-Address:" + textboxMACAddress.Text);
                }

                MessageBox.Show("Request File Generated");
            }
            else if (result == DialogResult.Cancel)
            {
                MessageBox.Show("No folder was selected.", "Folder error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        public void AddText(FileStream fs, string value)
        {

            byte[] info = new UTF8Encoding(true).GetBytes(BitConverter.ToString(Encoding.Default.GetBytes(value)).Replace("-", ""));

            fs.Write(info, 0, info.Length);
        }
        #endregion

        #region Activation File Methods
        private void buttonActivateLicence_Click(object sender, EventArgs e)
        {
            try
            {
               
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
                {
                    filePath = openFileDialog1.FileName;
                    if (!File.Exists(filePath))
                    {
                        MessageBox.Show("Request File does not exist at " + filePath, "Folder error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    getDataFromActivationFile(filePath);
                    byte[] cipherText = UTF8Encoding.Unicode.GetBytes(serverActivationData["Data"]); //Unicode for getting same number of bytes when encrypted
                    byte[] aesiv = UTF8Encoding.Unicode.GetBytes(serverActivationData["AES-IV"]);


                    if (GetMacAddress() == Decrypt(cipherText, getAESKey(), aesiv))
                    {
                        MessageBox.Show("Software activated ");
                        Settings1.Default.MacAddress = Decrypt(cipherText, getAESKey(), aesiv); // save mac address in userconfig settings
                        Settings1.Default.Activated = true;
                        this.Close();
                    }
                    else
                    {
                        Settings1.Default.Activated = false;
                        MessageBox.Show("Error in activating software.");
                    }
                    Settings1.Default.Save();
                }
                else if (result == DialogResult.Cancel)
                {
                    MessageBox.Show("No folder was selected.", "Folder error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Activating Software.Check Activation file or contact us at info@digireach.com");
            }
        }
        public void getDataFromActivationFile(string filePath)
        {
            string fileData = "";
            
            using (FileStream fs = File.OpenRead(filePath))
            {               
                fileData=DecryptFile(fs, null);               
                
                fileData.Split(',').ToList().ForEach(s =>
                {
                    string key = s.Split(':')[0];
                    string value = s.Split(':')[1];
                    serverActivationData[key] = value;
                });                
            }
        }       
        public string DecryptFile(FileStream fs, byte[] encryptedData)
        {
            string plainText = "";
            string skey = "digitalreach@123";
            byte[] b = new byte[1024];
            byte[] cipheredText = null;
            int bt;
            int i = 0;
            byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);
            byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

            UTF8Encoding temp = new UTF8Encoding(true);
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    while ((bt = fs.ReadByte()) != -1)
                    {
                        b[i++] = (byte)bt;
                    }
                    cipheredText = new byte[i];
                    Array.Copy(b, 0, cipheredText, 0, i);
                    String t1 = System.Text.Encoding.UTF8.GetString(cipheredText);
                    t1 = Regex.Replace(t1, @"[\0]", "");

                    aes.KeySize = 128;          // in bits
                    aes.Key = key;  // 16 bytes for 128 bit encryption
                    aes.IV = IV;   // AES needs a 16-byte IV
                    aes.Padding = PaddingMode.PKCS7;                   

                    using (MemoryStream ms = new MemoryStream(cipheredText))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            
                            using (StreamReader sw = new StreamReader(cs))
                                plainText += sw.ReadLine();
                        }

                    }
                    string demo = plainText.Split(',')[3];                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading activation file.");
                return null;
            }
            return plainText;
        }
        #endregion

        #region Form Events  
        private void buttonActivateLater_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClientSideLicense_Load(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
