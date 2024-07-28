using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace CodeFandango.Flamenco.Web.Portal.Setup
{
    public class ConfigureRoles : IOneTimeSetup
    {
        private readonly RoleManager<FlamencoRole> roleManager;
        private readonly IConfiguration config;
        private readonly UserManager<FlamencoIdentity> userManager;

        public ConfigureRoles(RoleManager<FlamencoRole> roleManager, IConfiguration config, UserManager<FlamencoIdentity> userManager)
        {
            this.roleManager = roleManager;
            this.config = config;
            this.userManager = userManager;
        }

        public void Run()
        {
            string[] roles = new string[] { "DatabaseAdmin", "SupplierAdmin", "ClientAdmin" };

            foreach (string role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    var identityRole = new FlamencoRole
                    {
                        Name = role
                    };

                    IdentityResult result = roleManager.CreateAsync(identityRole).Result;
                }
            }

            // Get the admin user from the local configuration
            var flamencoConfigSection = config.GetSection("flamenco");
            if (flamencoConfigSection.Exists())
            {
                var adminUser = flamencoConfigSection.GetSection("adminUser");
                if (adminUser.Exists())
                {
                    string email = adminUser["email"] ?? throw new Exception("Unable to assign role to admin user - email not found.");
                    var user = userManager.FindByEmailAsync(email).Result;

                    if (user != null)
                    {
                        foreach (var role in roles)
                        {
                            if (!userManager.IsInRoleAsync(user, role).Result)
                            {
                                userManager.AddToRoleAsync(user, role).Wait();
                            }
                        }
                    }
                }
            }

        }
    }
}
    