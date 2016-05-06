using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTEditData
{
    static class DateTimeConvert
    {
        private const char PADDING = '0';

        public static string ToRealTime(string time)
        {
            string formattedTime = time.PadRight(7, PADDING);

            int _time;
            if (!int.TryParse(formattedTime, out _time))
                return null;

            string hour = time.Substring(0, 2);
            string minute = time.Substring(2, 2);
            string second = time.Substring(4, 2);
            string millisecond = time.Substring(6, 1);

            return $"{hour}:{minute}:{second}.{millisecond}";
        }

        public static string ToRwdTime(string time)
        {
            string[] formattedTime = time.Split(':');
            string hour = formattedTime[0].PadLeft(2, PADDING);
            string minute = formattedTime[1].PadLeft(2, PADDING);
            string second = formattedTime[2].Replace(".", "").PadLeft(3, PADDING).Substring(0, 3);

            return $"{hour}{minute}{second}";
        }

        public static DateTime? RwdToReal(string date)
        {
            int x = 0;
            if (!int.TryParse(date, out x))
                return null;
            else if (date.Length != 8)
                return null;
            else
            {
                DateTime dt = DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                return dt;
            }
        }
    }
}
