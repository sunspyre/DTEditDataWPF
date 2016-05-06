using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DTEditData
{
    static class ExceptionHandler
    {
        public static void Handle(Exception ex)
        {
            //General entry point for exceptions
            MessageBox.Show(ex.Message);
            //LogToFile(ex);
        }

        static void LogToFile(Exception ex)
        {

        }
    }
}
