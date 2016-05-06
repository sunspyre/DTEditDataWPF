using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTrack.IO.Structs
{
    public class Badge : Record
    {
        private string badgeId;

        public string BadgeId
        {
            get { return badgeId; }
            set { badgeId = value.PadLeft(6, '0'); }
        }

        private string buttonId;

        public string ButtonId
        {
            get { return buttonId; }
            set { buttonId = value.PadLeft(12, '0'); }
        }

        public string Desc { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int Special { get; set; }
        public string Misc { get; set; }
        public string ExtraDetails { get; set; }

    }
}
