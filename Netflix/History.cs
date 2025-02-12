using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix
{
    internal class History : BaseClass
    {
        public static int AutoIncrementedID = 1;

        public History(string title, string genre, int duration) : base( title,  genre,  duration)
        {
            Id = AutoIncrementedID++;
        }
    }
}
