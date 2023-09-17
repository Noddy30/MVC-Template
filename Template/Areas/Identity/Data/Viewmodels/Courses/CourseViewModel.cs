using System;
namespace Template.Areas.Identity.Data.ViewModels.Courses
{
	public class CourseViewModel
	{
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created { get; set; }
        public bool isDeleted { get; set; }

        public string Name { get; set; }
    }
}

