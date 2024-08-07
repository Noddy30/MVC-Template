﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Areas.Identity.Data;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Models.FrontEnd;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Models;

namespace Template.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    //public DbSet<Player> Players { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<GolfCourse> GolfCourses { get; set; }
    public DbSet<ScoreCard> ScoreCards { get; set; }
    public DbSet<TeeBox> TeeBoxes { get; set; }
    public DbSet<Tees> Tees { get; set; }
    public DbSet<WhereTo> WhereTos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
