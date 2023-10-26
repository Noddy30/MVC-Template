using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Models.TeeBoxes;

namespace Template.Areas.Identity.Data.Models.ScoreCards
{
	public class ScoreCard : BaseModel
	{
        public int? Hole { get; set; } = 0;

        public int? Par { get; set; } = 0;

        [JsonProperty("tees")]
        public List<Tees> Tees { get; set; }

        public float? Handicap { get; set; } = 0;
    }
}

