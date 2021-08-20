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

namespace SMTPTestSuite
{
    public partial class FormSendRaw : Form
    {
        private ClassFormConfig _formConfig = null;
        private Logging.Logger _logger = null;

        public FormSendRaw()
        {
            InitializeComponent();
            _formConfig = new ClassFormConfig(this);
            _logger = new Logging.Logger();
            _logger.LogListBox = listBoxLog;
            _logger.LogListBoxAutoScroll = true;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            try
            {
                SMTPSessionOutgoing oSMTPSender = new SMTPSessionOutgoing(textBoxSMTPServer.Text, Convert.ToInt32(textBoxSMTPServerPort.Text),
                    _logger, checkBoxStartTLS.Checked, checkBoxAcceptAllCerts.Checked);
                oSMTPSender.SendMessage(textBoxMAILFROM.Text, textBoxRCPTTO.Text, textBoxDATA.Text);
            }
            catch { }
        }

        private void listBoxLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxLog.SelectedIndex < 0)
                return;

            try
            {
                MessageBox.Show(this, listBoxLog.SelectedItem.ToString(), "Log Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }
    }
}
