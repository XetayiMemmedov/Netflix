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
        public string Title { get; set; }
        public int Duration { get; set; }
        public Genre GenrE { get; set; }
        public int ViewCount { get; set; }

        public Movie(string title, Genre genre, int duration)
        {
            GenrE = genre;
            Title = title;
            Duration = duration;
            ViewCount = 0;
            Id = AutoIncrementedID++;
        }
    }
}
