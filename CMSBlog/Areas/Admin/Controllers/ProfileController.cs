using CMSBlog.Areas.Admin.Helpers;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMSBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            
        }
        public async Task< IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(user);
        }
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(user);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser applicationUser)
        {
                var user = await _userManager.GetUserAsync(User);
            try
            {
                user.PhoneNumber = applicationUser.PhoneNumber;
                user.UserName = applicationUser.UserName;
                user.Email = applicationUser.Email;
                await _userManager.UpdateAsync(user);
                TempData["MessageType"] = MessageType.SaveSuccess;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["MessageType"] = MessageType.SaveFailed;
            }

            return View(user);
        }
    }
}
