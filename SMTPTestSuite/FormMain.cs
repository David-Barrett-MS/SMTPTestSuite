/*
 * By David Barrett, Microsoft Ltd. 2018 - 2023. Use at your own risk.  No warranties are given.
 * 
 * DISCLAIMER:
 * THIS CODE IS SAMPLE CODE. THESE SAMPLES ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND.
 * MICROSOFT FURTHER DISCLAIMS ALL IMPLIED WARRANTIES INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OF MERCHANTABILITY OR OF FITNESS FOR
 * A PARTICULAR PURPOSE. THE ENTIRE RISK ARISING OUT OF THE USE OR PERFORMANCE OF THE SAMPLES REMAINS WITH YOU. IN NO EVENT SHALL
 * MICROSOFT OR ITS SUPPLIERS BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS,
 * BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR OTHER PECUNIARY LOSS) ARISING OUT OF THE USE OF OR INABILITY TO USE THE
 * SAMPLES, EVEN IF MICROSOFT HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. BECAUSE SOME STATES DO NOT ALLOW THE EXCLUSION OR LIMITATION
 * OF LIABILITY FOR CONSEQUENTIAL OR INCIDENTAL DAMAGES, THE ABOVE LIMITATION MAY NOT APPLY TO YOU.
 * */

using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SMTPTestSuite
{
    public partial class FormMain : Form
    {
        SmtpMock _smtpMock = null;
        Logger _logger = null;
        Encoding.FormBase64 _formBase64 = null;
        private ClassFormConfig _formConfig = null;

        public FormMain()
        {
            InitializeComponent();
            // Add our form configuration helper

            _logger = new Logger(false, "");
            _logger.LogListBox = listBoxLog;
            _logger.LogListBoxAutoScroll = true;
            _smtpMock = new SmtpMock(_logger);
            _smtpMock.Start();

            _formConfig = new ClassFormConfig(this, true, true);
            _formConfig.ExcludedControls.Add(listBoxLog);
            _formConfig.ExcludedControls.Add(textBoxLogEntry);
            _formConfig.Initialise();
            //listBoxLog.Focus();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            _smtpMock.Stop();
            _smtpMock = null;
            Close();
            Dispose();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _smtpMock?.Stop();
            _smtpMock = null;
        }

        private void buttonSMTPSend_Click(object sender, EventArgs e)
        {
            FormSendBulk oForm = new FormSendBulk(listBoxLog);
            oForm.Show(this);
        }

        private void checkBoxRequireAuth_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                _smtpMock.OpenRelay = !checkBoxRequireAuth.Checked;
            }
            catch { }
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            listBoxLog.Items.Clear();
            textBoxLogEntry.Clear();
        }

        private void buttonSendRaw_Click(object sender, EventArgs e)
        {
            FormSendRaw oForm = new FormSendRaw();
            oForm.Show(this);
        }

        private void buttonBase64_Click(object sender, EventArgs e)
        {
            if (_formBase64 == null)
            {
                _formBase64 = new Encoding.FormBase64();
                _formBase64.Show();
                return;
            }
            if (_formBase64.Visible) return;
            _formBase64.Show();
        }

        private void listBoxLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxLog.SelectedIndex < 0)
                textBoxLogEntry.Text = "";
            else if (listBoxLog.SelectedItem != null)
                textBoxLogEntry.Text = listBoxLog.SelectedItem.ToString();
        }

        private void buttonChooseLogFolder_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog oDialog = new SaveFileDialog();
            try
            {
                oDialog.FileName = textBoxLogFile.Text;
            }
            catch
            {
            }
            oDialog.Filter = "Log files (*.log)|*.log|Text files (*.txt)|*.txt|All files (*.*)|*.* ";
            DialogResult result = oDialog.ShowDialog(this);
            if (result != System.Windows.Forms.DialogResult.OK)
                return;
            
            textBoxLogFile.Text = oDialog.FileName;
        }

        private void buttonChooseMessageFolder_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog oDialog = new FolderBrowserDialog();
            try
            {
                if (!String.IsNullOrEmpty(textBoxReceivedMessagesFolder.Text))
                    oDialog.SelectedPath = System.IO.Path.GetFullPath(textBoxReceivedMessagesFolder.Text);
            }
            catch
            {
                oDialog.SelectedPath = Application.StartupPath;
            }
            oDialog.Description = "Select folder to store messages";
            DialogResult result = oDialog.ShowDialog(this);
            if (result != System.Windows.Forms.DialogResult.OK)
                return;
            textBoxReceivedMessagesFolder.Text = oDialog.SelectedPath;
        }

        private void textBoxReceivedMessagesFolder_TextChanged(object sender, EventArgs e)
        {
            _smtpMock.MessageStoreFolder = textBoxReceivedMessagesFolder.Text;
            //_formConfig.SaveConfig();
        }

        private void checkBoxRejectAllMessages_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRejectAllMessages.Checked)
            {
                _smtpMock.RejectMessage = comboBoxRejectionCode.Text;
            }
            else
                _smtpMock.RejectMessage = String.Empty;
        }

        private void comboBoxRejectionCode_TextUpdate(object sender, EventArgs e)
        {
            if (checkBoxRejectAllMessages.Checked)
                _smtpMock.RejectMessage = comboBoxRejectionCode.Text;
        }

        private void checkBoxEnableTLS_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnableTLS.Checked)
            {
                PopulateCertificates();
                if (comboBoxTLSCertificate.Items.Count < 1)
                    checkBoxEnableTLS.Checked = false;
                else if (comboBoxTLSCertificate.Items.Count == 1)
                    comboBoxTLSCertificate.SelectedIndex = 0;
            }
            else
                _smtpMock.TLSCertificate = null;
        }

        private void PopulateCertificates()
        {
            // Populate combobox with any valid certificates found in their store

            X509Store x509Store = null;
            comboBoxTLSCertificate.Items.Clear();
            try
            {
                x509Store = new X509Store("MY", StoreLocation.CurrentUser);
                x509Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            }
            catch { }

            if (x509Store == null)
                return;

            // Store opened ok, so now we read the certificates
            foreach (X509Certificate2 x509Cert in x509Store.Certificates)
            {
                try
                {
                    comboBoxTLSCertificate.Items.Add(x509Cert);
                }
                catch { }
            }

            x509Store.Close();
        }

        private void comboBoxTLSCertificate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTLSCertificate.SelectedIndex < 0)
                return;

            _smtpMock.TLSCertificate = (X509Certificate)comboBoxTLSCertificate.SelectedItem;
        }

        private void checkBoxValidateDomain_CheckedChanged(object sender, EventArgs e)
        {
            _smtpMock.AcceptedDomainList.Clear();
            if (checkBoxValidateDomain.Checked)
            {
                if (listBoxDomains.Items.Count > 0)
                    for (int i = 0; i < listBoxDomains.Items.Count; i++)
                        _smtpMock.AcceptedDomainList.Add(((string)listBoxDomains.Items[i]).ToLower());
                else
                    checkBoxValidateDomain.Checked = false;
            }
        }

        private void buttonAddDomain_Click(object sender, EventArgs e)
        {
            using (FormUserPrompt formUserPrompt = new FormUserPrompt())
            {
                string newDomain = formUserPrompt.PromptForInput("Enter domain to accept email for", "Add domain", this);
                if (String.IsNullOrEmpty(newDomain))
                    return;
                listBoxDomains.Items.Add(newDomain);
                checkBoxValidateDomain_CheckedChanged(this, null);
            }
        }

        private void buttonRemoveDomain_Click(object sender, EventArgs e)
        {
            if (listBoxDomains.SelectedIndex < 0)
                return;

            listBoxDomains.Items.RemoveAt(listBoxDomains.SelectedIndex);
            checkBoxValidateDomain_CheckedChanged(this, null);
        }


        private void textBoxLogFile_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxLogFile.Text))
                _logger.LogFile = textBoxLogFile.Text;
            //_formConfig.SaveConfig();
        }

        private void checkBoxEnableSpamhaus_CheckedChanged(object sender, EventArgs e)
        {
            _smtpMock.ConnectionValidator.CheckSpamhaus = checkBoxEnableSpamhaus.Checked;
            checkBoxBlockSpamhaus.Enabled = checkBoxEnableSpamhaus.Checked;
        }

        private void checkBoxFolderPerRecipient_CheckedChanged(object sender, EventArgs e)
        {
            _smtpMock.UseRecipientSubfolders = checkBoxFolderPerRecipient.Checked;
            //_formConfig.SaveConfig();
        }

        private void checkBoxBlockSpamhaus_CheckedChanged(object sender, EventArgs e)
        {
            _smtpMock.ConnectionValidator.BlockSpamhaus = checkBoxBlockSpamhaus.Checked;
        }

        private void listBoxLog_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                listBoxLog.SelectedIndex = -1;
        }
    }
}
