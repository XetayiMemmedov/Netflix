using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix
{
    internal class BaseClass
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string GenreName { get; set; }
        public int ViewCount { get; set; }
        public BaseClass(string title, string genre, int duration)
        {
            Title = title;
            GenreName = genre;
            Duration = duration;
        }
    }
}
