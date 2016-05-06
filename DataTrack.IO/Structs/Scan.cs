using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTrack.IO.Structs
{
    public class Scan : Record
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Device { get; set; }
        public string Probe { get; set; }
        public string Button { get; set; }
    }
}
