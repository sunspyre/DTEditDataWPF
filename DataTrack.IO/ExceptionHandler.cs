using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace DTEditData
{
    public static class ExceptionHandler
    {
        public static void Handle(Exception ex, string extraMessage = "")
        {
            //General entry point for exceptions
            //MessageBox.Show(ex.Message);
            LogToFile(ex, extraMessage);
        }

        private static void LogToFile(Exception ex, string extraMessage)
        {
            try
            {
                using (var sw = new StreamWriter("log.txt", true))
                {
                    sw.WriteLine($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} {ex.Message} {extraMessage}");
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show($"There was an error writing to the error log file.{Environment.NewLine}{exc.Message}");
            }
        }
    }
}
