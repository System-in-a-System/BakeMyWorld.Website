using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeMyWorld.ConsoleManager
{
    class Admin
    {
        public Admin()
        {

        }
        
        public Admin(string adminName, string adminPassword)
        {
            AdminName = adminName;
            AdminPassword = adminPassword;
        }

        public string AdminName { get; set; }
        public string AdminPassword { get; set; }
    }
}
