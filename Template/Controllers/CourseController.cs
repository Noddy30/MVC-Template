using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.Areas.Identity.Data.Viewmodels.Courses;
using Template.Areas.Identity.Data.Viewmodels.Users;
using Template.Repositories.Courses;
using Template.Services;

namespace Template.Controllers
{
	public class CourseController : Controller
	{
		public readonly ICourseRepository _courseRepository;
		public readonly IRYZEGolfService _RYZEGolfService;

		public CourseController(ICourseRepository courseRepository,
            IRYZEGolfService RYZEGolfService)
		{
			_courseRepository = courseRepository;
            _RYZEGolfService = RYZEGolfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Paginate()
        {
            var data = await _courseRepository.GetAllCoursesAsync();
            return Json(data);
        }

        [HttpGet]
        public IActionResult CourseDataFromAPI()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseDataFromAPI(string courseName)
        {
            var data = await _RYZEGolfService.GetGolfCoursesAsync(courseName);
            if(data != null)
            {
                await _RYZEGolfService.SaveGolfCoursesAsync(data);
            }
            
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> EditCourse(string id)
        {
            var model = await _courseRepository.GetModelAsync(id);
            
            var viewModel = new GolfCourseViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Phone = model.Phone,
                Website = model.Website,
                Address = model.Address,
                City = model.City,
                State = model.State,
                Zip = model.Zip,
                Country = model.Country,
                Coordinates = model.Coordinates,
                Holes = model.Holes,
                GreenGrass = model.GreenGrass,
                FairwayGrass = model.FairwayGrass,
                //Scorecard = ,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(string id, GolfCourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await _courseRepository.UpdateAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                await _courseRepository.DeleteAsync(id);
            }

            return RedirectToAction("Index");
        }
    }
}

