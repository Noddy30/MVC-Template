using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.Areas.Identity.Data;
using Template.Areas.Identity.Data.Models.FrontEnd;
using Template.Areas.Identity.Data.Viewmodels.FrontEnd;
using Template.Areas.Identity.Data.Viewmodels.Users;
using Template.Controllers;
using Template.Repositories.Users;
using Template.Repositories.WhereTo;

namespace Template.Areas.FrontEnd
{
    [Authorize(Roles = "User")]
    public class HomeFrontEndController : Controller
    {
        private readonly IWhereToRepository _whereToRepository;

        public HomeFrontEndController(IWhereToRepository whereToRepository)
        {
            _whereToRepository = whereToRepository;
        }

        public IActionResult Index()
        {
            return View("~/Views/HomeFrontEnd/Index.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDestination(WhereToViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _whereToRepository.CreateAsync(viewModel);
            return RedirectToAction("Index");
        }
    }
}

