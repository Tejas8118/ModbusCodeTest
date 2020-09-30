namespace ModbusMasterFramework
{
    partial class Datalogform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Datalogform));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonSaveSelectedScan = new System.Windows.Forms.RadioButton();
            this.radioButtonSaveEveryScan = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDatalogDelay = new System.Windows.Forms.ComboBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonSaveSelectedScan);
            this.groupBox1.Controls.Add(this.radioButtonSaveEveryScan);
            this.groupBox1.Location = new System.Drawing.Point(89, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option";
            // 
            // radioButtonSaveSelectedScan
            // 
            this.radioButtonSaveSelectedScan.AutoSize = true;
            this.radioButtonSaveSelectedScan.Location = new System.Drawing.Point(28, 42);
            this.radioButtonSaveSelectedScan.Name = "radioButtonSaveSelectedScan";
            this.radioButtonSaveSelectedScan.Size = new System.Drawing.Size(123, 17);
            this.radioButtonSaveSelectedScan.TabIndex = 1;
            this.radioButtonSaveSelectedScan.Text = "Save Selected Scan";
            this.radioButtonSaveSelectedScan.UseVisualStyleBackColor = true;
            this.radioButtonSaveSelectedScan.CheckedChanged += new System.EventHandler(this.DatalogRadiobutton_Change);
            // 
            // radioButtonSaveEveryScan
            // 
            this.radioButtonSaveEveryScan.AutoSize = true;
            this.radioButtonSaveEveryScan.Checked = true;
            this.radioButtonSaveEveryScan.Location = new System.Drawing.Point(28, 19);
            this.radioButtonSaveEveryScan.Name = "radioButtonSaveEveryScan";
            this.radioButtonSaveEveryScan.Size = new System.Drawing.Size(108, 17);
            this.radioButtonSaveEveryScan.TabIndex = 0;
            this.radioButtonSaveEveryScan.TabStop = true;
            this.radioButtonSaveEveryScan.Text = "Save Every Scan";
            this.radioButtonSaveEveryScan.UseVisualStyleBackColor = true;
            this.radioButtonSaveEveryScan.CheckedChanged += new System.EventHandler(this.DatalogRadiobutton_Change);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Frequency: ";
            // 
            // comboBoxDatalogDelay
            // 
            this.comboBoxDatalogDelay.Enabled = false;
            this.comboBoxDatalogDelay.FormattingEnabled = true;
            this.comboBoxDatalogDelay.Items.AddRange(new object[] {
            "1 Minutes",
            "5 Minutes",
            "10 Minutes",
            "15 Minutes",
            "30 Minutes",
            "60 Minutes"});
            this.comboBoxDatalogDelay.Location = new System.Drawing.Point(155, 122);
            this.comboBoxDatalogDelay.Name = "comboBoxDatalogDelay";
            this.comboBoxDatalogDelay.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDatalogDelay.TabIndex = 2;
            this.comboBoxDatalogDelay.SelectedIndexChanged += new System.EventHandler(this.ChangeTimeInterval);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(89, 155);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(184, 155);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // Datalogform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 190);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.comboBoxDatalogDelay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Datalogform";
            this.Text = "Datalog Frequency";
            this.Load += new System.EventHandler(this.Datalogform_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonSaveSelectedScan;
        private System.Windows.Forms.RadioButton radioButtonSaveEveryScan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDatalogDelay;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}