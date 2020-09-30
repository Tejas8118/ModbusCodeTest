using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ModbusMasterFramework
{
    public class FormSaveSetting
    {
        const string appName = "ModbusMaster";
        Dictionary<string, string> formSettingsContainer = new Dictionary<string, string>();
       
        //Get or create your Software Registrykey from Your Computor.
        RegistryKey getRegistryKey()
        {
            RegistryKey reg_key =
                Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey sub_key = reg_key.CreateSubKey(appName);

            return sub_key;
            //sub_key.SetValue(name, value);
        }

        // Save dictonary Key/Value into your Software registry key.
        public void saveValuesIntoRegistry()
        {
            RegistryKey formSettings_RegSubKey = getRegistryKey();
            foreach (KeyValuePair<string, string> control in formSettingsContainer)
            {
                formSettings_RegSubKey.SetValue(control.Key, control.Value);
            }

        }

        // Get Key/Value from Registerkey and save Key/Value into dictionary.
        public void getValuesFromRegistry()
        {
            RegistryKey formSettings_RegSubKey = getRegistryKey();
            string[] registryKeyNames = formSettings_RegSubKey.GetValueNames();

            foreach (string key in registryKeyNames)
            {
                formSettingsContainer.Add(key, formSettings_RegSubKey.GetValue(key).ToString());
            }
        }
        
        // Set form parameter from dictionary.
        public void saveValuesIntoForm(Form1 form1)
        {
            //getting current connection type, RTU and TCP form settings and saving into dictionary variable

            dynamic controls = null;
            ConnectForm connectForm = form1.connectForm ;

            //connection type settings
            form1.connectForm ._connectType = formSettingsContainer["ConnectionType"];

            //TCP settings
            controls = connectForm.groupBoxTCP.Controls.OfType<TextBox>().ToList();
            foreach (TextBox textBox in controls)
            {
                textBox.Text = formSettingsContainer[textBox.Name];// here textbox name and dictinary key name are same
            }

            //RTU settings
            controls = connectForm.groupBoxRTU.Controls.OfType<ComboBox>().ToList();
            foreach (ComboBox comboBox in controls)
            {
                comboBox.SelectedItem = formSettingsContainer[comboBox.Name];// here Combobox name and dictinary key name are same
            }

            //slave id and delay settings
            controls = form1.groupBoxMasterIdDelay.Controls.OfType<TextBox>().ToList();
            foreach (TextBox textBox in controls)
            {
                textBox.Text = formSettingsContainer[textBox.Name]; // here textbox name and dictinary key name are same
            }
        }

        //Get parameter from form and save into Dictionary.
        public void getValuesFromForm(Form1 form1)
        {
            //getting current connection type, RTU and TCP form settings and saving into dictionary variable

            dynamic controls = null;
            ConnectForm connectForm = form1.connectForm ;



            //connection type settings
            formSettingsContainer["ConnectionType"] = form1.connectForm ._connectType;

            //TCP settings
            controls = connectForm.groupBoxTCP.Controls.OfType<TextBox>().ToList();
            foreach (TextBox textBox in controls)
            {
                formSettingsContainer[textBox.Name] = textBox.Text;
            }

            //RTU settings
            controls = connectForm.groupBoxRTU.Controls.OfType<ComboBox>().ToList();
            foreach (ComboBox comboBox in controls)
            {
                formSettingsContainer[comboBox.Name] = comboBox.SelectedItem.ToString();
            }

            //slave id and delay settings
            controls = form1.groupBoxMasterIdDelay.Controls.OfType<TextBox>().ToList();
            foreach (TextBox textBox in controls)
            {
                formSettingsContainer[textBox.Name] = textBox.Text;
            }
        }


    }
}