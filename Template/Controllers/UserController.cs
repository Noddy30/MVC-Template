using System;
using Microsoft.AspNetCore.Mvc;

namespace Template.Controllers
{
	public class UserController: Controller
	{
        private readonly ILogger<HomeController> _logger;

        public UserController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> RegisterUser()
        //{

        //}
    }
}

