using System;
using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Areas.Identity.Data.Viewmodels.Courses;
using Template.Data;

namespace Template.Services
{
    public interface IRYZEGolfService
    {
        Task<List<GolfCourseViewModel>> GetGolfCoursesAsync(string courseName);
        Task SaveGolfCoursesAsync(List<GolfCourseViewModel> golfCourses);
    }
    public class RYZEGolfService : IRYZEGolfService
    {
        private readonly AppDbContext _dbContext;
        private readonly string _rapidApiKey = "1ea3857bc7mshb3904505b436e6fp123a91jsn8d255db118e5";
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public RYZEGolfService(AppDbContext dbContext, IMapper mapper)
		{
            _dbContext = dbContext;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _rapidApiKey);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "golf-course-api.p.rapidapi.com");
        }

        public async Task<List<GolfCourseViewModel>> GetGolfCoursesAsync(string courseName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://golf-course-api.p.rapidapi.com/search?name={courseName}");

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();

                    try
                    {
                        GolfCourseViewModel golfCourse = JsonConvert.DeserializeObject<GolfCourseViewModel>(body);
                        return new List<GolfCourseViewModel> { golfCourse };
                    }
                    catch (JsonSerializationException)
                    {
                        List<GolfCourseViewModel> golfCourses = JsonConvert.DeserializeObject<List<GolfCourseViewModel>>(body);
                        return golfCourses;
                    }
                }

                // Handle error cases if necessary
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveGolfCoursesAsync(List<GolfCourseViewModel> golfCourses)
        {
            try
            {
                // Map GolfCourseViewModel to GolfCourse using AutoMapper
                var golfCourseEntities = _mapper.Map<List<GolfCourse>>(golfCourses);

                foreach (var golfCourse in golfCourseEntities)
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

