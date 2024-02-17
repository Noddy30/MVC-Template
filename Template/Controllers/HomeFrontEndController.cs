using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Template.Areas.FrontEnd
{
    [Authorize(Roles = "User")]
    public class HomeFrontEndController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/HomeFrontEnd/Index.cshtml");
        }
    }
}

