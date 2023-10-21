using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Areas.Identity.Data.Viewmodels.Courses;

namespace Template.Areas.Identity.Data.Viewmodels.ScoreCards
{
	public class ScoreCardViewModel
	{
        public int Hole { get; set; }
        public int Par { get; set; }
        public TeeBox Tees { get; set; }
        public int Handicap { get; set; }
    }
}

