using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Viewmodels.Courses;

namespace Template.Areas.Identity.Data.Viewmodels.ScoreCards
{
	public class ScoreCardViewModel
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public int Hole { get; set; }

        [Required]
        public int Par { get; set; }

        [Required]
        public string Tees { get; set; }

        [Required]
        public int Handicap { get; set; }

        [ForeignKey("GolfCourseId")]
        public GolfCourseViewModel GolfCourse { get; set; }
    }
}

