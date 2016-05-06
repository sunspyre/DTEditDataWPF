using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataTrack.IO
{
    class Scan : Record
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Device { get; set; }
        public string Probe { get; set; }
        public string Button { get; set; }
    }

    class RwdReader : IReader
    {
        string _path;
        List<Record> _list;

        public RwdReader(string path)
        {
            _path = path;
            _list = new List<Record>();

            using (StreamReader sr = new StreamReader(_path))
            {

            }
        }

        public List<Record> GetList()
        {
            return _list;
        }
    }
}
