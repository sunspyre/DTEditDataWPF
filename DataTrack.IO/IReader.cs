using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTrack.IO
{
    interface IReader
    {
        List<Record> GetList();
    }
}
