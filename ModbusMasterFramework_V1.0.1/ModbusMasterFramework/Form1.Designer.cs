namespace ModbusMasterFramework
{
    partial class Form1
    {

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
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBoxMasterIdDelay = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxReceiveDelay = new System.Windows.Forms.TextBox();
            this.textBoxMasterId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxRequestDelay = new System.Windows.Forms.TextBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.listBoxCommLog = new System.Windows.Forms.ListBox();
            this.groupBoxDatatype = new System.Windows.Forms.GroupBox();
            this.comboBoxDatatype = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxStartAdress = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxFunctionCode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTotalRegister = new System.Windows.Forms.TextBox();
            this.groupBoxFormat = new System.Windows.Forms.GroupBox();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.requestCountLabel = new System.Windows.Forms.Label();
            this.responseCountLabel = new System.Windows.Forms.Label();
            this.responseNameLabel = new System.Windows.Forms.Label();
            this.buttonWriteMultiRegister = new System.Windows.Forms.Button();
            this.requestNameLabel = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.groupBoxDataTable = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonShowLog = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datalogONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datalogOFFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.registrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSoftwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelDataLoggingFlag = new System.Windows.Forms.Label();
            this.labelDataLogging = new System.Windows.Forms.Label();
            this.labeldisplay = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelDataReadingStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelConnectionType = new System.Windows.Forms.Label();
            this.groupBoxMasterIdDelay.SuspendLayout();
            this.groupBoxDatatype.SuspendLayout();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxFormat.SuspendLayout();
            this.groupBoxDataTable.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMasterIdDelay
            // 
            this.groupBoxMasterIdDelay.Controls.Add(this.label3);
            this.groupBoxMasterIdDelay.Controls.Add(this.textBoxReceiveDelay);
            this.groupBoxMasterIdDelay.Controls.Add(this.textBoxMasterId);
            this.groupBoxMasterIdDelay.Controls.Add(this.label1);
            this.groupBoxMasterIdDelay.Controls.Add(this.label7);
            this.groupBoxMasterIdDelay.Controls.Add(this.textBoxRequestDelay);
            this.groupBoxMasterIdDelay.Location = new System.Drawing.Point(3, 23);
            this.groupBoxMasterIdDelay.Name = "groupBoxMasterIdDelay";
            this.groupBoxMasterIdDelay.Size = new System.Drawing.Size(437, 35);
            this.groupBoxMasterIdDelay.TabIndex = 37;
            this.groupBoxMasterIdDelay.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(264, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 18);
            this.label3.TabIndex = 67;
            this.label3.Text = "Receive Delay (ms)";
            // 
            // textBoxReceiveDelay
            // 
            this.textBoxReceiveDelay.Location = new System.Drawing.Point(378, 11);
            this.textBoxReceiveDelay.Name = "textBoxReceiveDelay";
            this.textBoxReceiveDelay.Size = new System.Drawing.Size(41, 20);
            this.textBoxReceiveDelay.TabIndex = 66;
            this.textBoxReceiveDelay.Text = "400";
            this.textBoxReceiveDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxReceiveDelay.Enter += new System.EventHandler(this.txtData_Enter);
            this.textBoxReceiveDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataIntegerKeyPress);
            this.textBoxReceiveDelay.Leave += new System.EventHandler(this.TxtDataLeave);
            // 
            // textBoxMasterId
            // 
            this.textBoxMasterId.Location = new System.Drawing.Point(65, 10);
            this.textBoxMasterId.MaxLength = 3;
            this.textBoxMasterId.Name = "textBoxMasterId";
            this.textBoxMasterId.Size = new System.Drawing.Size(35, 20);
            this.textBoxMasterId.TabIndex = 27;
            this.textBoxMasterId.Text = "1";
            this.textBoxMasterId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxMasterId.Enter += new System.EventHandler(this.txtData_Enter);
            this.textBoxMasterId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataIntegerKeyPress);
            this.textBoxMasterId.Leave += new System.EventHandler(this.TxtDataLeave);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(107, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 18);
            this.label1.TabIndex = 30;
            this.label1.Text = "Request Delay (ms)";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(4, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 18);
            this.label7.TabIndex = 28;
            this.label7.Text = "Slave ID";
            // 
            // textBoxRequestDelay
            // 
            this.textBoxRequestDelay.Location = new System.Drawing.Point(210, 10);
            this.textBoxRequestDelay.Name = "textBoxRequestDelay";
            this.textBoxRequestDelay.Size = new System.Drawing.Size(41, 20);
            this.textBoxRequestDelay.TabIndex = 29;
            this.textBoxRequestDelay.Text = "1000";
            this.textBoxRequestDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxRequestDelay.Enter += new System.EventHandler(this.txtData_Enter);
            this.textBoxRequestDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataIntegerKeyPress);
            this.textBoxRequestDelay.Leave += new System.EventHandler(this.TxtDataLeave);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDisconnect.ForeColor = System.Drawing.Color.Black;
            this.buttonDisconnect.Location = new System.Drawing.Point(703, 29);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(71, 28);
            this.buttonDisconnect.TabIndex = 39;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = false;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonConnect.ForeColor = System.Drawing.Color.Black;
            this.buttonConnect.Location = new System.Drawing.Point(622, 29);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(71, 28);
            this.buttonConnect.TabIndex = 38;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // listBoxCommLog
            // 
            this.listBoxCommLog.BackColor = System.Drawing.Color.Black;
            this.listBoxCommLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.listBoxCommLog.FormattingEnabled = true;
            this.listBoxCommLog.HorizontalScrollbar = true;
            this.listBoxCommLog.Location = new System.Drawing.Point(8, 16);
            this.listBoxCommLog.Name = "listBoxCommLog";
            this.listBoxCommLog.Size = new System.Drawing.Size(750, 394);
            this.listBoxCommLog.TabIndex = 3;
            this.listBoxCommLog.Visible = false;
            // 
            // groupBoxDatatype
            // 
            this.groupBoxDatatype.Controls.Add(this.comboBoxDatatype);
            this.groupBoxDatatype.Location = new System.Drawing.Point(487, 63);
            this.groupBoxDatatype.Name = "groupBoxDatatype";
            this.groupBoxDatatype.Size = new System.Drawing.Size(136, 43);
            this.groupBoxDatatype.TabIndex = 41;
            this.groupBoxDatatype.TabStop = false;
            this.groupBoxDatatype.Text = "Datatype";
            // 
            // comboBoxDatatype
            // 
            this.comboBoxDatatype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatatype.FormattingEnabled = true;
            this.comboBoxDatatype.Items.AddRange(new object[] {
            "Int32",
            "Unsigned Int32",
            "Int16",
            "Unsigned Int16",
            "Float",
            "Binary",
            "Hex"});
            this.comboBoxDatatype.Location = new System.Drawing.Point(7, 14);
            this.comboBoxDatatype.Name = "comboBoxDatatype";
            this.comboBoxDatatype.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDatatype.TabIndex = 67;
            this.comboBoxDatatype.SelectedIndexChanged += new System.EventHandler(this.DatatypeChanged);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 14);
            this.label11.TabIndex = 45;
            this.label11.Text = "Start Address";
            // 
            // textBoxStartAdress
            // 
            this.textBoxStartAdress.Location = new System.Drawing.Point(11, 30);
            this.textBoxStartAdress.MaxLength = 5;
            this.textBoxStartAdress.Name = "textBoxStartAdress";
            this.textBoxStartAdress.Size = new System.Drawing.Size(54, 20);
            this.textBoxStartAdress.TabIndex = 44;
            this.textBoxStartAdress.Text = "1";
            this.textBoxStartAdress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxStartAdress.Enter += new System.EventHandler(this.txtData_Enter);
            this.textBoxStartAdress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataIntegerKeyPress);
            this.textBoxStartAdress.Leave += new System.EventHandler(this.TxtDataLeave);
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.label4);
            this.groupBoxInput.Controls.Add(this.comboBoxFunctionCode);
            this.groupBoxInput.Controls.Add(this.label2);
            this.groupBoxInput.Controls.Add(this.textBoxStartAdress);
            this.groupBoxInput.Controls.Add(this.label11);
            this.groupBoxInput.Controls.Add(this.textBoxTotalRegister);
            this.groupBoxInput.Location = new System.Drawing.Point(4, 59);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(321, 55);
            this.groupBoxInput.TabIndex = 4;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Input";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(206, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 14);
            this.label4.TabIndex = 67;
            this.label4.Text = "Function Code";
            // 
            // comboBoxFunctionCode
            // 
            this.comboBoxFunctionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFunctionCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxFunctionCode.FormattingEnabled = true;
            this.comboBoxFunctionCode.Items.AddRange(new object[] {
            "01 Coil Status",
            "02 Input Status  ",
            "03 Holding Register",
            "04 Input Registeer"});
            this.comboBoxFunctionCode.Location = new System.Drawing.Point(190, 29);
            this.comboBoxFunctionCode.Name = "comboBoxFunctionCode";
            this.comboBoxFunctionCode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFunctionCode.TabIndex = 66;
            this.comboBoxFunctionCode.SelectedIndexChanged += new System.EventHandler(this.SelectindechangedfunctionCode);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(91, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 14);
            this.label2.TabIndex = 34;
            this.label2.Text = "Total Register";
            // 
            // textBoxTotalRegister
            // 
            this.textBoxTotalRegister.Location = new System.Drawing.Point(97, 30);
            this.textBoxTotalRegister.MaxLength = 4;
            this.textBoxTotalRegister.Name = "textBoxTotalRegister";
            this.textBoxTotalRegister.Size = new System.Drawing.Size(56, 20);
            this.textBoxTotalRegister.TabIndex = 33;
            this.textBoxTotalRegister.Text = "10";
            this.textBoxTotalRegister.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTotalRegister.Enter += new System.EventHandler(this.txtData_Enter);
            this.textBoxTotalRegister.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataIntegerKeyPress);
            this.textBoxTotalRegister.Leave += new System.EventHandler(this.TxtDataLeave);
            // 
            // groupBoxFormat
            // 
            this.groupBoxFormat.Controls.Add(this.comboBoxFormat);
            this.groupBoxFormat.Location = new System.Drawing.Point(635, 63);
            this.groupBoxFormat.Name = "groupBoxFormat";
            this.groupBoxFormat.Size = new System.Drawing.Size(136, 43);
            this.groupBoxFormat.TabIndex = 5;
            this.groupBoxFormat.TabStop = false;
            this.groupBoxFormat.Text = "Format";
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Items.AddRange(new object[] {
            "Big Endian (ABCD)",
            "Little Endian (DCBA)",
            "Mid Big Endian (BADC)",
            "Mid Little Endian (CDAB)"});
            this.comboBoxFormat.Location = new System.Drawing.Point(10, 19);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFormat.TabIndex = 68;
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.DisplayFormatModeChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonClear.ForeColor = System.Drawing.Color.Black;
            this.buttonClear.Location = new System.Drawing.Point(538, 126);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(84, 23);
            this.buttonClear.TabIndex = 58;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // requestCountLabel
            // 
            this.requestCountLabel.AutoSize = true;
            this.requestCountLabel.Location = new System.Drawing.Point(747, 118);
            this.requestCountLabel.Name = "requestCountLabel";
            this.requestCountLabel.Size = new System.Drawing.Size(13, 13);
            this.requestCountLabel.TabIndex = 65;
            this.requestCountLabel.Text = "0";
            // 
            // responseCountLabel
            // 
            this.responseCountLabel.AutoSize = true;
            this.responseCountLabel.Location = new System.Drawing.Point(747, 136);
            this.responseCountLabel.Name = "responseCountLabel";
            this.responseCountLabel.Size = new System.Drawing.Size(13, 13);
            this.responseCountLabel.TabIndex = 64;
            this.responseCountLabel.Text = "0";
            // 
            // responseNameLabel
            // 
            this.responseNameLabel.AutoSize = true;
            this.responseNameLabel.Location = new System.Drawing.Point(631, 135);
            this.responseNameLabel.Name = "responseNameLabel";
            this.responseNameLabel.Size = new System.Drawing.Size(110, 13);
            this.responseNameLabel.TabIndex = 62;
            this.responseNameLabel.Text = "Number of Response:";
            // 
            // buttonWriteMultiRegister
            // 
            this.buttonWriteMultiRegister.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonWriteMultiRegister.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonWriteMultiRegister.ForeColor = System.Drawing.Color.Black;
            this.buttonWriteMultiRegister.Location = new System.Drawing.Point(194, 123);
            this.buttonWriteMultiRegister.Name = "buttonWriteMultiRegister";
            this.buttonWriteMultiRegister.Size = new System.Drawing.Size(159, 28);
            this.buttonWriteMultiRegister.TabIndex = 61;
            this.buttonWriteMultiRegister.Text = "Write Multiple Coil/Register";
            this.buttonWriteMultiRegister.UseVisualStyleBackColor = false;
            this.buttonWriteMultiRegister.Click += new System.EventHandler(this.buttonWriteMultiRegister_Click);
            // 
            // requestNameLabel
            // 
            this.requestNameLabel.AutoSize = true;
            this.requestNameLabel.Location = new System.Drawing.Point(634, 117);
            this.requestNameLabel.Name = "requestNameLabel";
            this.requestNameLabel.Size = new System.Drawing.Size(107, 13);
            this.requestNameLabel.TabIndex = 63;
            this.requestNameLabel.Text = "Number of Requests:";
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonStart.ForeColor = System.Drawing.Color.Black;
            this.buttonStart.Location = new System.Drawing.Point(8, 123);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(86, 28);
            this.buttonStart.TabIndex = 60;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonStop.ForeColor = System.Drawing.Color.Black;
            this.buttonStop.Location = new System.Drawing.Point(101, 123);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(86, 28);
            this.buttonStop.TabIndex = 59;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // groupBoxDataTable
            // 
            this.groupBoxDataTable.Controls.Add(this.panel1);
            this.groupBoxDataTable.Location = new System.Drawing.Point(1, 153);
            this.groupBoxDataTable.Name = "groupBoxDataTable";
            this.groupBoxDataTable.Size = new System.Drawing.Size(772, 415);
            this.groupBoxDataTable.TabIndex = 57;
            this.groupBoxDataTable.TabStop = false;
            this.groupBoxDataTable.Text = "Register Data Table";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.listBoxCommLog);
            this.panel1.Location = new System.Drawing.Point(6, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 390);
            this.panel1.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // buttonShowLog
            // 
            this.buttonShowLog.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonShowLog.Location = new System.Drawing.Point(453, 126);
            this.buttonShowLog.Name = "buttonShowLog";
            this.buttonShowLog.Size = new System.Drawing.Size(75, 23);
            this.buttonShowLog.TabIndex = 66;
            this.buttonShowLog.Text = "Showlog";
            this.buttonShowLog.UseVisualStyleBackColor = true;
            this.buttonShowLog.Click += new System.EventHandler(this.buttonShowLog_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.setupToolStripMenuItem,
            this.toolStripMenuItem2});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(784, 29);
            this.menuStrip1.TabIndex = 70;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importSettingsToolStripMenuItem,
            this.exportSettingsToolStripMenuItem,
            this.datalogONToolStripMenuItem,
            this.datalogOFFToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(29, 25);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // importSettingsToolStripMenuItem
            // 
            this.importSettingsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.importSettingsToolStripMenuItem.Name = "importSettingsToolStripMenuItem";
            this.importSettingsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.importSettingsToolStripMenuItem.Size = new System.Drawing.Size(155, 20);
            this.importSettingsToolStripMenuItem.Text = "Import Settings";
            this.importSettingsToolStripMenuItem.Click += new System.EventHandler(this.ImportExportMenuSettings_Click);
            // 
            // exportSettingsToolStripMenuItem
            // 
            this.exportSettingsToolStripMenuItem.Name = "exportSettingsToolStripMenuItem";
            this.exportSettingsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exportSettingsToolStripMenuItem.Text = "Export Settings";
            this.exportSettingsToolStripMenuItem.Click += new System.EventHandler(this.ImportExportMenuSettings_Click);
            // 
            // datalogONToolStripMenuItem
            // 
            this.datalogONToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.datalogONToolStripMenuItem.Name = "datalogONToolStripMenuItem";
            this.datalogONToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.datalogONToolStripMenuItem.Text = "Datalog ON";
            // 
            // datalogOFFToolStripMenuItem
            // 
            this.datalogOFFToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.datalogOFFToolStripMenuItem.Enabled = false;
            this.datalogOFFToolStripMenuItem.Name = "datalogOFFToolStripMenuItem";
            this.datalogOFFToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.datalogOFFToolStripMenuItem.Text = "Datalog OFF";
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(41, 25);
            this.setupToolStripMenuItem.Text = "Setup";
            this.setupToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrationToolStripMenuItem,
            this.aboutSoftwareToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenuItem2.Size = new System.Drawing.Size(36, 25);
            this.toolStripMenuItem2.Text = "Help";
            this.toolStripMenuItem2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // registrationToolStripMenuItem
            // 
            this.registrationToolStripMenuItem.Name = "registrationToolStripMenuItem";
            this.registrationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.registrationToolStripMenuItem.Text = "Registration";
            this.registrationToolStripMenuItem.Click += new System.EventHandler(this.Registration);
            // 
            // aboutSoftwareToolStripMenuItem
            // 
            this.aboutSoftwareToolStripMenuItem.Name = "aboutSoftwareToolStripMenuItem";
            this.aboutSoftwareToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutSoftwareToolStripMenuItem.Text = "User Guide";
            this.aboutSoftwareToolStripMenuItem.Click += new System.EventHandler(this.aboutSoftwareToolStripMenuItem_Click);
            // 
            // labelDataLoggingFlag
            // 
            this.labelDataLoggingFlag.AutoSize = true;
            this.labelDataLoggingFlag.BackColor = System.Drawing.Color.LightYellow;
            this.labelDataLoggingFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDataLoggingFlag.ForeColor = System.Drawing.Color.Red;
            this.labelDataLoggingFlag.Location = new System.Drawing.Point(677, 599);
            this.labelDataLoggingFlag.Name = "labelDataLoggingFlag";
            this.labelDataLoggingFlag.Size = new System.Drawing.Size(30, 13);
            this.labelDataLoggingFlag.TabIndex = 72;
            this.labelDataLoggingFlag.Text = "OFF";
            this.labelDataLoggingFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDataLogging
            // 
            this.labelDataLogging.AutoSize = true;
            this.labelDataLogging.Location = new System.Drawing.Point(596, 599);
            this.labelDataLogging.Name = "labelDataLogging";
            this.labelDataLogging.Size = new System.Drawing.Size(74, 13);
            this.labelDataLogging.TabIndex = 71;
            this.labelDataLogging.Text = "Data Logging:";
            // 
            // labeldisplay
            // 
            this.labeldisplay.AutoSize = true;
            this.labeldisplay.BackColor = System.Drawing.Color.LightYellow;
            this.labeldisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeldisplay.ForeColor = System.Drawing.Color.Red;
            this.labeldisplay.Location = new System.Drawing.Point(112, 577);
            this.labeldisplay.Name = "labeldisplay";
            this.labeldisplay.Size = new System.Drawing.Size(85, 13);
            this.labeldisplay.TabIndex = 73;
            this.labeldisplay.Text = "Disconnected";
            this.labeldisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 577);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Connection:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(562, 576);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 13);
            this.label6.TabIndex = 75;
            this.label6.Text = "Data Reading Status:";
            // 
            // labelDataReadingStatus
            // 
            this.labelDataReadingStatus.AutoSize = true;
            this.labelDataReadingStatus.BackColor = System.Drawing.Color.LightYellow;
            this.labelDataReadingStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDataReadingStatus.ForeColor = System.Drawing.Color.Red;
            this.labelDataReadingStatus.Location = new System.Drawing.Point(677, 577);
            this.labelDataReadingStatus.Name = "labelDataReadingStatus";
            this.labelDataReadingStatus.Size = new System.Drawing.Size(78, 13);
            this.labelDataReadingStatus.TabIndex = 76;
            this.labelDataReadingStatus.Text = "Not Reading";
            this.labelDataReadingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 599);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 77;
            this.label8.Text = "Connection Type:";
            // 
            // labelConnectionType
            // 
            this.labelConnectionType.AutoSize = true;
            this.labelConnectionType.BackColor = System.Drawing.Color.LightYellow;
            this.labelConnectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectionType.ForeColor = System.Drawing.Color.Red;
            this.labelConnectionType.Location = new System.Drawing.Point(112, 599);
            this.labelConnectionType.Name = "labelConnectionType";
            this.labelConnectionType.Size = new System.Drawing.Size(37, 13);
            this.labelConnectionType.TabIndex = 78;
            this.labelConnectionType.Text = "None";
            this.labelConnectionType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(784, 621);
            this.Controls.Add(this.labelConnectionType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelDataReadingStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labeldisplay);
            this.Controls.Add(this.labelDataLoggingFlag);
            this.Controls.Add(this.labelDataLogging);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.buttonShowLog);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.requestCountLabel);
            this.Controls.Add(this.responseCountLabel);
            this.Controls.Add(this.responseNameLabel);
            this.Controls.Add(this.buttonWriteMultiRegister);
            this.Controls.Add(this.requestNameLabel);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.groupBoxDataTable);
            this.Controls.Add(this.groupBoxFormat);
            this.Controls.Add(this.groupBoxDatatype);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.groupBoxMasterIdDelay);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonDisconnect);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Modbus Master";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.beforeClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxMasterIdDelay.ResumeLayout(false);
            this.groupBoxMasterIdDelay.PerformLayout();
            this.groupBoxDatatype.ResumeLayout(false);
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxFormat.ResumeLayout(false);
            this.groupBoxDataTable.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        public System.Windows.Forms.GroupBox groupBoxMasterIdDelay;
        public System.Windows.Forms.TextBox textBoxMasterId;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox textBoxRequestDelay;
        public System.Windows.Forms.Button buttonDisconnect;
        public System.Windows.Forms.Button buttonConnect;
        public System.Windows.Forms.ListBox listBoxCommLog;
        public System.Windows.Forms.GroupBox groupBoxDatatype;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox textBoxStartAdress;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.GroupBox groupBoxInput;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxTotalRegister;
        public System.Windows.Forms.GroupBox groupBoxFormat;
        public System.Windows.Forms.Button buttonClear;
        public System.Windows.Forms.Label requestCountLabel;
        public System.Windows.Forms.Label responseCountLabel;
        public System.Windows.Forms.Label responseNameLabel;
        public System.Windows.Forms.Button buttonWriteMultiRegister;
        public System.Windows.Forms.Label requestNameLabel;
        public System.Windows.Forms.Button buttonStart;
        public System.Windows.Forms.Button buttonStop;
        public System.Windows.Forms.GroupBox groupBoxDataTable;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox textBoxReceiveDelay;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.ComboBox comboBoxFunctionCode;
        public System.Windows.Forms.ComboBox comboBoxDatatype;
        public System.Windows.Forms.ComboBox comboBoxFormat;
        public System.Windows.Forms.Button buttonShowLog;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem importSettingsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem datalogONToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem datalogOFFToolStripMenuItem;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem registrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSoftwareToolStripMenuItem;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label labelDataLoggingFlag;
        public System.Windows.Forms.Label labelDataLogging;
        public System.Windows.Forms.Label labeldisplay;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label labelDataReadingStatus;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label labelConnectionType;
        private System.Windows.Forms.ToolStripMenuItem exportSettingsToolStripMenuItem;
    }
}