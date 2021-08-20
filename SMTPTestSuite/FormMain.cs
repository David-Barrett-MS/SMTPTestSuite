/*
 * By David Barrett, Microsoft Ltd. 2018. Use at your own risk.  No warranties are given.
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
using System.Windows.Forms;
using Logging;

namespace SMTPTestSuite
{
    public partial class FormMain : Form
    {
        SmtpMock smtpMock = null;
        Logger _logger = null;
        Encoding.FormBase64 _formBase64 = null;
        string _messageLogFolder = "";

        public FormMain()
        {
            InitializeComponent();
            _logger = new Logger(false, "");
            _logger.LogListBox = listBoxLog;
            _logger.LogListBoxAutoScroll = true;
            smtpMock = new SmtpMock(_logger);
            smtpMock.Start();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            smtpMock.Stop();
            smtpMock = null;
            this.Dispose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            smtpMock.Stop();
            smtpMock = null;
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
                smtpMock.OpenRelay = !checkBoxRequireAuth.Checked;
            }
            catch { }
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            listBoxLog.Items.Clear();
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
            System.Windows.Forms.OpenFileDialog oDialog = new OpenFileDialog();
            try
            {
                oDialog.FileName = _logger.LogFile;
            }
            catch
            {
            }
            oDialog.Filter = "Text files (*.txt)|*.txt|Log files (*.log)|*.log|All files (*.*)|*.* ";
            DialogResult result = oDialog.ShowDialog(this);
            if (result != System.Windows.Forms.DialogResult.OK)
                return;
            _logger.LogFile = oDialog.FileName;
        }

        private void buttonChooseMessageFolder_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog oDialog = new FolderBrowserDialog();
            try
            {
                oDialog.SelectedPath = System.IO.Path.GetFullPath(_messageLogFolder);
            }
            catch
            {
                oDialog.SelectedPath = Application.StartupPath;
            }
            oDialog.Description = "Select folder to store messages";
            DialogResult result = oDialog.ShowDialog(this);
            if (result != System.Windows.Forms.DialogResult.OK)
                return;
            _messageLogFolder = oDialog.SelectedPath;
            textBoxReceivedMessagesFolder.Text = _messageLogFolder;
        }

        private void textBoxReceivedMessagesFolder_TextChanged(object sender, EventArgs e)
        {
            smtpMock.MessageStoreFolder = textBoxReceivedMessagesFolder.Text;
        }

        private void checkBoxRejectAllMessages_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRejectAllMessages.Checked)
            {
                smtpMock.RejectMessage = comboBoxRejectionCode.Text;
            }
            else
                smtpMock.RejectMessage = String.Empty;
        }

        private void comboBoxRejectionCode_TextUpdate(object sender, EventArgs e)
        {
            if (checkBoxRejectAllMessages.Checked)
                smtpMock.RejectMessage = comboBoxRejectionCode.Text;
        }
    }
}
