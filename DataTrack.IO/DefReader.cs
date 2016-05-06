using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DataTrack.IO;
using System.Linq;
using System.Text;
using DataTrack.IO.Structs;

namespace DataTrack.IO
{
    public class DefReader : IReader
    {
        private string _path;
        private List<Record> _badgeList;
        private List<DataTrackFile> _fileList;

        public DefReader(string path)
        {
            _path = path;
        }

        public IEnumerable<DataTrackFile> GetFolderContents()
        {
            _fileList = new List<DataTrackFile>();

            DirectoryInfo di = new DirectoryInfo($"{_path}\\{Folder.Def}");
            FileInfo[] files = di.GetFiles("*.def");

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

        public IEnumerable<Record> GetFileContents(string fileName)
        {
            switch (fileName.ToLower())
            {
                case "badge.def":
                    return ParseBadge(fileName);
                default:
                    return null;
            }
        }

        private IEnumerable<Record> ParseBadge(string fileName)
        {
            _badgeList = new List<Record>();
            string[] _contents;
            try
            {
                using (StreamReader sr = new StreamReader($"{_path}\\{Folder.Def}\\{fileName}"))
                {
                    _contents = sr.ReadToEnd().Split('\n');
                }
                for (int i = 2; i < _contents.Length; i += 2)
                {
                    if (string.IsNullOrEmpty(_contents[i]) ||
                        string.IsNullOrEmpty(_contents[i + 1]) ||
                        (i + 1) > _contents.Length)
                        continue;
                    string[] line = _contents[i].Replace("\r", string.Empty).Split(';');
                    string extraDetails = _contents[i + 1].Replace("\r", string.Empty);
                    if (line.Length < 8) //Make sure there are at least 8 amount of elements per badge line
                        continue;

                    _badgeList.Add(new Badge
                    {
                        //Hash = line[0] Unneccesary
                        Type1 = line[1],
                        Type2 = line[2],
                        BadgeId = line[3],
                        Special = int.Parse(line[4]),
                        ButtonId = line[5],
                        Desc = line[6],
                        Misc = line[7],
                        ExtraDetails = extraDetails
                    });
                }
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message);
            }
            return _badgeList;
        }


    }
}
