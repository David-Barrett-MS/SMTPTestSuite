using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SMTPTestSuite.Encoding
{
    public partial class FormBase64 : Form
    {
        public FormBase64()
        {
            InitializeComponent();
        }

        private void buttonEncodeText_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] SourceText = System.Text.Encoding.UTF8.GetBytes(textBoxText.Text);
                textBoxBase64.Text = System.Convert.ToBase64String(SourceText);
            }
            catch (Exception ex)
            {
                textBoxBase64.Text = "Error occurred: " + ex.Message;
            }
        }

        private void buttonDecodeToText_Click(object sender, EventArgs e)
        {
            byte[] SourceEncoding=null;
            try
            {
                SourceEncoding = System.Convert.FromBase64String(textBoxBase64.Text);
            }
            catch (Exception ex)
            {
                textBoxText.Text = "Error occurred: " + ex.Message;
                return;
            }
            try
            {
                textBoxText.Text = System.Text.Encoding.UTF8.GetString(SourceEncoding);
            }
            catch (Exception ex)
            {
                textBoxText.Text = "Error occurred: " + ex.Message;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonDecodeToFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog oSaveDialog = new SaveFileDialog();
                oSaveDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
                oSaveDialog.FilterIndex = 2;
                oSaveDialog.RestoreDirectory = true;

                if (oSaveDialog.ShowDialog() != DialogResult.OK)
                    return;

                Stream oStream = oSaveDialog.OpenFile();
                if (!(oStream == null))
                {
                    byte[] Data = System.Convert.FromBase64String(textBoxBase64.Text);
                    oStream.Write(Data, 0, Data.Length);
                    oStream.Close();
                }
                oSaveDialog.Dispose();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error occurred: " + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void buttonEncodeFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog oOpenDialog = new OpenFileDialog();
                oOpenDialog.Filter = "All files (*.*)|*.*";
                oOpenDialog.FilterIndex = 1;
                oOpenDialog.RestoreDirectory = true;

                if (oOpenDialog.ShowDialog() != DialogResult.OK)
                    return;

                textBoxBase64.Text = "";
                this.Refresh();
                Stream oStream = oOpenDialog.OpenFile();
                byte[] data=new byte[oStream.Length];
                oStream.Read(data, 0, Convert.ToInt32(oStream.Length));
                oStream.Close();
                textBoxBase64.Text = Convert.ToBase64String(data);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error occurred: " + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }
    }
}
