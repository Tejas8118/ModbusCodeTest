namespace ModbusMasterFramework
{
    partial class Validate
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
            this.productCodeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.validateButton = new System.Windows.Forms.Button();
            this.activateLaterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // productCodeTextBox
            // 
            this.productCodeTextBox.Location = new System.Drawing.Point(131, 26);
            this.productCodeTextBox.Name = "productCodeTextBox";
            this.productCodeTextBox.Size = new System.Drawing.Size(355, 20);
            this.productCodeTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter Activation Code:";
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(104, 72);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(93, 23);
            this.validateButton.TabIndex = 3;
            this.validateButton.Text = "Activate Now";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // activateLaterButton
            // 
            this.activateLaterButton.Location = new System.Drawing.Point(250, 72);
            this.activateLaterButton.Name = "activateLaterButton";
            this.activateLaterButton.Size = new System.Drawing.Size(117, 23);
            this.activateLaterButton.TabIndex = 4;
            this.activateLaterButton.Text = "Activate Later";
            this.activateLaterButton.UseVisualStyleBackColor = true;
            this.activateLaterButton.Click += new System.EventHandler(this.activateLaterButton_Click);
            // 
            // Validate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(501, 107);
            this.Controls.Add(this.activateLaterButton);
            this.Controls.Add(this.validateButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.productCodeTextBox);
            this.Name = "Validate";
            this.Text = "Validate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox productCodeTextBox;
        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.Button activateLaterButton;
    }
}