using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTEditData
{
    static class Trim
    {
        public static string MirrorText(string value, bool option)
        {
            if (option)
                return value.Replace("(M) ", "");
            else
                return value;
        }
    }
}
