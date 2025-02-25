using CMSBlog.Models;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace CMSBlog.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
        }
        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            if (string.IsNullOrEmpty(ReturnUrl))
                 return View();
            HttpContext.Response.Cookies.Append("ReturnUrl", ReturnUrl);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)

            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User is null)
                    return View();
                var resultSignIn = await _signInManager.PasswordSignInAsync(User, model.Password, true, true);



                if (resultSignIn.Succeeded)
                {
                    if (string.IsNullOrEmpty(HttpContext.Request.Cookies["ReturnUrl"]))
                        return Redirect("~/");
                   return Redirect(HttpContext.Request.Cookies["ReturnUrl"]);
                }
            }

            return View(model);
        }
        public async Task< IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
          return  Redirect("~/");
        }

        [HttpGet]
        public IActionResult Register()
        { 

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
               var User = await _userManager.FindByEmailAsync(model.Email);
                if(User is null)
                {
                    ApplicationUser newUser = new ApplicationUser
                    {
                        Email = model.Email,
                        Name = model.Name,
                        UserName = model.Email,
                        Blocked = false
                    };
           
                  var Result = await _userManager.CreateAsync(newUser, model.Paassord);
                   if (Result.Succeeded)
                    {
                     var Login = await _signInManager.PasswordSignInAsync(model.Email,model.Paassord,true,true);
                        if (Login.Succeeded)
                        {
                              return Redirect("/Admin");

                        }
                    }
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            
            
            }
            return View();
        }
      
    }
}
