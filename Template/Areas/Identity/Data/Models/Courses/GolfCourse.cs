using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.Models.TeeBoxes;

namespace Template.Areas.Identity.Data.Models.Courses
{
	public class GolfCourse 
	{
        [Key]
        [JsonProperty("_id")]
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
        public List<ScoreCard> Scorecard { get; set; }
        public List<TeeBox> TeeBoxes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

