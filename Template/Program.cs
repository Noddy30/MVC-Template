using System.Net.Mail;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Template.Areas.Identity.Data;
using Template.Data;
using Template.Repositories.Courses;
using Template.Repositories.Players;
using Template.Repositories.ScoreCards;
using Template.Repositories.TeeBoxes;
using Template.Repositories.Users;
using Template.Services;
using AutoMapper;
using Template.Areas.Identity.Data.MapperProfiles;

namespace Template
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Automapper
            builder.Services.AddAutoMapper(typeof(TeesProfile), typeof(TeeBoxProfile), typeof(ScoreCardProfile), typeof(GolfCourseProfile),typeof(Program));

            // Add Repositories
            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddTransient<ICourseRepository, CourseRepository>();
            builder.Services.AddTransient<IScoreCardRepository, ScoreCardRepository>();
            builder.Services.AddTransient<ITeeBoxRepository, TeeBoxRepository>();
            builder.Services.AddTransient<IRYZEGolfService, RYZEGolfService>();


            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); ;

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            // Seed Roles
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "SuperAdmin", "Admin", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            //Seed User
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string email = "neduan41@gmail.com";
                string password = "N@ude1998";


                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new ApplicationUser();
                    user.Id = "a92bee58-6f38-4076-9207-ad09502b43cb";

                    user.UserName = email;
                    user.Email = email;
                    user.FirstName = "Eduan";
                    user.LastName = "Name";
                    user.PhoneNumber = "0837832973";
                    user.IsDeleted = false;

                    var createUserResult = await userManager.CreateAsync(user, password);

                    if (createUserResult.Succeeded)
                    {
                        // Now, assign the role to the user
                        await userManager.AddToRoleAsync(user, "SuperAdmin");
                    }
                }

                // Define the second user
                string email2 = "mbotha181@gmail.com";
                string password2 = "P@ssword123";

                if (await userManager.FindByEmailAsync(email2) == null)
                {
                    var user2 = new ApplicationUser
                    {
                        Id = "ea01191c-e6f4-4d8f-8dee-6e6c12e66afc",
                        UserName = email2,
                        Email = email2,
                        FirstName = "Spoegie",
                        LastName = "MPDB",
                        PhoneNumber = "0832756808",
                        IsDeleted = false
                    };

                    var createUserResult2 = await userManager.CreateAsync(user2, password2);

                    if (createUserResult2.Succeeded)
                    {
                        // Assign a role to the second user if needed
                        await userManager.AddToRoleAsync(user2, "SuperAdmin");
                    }
                }
            }
            
            app.Run();
        }
    }
}

