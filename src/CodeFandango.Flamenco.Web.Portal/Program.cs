using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Web.Portal.Interfaces;
using CodeFandango.Flamenco.Web.Portal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeFandango.Flamenco.Web.Portal
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var localSettings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "flamenco", "appsettings.local.json");
            if (File.Exists(localSettings))
                builder.Configuration.AddJsonFile(localSettings, optional: true);

            // Add services to the container.
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddDbContext<FlamencoDb>((serviceProvider, options) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                var dbId = httpContextAccessor.HttpContext?.Request.Cookies["DatabaseId"];

                if (dbId == null)
                {
#if DEBUG
                    var cs = config.GetConnectionString("Migrations");
                    options.UseSqlServer(cs);
                    return;
#else
                    throw new InvalidOperationException("DatabaseId not found in session");
#endif
                }

                var connectionStringSection = config.GetRequiredSection("Databases").GetRequiredSection(dbId);
                var connectionString = connectionStringSection["ConnectionString"];

                options.UseSqlServer(connectionString);

            });

            builder.Services.AddTransient<IDataAccess, DataAccess.DataAccess>();
            builder.Services.AddTransient<IDataMapper, DataMapper>();

            builder.Services.AddTransient<IStudyService, StudyService>();
            builder.Services.AddTransient<ISurveyService, SurveyService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IParticipationService, ParticipationService>();
            builder.Services.AddTransient<IUniqueCodeGenerator, UniqueCodeGenerator>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<FlamencoIdentity>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<FlamencoRole>()
                .AddRoleManager<RoleManager<FlamencoRole>>()
                .AddEntityFrameworkStores<FlamencoDb>();

            builder.Services.AddControllersWithViews();

            var oneTimeSetupTasks = AddOneTimeSetupTasksToDI(builder.Services);

            // Set a custom login URL
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Path = "/";
                options.Cookie.Name = "FlamencoSession";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            var app = builder.Build();

            //ProcessFlamencoConfiguration(builder, app);
            //ProcessOneTimeSetupTasks(oneTimeSetupTasks, app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void ProcessOneTimeSetupTasks(List<Type> oneTimeSetupTasks, WebApplication app)
        {
            // Get service scope factory
            var factory = app.Services.GetRequiredService<IServiceScopeFactory>();
            var scope = factory.CreateScope();

            foreach (var taskType in oneTimeSetupTasks)
            {
                // Get the task from DI
                var task = scope.ServiceProvider.GetRequiredService(taskType) as IOneTimeSetup;
                try
                {
                    task?.Run();
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "One-time setup task failed");
                }
            }
        }

        private static List<Type> AddOneTimeSetupTasksToDI(IServiceCollection services)
        {
            var OneTimeSetupTasks = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IOneTimeSetup).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .ToList();

            OneTimeSetupTasks.ForEach(type => services.AddTransient(type));

            return OneTimeSetupTasks;
        }

        private static void ProcessFlamencoConfiguration(WebApplicationBuilder builder, WebApplication app)
        {
            var flamencoConfigSection = builder.Configuration.GetSection("flamenco");
            if (flamencoConfigSection.Exists())
            {
                CreateDefaultUser(app, flamencoConfigSection);
            }
        }

        private static void CreateDefaultUser(WebApplication app, IConfigurationSection flamencoConfigSection)
        {
            var createUser = flamencoConfigSection.GetSection("adminUser");
            if (createUser.Exists())
            {
                var email = createUser["email"];
                if (string.IsNullOrEmpty(email))
                {
                    throw new InvalidOperationException("Email not found in configuration");
                }

                // Get the user manager from a DI factory
                var factory = app.Services.GetRequiredService<IServiceScopeFactory>();

                var userManager = factory.CreateScope().ServiceProvider.GetRequiredService<UserManager<FlamencoIdentity>>();
                if (userManager.FindByEmailAsync(email).Result != null)
                {
                    app.Logger.LogInformation("User already exists");
                    return;
                }
                var user = new FlamencoIdentity
                {
                    UserName = createUser["username"] ?? throw new InvalidOperationException("Username not found in configuration"),
                    Email = createUser["email"],
                    EmailConfirmed = true
                };

                var password = createUser["password"];
                if (string.IsNullOrEmpty(password))
                {
                    throw new InvalidOperationException("Password not found in configuration");
                }

                var result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded)
                {
                    app.Logger.LogInformation("User created successfully");
                }
                else
                {
                    app.Logger.LogError("User creation failed");
                    foreach (var error in result.Errors)
                    {
                        app.Logger.LogError(error.Description);
                    }
                }
            }
        }
    }
}
