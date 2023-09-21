using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Viewmodels.Courses;

namespace Template.Areas.Identity.Data.Viewmodels.TeeBoxes
{
	public class TeeBoxViewModel
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Tee { get; set; }

        [Required]
        public double Slope { get; set; }

        [Required]
        public double Handicap { get; set; }

        [ForeignKey("GolfCourseId")]
        public GolfCourseViewModel GolfCourse { get; set; }
    }
}

