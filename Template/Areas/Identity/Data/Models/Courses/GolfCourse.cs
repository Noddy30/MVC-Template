using System;
using System.ComponentModel.DataAnnotations;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.Models.TeeBoxes;

namespace Template.Areas.Identity.Data.Models.Courses
{
	public class GolfCourse :BaseModel
	{
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Website { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Coordinates { get; set; }

        [Required]
        public int Holes { get; set; }

        [Required]
        public string LengthFormat { get; set; }

        [Required]
        public string GreenGrass { get; set; }

        [Required]
        public string FairwayGrass { get; set; }

        public List<ScoreCard> Scorecards { get; set; }

        public List<TeeBox> TeeBoxes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}

