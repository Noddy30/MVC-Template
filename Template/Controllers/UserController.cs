using System;
using Microsoft.AspNetCore.Mvc;
using Template.Areas.Identity.Data;
using Template.Areas.Identity.Data.Viewmodels;
using Template.Repositories.Users;

namespace Template.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<HomeController> logger,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Paginate()
        {
            var data = await _userRepository.GetAllUsersAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userRepository.GetModelAsync(id);
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.FirstName,
                LastName = user.LastName
            };

            return View(userViewModel);
        }

    }
}

