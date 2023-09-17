using System;
using Microsoft.AspNetCore.Mvc;
using Template.Repositories.Courses;

namespace Template.Controllers
{
	public class CourseController : Controller
	{
		public readonly ICourseRepository _courseRepository;
		public CourseController(ICourseRepository courseRepository)
		{
			_courseRepository = courseRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}

