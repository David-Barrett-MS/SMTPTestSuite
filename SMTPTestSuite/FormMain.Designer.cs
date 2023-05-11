namespace SMTPTestSuite
{
    partial class FormMain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxFolderPerRecipient = new System.Windows.Forms.CheckBox();
            this.buttonChooseMessageFolder = new System.Windows.Forms.Button();
            this.buttonChooseLogFolder = new System.Windows.Forms.Button();
            this.textBoxLogFile = new System.Windows.Forms.TextBox();
            this.textBoxReceivedMessagesFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRemoveDomain = new System.Windows.Forms.Button();
            this.buttonAddDomain = new System.Windows.Forms.Button();
            this.listBoxDomains = new System.Windows.Forms.ListBox();
            this.checkBoxValidateDomain = new System.Windows.Forms.CheckBox();
            this.comboBoxTLSCertificate = new System.Windows.Forms.ComboBox();
            this.checkBoxEnableTLS = new System.Windows.Forms.CheckBox();
            this.comboBoxRejectionCode = new System.Windows.Forms.ComboBox();
            this.checkBoxRejectAllMessages = new System.Windows.Forms.CheckBox();
            this.checkBoxRequireAuth = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableSpamhaus = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonBase64 = new System.Windows.Forms.Button();
            this.buttonSendRaw = new System.Windows.Forms.Button();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSMTPSend = new System.Windows.Forms.Button();
            this.textBoxLogEntry = new System.Windows.Forms.TextBox();
            this.checkBoxBlockSpamhaus = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxFolderPerRecipient);
            this.groupBox1.Controls.Add(this.buttonChooseMessageFolder);
            this.groupBox1.Controls.Add(this.buttonChooseLogFolder);
            this.groupBox1.Controls.Add(this.textBoxLogFile);
            this.groupBox1.Controls.Add(this.textBoxReceivedMessagesFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Folders";
            // 
            // checkBoxFolderPerRecipient
            // 
            this.checkBoxFolderPerRecipient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxFolderPerRecipient.AutoSize = true;
            this.checkBoxFolderPerRecipient.Checked = true;
            this.checkBoxFolderPerRecipient.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFolderPerRecipient.Location = new System.Drawing.Point(349, 75);
            this.checkBoxFolderPerRecipient.Name = "checkBoxFolderPerRecipient";
            this.checkBoxFolderPerRecipient.Size = new System.Drawing.Size(164, 17);
            this.checkBoxFolderPerRecipient.TabIndex = 8;
            this.checkBoxFolderPerRecipient.Text = "Create subfolder per recipient";
            this.checkBoxFolderPerRecipient.UseVisualStyleBackColor = true;
            this.checkBoxFolderPerRecipient.CheckedChanged += new System.EventHandler(this.checkBoxFolderPerRecipient_CheckedChanged);
            // 
            // buttonChooseMessageFolder
            // 
            this.buttonChooseMessageFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChooseMessageFolder.Image = global::SMTPTestSuite.Properties.Resources.OpenFolder;
            this.buttonChooseMessageFolder.Location = new System.Drawing.Point(484, 46);
            this.buttonChooseMessageFolder.Name = "buttonChooseMessageFolder";
            this.buttonChooseMessageFolder.Size = new System.Drawing.Size(29, 23);
            this.buttonChooseMessageFolder.TabIndex = 7;
            this.buttonChooseMessageFolder.UseVisualStyleBackColor = true;
            this.buttonChooseMessageFolder.Click += new System.EventHandler(this.buttonChooseMessageFolder_Click);
            // 
            // buttonChooseLogFolder
            // 
            this.buttonChooseLogFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChooseLogFolder.Image = global::SMTPTestSuite.Properties.Resources.OpenFolder;
            this.buttonChooseLogFolder.Location = new System.Drawing.Point(484, 21);
            this.buttonChooseLogFolder.Name = "buttonChooseLogFolder";
            this.buttonChooseLogFolder.Size = new System.Drawing.Size(29, 23);
            this.buttonChooseLogFolder.TabIndex = 6;
            this.buttonChooseLogFolder.UseVisualStyleBackColor = true;
            this.buttonChooseLogFolder.Click += new System.EventHandler(this.buttonChooseLogFolder_Click);
            // 
            // textBoxLogFile
            // 
            this.textBoxLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogFile.Enabled = false;
            this.textBoxLogFile.Location = new System.Drawing.Point(118, 23);
            this.textBoxLogFile.Name = "textBoxLogFile";
            this.textBoxLogFile.Size = new System.Drawing.Size(365, 20);
            this.textBoxLogFile.TabIndex = 21;
            this.textBoxLogFile.TextChanged += new System.EventHandler(this.textBoxLogFile_TextChanged);
            // 
            // textBoxReceivedMessagesFolder
            // 
            this.textBoxReceivedMessagesFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReceivedMessagesFolder.Enabled = false;
            this.textBoxReceivedMessagesFolder.HideSelection = false;
            this.textBoxReceivedMessagesFolder.Location = new System.Drawing.Point(118, 48);
            this.textBoxReceivedMessagesFolder.Name = "textBoxReceivedMessagesFolder";
            this.textBoxReceivedMessagesFolder.Size = new System.Drawing.Size(365, 20);
            this.textBoxReceivedMessagesFolder.TabIndex = 22;
            this.textBoxReceivedMessagesFolder.TextChanged += new System.EventHandler(this.textBoxReceivedMessagesFolder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Received messages:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Logging:";
            // 
            // listBoxLog
            // 
            this.listBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 127);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(519, 329);
            this.listBoxLog.TabIndex = 0;
            this.listBoxLog.Tag = "NoConfigSave";
            this.listBoxLog.SelectedIndexChanged += new System.EventHandler(this.listBoxLog_SelectedIndexChanged);
            this.listBoxLog.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxLog_MouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonRemoveDomain);
            this.groupBox2.Controls.Add(this.buttonAddDomain);
            this.groupBox2.Controls.Add(this.listBoxDomains);
            this.groupBox2.Controls.Add(this.checkBoxValidateDomain);
            this.groupBox2.Controls.Add(this.comboBoxTLSCertificate);
            this.groupBox2.Controls.Add(this.checkBoxEnableTLS);
            this.groupBox2.Controls.Add(this.comboBoxRejectionCode);
            this.groupBox2.Controls.Add(this.checkBoxRejectAllMessages);
            this.groupBox2.Controls.Add(this.checkBoxRequireAuth);
            this.groupBox2.Location = new System.Drawing.Point(545, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 386);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SMTP Options";
            // 
            // buttonRemoveDomain
            // 
            this.buttonRemoveDomain.Image = global::SMTPTestSuite.Properties.Resources.Remove;
            this.buttonRemoveDomain.Location = new System.Drawing.Point(151, 221);
            this.buttonRemoveDomain.Name = "buttonRemoveDomain";
            this.buttonRemoveDomain.Size = new System.Drawing.Size(22, 22);
            this.buttonRemoveDomain.TabIndex = 17;
            this.buttonRemoveDomain.UseVisualStyleBackColor = true;
            this.buttonRemoveDomain.Click += new System.EventHandler(this.buttonRemoveDomain_Click);
            // 
            // buttonAddDomain
            // 
            this.buttonAddDomain.Image = global::SMTPTestSuite.Properties.Resources.Add;
            this.buttonAddDomain.Location = new System.Drawing.Point(129, 221);
            this.buttonAddDomain.Name = "buttonAddDomain";
            this.buttonAddDomain.Size = new System.Drawing.Size(22, 22);
            this.buttonAddDomain.TabIndex = 16;
            this.buttonAddDomain.UseVisualStyleBackColor = true;
            this.buttonAddDomain.Click += new System.EventHandler(this.buttonAddDomain_Click);
            // 
            // listBoxDomains
            // 
            this.listBoxDomains.FormattingEnabled = true;
            this.listBoxDomains.Items.AddRange(new object[] {
            "emersmtp.com"});
            this.listBoxDomains.Location = new System.Drawing.Point(26, 165);
            this.listBoxDomains.Name = "listBoxDomains";
            this.listBoxDomains.Size = new System.Drawing.Size(146, 56);
            this.listBoxDomains.TabIndex = 15;
            // 
            // checkBoxValidateDomain
            // 
            this.checkBoxValidateDomain.AutoSize = true;
            this.checkBoxValidateDomain.Location = new System.Drawing.Point(6, 142);
            this.checkBoxValidateDomain.Name = "checkBoxValidateDomain";
            this.checkBoxValidateDomain.Size = new System.Drawing.Size(147, 17);
            this.checkBoxValidateDomain.TabIndex = 14;
            this.checkBoxValidateDomain.Text = "Validate receiving domain";
            this.checkBoxValidateDomain.UseVisualStyleBackColor = true;
            this.checkBoxValidateDomain.CheckedChanged += new System.EventHandler(this.checkBoxValidateDomain_CheckedChanged);
            // 
            // comboBoxTLSCertificate
            // 
            this.comboBoxTLSCertificate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTLSCertificate.FormattingEnabled = true;
            this.comboBoxTLSCertificate.Location = new System.Drawing.Point(26, 115);
            this.comboBoxTLSCertificate.Name = "comboBoxTLSCertificate";
            this.comboBoxTLSCertificate.Size = new System.Drawing.Size(146, 21);
            this.comboBoxTLSCertificate.TabIndex = 13;
            this.comboBoxTLSCertificate.SelectedIndexChanged += new System.EventHandler(this.comboBoxTLSCertificate_SelectedIndexChanged);
            // 
            // checkBoxEnableTLS
            // 
            this.checkBoxEnableTLS.AutoSize = true;
            this.checkBoxEnableTLS.Location = new System.Drawing.Point(6, 92);
            this.checkBoxEnableTLS.Name = "checkBoxEnableTLS";
            this.checkBoxEnableTLS.Size = new System.Drawing.Size(118, 17);
            this.checkBoxEnableTLS.TabIndex = 12;
            this.checkBoxEnableTLS.Text = "Enable STARTTLS";
            this.checkBoxEnableTLS.UseVisualStyleBackColor = true;
            this.checkBoxEnableTLS.CheckedChanged += new System.EventHandler(this.checkBoxEnableTLS_CheckedChanged);
            // 
            // comboBoxRejectionCode
            // 
            this.comboBoxRejectionCode.FormattingEnabled = true;
            this.comboBoxRejectionCode.Items.AddRange(new object[] {
            "510 Bad address",
            "511 Bad address",
            "523 Size exceeded",
            "550 Recipient does not exist",
            "552 Target mailbox full",
            "554 Permanent failure"});
            this.comboBoxRejectionCode.Location = new System.Drawing.Point(26, 65);
            this.comboBoxRejectionCode.Name = "comboBoxRejectionCode";
            this.comboBoxRejectionCode.Size = new System.Drawing.Size(146, 21);
            this.comboBoxRejectionCode.TabIndex = 11;
            this.comboBoxRejectionCode.Text = "554 Permanent failure";
            this.comboBoxRejectionCode.TextUpdate += new System.EventHandler(this.comboBoxRejectionCode_TextUpdate);
            // 
            // checkBoxRejectAllMessages
            // 
            this.checkBoxRejectAllMessages.AutoSize = true;
            this.checkBoxRejectAllMessages.Location = new System.Drawing.Point(6, 42);
            this.checkBoxRejectAllMessages.Name = "checkBoxRejectAllMessages";
            this.checkBoxRejectAllMessages.Size = new System.Drawing.Size(145, 17);
            this.checkBoxRejectAllMessages.TabIndex = 10;
            this.checkBoxRejectAllMessages.Text = "Reject all messages with:";
            this.checkBoxRejectAllMessages.UseVisualStyleBackColor = true;
            this.checkBoxRejectAllMessages.CheckedChanged += new System.EventHandler(this.checkBoxRejectAllMessages_CheckedChanged);
            // 
            // checkBoxRequireAuth
            // 
            this.checkBoxRequireAuth.AutoSize = true;
            this.checkBoxRequireAuth.Location = new System.Drawing.Point(6, 19);
            this.checkBoxRequireAuth.Name = "checkBoxRequireAuth";
            this.checkBoxRequireAuth.Size = new System.Drawing.Size(88, 17);
            this.checkBoxRequireAuth.TabIndex = 9;
            this.checkBoxRequireAuth.Text = "Require Auth";
            this.checkBoxRequireAuth.UseVisualStyleBackColor = true;
            this.checkBoxRequireAuth.CheckedChanged += new System.EventHandler(this.checkBoxRequireAuth_CheckedChanged);
            // 
            // checkBoxEnableSpamhaus
            // 
            this.checkBoxEnableSpamhaus.AutoSize = true;
            this.checkBoxEnableSpamhaus.Location = new System.Drawing.Point(6, 19);
            this.checkBoxEnableSpamhaus.Name = "checkBoxEnableSpamhaus";
            this.checkBoxEnableSpamhaus.Size = new System.Drawing.Size(98, 17);
            this.checkBoxEnableSpamhaus.TabIndex = 18;
            this.checkBoxEnableSpamhaus.Text = "Check blocklist";
            this.checkBoxEnableSpamhaus.UseVisualStyleBackColor = true;
            this.checkBoxEnableSpamhaus.CheckedChanged += new System.EventHandler(this.checkBoxEnableSpamhaus_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.buttonBase64);
            this.panel1.Controls.Add(this.buttonSendRaw);
            this.panel1.Controls.Add(this.buttonClearLog);
            this.panel1.Controls.Add(this.buttonExit);
            this.panel1.Controls.Add(this.buttonSMTPSend);
            this.panel1.Location = new System.Drawing.Point(0, 489);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 37);
            this.panel1.TabIndex = 7;
            // 
            // buttonBase64
            // 
            this.buttonBase64.Location = new System.Drawing.Point(190, 6);
            this.buttonBase64.Name = "buttonBase64";
            this.buttonBase64.Size = new System.Drawing.Size(75, 23);
            this.buttonBase64.TabIndex = 3;
            this.buttonBase64.Text = "Base64...";
            this.buttonBase64.UseVisualStyleBackColor = true;
            this.buttonBase64.Click += new System.EventHandler(this.buttonBase64_Click);
            // 
            // buttonSendRaw
            // 
            this.buttonSendRaw.Location = new System.Drawing.Point(101, 6);
            this.buttonSendRaw.Name = "buttonSendRaw";
            this.buttonSendRaw.Size = new System.Drawing.Size(83, 23);
            this.buttonSendRaw.TabIndex = 2;
            this.buttonSendRaw.Text = "Send Raw...";
            this.buttonSendRaw.UseVisualStyleBackColor = true;
            this.buttonSendRaw.Click += new System.EventHandler(this.buttonSendRaw_Click);
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearLog.Location = new System.Drawing.Point(456, 6);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(75, 23);
            this.buttonClearLog.TabIndex = 4;
            this.buttonClearLog.Text = "Clear log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.Location = new System.Drawing.Point(648, 7);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 5;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonSMTPSend
            // 
            this.buttonSMTPSend.Location = new System.Drawing.Point(12, 6);
            this.buttonSMTPSend.Name = "buttonSMTPSend";
            this.buttonSMTPSend.Size = new System.Drawing.Size(83, 23);
            this.buttonSMTPSend.TabIndex = 1;
            this.buttonSMTPSend.Text = "Send Bulk...";
            this.buttonSMTPSend.UseVisualStyleBackColor = true;
            this.buttonSMTPSend.Click += new System.EventHandler(this.buttonSMTPSend_Click);
            // 
            // textBoxLogEntry
            // 
            this.textBoxLogEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogEntry.Location = new System.Drawing.Point(12, 458);
            this.textBoxLogEntry.Name = "textBoxLogEntry";
            this.textBoxLogEntry.Size = new System.Drawing.Size(519, 20);
            this.textBoxLogEntry.TabIndex = 20;
            this.textBoxLogEntry.Tag = "NoConfigSave";
            // 
            // checkBoxBlockSpamhaus
            // 
            this.checkBoxBlockSpamhaus.AutoSize = true;
            this.checkBoxBlockSpamhaus.Enabled = false;
            this.checkBoxBlockSpamhaus.Location = new System.Drawing.Point(6, 42);
            this.checkBoxBlockSpamhaus.Name = "checkBoxBlockSpamhaus";
            this.checkBoxBlockSpamhaus.Size = new System.Drawing.Size(98, 17);
            this.checkBoxBlockSpamhaus.TabIndex = 19;
            this.checkBoxBlockSpamhaus.Text = "Block listed IPs";
            this.checkBoxBlockSpamhaus.UseVisualStyleBackColor = true;
            this.checkBoxBlockSpamhaus.CheckedChanged += new System.EventHandler(this.checkBoxBlockSpamhaus_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.checkBoxBlockSpamhaus);
            this.groupBox3.Controls.Add(this.checkBoxEnableSpamhaus);
            this.groupBox3.Location = new System.Drawing.Point(545, 411);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(178, 67);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Spamhaus";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 526);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.textBoxLogEntry);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormMain";
            this.Text = "SMTP Test Suite";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonChooseMessageFolder;
        private System.Windows.Forms.Button buttonChooseLogFolder;
        private System.Windows.Forms.TextBox textBoxLogFile;
        private System.Windows.Forms.TextBox textBoxReceivedMessagesFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxRequireAuth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonBase64;
        private System.Windows.Forms.Button buttonSendRaw;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonSMTPSend;
        private System.Windows.Forms.TextBox textBoxLogEntry;
        private System.Windows.Forms.ComboBox comboBoxRejectionCode;
        private System.Windows.Forms.CheckBox checkBoxRejectAllMessages;
        private System.Windows.Forms.CheckBox checkBoxFolderPerRecipient;
        private System.Windows.Forms.ComboBox comboBoxTLSCertificate;
        private System.Windows.Forms.CheckBox checkBoxEnableTLS;
        private System.Windows.Forms.ListBox listBoxDomains;
        private System.Windows.Forms.CheckBox checkBoxValidateDomain;
        private System.Windows.Forms.Button buttonAddDomain;
        private System.Windows.Forms.Button buttonRemoveDomain;
        private System.Windows.Forms.CheckBox checkBoxEnableSpamhaus;
        private System.Windows.Forms.CheckBox checkBoxBlockSpamhaus;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

