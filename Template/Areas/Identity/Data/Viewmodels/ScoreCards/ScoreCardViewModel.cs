using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Areas.Identity.Data.Viewmodels.Courses;
using Template.Areas.Identity.Data.Viewmodels.TeeBoxes;

namespace Template.Areas.Identity.Data.Viewmodels.ScoreCards
{
	public class ScoreCardViewModel
	{
        public int? Hole { get; set; } = 0;

        public int? Par { get; set; } = 0;

        [JsonProperty("tees")]
        public Dictionary<string, Tees> Tees { get; set; }

        public float? Handicap { get; set; } = 0;
    }
}

