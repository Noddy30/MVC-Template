using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Viewmodels.Courses;

namespace Template.Areas.Identity.Data.Viewmodels.TeeBoxes
{
	public class TeeBoxViewModel
	{
        public string Color { get; set; } = "Default Color";
        public int Yards { get; set; }
    }
}

