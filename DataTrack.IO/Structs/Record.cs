using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTrack.IO.Structs
{
    public abstract class Record
    {
        public RecordType Type { get; set; }
    }

    public enum RecordType
    {
        Badge,
        Scan
    }
}
