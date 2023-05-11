namespace SMTPTestSuite
{
    partial class FormSendRaw
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRCPTTO = new System.Windows.Forms.TextBox();
            this.textBoxMAILFROM = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonUpdateMessageId = new System.Windows.Forms.Button();
            this.textBoxDATA = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSMTPServerPort = new System.Windows.Forms.TextBox();
            this.textBoxSMTPServer = new System.Windows.Forms.TextBox();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.checkBoxStartTLS = new System.Windows.Forms.CheckBox();
            this.checkBoxAcceptAllCerts = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxRCPTTO);
            this.groupBox1.Controls.Add(this.textBoxMAILFROM);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SMTP Envelope";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "RCPT TO:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "MAIL FROM:";
            // 
            // textBoxRCPTTO
            // 
            this.textBoxRCPTTO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRCPTTO.Location = new System.Drawing.Point(81, 45);
            this.textBoxRCPTTO.Name = "textBoxRCPTTO";
            this.textBoxRCPTTO.Size = new System.Drawing.Size(386, 20);
            this.textBoxRCPTTO.TabIndex = 1;
            this.textBoxRCPTTO.Text = "administrator@exch2k10.local";
            // 
            // textBoxMAILFROM
            // 
            this.textBoxMAILFROM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMAILFROM.Location = new System.Drawing.Point(81, 19);
            this.textBoxMAILFROM.Name = "textBoxMAILFROM";
            this.textBoxMAILFROM.Size = new System.Drawing.Size(386, 20);
            this.textBoxMAILFROM.TabIndex = 0;
            this.textBoxMAILFROM.Text = "dave@cedit.biz";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonUpdateMessageId);
            this.groupBox2.Controls.Add(this.textBoxDATA);
            this.groupBox2.Location = new System.Drawing.Point(12, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 227);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DATA (message, including headers, excluding <CRLF>.<CRLF>)";
            // 
            // buttonUpdateMessageId
            // 
            this.buttonUpdateMessageId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateMessageId.Location = new System.Drawing.Point(352, 198);
            this.buttonUpdateMessageId.Name = "buttonUpdateMessageId";
            this.buttonUpdateMessageId.Size = new System.Drawing.Size(114, 23);
            this.buttonUpdateMessageId.TabIndex = 8;
            this.buttonUpdateMessageId.Text = "Update message Id";
            this.buttonUpdateMessageId.UseVisualStyleBackColor = true;
            this.buttonUpdateMessageId.Click += new System.EventHandler(this.buttonUpdateMessageId_Click);
            // 
            // textBoxDATA
            // 
            this.textBoxDATA.AcceptsReturn = true;
            this.textBoxDATA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDATA.Location = new System.Drawing.Point(3, 16);
            this.textBoxDATA.Multiline = true;
            this.textBoxDATA.Name = "textBoxDATA";
            this.textBoxDATA.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDATA.Size = new System.Drawing.Size(463, 176);
            this.textBoxDATA.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.textBoxSMTPServerPort);
            this.groupBox3.Controls.Add(this.textBoxSMTPServer);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(473, 50);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SMTP Server";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(332, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Address:";
            // 
            // textBoxSMTPServerPort
            // 
            this.textBoxSMTPServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSMTPServerPort.Location = new System.Drawing.Point(367, 19);
            this.textBoxSMTPServerPort.Name = "textBoxSMTPServerPort";
            this.textBoxSMTPServerPort.Size = new System.Drawing.Size(100, 20);
            this.textBoxSMTPServerPort.TabIndex = 1;
            this.textBoxSMTPServerPort.Text = "25";
            // 
            // textBoxSMTPServer
            // 
            this.textBoxSMTPServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSMTPServer.Location = new System.Drawing.Point(60, 19);
            this.textBoxSMTPServer.Name = "textBoxSMTPServer";
            this.textBoxSMTPServer.Size = new System.Drawing.Size(266, 20);
            this.textBoxSMTPServer.TabIndex = 0;
            this.textBoxSMTPServer.Text = "ex1.exch2k10.local";
            // 
            // listBoxLog
            // 
            this.listBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 384);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(553, 160);
            this.listBoxLog.TabIndex = 3;
            this.listBoxLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxLog_MouseDoubleClick);
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(490, 355);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 4;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(491, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // checkBoxStartTLS
            // 
            this.checkBoxStartTLS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxStartTLS.AutoSize = true;
            this.checkBoxStartTLS.Location = new System.Drawing.Point(491, 332);
            this.checkBoxStartTLS.Name = "checkBoxStartTLS";
            this.checkBoxStartTLS.Size = new System.Drawing.Size(82, 17);
            this.checkBoxStartTLS.TabIndex = 6;
            this.checkBoxStartTLS.Text = "STARTTLS";
            this.checkBoxStartTLS.UseVisualStyleBackColor = true;
            // 
            // checkBoxAcceptAllCerts
            // 
            this.checkBoxAcceptAllCerts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAcceptAllCerts.AutoSize = true;
            this.checkBoxAcceptAllCerts.Location = new System.Drawing.Point(491, 311);
            this.checkBoxAcceptAllCerts.Name = "checkBoxAcceptAllCerts";
            this.checkBoxAcceptAllCerts.Size = new System.Drawing.Size(73, 17);
            this.checkBoxAcceptAllCerts.TabIndex = 7;
            this.checkBoxAcceptAllCerts.Text = "Accept all";
            this.checkBoxAcceptAllCerts.UseVisualStyleBackColor = true;
            // 
            // FormSendRaw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 555);
            this.Controls.Add(this.checkBoxAcceptAllCerts);
            this.Controls.Add(this.checkBoxStartTLS);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(594, 430);
            this.Name = "FormSendRaw";
            this.Text = "Send SMTP data";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRCPTTO;
        private System.Windows.Forms.TextBox textBoxMAILFROM;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxDATA;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSMTPServerPort;
        private System.Windows.Forms.TextBox textBoxSMTPServer;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox checkBoxStartTLS;
        private System.Windows.Forms.CheckBox checkBoxAcceptAllCerts;
        private System.Windows.Forms.Button buttonUpdateMessageId;
    }
}