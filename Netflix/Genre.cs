using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Netflix
{
    internal class Genre
    {
        public int Id { get; set;}
        public static int AutoIncrementedID = 1;
        public string GenreName {  get; set; }

        public Genre(string genrename)
        {
            GenreName = genrename;
            Id = AutoIncrementedID++;
        }
    }
}
