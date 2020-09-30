using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using ModbusMasterFramework.Properties;
using CustomProviderLibrary;

namespace ModbusMasterFramework
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

                //Check Applicatioon Run or not
                const string appName = "ModbusMaster";
                bool createdNew;

                Mutex process = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                //app is already running! Exiting the application  
                MessageBox.Show("Application is already running");
                    return;
                }

            //// Encrtpting user.config file            
            ConfigurationProtection.ProtectConfiguration();

            if (String.IsNullOrWhiteSpace(Settings1.Default.MacAddress) || Settings1.Default.MacAddress.Length != 12 || !GetMacAddress())
            {
                    Settings1.Default.Activated = false;
                    Settings1.Default.Save();
                }
                else
                {
                    Settings1.Default.Activated = true;
                    Settings1.Default.Save();
                }
            
                    
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        static bool GetMacAddress()
        {
            string addr = "";
            foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces())
            {
                addr = n.GetPhysicalAddress().ToString();
                if (addr.Equals(Settings1.Default.MacAddress) && (!String.IsNullOrWhiteSpace(addr)))
                {    
                    return true;
                }                
            }

            return false;
        }
    }
}
