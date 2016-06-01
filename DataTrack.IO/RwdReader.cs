using DataTrack.IO.Structs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DataTrack.IO
{
    public class RwdReader : IReader
    {
        string _path;
        
        List<DataTrackFile> _fileList;
        List<Record> _recordList;

        public RwdReader(string path)
        {
            _path = path;
        }

        /// <summary>
        /// returns all .RWD files from a given folder
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataTrackFile> GetFolderContents()
        {
            _fileList = new List<DataTrackFile>();

            DirectoryInfo di = new DirectoryInfo($"{_path}\\{Folder.Data}");
            FileInfo[] files = di.GetFiles("*.rwd");

            foreach (var item in files)
            {
                _fileList.Add(new DataTrackFile
                {
                    FileName = item.Name,
                    Path = item.FullName,
                    LastAccessed = item.LastAccessTime
                });
            }
            return _fileList;
        }

        /// <summary>
        /// Returns all records from a given .RWD file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public IEnumerable<Record> GetFileContents(string fileName)
        {
            _recordList = new List<Record>();
            string[] contents;
            using (var sr = new StreamReader(fileName))
            {
                contents = sr.ReadToEnd().Replace("\r","").Split('\n');
            }
            foreach (string item in contents)
            {
                if (string.IsNullOrEmpty(item)) continue;
                else if (item.Length < 31) continue; //RWD has 42 total characters (31 without button ID)
                else if (item.Split(' ').Length != 4) continue; //RWD has 4 segments

                string[] line = item.Split(' ');
                string time = line[0];
                string probeType = line[1];
                string probeId = line[2];
                string button = line[3].Substring(0,12);
                //string flag = "";

                _recordList.Add(new Scan
                {
                    Button = button,
                    Time = DateTime.ParseExact(time, "yyyyMMddHHmmsst", CultureInfo.InvariantCulture),
                    Device = probeType,
                    Probe = probeId,
                    Type = RecordType.Scan
                });
            }

            return _recordList;
        }
    }
}
