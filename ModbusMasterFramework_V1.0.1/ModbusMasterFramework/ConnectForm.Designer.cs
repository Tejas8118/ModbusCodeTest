namespace ModbusMasterFramework
{
    partial class ConnectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectForm));
            this.groupBoxTCP = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.groupBoxRTU = new System.Windows.Forms.GroupBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboBoxStopBits = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.labelParity = new System.Windows.Forms.Label();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxDataBits = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxSerialPorts = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonRTU = new System.Windows.Forms.RadioButton();
            this.radioButtonTCP = new System.Windows.Forms.RadioButton();
            this.groupBoxTCP.SuspendLayout();
            this.groupBoxRTU.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTCP
            // 
            this.groupBoxTCP.Controls.Add(this.label8);
            this.groupBoxTCP.Controls.Add(this.textBoxIP);
            this.groupBoxTCP.Controls.Add(this.label6);
            this.groupBoxTCP.Controls.Add(this.textBoxPort);
            this.groupBoxTCP.Location = new System.Drawing.Point(115, 14);
            this.groupBoxTCP.Name = "groupBoxTCP";
            this.groupBoxTCP.Size = new System.Drawing.Size(303, 40);
            this.groupBoxTCP.TabIndex = 4;
            this.groupBoxTCP.TabStop = false;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(134, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 14);
            this.label8.TabIndex = 11;
            this.label8.Text = "IP Address";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(197, 14);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxIP.TabIndex = 10;
            this.textBoxIP.Text = "127.0.0.1";
            this.textBoxIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(33, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "Port";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(65, 11);
            this.textBoxPort.MaxLength = 5;
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(53, 20);
            this.textBoxPort.TabIndex = 8;
            this.textBoxPort.Text = "502";
            this.textBoxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBoxRTU
            // 
            this.groupBoxRTU.Controls.Add(this.buttonSearch);
            this.groupBoxRTU.Controls.Add(this.comboBoxStopBits);
            this.groupBoxRTU.Controls.Add(this.label10);
            this.groupBoxRTU.Controls.Add(this.comboBoxParity);
            this.groupBoxRTU.Controls.Add(this.labelParity);
            this.groupBoxRTU.Controls.Add(this.comboBoxBaudRate);
            this.groupBoxRTU.Controls.Add(this.label5);
            this.groupBoxRTU.Controls.Add(this.comboBoxDataBits);
            this.groupBoxRTU.Controls.Add(this.label9);
            this.groupBoxRTU.Controls.Add(this.comboBoxSerialPorts);
            this.groupBoxRTU.Controls.Add(this.label4);
            this.groupBoxRTU.Enabled = false;
            this.groupBoxRTU.Location = new System.Drawing.Point(115, 78);
            this.groupBoxRTU.Name = "groupBoxRTU";
            this.groupBoxRTU.Size = new System.Drawing.Size(369, 122);
            this.groupBoxRTU.TabIndex = 27;
            this.groupBoxRTU.TabStop = false;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSearch.Location = new System.Drawing.Point(152, 14);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(73, 23);
            this.buttonSearch.TabIndex = 29;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // comboBoxStopBits
            // 
            this.comboBoxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStopBits.FormattingEnabled = true;
            this.comboBoxStopBits.Items.AddRange(new object[] {
            "None",
            "1 Bit",
            "1.5 Bits",
            "2 Bits"});
            this.comboBoxStopBits.Location = new System.Drawing.Point(230, 84);
            this.comboBoxStopBits.Name = "comboBoxStopBits";
            this.comboBoxStopBits.Size = new System.Drawing.Size(73, 21);
            this.comboBoxStopBits.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(169, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Stop Bits";
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Location = new System.Drawing.Point(64, 56);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(73, 21);
            this.comboBoxParity.TabIndex = 23;
            // 
            // labelParity
            // 
            this.labelParity.AutoSize = true;
            this.labelParity.Location = new System.Drawing.Point(12, 59);
            this.labelParity.Name = "labelParity";
            this.labelParity.Size = new System.Drawing.Size(33, 13);
            this.labelParity.TabIndex = 22;
            this.labelParity.Text = "Parity";
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaudRate.FormattingEnabled = true;
            this.comboBoxBaudRate.Items.AddRange(new object[] {
            "128000",
            "115200",
            "57600",
            "38400",
            "19200",
            "14400",
            "9600",
            "7200",
            "4800",
            "2400",
            "1800",
            "1200",
            "600",
            "300",
            "150"});
            this.comboBoxBaudRate.Location = new System.Drawing.Point(230, 54);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(73, 21);
            this.comboBoxBaudRate.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Baud Rate";
            // 
            // comboBoxDataBits
            // 
            this.comboBoxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataBits.FormattingEnabled = true;
            this.comboBoxDataBits.Items.AddRange(new object[] {
            "7 Bits",
            "8 Bits"});
            this.comboBoxDataBits.Location = new System.Drawing.Point(64, 89);
            this.comboBoxDataBits.Name = "comboBoxDataBits";
            this.comboBoxDataBits.Size = new System.Drawing.Size(75, 21);
            this.comboBoxDataBits.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Data Bits";
            // 
            // comboBoxSerialPorts
            // 
            this.comboBoxSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialPorts.FormattingEnabled = true;
            this.comboBoxSerialPorts.Location = new System.Drawing.Point(64, 16);
            this.comboBoxSerialPorts.Name = "comboBoxSerialPorts";
            this.comboBoxSerialPorts.Size = new System.Drawing.Size(73, 21);
            this.comboBoxSerialPorts.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port Name";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(151, 206);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 28;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(252, 206);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 29;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // radioButtonRTU
            // 
            this.radioButtonRTU.AutoSize = true;
            this.radioButtonRTU.Location = new System.Drawing.Point(46, 105);
            this.radioButtonRTU.Name = "radioButtonRTU";
            this.radioButtonRTU.Size = new System.Drawing.Size(48, 17);
            this.radioButtonRTU.TabIndex = 3;
            this.radioButtonRTU.Text = "RTU";
            this.radioButtonRTU.UseVisualStyleBackColor = true;
            this.radioButtonRTU.CheckedChanged += new System.EventHandler(this.RadioButtonModeChanged);
            // 
            // radioButtonTCP
            // 
            this.radioButtonTCP.Checked = true;
            this.radioButtonTCP.Location = new System.Drawing.Point(46, 37);
            this.radioButtonTCP.Name = "radioButtonTCP";
            this.radioButtonTCP.Size = new System.Drawing.Size(46, 17);
            this.radioButtonTCP.TabIndex = 1;
            this.radioButtonTCP.TabStop = true;
            this.radioButtonTCP.Text = "TCP";
            this.radioButtonTCP.UseVisualStyleBackColor = true;
            this.radioButtonTCP.CheckedChanged += new System.EventHandler(this.RadioButtonModeChanged);
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 241);
            this.Controls.Add(this.radioButtonRTU);
            this.Controls.Add(this.radioButtonTCP);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxRTU);
            this.Controls.Add(this.groupBoxTCP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectForm";
            this.Text = "Connection Details";
            this.Load += new System.EventHandler(this.ConnectForm_Load);
            this.groupBoxTCP.ResumeLayout(false);
            this.groupBoxTCP.PerformLayout();
            this.groupBoxRTU.ResumeLayout(false);
            this.groupBoxRTU.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.GroupBox groupBoxTCP;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox textBoxIP;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBoxPort;
        public System.Windows.Forms.GroupBox groupBoxRTU;
        public System.Windows.Forms.ComboBox comboBoxStopBits;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.ComboBox comboBoxParity;
        public System.Windows.Forms.Label labelParity;
        public System.Windows.Forms.ComboBox comboBoxBaudRate;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox comboBoxDataBits;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox comboBoxSerialPorts;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.RadioButton radioButtonRTU;
        public System.Windows.Forms.RadioButton radioButtonTCP;
        private System.Windows.Forms.Button buttonSearch;
        public System.Windows.Forms.Button buttonOk;
    }
}