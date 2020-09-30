namespace ModbusMasterFramework
{
    partial class WriteRequestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriteRequestForm));
            this.writeRequestFormHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSendWriteRequest = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelWriteStartAddress = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelWriteTotalRegister = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // writeRequestFormHeader
            // 
            this.writeRequestFormHeader.AutoSize = true;
            this.writeRequestFormHeader.Location = new System.Drawing.Point(46, 12);
            this.writeRequestFormHeader.Name = "writeRequestFormHeader";
            this.writeRequestFormHeader.Size = new System.Drawing.Size(76, 13);
            this.writeRequestFormHeader.TabIndex = 0;
            this.writeRequestFormHeader.Text = "Start Address :";
            
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 227);
            this.panel1.TabIndex = 1;
            // 
            // buttonSendWriteRequest
            // 
            this.buttonSendWriteRequest.Location = new System.Drawing.Point(137, 295);
            this.buttonSendWriteRequest.Name = "buttonSendWriteRequest";
            this.buttonSendWriteRequest.Size = new System.Drawing.Size(75, 23);
            this.buttonSendWriteRequest.TabIndex = 2;
            this.buttonSendWriteRequest.Text = "Submit";
            this.buttonSendWriteRequest.UseVisualStyleBackColor = true;
            
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 252);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Register Data";
            // 
            // labelWriteStartAddress
            // 
            this.labelWriteStartAddress.Location = new System.Drawing.Point(124, 12);
            this.labelWriteStartAddress.Name = "labelWriteStartAddress";
            this.labelWriteStartAddress.Size = new System.Drawing.Size(55, 13);
            this.labelWriteStartAddress.TabIndex = 4;
            this.labelWriteStartAddress.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Total Register :";
            // 
            // labelWriteTotalRegister
            // 
            this.labelWriteTotalRegister.Location = new System.Drawing.Point(266, 13);
            this.labelWriteTotalRegister.Name = "labelWriteTotalRegister";
            this.labelWriteTotalRegister.Size = new System.Drawing.Size(76, 13);
            this.labelWriteTotalRegister.TabIndex = 6;
            this.labelWriteTotalRegister.Text = "0";
            // 
            // WriteRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 323);
            this.Controls.Add(this.labelWriteTotalRegister);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelWriteStartAddress);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSendWriteRequest);
            this.Controls.Add(this.writeRequestFormHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WriteRequestForm";
            this.Text = "Write Multiple Coil/Register";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label writeRequestFormHeader;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button buttonSendWriteRequest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelWriteStartAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelWriteTotalRegister;
    }
}