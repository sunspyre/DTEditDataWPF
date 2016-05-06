using DataTrack.IO.Structs;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Record> GetFileContents(string fileName)
        {
            _recordList = new List<Record>();
            using (var sr = new StreamReader(fileName))
            {
                //add files to list
            }

            return _recordList;
        }
    }
}
