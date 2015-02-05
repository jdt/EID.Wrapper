namespace Tests
{
    partial class MainForm
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
            this.btGetTokenLabel = new System.Windows.Forms.Button();
            this.btGetKeysList = new System.Windows.Forms.Button();
            this.btGenerateKeyRSA = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbTokenPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.udTokenIndex = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbProviderDll = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btSignAndVerify = new System.Windows.Forms.Button();
            this.btGetSupportedMechanizms = new System.Windows.Forms.Button();
            this.btLAddCertificate = new System.Windows.Forms.Button();
            this.btDeleteGeneratedKey = new System.Windows.Forms.Button();
            this.btGenerateKeyGOST = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btCertificateVerify = new System.Windows.Forms.Button();
            this.btGetCertificateInfo = new System.Windows.Forms.Button();
            this.btPkcs7SignAndVerify = new System.Windows.Forms.Button();
            this.btIssueACertificateRequest = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTokenIndex)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btGetTokenLabel
            // 
            this.btGetTokenLabel.Location = new System.Drawing.Point(16, 130);
            this.btGetTokenLabel.Name = "btGetTokenLabel";
            this.btGetTokenLabel.Size = new System.Drawing.Size(193, 41);
            this.btGetTokenLabel.TabIndex = 0;
            this.btGetTokenLabel.Text = "Get token labels";
            this.btGetTokenLabel.UseVisualStyleBackColor = true;
            this.btGetTokenLabel.Click += new System.EventHandler(this.btGetTokenLabel_Click);
            // 
            // btGetKeysList
            // 
            this.btGetKeysList.Location = new System.Drawing.Point(16, 36);
            this.btGetKeysList.Name = "btGetKeysList";
            this.btGetKeysList.Size = new System.Drawing.Size(193, 41);
            this.btGetKeysList.TabIndex = 1;
            this.btGetKeysList.Text = "Get token objects";
            this.btGetKeysList.UseVisualStyleBackColor = true;
            this.btGetKeysList.Click += new System.EventHandler(this.btGetKeysList_Click);
            // 
            // btGenerateKeyRSA
            // 
            this.btGenerateKeyRSA.Location = new System.Drawing.Point(230, 36);
            this.btGenerateKeyRSA.Name = "btGenerateKeyRSA";
            this.btGenerateKeyRSA.Size = new System.Drawing.Size(193, 41);
            this.btGenerateKeyRSA.TabIndex = 1;
            this.btGenerateKeyRSA.Text = "Generate key (RSA)";
            this.btGenerateKeyRSA.UseVisualStyleBackColor = true;
            this.btGenerateKeyRSA.Click += new System.EventHandler(this.btGenerateKeyRSA_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbTokenPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.udTokenIndex);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbProviderDll);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 148);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // tbTokenPassword
            // 
            this.tbTokenPassword.Location = new System.Drawing.Point(166, 103);
            this.tbTokenPassword.Name = "tbTokenPassword";
            this.tbTokenPassword.Size = new System.Drawing.Size(254, 22);
            this.tbTokenPassword.TabIndex = 5;
            this.tbTokenPassword.Text = "12345678";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password for token";
            // 
            // udTokenIndex
            // 
            this.udTokenIndex.Location = new System.Drawing.Point(166, 69);
            this.udTokenIndex.Name = "udTokenIndex";
            this.udTokenIndex.Size = new System.Drawing.Size(49, 22);
            this.udTokenIndex.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Token index";
            // 
            // cbProviderDll
            // 
            this.cbProviderDll.FormattingEnabled = true;
            this.cbProviderDll.Items.AddRange(new object[] {
            "eTPKCS11g.dll",
            "opensc-pkcs11.dll",
            "etpkcs11.dll",
            "siecap11.dll",
            "rtPKCS11.dll"});
            this.cbProviderDll.Location = new System.Drawing.Point(166, 31);
            this.cbProviderDll.Name = "cbProviderDll";
            this.cbProviderDll.Size = new System.Drawing.Size(254, 24);
            this.cbProviderDll.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Provider DLL";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btSignAndVerify);
            this.groupBox2.Controls.Add(this.btGetSupportedMechanizms);
            this.groupBox2.Controls.Add(this.btLAddCertificate);
            this.groupBox2.Controls.Add(this.btDeleteGeneratedKey);
            this.groupBox2.Controls.Add(this.btGenerateKeyGOST);
            this.groupBox2.Controls.Add(this.btGenerateKeyRSA);
            this.groupBox2.Controls.Add(this.btGetKeysList);
            this.groupBox2.Controls.Add(this.btGetTokenLabel);
            this.groupBox2.Location = new System.Drawing.Point(12, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 280);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Actions";
            // 
            // btSignAndVerify
            // 
            this.btSignAndVerify.Location = new System.Drawing.Point(230, 224);
            this.btSignAndVerify.Name = "btSignAndVerify";
            this.btSignAndVerify.Size = new System.Drawing.Size(193, 41);
            this.btSignAndVerify.TabIndex = 5;
            this.btSignAndVerify.Text = "Sign and verify";
            this.btSignAndVerify.UseVisualStyleBackColor = true;
            this.btSignAndVerify.Click += new System.EventHandler(this.btSignAndVerify_Click);
            // 
            // btGetSupportedMechanizms
            // 
            this.btGetSupportedMechanizms.Location = new System.Drawing.Point(16, 83);
            this.btGetSupportedMechanizms.Name = "btGetSupportedMechanizms";
            this.btGetSupportedMechanizms.Size = new System.Drawing.Size(193, 41);
            this.btGetSupportedMechanizms.TabIndex = 4;
            this.btGetSupportedMechanizms.Text = "Get supported mechanisms";
            this.btGetSupportedMechanizms.UseVisualStyleBackColor = true;
            this.btGetSupportedMechanizms.Click += new System.EventHandler(this.btGetSupportedMechanizms_Click);
            // 
            // btLAddCertificate
            // 
            this.btLAddCertificate.Location = new System.Drawing.Point(230, 130);
            this.btLAddCertificate.Name = "btLAddCertificate";
            this.btLAddCertificate.Size = new System.Drawing.Size(193, 41);
            this.btLAddCertificate.TabIndex = 3;
            this.btLAddCertificate.Text = "Add Certificate";
            this.btLAddCertificate.UseVisualStyleBackColor = true;
            this.btLAddCertificate.Click += new System.EventHandler(this.btLAddCertificate_Click);
            // 
            // btDeleteGeneratedKey
            // 
            this.btDeleteGeneratedKey.Location = new System.Drawing.Point(230, 177);
            this.btDeleteGeneratedKey.Name = "btDeleteGeneratedKey";
            this.btDeleteGeneratedKey.Size = new System.Drawing.Size(193, 41);
            this.btDeleteGeneratedKey.TabIndex = 2;
            this.btDeleteGeneratedKey.Text = "Delete generated keys";
            this.btDeleteGeneratedKey.UseVisualStyleBackColor = true;
            this.btDeleteGeneratedKey.Click += new System.EventHandler(this.btDeleteGeneratedKey_Click);
            // 
            // btGenerateKeyGOST
            // 
            this.btGenerateKeyGOST.Location = new System.Drawing.Point(230, 83);
            this.btGenerateKeyGOST.Name = "btGenerateKeyGOST";
            this.btGenerateKeyGOST.Size = new System.Drawing.Size(193, 41);
            this.btGenerateKeyGOST.TabIndex = 1;
            this.btGenerateKeyGOST.Text = "Generate key (GOST)";
            this.btGenerateKeyGOST.UseVisualStyleBackColor = true;
            this.btGenerateKeyGOST.Click += new System.EventHandler(this.btGenerateKeyGOST_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btCertificateVerify);
            this.groupBox3.Controls.Add(this.btGetCertificateInfo);
            this.groupBox3.Controls.Add(this.btPkcs7SignAndVerify);
            this.groupBox3.Controls.Add(this.btIssueACertificateRequest);
            this.groupBox3.Location = new System.Drawing.Point(19, 455);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(434, 131);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EToken specific actinos";
            // 
            // btCertificateVerify
            // 
            this.btCertificateVerify.Location = new System.Drawing.Point(223, 79);
            this.btCertificateVerify.Name = "btCertificateVerify";
            this.btCertificateVerify.Size = new System.Drawing.Size(193, 41);
            this.btCertificateVerify.TabIndex = 9;
            this.btCertificateVerify.Text = "Verify certificate";
            this.btCertificateVerify.UseVisualStyleBackColor = true;
            this.btCertificateVerify.Click += new System.EventHandler(this.btCertificateVerify_Click);
            // 
            // btGetCertificateInfo
            // 
            this.btGetCertificateInfo.Location = new System.Drawing.Point(9, 79);
            this.btGetCertificateInfo.Name = "btGetCertificateInfo";
            this.btGetCertificateInfo.Size = new System.Drawing.Size(193, 41);
            this.btGetCertificateInfo.TabIndex = 8;
            this.btGetCertificateInfo.Text = "Get certificate info";
            this.btGetCertificateInfo.UseVisualStyleBackColor = true;
            this.btGetCertificateInfo.Click += new System.EventHandler(this.btGetCertificateInfo_Click);
            // 
            // btPkcs7SignAndVerify
            // 
            this.btPkcs7SignAndVerify.Location = new System.Drawing.Point(223, 32);
            this.btPkcs7SignAndVerify.Name = "btPkcs7SignAndVerify";
            this.btPkcs7SignAndVerify.Size = new System.Drawing.Size(193, 41);
            this.btPkcs7SignAndVerify.TabIndex = 7;
            this.btPkcs7SignAndVerify.Text = "pkcs7 Sign and verify";
            this.btPkcs7SignAndVerify.UseVisualStyleBackColor = true;
            this.btPkcs7SignAndVerify.Click += new System.EventHandler(this.btPkcs7SignAndVerify_Click);
            // 
            // btIssueACertificateRequest
            // 
            this.btIssueACertificateRequest.Location = new System.Drawing.Point(9, 32);
            this.btIssueACertificateRequest.Name = "btIssueACertificateRequest";
            this.btIssueACertificateRequest.Size = new System.Drawing.Size(193, 41);
            this.btIssueACertificateRequest.TabIndex = 6;
            this.btIssueACertificateRequest.Text = "Issue a certificate request";
            this.btIssueACertificateRequest.UseVisualStyleBackColor = true;
            this.btIssueACertificateRequest.Click += new System.EventHandler(this.btIssueACertificateRequest_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 598);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test of all major token functions";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTokenIndex)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btGetTokenLabel;
        private System.Windows.Forms.Button btGetKeysList;
        private System.Windows.Forms.Button btGenerateKeyRSA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbTokenPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown udTokenIndex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbProviderDll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btDeleteGeneratedKey;
        private System.Windows.Forms.Button btLAddCertificate;
        private System.Windows.Forms.Button btGenerateKeyGOST;
        private System.Windows.Forms.Button btGetSupportedMechanizms;
        private System.Windows.Forms.Button btSignAndVerify;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btIssueACertificateRequest;
        private System.Windows.Forms.Button btPkcs7SignAndVerify;
        private System.Windows.Forms.Button btGetCertificateInfo;
        private System.Windows.Forms.Button btCertificateVerify;
    }
}

