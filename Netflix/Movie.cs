using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix
{
    internal class Movie:BaseClass
    {
        public static int AutoIncrementedID = 1;
       
        public Movie(string title, string genre, int duration):base(title, genre, duration)
        {
            Title = title;
            GenreName = genre;
            Duration = duration;
            ViewCount = 0;
            Id = AutoIncrementedID++;
        }
    }
}
