using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Viewmodels.Courses;

namespace Template.Areas.Identity.Data.Viewmodels.TeeBoxes
{
    public class TeesViewModel : BaseViewModel
    {
        [JsonProperty("color")]
        public string? Color { get; set; } = "White";

        [JsonProperty("yards")]
        public float? Yards { get; set; }
    }

    public class TeeBoxViewModel
	{
        [JsonProperty("tee")]
        public string? Tee { get; set; } = string.Empty;

        [JsonProperty("slope")]
        public int? Slope { get; set; } = 0;

        [JsonProperty("handicap")]
        public float? Handicap { get; set; } = 0;
    }
}

