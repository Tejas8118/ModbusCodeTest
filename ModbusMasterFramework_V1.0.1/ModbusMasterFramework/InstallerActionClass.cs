﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusMasterFramework
{
    [RunInstaller(true)]
    public partial class InstallerActionClass : System.Configuration.Install.Installer
    {
        public InstallerActionClass()
        {
            InitializeComponent();
        }
        public override void Install(IDictionary savedState)

        {
            
            base.Install(savedState);
            
            
            //Add custom code here

        }





        public override void Rollback(IDictionary savedState)

        {

            base.Rollback(savedState);

            //Add custom code here

        }



        public override void Commit(IDictionary savedState)

        {

            base.Commit(savedState);

            //Add custom code here

        }





        public override void Uninstall(IDictionary savedState)

        {

            Process application = null;

            foreach (var process in Process.GetProcesses())

            {

                if (!process.ProcessName.ToLower().Contains("ModbusMasterFramework")) continue;

                application = process;

                break;

            }



            if (application != null && application.Responding)

            {

                application.Kill();

                base.Uninstall(savedState);

            }

        }
    }
}
