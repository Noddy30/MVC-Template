using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Models.TeeBoxes;

namespace Template.Areas.Identity.Data.Models.ScoreCards
{
	public class ScoreCard : BaseModel
	{
        public int Hole { get; set; }
        public int Par { get; set; }
        public TeeBox Tees { get; set; }
        public int Handicap { get; set; }
    }
}

