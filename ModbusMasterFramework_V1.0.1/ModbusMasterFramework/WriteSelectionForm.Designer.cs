namespace ModbusMasterFramework
{
    partial class WriteSelectionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSubmitWrite = new System.Windows.Forms.Button();
            this.textBoxWriteStartAddress = new System.Windows.Forms.TextBox();
            this.textBoxWriteTotalRegister = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonMultipleCoils = new System.Windows.Forms.RadioButton();
            this.radioButtonMultipleRegisters = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Address";
            // 
            // buttonSubmitWrite
            // 
            this.buttonSubmitWrite.Location = new System.Drawing.Point(126, 85);
            this.buttonSubmitWrite.Name = "buttonSubmitWrite";
            this.buttonSubmitWrite.Size = new System.Drawing.Size(97, 23);
            this.buttonSubmitWrite.TabIndex = 1;
            this.buttonSubmitWrite.Text = "Confirm";
            this.buttonSubmitWrite.UseVisualStyleBackColor = true;
            // 
            // textBoxWriteStartAddress
            // 
            this.textBoxWriteStartAddress.Location = new System.Drawing.Point(99, 52);
            this.textBoxWriteStartAddress.Name = "textBoxWriteStartAddress";
            this.textBoxWriteStartAddress.Size = new System.Drawing.Size(58, 20);
            this.textBoxWriteStartAddress.TabIndex = 2;
            this.textBoxWriteStartAddress.Enter += new System.EventHandler(this.txtData_Enter);
            this.textBoxWriteStartAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataIntegerKeyPress);
            this.textBoxWriteStartAddress.Leave += new System.EventHandler(this.TxtUInt16DataLeave);
            // 
            // textBoxWriteTotalRegister
            // 
            this.textBoxWriteTotalRegister.Location = new System.Drawing.Point(283, 52);
            this.textBoxWriteTotalRegister.Name = "textBoxWriteTotalRegister";
            this.textBoxWriteTotalRegister.Size = new System.Drawing.Size(60, 20);
            this.textBoxWriteTotalRegister.TabIndex = 4;
            this.textBoxWriteTotalRegister.Enter += new System.EventHandler(this.txtData_Enter);
            this.textBoxWriteTotalRegister.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataIntegerKeyPress);
            this.textBoxWriteTotalRegister.Leave += new System.EventHandler(this.TxtUInt16DataLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total Register";
            // 
            // radioButtonMultipleCoils
            // 
            this.radioButtonMultipleCoils.AutoSize = true;
            this.radioButtonMultipleCoils.Checked = true;
            this.radioButtonMultipleCoils.Location = new System.Drawing.Point(16, 17);
            this.radioButtonMultipleCoils.Name = "radioButtonMultipleCoils";
            this.radioButtonMultipleCoils.Size = new System.Drawing.Size(86, 17);
            this.radioButtonMultipleCoils.TabIndex = 5;
            this.radioButtonMultipleCoils.TabStop = true;
            this.radioButtonMultipleCoils.Text = "Multiple Coils";
            this.radioButtonMultipleCoils.UseVisualStyleBackColor = true;
            this.radioButtonMultipleCoils.CheckedChanged += new System.EventHandler(this.writeFunctionCodeChanged);
            // 
            // radioButtonMultipleRegisters
            // 
            this.radioButtonMultipleRegisters.AutoSize = true;
            this.radioButtonMultipleRegisters.Location = new System.Drawing.Point(157, 16);
            this.radioButtonMultipleRegisters.Name = "radioButtonMultipleRegisters";
            this.radioButtonMultipleRegisters.Size = new System.Drawing.Size(142, 17);
            this.radioButtonMultipleRegisters.TabIndex = 6;
            this.radioButtonMultipleRegisters.Text = "Multiple Holding Register";
            this.radioButtonMultipleRegisters.UseVisualStyleBackColor = true;
            this.radioButtonMultipleRegisters.CheckedChanged += new System.EventHandler(this.writeFunctionCodeChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonMultipleCoils);
            this.groupBox1.Controls.Add(this.radioButtonMultipleRegisters);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 42);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select option to write";
            // 
            // WriteSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 117);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxWriteTotalRegister);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxWriteStartAddress);
            this.Controls.Add(this.buttonSubmitWrite);
            this.Controls.Add(this.label1);
            this.Name = "WriteSelectionForm";
            this.Text = "WriteSelectionForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button buttonSubmitWrite;
        private System.Windows.Forms.TextBox textBoxWriteStartAddress;
        private System.Windows.Forms.TextBox textBoxWriteTotalRegister;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonMultipleCoils;
        private System.Windows.Forms.RadioButton radioButtonMultipleRegisters;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}