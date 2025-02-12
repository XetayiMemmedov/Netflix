using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix
{
    public enum UserRole
    {
        Admin,
        User
    }
    public class User()
    {

        public string Password { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }
    }
    

}
