using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace CodeFandango.Flamenco.Web.Portal.Controllers
{
    public class DbFreeAccountController : Controller
    {
        private readonly IHttpContextAccessor http;
        private IEnumerable<IConfigurationSection> databases;

        public DbFreeAccountController(IConfiguration config, IHttpContextAccessor http)
        {
            databases = config.GetSection("Databases").GetChildren();
            this.http = http;
        }

        [Route("/account/login")]
        public IActionResult Login(string returnUrl = "/")
        {
            if (databases.Count() == 1)
            {
                http.HttpContext?.Response.Cookies.Append("DatabaseId", databases.First().Key);
            }

            return View("Views/Account/Login.cshtml");
        }

    }

    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<FlamencoIdentity> signIn;
        private readonly UserManager<FlamencoIdentity> userManager;
        private readonly IHttpContextAccessor http;

        public AccountController(SignInManager<FlamencoIdentity> signIn, UserManager<FlamencoIdentity> userManager, IHttpContextAccessor http)
        {
            this.signIn = signIn;
            this.userManager = userManager;
            this.http = http;
        }
 
        [AllowAnonymous]
        [HttpPost("/account/login")]
        public IActionResult Login(string username, string password, string returnUrl = "/")
        {
            // Login using the signin manager
            var result = signIn.PasswordSignInAsync(username, password, false, false).Result;
            if (result.Succeeded)
            {
                // get the claims principal
                var user = userManager.FindByNameAsync(username).Result;
                var principal = signIn.CreateUserPrincipalAsync(user).Result;
                SignIn(principal);

                return Redirect(returnUrl ?? "/");
            }

            return Unauthorized();
        }

        [Route("/account/logout")]
        public IActionResult Logout(string returnUrl = "/")
        {
            signIn.SignOutAsync().Wait();
            http.HttpContext?.Response.Cookies.Delete("DatabaseId");
            return Redirect(returnUrl ?? "/");
        }
    }
}
