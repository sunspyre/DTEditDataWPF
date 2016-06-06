using DataTrack.IO.Structs;
using DTEditData;
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
            try
            {
                _recordList = new List<Record>();
                string[] contents;
                long lineNumber = 0;

                using (var sr = new StreamReader(_path))
                {
                    contents = sr.ReadToEnd().Replace("\r", "").Split('\n');
                }
                foreach (string item in contents)
                {
                    lineNumber++;
                    //Can't split by space because probe names could have a space
                    //string[] line = item.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                    if (string.IsNullOrEmpty(item)) continue;
                    else if (item.Length < 31) continue; //RWD has 42 total characters (31 without button ID)
                    //else if (line.Length != 4) continue; //RWD has 4 segments
                    string line = item.PadRight(50, ' '); //make sure no OutOfIndex error thrown below

                    string time = line.Substring(0,15);
                    string probeType = line.Substring(16, 2);
                    string probeId = line.Substring(19, 10);
                    string button = line.Substring(30, 12);
                    if (string.IsNullOrWhiteSpace(time) ||
                        string.IsNullOrWhiteSpace(probeType) ||
                        string.IsNullOrWhiteSpace(probeId) ||
                        string.IsNullOrWhiteSpace(button))
                    {
                        ExceptionHandler.Handle(new Exception(), $"Could not parse line {lineNumber} of {_path}. Entry skipped.");
                        continue;
                    }

                    _recordList.Add(new Scan
                    {
                        Button = button,
                        Time = DateTime.ParseExact(time, "yyyyMMddHHmmssf", CultureInfo.InvariantCulture),
                        Device = probeType,
                        Probe = probeId,
                        Type = RecordType.Scan
                    });
                }

                return _recordList;
            }
            catch (Exception ex) { ExceptionHandler.Handle(ex); return null; }

        }

    }
}
