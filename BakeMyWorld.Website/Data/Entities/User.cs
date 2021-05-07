using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeMyWorld.Website.Data.Entities
{
    public class User : IdentityUser
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
    }
}
