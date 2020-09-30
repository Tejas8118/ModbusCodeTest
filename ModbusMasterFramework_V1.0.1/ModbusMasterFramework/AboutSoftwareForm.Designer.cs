namespace ModbusMasterFramework
{
    partial class AboutSoftwareForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutSoftwareForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.enterProductKeyLabel = new System.Windows.Forms.Label();
            this.buttonEnterProductKey = new System.Windows.Forms.Button();
            this.registrationLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "digiReachLogo.ico");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.enterProductKeyLabel);
            this.groupBox1.Controls.Add(this.buttonEnterProductKey);
            this.groupBox1.Controls.Add(this.registrationLabel);
            this.groupBox1.Location = new System.Drawing.Point(228, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registration";
           
            // 
            // enterProductKeyLabel
            // 
            this.enterProductKeyLabel.Location = new System.Drawing.Point(6, 57);
            this.enterProductKeyLabel.Name = "enterProductKeyLabel";
            this.enterProductKeyLabel.Size = new System.Drawing.Size(173, 23);
            this.enterProductKeyLabel.TabIndex = 10;
            this.enterProductKeyLabel.Text = "Click below to enter Product Key.";
            this.enterProductKeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonEnterPK
            // 
            this.buttonEnterProductKey.Location = new System.Drawing.Point(38, 100);
            this.buttonEnterProductKey.Name = "buttonEnterProductKey";
            this.buttonEnterProductKey.Size = new System.Drawing.Size(86, 23);
            this.buttonEnterProductKey.TabIndex = 1;
            this.buttonEnterProductKey.Text = "Product Key";
            this.buttonEnterProductKey.UseVisualStyleBackColor = true;
            this.buttonEnterProductKey.Click += new System.EventHandler(this.buttonEnterProductKey_Click);
            // 
            // registrationLabel
            // 
            this.registrationLabel.Location = new System.Drawing.Point(6, 16);
            this.registrationLabel.Name = "registrationLabel";
            this.registrationLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.registrationLabel.Size = new System.Drawing.Size(152, 32);
            this.registrationLabel.TabIndex = 0;
            this.registrationLabel.Text = "This is an unregistered copy of Modbus Master.";
            this.registrationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
           
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 119);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 49);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Version :";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(53, 53);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(31, 13);
            this.versionLabel.TabIndex = 3;
            this.versionLabel.Text = "1.0.0";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(303, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Copyright © 2020";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(349, 35);
            this.label4.TabIndex = 5;
            this.label4.Text = "502, Venus Atlantis, Anandnagar Rd, Prahlad Nagar, Ahmedabad, Gujarat, India 3800" +
    "15 ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(1, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "Email : info@digireach.com";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(-2, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Website :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(47, 230);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(141, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://www.digireach.com/";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 80);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.label7.Size = new System.Drawing.Size(210, 35);
            this.label7.TabIndex = 9;
            this.label7.Text = "For any bug/issue/registration of product contact us as given below.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "Product :";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(53, 9);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.label8.Size = new System.Drawing.Size(175, 44);
            this.label8.TabIndex = 11;
            this.label8.Text = "Modbus Master Simulator supports Modbus protocol over TCP/IP and RTU connection.";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 247);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(172, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "Contact : +91 966 2540 922";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AboutSoftwareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(437, 265);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutSoftwareForm";
            this.Text = "Modbus Master Information";
            this.Load += new System.EventHandler(this.AboutSoftwareForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label registrationLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Button buttonEnterProductKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label enterProductKeyLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}