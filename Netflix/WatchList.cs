using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix
{
    internal class WatchList : BaseClass
    {
        public static int AutoIncrementedID = 1;

        public WatchList(string title, string genre, int duration) : base( title,  genre,  duration)
        {
            Id = AutoIncrementedID++;
        }
    }
}

