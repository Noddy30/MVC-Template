using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Areas.Identity.Data.Viewmodels.ScoreCards;
using Template.Areas.Identity.Data.Viewmodels.TeeBoxes;

namespace Template.Areas.Identity.Data.Viewmodels.Courses
{
	public class GolfCourseViewModel
	{
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; } = "";

        [JsonProperty("website")]
        public string? Website { get; set; } = "";

        [JsonProperty("address")]
        public string? Address { get; set; } = "";

        [JsonProperty("city")]
        public string? City { get; set; } = "";

        [JsonProperty("state")]
        public string? State { get; set; } = "";

        [JsonProperty("zip")]
        public string? Zip { get; set; } = "";

        [JsonProperty("country")]
        public string? Country { get; set; } = "";

        [JsonProperty("coordinates")]
        public string? Coordinates { get; set; } = "";

        [JsonProperty("holes")]
        public int? Holes { get; set; } = 0;

        [JsonProperty("lengthFormat")]
        public string? LengthFormat { get; set; } = "";

        [JsonProperty("greenGrass")]
        public string? GreenGrass { get; set; } = "";

        [JsonProperty("fairwayGrass")]
        public string? FairwayGrass { get; set; } = "";

        [JsonProperty("scorecard")]
        public List<ScoreCardViewModel> Scorecard { get; set; }

        [JsonProperty("teeBoxes")]
        public List<TeeBoxViewModel> TeeBoxes { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}

