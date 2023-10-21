using System;
using System.ComponentModel.DataAnnotations;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Areas.Identity.Data.Viewmodels.ScoreCards;
using Template.Areas.Identity.Data.Viewmodels.TeeBoxes;

namespace Template.Areas.Identity.Data.Viewmodels.Courses
{
	public class GolfCourseViewModel
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Coordinates { get; set; }
        public int Holes { get; set; }
        public string LengthFormat { get; set; }
        public string GreenGrass { get; set; }
        public string FairwayGrass { get; set; }
        public List<ScoreCardViewModel> Scorecard { get; set; }
        public List<TeeBoxViewModel> TeeBoxes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

