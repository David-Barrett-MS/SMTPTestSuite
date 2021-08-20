/*
  DISCLAIMER:
THIS CODE IS SAMPLE CODE. THESE SAMPLES ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND.
MICROSOFT FURTHER DISCLAIMS ALL IMPLIED WARRANTIES INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OF MERCHANTABILITY OR OF FITNESS FOR
A PARTICULAR PURPOSE. THE ENTIRE RISK ARISING OUT OF THE USE OR PERFORMANCE OF THE SAMPLES REMAINS WITH YOU. IN NO EVENT SHALL
MICROSOFT OR ITS SUPPLIERS BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS,
BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR OTHER PECUNIARY LOSS) ARISING OUT OF THE USE OF OR INABILITY TO USE THE
SAMPLES, EVEN IF MICROSOFT HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. BECAUSE SOME STATES DO NOT ALLOW THE EXCLUSION OR LIMITATION
OF LIABILITY FOR CONSEQUENTIAL OR INCIDENTAL DAMAGES, THE ABOVE LIMITATION MAY NOT APPLY TO YOU.
 * */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Text;

namespace Logging
{
    public class Logger
    {
        bool bLogToConsole = false;                 // Are we logging to console?
        String sLogToFile = "";                     // Filename of logfile (or blank if not logging to file)
        StreamWriter oLogFile = null;
        public delegate void LogEventHandler
            (String NotificationEvent);             // Declare the delegate function to allow a log callback
        LogEventHandler EventCallback;              // The callback for logs (can be null)
        private ListBox oLogListBox = null;         // The listbox to log to (can be null)
        private bool bListBoxAutoScroll = false;    // Whether to autoscroll the listbox

        public Logger()
        {
            bLogToConsole = false;
            sLogToFile = "";
        }

        public Logger(bool LogToConsole, String LogToFile)
        {
            bLogToConsole = LogToConsole;
            sLogToFile = LogToFile;
            if (sLogToFile != "")
                oLogFile = File.AppendText(LogToFile);
        }

        ~Logger()
        {
            this.Close();
        }

        public string LogFile
        {
            get { return sLogToFile; }
            set
            {
                try
                {
                    oLogFile.Close();
                }
                catch { }
                oLogFile = null;
                sLogToFile = value;
                if (sLogToFile != "")
                    oLogFile = File.AppendText(sLogToFile);
            }
        }

        public LogEventHandler EventHandler
        {
            // Set the callback for the logging (this callback is sent the log details anytime something is logged)
            set { EventCallback = value; }
        }

        public ListBox LogListBox
        {
            // Get or set the listbox to which data is logged
            get { return oLogListBox; }
            set { oLogListBox = value; }
        }

        public bool LogListBoxAutoScroll
        {
            // Turn autoscroll on or off
            get { return bListBoxAutoScroll; }
            set { bListBoxAutoScroll = value; }
        }

        public void Log(String LogText, bool SectionBreakBefore = false, bool DoNotLogToFile = false)
        {
            // Add a log entry
            try
            {
                if (bLogToConsole)
                {
                    if (SectionBreakBefore)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("");
                    }
                    Console.WriteLine(LogText);
                }
            }
            catch
            {
            }

            try
            {
                if (!(EventCallback == null)) EventCallback(LogText);
            }
            catch { }

            try
            {
                if (!(oLogListBox == null))
                {
                    if (oLogListBox.InvokeRequired)
                    {
                        // We are running on a different thread (very likely as this is a callback function)
                        oLogListBox.Invoke(new MethodInvoker(delegate
                        {
                            oLogListBox.Items.Add(LogText);
                        }));
                        if (bListBoxAutoScroll)
                        {
                            oLogListBox.Invoke(new MethodInvoker(delegate
                            {
                                oLogListBox.SetSelected(oLogListBox.Items.Count - 1, true);
                                oLogListBox.Update();
                                oLogListBox.SetSelected(oLogListBox.Items.Count - 1, false);
                            }));
                        }
                        else
                            oLogListBox.Update();
                    }
                    else
                    {
                        oLogListBox.Items.Add(LogText);
                        if (bListBoxAutoScroll)
                        {
                            oLogListBox.SetSelected(oLogListBox.Items.Count - 1, true);
                            oLogListBox.Update();
                            oLogListBox.SetSelected(oLogListBox.Items.Count - 1, false);
                        }
                        else
                            oLogListBox.Update();
                    }
                }
            }
            catch { }

            if (DoNotLogToFile) return;

            try
            {
                if (oLogFile != null)
                {
                    if (SectionBreakBefore)
                    {
                        oLogFile.WriteLine();
                        oLogFile.WriteLine(new String('-', 80));
                        oLogFile.WriteLine();
                    }
                    oLogFile.WriteLine(LogText);
                    oLogFile.Flush();
                }
            }
            catch
            {
            }

        }

        public void Close()
        {
            // Tidy up the log-file, if we are using it
            try
            {
                oLogFile?.Flush();
                oLogFile?.Close();
            }
            catch
            {
                // We do not care about any errors here
            }
        }

    }
}
