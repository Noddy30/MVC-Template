using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Areas.Identity.Data.Models.Courses;

namespace Template.Areas.Identity.Data.Models.TeeBoxes
{
	public class TeeBox : BaseModel
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
        public GolfCourse GolfCourse { get; set; }
    }
}

