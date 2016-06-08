using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataTrack.IO.Structs;
using System.Collections.Generic;

namespace DTEditData.Tests
{
    [TestClass]
    public class BadgeTests
    {
        [TestMethod]
        public void NewBadgePadsButtonId()
        {
            List<Badge> list = new List<Badge>();

            Badge badge = new Badge
            {
                BadgeId = "12ABC",
                ButtonId = "4DGC89D", //This will PadLeft up to 12 digits with a 0
                Desc = "Test Badge",
                ExtraDetails = "TEST;1;2;E;F;",
                Misc = "Activity",
                Special = 1,
                Type = RecordType.Badge,
                Type1 = "Profile",
                Type2 = "Element"
            };

            list.Add(badge);

            Assert.AreEqual(list[0].ButtonId, "000004DGC89D");
        }

        [TestMethod]
        public void NewBadgePadsBadgeId()
        {
            List<Badge> list = new List<Badge>();

            Badge badge = new Badge
            {
                BadgeId = "12ABC", //This will PadLeft up to 6 digits with a 0
                ButtonId = "4DGC89D", 
                Desc = "Test Badge",
                ExtraDetails = "TEST;1;2;E;F;",
                Misc = "Activity",
                Special = 1,
                Type = RecordType.Badge,
                Type1 = "Profile",
                Type2 = "Element"
            };

            list.Add(badge);

            Assert.AreEqual(list[0].BadgeId, "012ABC");
        }

        [TestMethod]
        public void BaseListCanHoldDerivedClasses()
        {
            List<Record> list = new List<Record>();
            list.Add(new Badge { Type = RecordType.Badge });
            list.Add(new Scan { Type = RecordType.Scan });
            Assert.IsTrue(list.Exists(x => x.Type == RecordType.Badge));
        }
    }
}
