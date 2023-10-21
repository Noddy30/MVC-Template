using System;
using System.Net.Http.Headers;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Data;

namespace Template.Services
{
    public interface IRYZEGolfService
    {
        Task<List<GolfCourse>> GetGolfCoursesAsync(string courseName);
        Task SaveGolfCoursesAsync(List<GolfCourse> golfCourses);
    }
    public class RYZEGolfService : IRYZEGolfService
    {
        private readonly AppDbContext _dbContext;
        private readonly string _rapidApiKey = "1ea3857bc7mshb3904505b436e6fp123a91jsn8d255db118e5";
        private readonly HttpClient _httpClient;

        public RYZEGolfService(AppDbContext dbContext)
		{
            _dbContext = dbContext;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _rapidApiKey);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "golf-course-api.p.rapidapi.com");
        }

        public async Task<List<GolfCourse>> GetGolfCoursesAsync(string courseName)
        {
            var response = await _httpClient.GetAsync($"https://golf-course-api.p.rapidapi.com/search?name={courseName}");

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var golfCourses = JsonConvert.DeserializeObject<List<GolfCourse>>(body);

                foreach (var course in golfCourses)
                {
                    foreach (var teeBox in course.TeeBoxes)
                    {
                        // Check and set default color if Color is null or empty
                        if (string.IsNullOrEmpty(teeBox.Color))
                        {
                            teeBox.Color = "Default Color";
                        }
                    }
                }

                return golfCourses;
            }

            // Handle error cases if necessary
            return null;
        }

        //public async Task<string> GetCourseData(string courseName)
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage
        //        {
        //            Method = HttpMethod.Get,
        //            RequestUri = new Uri("https://golf-course-api.p.rapidapi.com/search?name=" + courseName),
        //            Headers =
        //            {
        //                { "X-RapidAPI-Key", "1ea3857bc7mshb3904505b436e6fp123a91jsn8d255db118e5" },
        //                { "X-RapidAPI-Host", "golf-course-api.p.rapidapi.com" },
        //            },
        //        };
        //        using (var response = await client.SendAsync(request))
        //        {
        //            response.EnsureSuccessStatusCode();
        //            var body = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine(body);
        //            var golfCourses = JsonConvert.DeserializeObject<List<GolfCourse>>(body);
        //            return body;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public async Task SaveGolfCoursesAsync(List<GolfCourse> golfCourses)
        {
            try
            {
                foreach (var golfCourse in golfCourses)
                {
                    var existingCourse = _dbContext.GolfCourses.FirstOrDefault(c => c.Name == golfCourse.Name);

                    if (existingCourse != null)
                    {
                        // Update existing golf course properties
                        _dbContext.Entry(existingCourse).CurrentValues.SetValues(golfCourse);
                    }
                    else
                    {
                        // Add new golf course if it doesn't exist
                        _dbContext.GolfCourses.Add(golfCourse);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}

