using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrack.IO.Structs
{
    public class RwdRecord : Record
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Flag { get; set; }
        public string ProbeId { get; set; }
        public string BadgeId { get; set; }
        public int Special { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string Desc { get; set; }
        public string Misc { get; set; }
        public string ButtonId { get; set; }
        public string[] ExtraDetails { get; set; }


    }
}
