using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTEditData
{
    public static class ActivityLog
    {
        private static string _logPath = $"{Environment.CurrentDirectory}\\ActivityLog.txt";

        public static void Log(string message)
        {
            try
            {
                using (var sw = new StreamWriter(_logPath, true))
                {
                    sw.WriteLineAsync($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}||{Environment.MachineName}||{message}");
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler.Handle(ex, "Error writing to the activity log");
            }
        }
    }
}
