using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Template.Areas.Identity.Data;
using Template.Data;
using Template.Repositories.Players;
using Template.Repositories.Users;

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

            

            // Add Repositories
            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

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

                    var createUserResult = await userManager.CreateAsync(user, password);

                    if (createUserResult.Succeeded)
                    {
                        // Now, assign the role to the user
                        await userManager.AddToRoleAsync(user, "SuperAdmin");
                    }
                }
            }


            app.Run();
        }
    }
}

