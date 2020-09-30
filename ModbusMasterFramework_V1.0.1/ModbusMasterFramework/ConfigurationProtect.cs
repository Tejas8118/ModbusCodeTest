using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
 
namespace ModbusMasterFramework
{
    public class ConfigurationProtection
    {
        // Protect the connectionStrings section.
        public static void ProtectConfiguration()
        {
            
            //////////////////////////////////////////////////////////////
          
            // Get the application configuration file.
            Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.PerUserRoamingAndLocal);

            // Define the Rsa provider name.
            
            string provider = "TripleDESProtectedConfigurationProvider";

            // Get the section to protect.
            ConfigurationSection connStrings = config.GetSection("userSettings/ModbusMasterFramework.Properties.Settings1") ;
            
           
            if (connStrings != null)
            {
                if (!connStrings.SectionInformation.IsProtected)
                {
                    if (!connStrings.ElementInformation.IsLocked)
                    {
                        
                        // Protect the section.
                        connStrings.SectionInformation.ProtectSection(provider);
                        
                        connStrings.SectionInformation.ForceSave = true;
                        
                        config.Save(ConfigurationSaveMode.Full);

                    }
                }
            }
        }

        // Unprotect the connectionStrings section.
        private static void UnProtectConfiguration()
        {

            // Get the application configuration file.
            System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);

            // Get the section to unprotect.
            ConfigurationSection connStrings =
                config.ConnectionStrings;

            if (connStrings != null)
            {
                if (connStrings.SectionInformation.IsProtected)
                {
                    if (!connStrings.ElementInformation.IsLocked)
                    {
                        // Unprotect the section.
                        connStrings.SectionInformation.UnprotectSection();

                        connStrings.SectionInformation.ForceSave = true;
                        config.Save(ConfigurationSaveMode.Full);

                    }
                }
            }
        }
        
    }
}
