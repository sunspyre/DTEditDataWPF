using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataTrack.IO
{
    public class Badge
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

    public class BadgeReader
    {
        private string _path;
        private List<Badge> _badgeList;

        public BadgeReader(string path)
        {
            _path = path;
            _badgeList = new List<Badge>();
            string[] _contents;

            try
            {
                using (StreamReader sr = new StreamReader(path))
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
                    string extraDetails = _contents[i + 1];
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
        }

        public List<Badge> GetList()
        {
            return _badgeList;
        }
    }
}
