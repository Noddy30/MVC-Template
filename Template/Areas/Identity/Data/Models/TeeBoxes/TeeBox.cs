using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Areas.Identity.Data.Models.Courses;

namespace Template.Areas.Identity.Data.Models.TeeBoxes
{
	public class TeeBox: BaseModel
	{
        public string Color { get; set; } = "Default Color";
        public int Yards { get; set; }
    }
}

