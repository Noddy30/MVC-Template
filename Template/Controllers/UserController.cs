using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.Areas.Identity.Data;
using Template.Areas.Identity.Data.Viewmodels.Users;
using Template.Repositories.Users;

namespace Template.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ILogger<HomeController> logger,
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
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
            var userRole = await _userManager.GetRolesAsync(user) as List<string>;

            var applicationRole = new IdentityRole();
            if (userRole.Count != 0)
            {
                applicationRole = await _roleManager.FindByNameAsync(userRole?.FirstOrDefault());
            }

            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.FirstName,
                LastName = user.LastName,
                Role = (applicationRole != null) ? applicationRole.Name : "",
                Password = user.PasswordHash
                
        };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(string id, UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await _userRepository.UpdateUserAsync(id, viewModel);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            var userViewModel = new UserViewModel();
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string id, UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await _userRepository.CreateUserAsync(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            return Ok(_roleManager.Roles.OrderBy(x => x.Name));
        }
        
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                await _userRepository.DeleteAsync(id);
            }
            
            return RedirectToAction("Index");
        }
    }
}

