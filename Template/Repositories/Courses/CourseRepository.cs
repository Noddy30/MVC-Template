using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Template.Areas.Identity.Data;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Data;

namespace Template.Repositories.Courses
{
	public interface ICourseRepository
	{
        Task CreateAsync(GolfCourse model);
        Task UpdateAsync(string id, GolfCourse model);
        Task<GolfCourse?> GetModelAsync(string id);
        Task DeleteAsync(string id);
        Task RestoreAsync(string id);
    }
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _dbcontext;

        public CourseRepository(AppDbContext dbcontext)
		{
            _dbcontext = dbcontext;

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
            //_dbcontext.ApplicationUsers.Remove(model);
            //await _dbcontext.SaveChangesAsync();

            model.IsDeleted = true;
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<GolfCourse?> GetModelAsync(string id)
        {
            var model = await _dbcontext.GolfCourses.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (model == null)
            {
                return null;
            }
            return model;
        }

        public async Task RestoreAsync(string id)
        {
            var model = await _dbcontext.GolfCourses
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);

            model.IsDeleted = false;

            _dbcontext.Update(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, GolfCourse model)
        {
            var course = await _dbcontext.GolfCourses.Where(x => x.Id == id).FirstOrDefaultAsync();

            _dbcontext.GolfCourses.Update(model);
            await _dbcontext.SaveChangesAsync();
        }
    }
}

