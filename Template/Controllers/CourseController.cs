using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.PaginationDataTables;
using Template.Areas.Identity.Data.Viewmodels.Courses;
using Template.Areas.Identity.Data.Viewmodels.ScoreCards;
using Template.Areas.Identity.Data.Viewmodels.TeeBoxes;
using Template.Areas.Identity.Data.Viewmodels.Users;
using Template.Repositories.Courses;
using Template.Services;

namespace Template.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class CourseController : Controller
	{
		public readonly ICourseRepository _courseRepository;
		public readonly IRYZEGolfService _RYZEGolfService;
		public readonly IMapper _mapper;

		public CourseController(ICourseRepository courseRepository,
            IRYZEGolfService RYZEGolfService,
            IMapper mapper)
		{
			_courseRepository = courseRepository;
            _RYZEGolfService = RYZEGolfService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Paginate()
        //{
        //    var data = await _courseRepository.GetAllCoursesAsync();
        //    return Json(data);
        //}
        [HttpPost]
        public async Task<string> Paginate(GolfCoursePagination model)
        {
            var data = await _courseRepository.GetPaginated(model);
            return data;
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
            
            if (data != null && data.Any())
            {
                await _RYZEGolfService.SaveGolfCoursesAsync(data);
                return Ok("Golf courses found and saved successfully.");
            }
            else
            {
                return NotFound("No golf courses found.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditCourse(string id)
        {
            var model = await _courseRepository.GetModelAsync(id);

            if (model == null)
            {
                return View();
            }
            var viewModel = _mapper.Map<GolfCourseViewModel>(model);
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(string id, GolfCourseViewModel viewModel)
        {
            await _courseRepository.UpdateAsync(id, viewModel);
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

