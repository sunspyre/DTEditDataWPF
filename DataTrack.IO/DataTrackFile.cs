using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTrack.IO
{
    public class DataTrackFile
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public DateTime LastAccessed { get; set; }
    }
}
