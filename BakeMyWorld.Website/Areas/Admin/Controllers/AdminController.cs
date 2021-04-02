using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeMyWorld.Website.Controllers
{
    [Authorize(Roles = "Admin")]
    public abstract class AdminController : Controller
    {
        
    }
}
