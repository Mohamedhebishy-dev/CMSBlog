using CMSBlog.Areas.Admin.Helpers;
using CMSBlog.Areas.Admin.Models;
using CMSBlog.Models;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;

namespace CMSBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
    
        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public IActionResult Index()
        {
          var users =  _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> UserDetails(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return BadRequest("معرف المستخدم مطلوب.");
            }

            // جلب المستخدم
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound("المستخدم غير موجود.");
            }

            // جلب الأدوار الخاصة بالمستخدم
            var roles = await _userManager.GetRolesAsync(user);

            // إنشاء ViewModel
            var model = new UserDetailsViewModel
            {
                UserId = user.Id,
                Name = user.Name,
                ImageUser = user.ImageUser,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                Roles = roles.ToList()
            };

            return View(model);
        }

        #region oldEdit
        //public async Task<IActionResult> Edit(string Id)
        //{
        //    var user = await _userManager.FindByIdAsync(Id);


        //    return View(user);
        //}
        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public async Task<IActionResult> Edit(ApplicationUser applicationUser)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    try
        //    {
        //        user.PhoneNumber = applicationUser.PhoneNumber;
        //        user.UserName = applicationUser.UserName;
        //        user.Email = applicationUser.Email;
        //        await _userManager.UpdateAsync(user);
        //        TempData["MessageType"] = MessageType.SaveSuccess;
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["MessageType"] = MessageType.SaveFailed;
        //    }

        //    return View(user);
        //} 
        #endregion
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            // التحقق من صحة المعرف
            if (string.IsNullOrEmpty(id))
            {
                return NotFound("معرف المستخدم غير موجود.");
            }

            // جلب بيانات المستخدم
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("المستخدم غير موجود.");
            }

            // جلب الأدوار الحالية للمستخدم
            var userRoles = await _userManager.GetRolesAsync(user);

            // جلب جميع الأدوار المتاحة
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            // إعداد النموذج
            var model = new EditUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserRoles = userRoles.ToList(),
                AvailableRoles = allRoles
            };

            return View(model);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("المستخدم غير موجود.");
            }

            try
            {
                // تحديث بيانات المستخدم
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    TempData["MessageType"] = MessageType.SaveFailed;
                    return View(model);
                }

                // تحديث كلمة المرور
                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                    if (!removePasswordResult.Succeeded)
                    {
                        TempData["MessageType"] = MessageType.SaveFailed;
                        ModelState.AddModelError("", "فشل إزالة كلمة المرور القديمة.");
                        return View(model);
                    }

                    var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
                    if (!addPasswordResult.Succeeded)
                    {
                        TempData["MessageType"] = MessageType.SaveFailed;
                        ModelState.AddModelError("", "فشل إضافة كلمة المرور الجديدة.");
                        return View(model);
                    }
                }

                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                // إضافة الأدوار الجديدة

                string[] RolesToAdd = model.RolesToAdd.Split(",");

                foreach (var role in RolesToAdd)
                {
                    if (!await _userManager.IsInRoleAsync(user, role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }

                TempData["MessageType"] = MessageType.SaveSuccess;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MessageType.SaveFailed;
                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = _roleManager.Roles;
            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(RegisterViewModel model,string role)
        {

            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User is null)
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
                       await _userManager.AddToRoleAsync(newUser, role);
                     
                        TempData["MessageType"] = MessageType.SaveSuccess;
                        return Redirect("~/Admin/Users");

                    }
                    foreach (var error in Result.Errors)
                    {
                        TempData["MessageType"] = MessageType.SaveFailed;
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }


            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["MessageType"] = MessageType.DeleteSuccess;
                return RedirectToAction(nameof(Index));
            }
            TempData["MessageType"] = MessageType.DeleteFailed;
            ModelState.AddModelError("", "تعذر حذف .");
            return RedirectToAction(nameof(Index));
        }

    }
}
