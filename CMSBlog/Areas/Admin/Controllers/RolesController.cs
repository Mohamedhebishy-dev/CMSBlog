using CMSBlog.Areas.Admin.Helpers;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMSBlog.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager= signInManager;
            _userManager= userManager; 
            _roleManager= roleManager;
  
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

     
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ModelState.AddModelError("", "اسم الدور مطلوب");
                return View();
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(Name));

            if (result.Succeeded)
            {
                TempData["MessageType"] = MessageType.SaveSuccess;
                return RedirectToAction(nameof(Index));
            }
            TempData["MessageType"] = MessageType.SaveFailed;
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ModelState.AddModelError("", "اسم الدور مطلوب");
                return View();
            }

            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                TempData["MessageType"] = MessageType.UpdataFailed;
                return RedirectToAction(nameof(Index));
            }

            role.Name = Name;

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                TempData["MessageType"] = MessageType.UpdataSuccess;
                return RedirectToAction(nameof(Index));
            }
            TempData["MessageType"] = MessageType.UpdataFailed;
             RedirectToAction(nameof(Index));
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(role);
        }

   
     

        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                TempData["MessageType"] = MessageType.DeleteSuccess;
                return RedirectToAction(nameof(Index));
            }
            TempData["MessageType"] = MessageType.DeleteFailed;
            ModelState.AddModelError("", "تعذر حذف الدور.");
            return RedirectToAction(nameof(Index)); 
        }
    }
}
