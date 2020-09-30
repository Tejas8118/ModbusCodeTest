namespace ModbusMasterFramework
{
    partial class ClientSideLicense
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientSideLicense));
            this.textboxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxEmailid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxMACAddress = new System.Windows.Forms.TextBox();
            this.buttonGenerateRequestFile = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonActivateLicence = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonActivateLater = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // textboxName
            // 
            this.textboxName.Location = new System.Drawing.Point(123, 16);
            this.textboxName.Name = "textboxName";
            this.textboxName.Size = new System.Drawing.Size(345, 20);
            this.textboxName.TabIndex = 0;
            this.textboxName.Validating += new System.ComponentModel.CancelEventHandler(this.textboxName_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Your Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Email-Id :";
            // 
            // textboxEmailid
            // 
            this.textboxEmailid.Location = new System.Drawing.Point(123, 65);
            this.textboxEmailid.Name = "textboxEmailid";
            this.textboxEmailid.Size = new System.Drawing.Size(345, 20);
            this.textboxEmailid.TabIndex = 2;
            this.textboxEmailid.Validating += new System.ComponentModel.CancelEventHandler(this.textboxEmailid_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "MAC Address : ";
            this.label3.Visible = false;
            // 
            // textboxMACAddress
            // 
            this.textboxMACAddress.Location = new System.Drawing.Point(35, 114);
            this.textboxMACAddress.Name = "textboxMACAddress";
            this.textboxMACAddress.Size = new System.Drawing.Size(12, 20);
            this.textboxMACAddress.TabIndex = 4;
            this.textboxMACAddress.Visible = false;
            // 
            // buttonGenerateRequestFile
            // 
            this.buttonGenerateRequestFile.Location = new System.Drawing.Point(53, 114);
            this.buttonGenerateRequestFile.Name = "buttonGenerateRequestFile";
            this.buttonGenerateRequestFile.Size = new System.Drawing.Size(100, 36);
            this.buttonGenerateRequestFile.TabIndex = 6;
            this.buttonGenerateRequestFile.Text = "Generate Request File";
            this.buttonGenerateRequestFile.UseVisualStyleBackColor = true;
            this.buttonGenerateRequestFile.Click += new System.EventHandler(this.buttonGenerateRequestFile_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // buttonActivateLicence
            // 
            this.buttonActivateLicence.Location = new System.Drawing.Point(204, 114);
            this.buttonActivateLicence.Name = "buttonActivateLicence";
            this.buttonActivateLicence.Size = new System.Drawing.Size(89, 36);
            this.buttonActivateLicence.TabIndex = 12;
            this.buttonActivateLicence.Text = "Activate Software";
            this.buttonActivateLicence.UseVisualStyleBackColor = true;
            this.buttonActivateLicence.Click += new System.EventHandler(this.buttonActivateLicence_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonActivateLater
            // 
            this.buttonActivateLater.Location = new System.Drawing.Point(365, 114);
            this.buttonActivateLater.Name = "buttonActivateLater";
            this.buttonActivateLater.Size = new System.Drawing.Size(77, 36);
            this.buttonActivateLater.TabIndex = 13;
            this.buttonActivateLater.Text = "Activate Later";
            this.buttonActivateLater.UseVisualStyleBackColor = true;
            this.buttonActivateLater.Click += new System.EventHandler(this.buttonActivateLater_Click);
            // 
            // ClientSideLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(489, 156);
            this.Controls.Add(this.buttonActivateLater);
            this.Controls.Add(this.buttonActivateLicence);
            this.Controls.Add(this.buttonGenerateRequestFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxMACAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textboxEmailid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClientSideLicense";
            this.Text = "Activation Window";
            this.Load += new System.EventHandler(this.ClientSideLicense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxEmailid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxMACAddress;
        private System.Windows.Forms.Button buttonGenerateRequestFile;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button buttonActivateLicence;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonActivateLater;
    }
}

