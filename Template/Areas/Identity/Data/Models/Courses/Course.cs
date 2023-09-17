using System;
namespace Template.Areas.Identity.Data.Models.Courses
{
	public class Course 
	{
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created { get; set; } = DateTime.Now;
        public bool isDeleted { get; set; } = false;

        public string Name { get; set; }
    }
}

