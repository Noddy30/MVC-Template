using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Template.Areas.Identity.Data;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.PaginationDataTables;
using Template.Areas.Identity.Data.Viewmodels.Courses;
using Template.Data;
using Template.Helpers;

namespace Template.Repositories.Courses
{
	public interface ICourseRepository
	{
        Task CreateAsync(GolfCourse model);
        Task UpdateAsync(string id, GolfCourseViewModel viewModel);
        Task<GolfCourse?> GetModelAsync(string id);
        Task DeleteAsync(string id);
        //Task RestoreAsync(string id);
        Task<List<GolfCourse?>> GetAllCoursesAsync();
        Task<string> GetPaginated(GolfCoursePagination model);
    }
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CourseRepository(AppDbContext dbcontext, IMapper mapper)
		{
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task CreateAsync(GolfCourse model)
        {
            _dbcontext.GolfCourses.Add(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var model = await _dbcontext.GolfCourses.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (model == null)
            {
                return;
            }
            _dbcontext.GolfCourses.Remove(model);
            await _dbcontext.SaveChangesAsync();

        }

        public async Task<GolfCourse?> GetModelAsync(string id)
        {
            var model = await _dbcontext.GolfCourses
                .Include(i => i.Scorecard)
                    .ThenInclude(t => t.Tees)
                .Include(i => i.TeeBoxes)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (model == null)
            {
                return null;
            }
            return model;
        }

        public async Task UpdateAsync(string id, GolfCourseViewModel viewModel)
        {
            var course = await _dbcontext.GolfCourses
                .Include(i => i.Scorecard)
                    .ThenInclude(t => t.Tees)
                .Include(i => i.TeeBoxes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (course == null)
            {
                // Handle the case where the course with the given id is not found.
                // You can throw an exception or handle it as appropriate for your application.
                return;
            }

            course.UpdatedAt = DateTime.UtcNow;
            var model = _mapper.Map(viewModel, course);
            
            // Save the changes to the database.
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<GolfCourse?>> GetAllCoursesAsync()
        {
            var model = await _dbcontext.GolfCourses
                .ToListAsync();
            if (model == null)
            {
                return null;
            }
            return model;
        }
        public async Task<string> GetPaginated(GolfCoursePagination model)
        {
            var data = _dbcontext.GolfCourses.AsQueryable();

            // Apply search filter if it's provided
            if (!string.IsNullOrEmpty(model.SearchFilter))
            {
                data = data.Where(x => x.Name.ToUpper().Contains(model.SearchFilter.ToUpper()) ||
                                       x.City.ToUpper().Contains(model.SearchFilter.ToUpper()) ||
                                       x.Country.ToUpper().Contains(model.SearchFilter.ToUpper()) ||
                                       x.Holes.ToString().Contains(model.SearchFilter));
            }

            // Determine the sort direction
            bool isAscending = model.SortDir_0 == "asc";

            // Sort the data based on the column index
            data = model.SortCol_0 switch
            {
                0 => (isAscending) ? data.OrderBy(x => x.Name) : data.OrderByDescending(x => x.Name),
                1 => (isAscending) ? data.OrderBy(x => x.City) : data.OrderByDescending(x => x.City),
                2 => (isAscending) ? data.OrderBy(x => x.Country) : data.OrderByDescending(x => x.Country),
                3 => (isAscending) ? data.OrderBy(x => x.Holes) : data.OrderByDescending(x => x.Holes),
                _ => data // Handle default case if needed
            };

            var totalCount = await data.CountAsync();

            var displayedMembers = await data
                .Skip(model.DisplayStart)
                .Take(model.DisplayLength)
                .Select(a => new
                {
                    Name = a.Name,
                    Id = a.Id,
                    City = a.City,
                    Country = a.Country,
                    Holes = a.Holes
                })
                .ToListAsync();

            var result = new
            {
                Echo = model.Echo,
                TotalRecords = totalCount,
                TotalDisplayRecords = totalCount,
                data = displayedMembers
            };

            return JsonConvert.SerializeObject(result);
        }
        //public async Task<string> GetPaginated(GolfCoursePagination model)
        //{
        //    var data = _dbcontext.GolfCourses.AsQueryable();
        //    int pageSize = model.DisplayLength; // Number of records that should be shown in table
        //    int pageNumber = (model.DisplayStart / pageSize) + 1;
        //    var count = data.Count();
        //    if (!string.IsNullOrEmpty(model.SearchFilter))
        //    {
        //        data = data.Where(x => x.Name.ToUpper().Contains(model.SearchFilter.ToUpper()));
        //    }

        //    string propertyName = model.SortCol_0 switch
        //    {
        //        0 => "Name",
        //        1 => "City",
        //        2 => "Country",
        //        4 => "Holes",
        //        _ => "",
        //    };

        //    data = data.OrderByProperty(propertyName, model.SortDir_0 == "asc");
        //    var count2 = data.Count();
        //    var totalCount = await data.CountAsync();

        //    var displayedMembers = await data
        //        .Skip(model.DisplayStart)
        //        .Take(model.DisplayLength)
        //        .Select(a => new
        //        {
        //            Name = a.Name,
        //            Id = a.Id,
        //            City = a.City,
        //            Country = a.Country,
        //            Holes = a.Holes
        //        })
        //        .ToListAsync();
        //    var resultCount = displayedMembers.Count();
        //    var result = new
        //    {
        //        Echo = model.Echo,
        //        TotalRecords = totalCount,
        //        TotalDisplayRecords = totalCount,
        //        data = displayedMembers
        //    };

        //    return JsonConvert.SerializeObject(result);
        //}

    }
}

