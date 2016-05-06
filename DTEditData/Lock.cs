using DataTrack.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTEditData
{
    static class Lock
    {
        public static void Create(string date)
        {
            if (!IsLocked(date))
            {
                using (var sw = new StreamWriter($@"{Environment.CurrentDirectory}\{Folder.Data}\{Path.GetFileNameWithoutExtension(date)}.lck"))
                {
                    sw.WriteLine(Environment.MachineName);

                    sw.WriteLine($"User name: {Environment.UserName}");
                    sw.WriteLine($"Date of Lock: {DateTime.Now}");
                }
            }
        }

        public static void Remove(string date)
        {
            if (File.Exists($@"{Environment.CurrentDirectory}\{Folder.Data}\{Path.GetFileNameWithoutExtension(date)}.lck"))
            {
                File.Delete($@"{Environment.CurrentDirectory}\{Folder.Data}\{Path.GetFileNameWithoutExtension(date)}.lck");
            }
        }
        public static bool IsLocked(string date)
        {
            if (File.Exists($@"{Environment.CurrentDirectory}\{Folder.Data}\{Path.GetFileNameWithoutExtension(date)}.lck"))
            {
                if (GetLockUserName(date) != Environment.MachineName)
                    return true;
            }
            return false;
        }

        public static string GetLockUserName(string date)
        {
            string username;
            using (var sr = new StreamReader($@"{Environment.CurrentDirectory}\{Folder.Data}\{Path.GetFileNameWithoutExtension(date)}.lck"))
            {
                username = sr.ReadLine();
            }
            return username;
        }

        public static void RemoveUserLocks()
        {
            DirectoryInfo di = new DirectoryInfo($@"{Environment.CurrentDirectory}\{Folder.Data}");
            FileInfo[] files = di.GetFiles("*.lck");
            foreach (var item in files)
            {
                string file;
                using (var sr = new StreamReader(item.FullName))
                {
                    file = sr.ReadLine();
                }
                if (file == Environment.MachineName)
                {
                    File.Delete(item.FullName);
                }

            }
        }
    }
}
