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
            this.buttonChooseMessageFolder = new System.Windows.Forms.Button();
            this.buttonChooseLogFolder = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBoxReceivedMessagesFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxRequireAuth = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonBase64 = new System.Windows.Forms.Button();
            this.buttonSendRaw = new System.Windows.Forms.Button();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSMTPSend = new System.Windows.Forms.Button();
            this.textBoxLogEntry = new System.Windows.Forms.TextBox();
            this.checkBoxRejectAllMessages = new System.Windows.Forms.CheckBox();
            this.comboBoxRejectionCode = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonChooseMessageFolder);
            this.groupBox1.Controls.Add(this.buttonChooseLogFolder);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBoxReceivedMessagesFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Folders";
            // 
            // buttonChooseMessageFolder
            // 
            this.buttonChooseMessageFolder.Location = new System.Drawing.Point(484, 46);
            this.buttonChooseMessageFolder.Name = "buttonChooseMessageFolder";
            this.buttonChooseMessageFolder.Size = new System.Drawing.Size(29, 23);
            this.buttonChooseMessageFolder.TabIndex = 5;
            this.buttonChooseMessageFolder.Text = "...";
            this.buttonChooseMessageFolder.UseVisualStyleBackColor = true;
            this.buttonChooseMessageFolder.Click += new System.EventHandler(this.buttonChooseMessageFolder_Click);
            // 
            // buttonChooseLogFolder
            // 
            this.buttonChooseLogFolder.Location = new System.Drawing.Point(484, 21);
            this.buttonChooseLogFolder.Name = "buttonChooseLogFolder";
            this.buttonChooseLogFolder.Size = new System.Drawing.Size(29, 23);
            this.buttonChooseLogFolder.TabIndex = 4;
            this.buttonChooseLogFolder.Text = "...";
            this.buttonChooseLogFolder.UseVisualStyleBackColor = true;
            this.buttonChooseLogFolder.Click += new System.EventHandler(this.buttonChooseLogFolder_Click);
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(118, 23);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(365, 20);
            this.textBox2.TabIndex = 3;
            // 
            // textBoxReceivedMessagesFolder
            // 
            this.textBoxReceivedMessagesFolder.Enabled = false;
            this.textBoxReceivedMessagesFolder.HideSelection = false;
            this.textBoxReceivedMessagesFolder.Location = new System.Drawing.Point(118, 48);
            this.textBoxReceivedMessagesFolder.Name = "textBoxReceivedMessagesFolder";
            this.textBoxReceivedMessagesFolder.Size = new System.Drawing.Size(365, 20);
            this.textBoxReceivedMessagesFolder.TabIndex = 2;
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
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 127);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(519, 329);
            this.listBoxLog.TabIndex = 1;
            this.listBoxLog.Tag = "NoConfigSave";
            this.listBoxLog.SelectedIndexChanged += new System.EventHandler(this.listBoxLog_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxRejectionCode);
            this.groupBox2.Controls.Add(this.checkBoxRejectAllMessages);
            this.groupBox2.Controls.Add(this.checkBoxRequireAuth);
            this.groupBox2.Location = new System.Drawing.Point(545, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 438);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SMTP Options";
            // 
            // checkBoxRequireAuth
            // 
            this.checkBoxRequireAuth.AutoSize = true;
            this.checkBoxRequireAuth.Location = new System.Drawing.Point(6, 19);
            this.checkBoxRequireAuth.Name = "checkBoxRequireAuth";
            this.checkBoxRequireAuth.Size = new System.Drawing.Size(88, 17);
            this.checkBoxRequireAuth.TabIndex = 0;
            this.checkBoxRequireAuth.Text = "Require Auth";
            this.checkBoxRequireAuth.UseVisualStyleBackColor = true;
            this.checkBoxRequireAuth.CheckedChanged += new System.EventHandler(this.checkBoxRequireAuth_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonBase64);
            this.panel1.Controls.Add(this.buttonSendRaw);
            this.panel1.Controls.Add(this.buttonClearLog);
            this.panel1.Controls.Add(this.buttonExit);
            this.panel1.Controls.Add(this.buttonSMTPSend);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
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
            this.buttonBase64.TabIndex = 11;
            this.buttonBase64.Text = "Base64...";
            this.buttonBase64.UseVisualStyleBackColor = true;
            this.buttonBase64.Click += new System.EventHandler(this.buttonBase64_Click);
            // 
            // buttonSendRaw
            // 
            this.buttonSendRaw.Location = new System.Drawing.Point(101, 6);
            this.buttonSendRaw.Name = "buttonSendRaw";
            this.buttonSendRaw.Size = new System.Drawing.Size(83, 23);
            this.buttonSendRaw.TabIndex = 10;
            this.buttonSendRaw.Text = "Send Raw...";
            this.buttonSendRaw.UseVisualStyleBackColor = true;
            this.buttonSendRaw.Click += new System.EventHandler(this.buttonSendRaw_Click);
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(456, 6);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(75, 23);
            this.buttonClearLog.TabIndex = 9;
            this.buttonClearLog.Text = "Clear log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(648, 7);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 8;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonSMTPSend
            // 
            this.buttonSMTPSend.Location = new System.Drawing.Point(12, 6);
            this.buttonSMTPSend.Name = "buttonSMTPSend";
            this.buttonSMTPSend.Size = new System.Drawing.Size(83, 23);
            this.buttonSMTPSend.TabIndex = 7;
            this.buttonSMTPSend.Text = "Send Bulk...";
            this.buttonSMTPSend.UseVisualStyleBackColor = true;
            this.buttonSMTPSend.Click += new System.EventHandler(this.buttonSMTPSend_Click);
            // 
            // textBoxLogEntry
            // 
            this.textBoxLogEntry.Location = new System.Drawing.Point(12, 458);
            this.textBoxLogEntry.Name = "textBoxLogEntry";
            this.textBoxLogEntry.Size = new System.Drawing.Size(519, 20);
            this.textBoxLogEntry.TabIndex = 8;
            this.textBoxLogEntry.Tag = "NoConfigSave";
            // 
            // checkBoxRejectAllMessages
            // 
            this.checkBoxRejectAllMessages.AutoSize = true;
            this.checkBoxRejectAllMessages.Location = new System.Drawing.Point(6, 42);
            this.checkBoxRejectAllMessages.Name = "checkBoxRejectAllMessages";
            this.checkBoxRejectAllMessages.Size = new System.Drawing.Size(145, 17);
            this.checkBoxRejectAllMessages.TabIndex = 1;
            this.checkBoxRejectAllMessages.Text = "Reject all messages with:";
            this.checkBoxRejectAllMessages.UseVisualStyleBackColor = true;
            this.checkBoxRejectAllMessages.CheckedChanged += new System.EventHandler(this.checkBoxRejectAllMessages_CheckedChanged);
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
            this.comboBoxRejectionCode.TabIndex = 2;
            this.comboBoxRejectionCode.Text = "554 Permanent failure";
            this.comboBoxRejectionCode.TextUpdate += new System.EventHandler(this.comboBoxRejectionCode_TextUpdate);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 526);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxLogEntry);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormMain";
            this.Text = "SMTP Test Suite";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonChooseMessageFolder;
        private System.Windows.Forms.Button buttonChooseLogFolder;
        private System.Windows.Forms.TextBox textBox2;
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
    }
}

