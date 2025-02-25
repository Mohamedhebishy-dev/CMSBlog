using Bl.Contracts;
using Bl.Services;
using CMSBlog.Areas.Admin.Helpers;
using CMSBlog.Models;
using CMSBlog.Utlities;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;

namespace CMSBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SiteSettingsController : Controller
    {
         private readonly UserManager<ApplicationUser> _userManager;
         private readonly ISiteSettingsService _siteSettingsService;
        private readonly ICategoryService _categoryService;
        public SiteSettingsController(UserManager<ApplicationUser> userManager, ISiteSettingsService siteSettingsService, ICategoryService categoryService)
        {
            _userManager = userManager;
            _siteSettingsService = siteSettingsService;
            _categoryService = categoryService;
        }
        // GET: SiteSettings
        public IActionResult Index()
        {
            return View();
        }

        // GET: SiteSettings/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: SiteSettings/Create
        [HttpGet]
        public async Task< IActionResult> PrimarySettings()
        {
            PrimaryViewModel Model = new PrimaryViewModel();
         var siteSettings = await  _siteSettingsService.GetSiteSettingsAsync();
          
              //var menuItems = siteSettings.Menu != null
              //  ? JsonConvert.DeserializeObject<List<MegaMenuItem>>(siteSettings.Menu)
              //  : new List<MegaMenuItem>();
            //Model.Menu = menuItems;
            //Model.WebSiteName = siteSettings.WebSiteName;
            //Model.Adress = siteSettings.Adress;
            //Model.EmailAddress = siteSettings.EmailAddress;
            //Model.Description = siteSettings.Description;
            //Model.FacebookLink = siteSettings.FacebookLink;
            //Model.InstagramLink = siteSettings.InstagramLink;
            //Model.LinkedInLink = siteSettings.LinkedInLink;
            //Model.XLink = siteSettings.XLink;
            //Model.ColumnLinks1 = siteSettings.ColumnLinks1;
            //Model.ColumnLinks2 = siteSettings.ColumnLinks2;
            //Model.ContactNumber = siteSettings.ContactNumber;
            //Model.LogoName = siteSettings.LogoName;
           
            return View(siteSettings);
        }

        // POST: SiteSettings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult >PrimarySettings(SiteSettings siteSettings, List<IFormFile> Files)
        {
            try
            {

                    var ImageName = await Helper.UploadImage(Files, "WebSite", "Images");
                if (string.IsNullOrEmpty(ImageName))
                {
                    var siteSettingsOld = await _siteSettingsService.GetSiteSettingsAsync();
                    siteSettings.LogoName = siteSettingsOld.LogoName;
                    await _siteSettingsService.UpdateSiteSettingsAsync(siteSettings);
                    TempData["MessageType"] = MessageType.SaveSuccess;
                    return RedirectToAction("PrimarySettings");
                }
                    siteSettings.LogoName = ImageName;

                await _siteSettingsService.UpdateSiteSettingsAsync(siteSettings);
                TempData["MessageType"] = MessageType.SaveSuccess;
                return RedirectToAction("PrimarySettings");
            }

            catch
            {
                return View(siteSettings);
            }
        }

        // GET: SiteSettings/Edit/5
        public async Task< IActionResult> HomeSettings(int id)
        {
           var settings = await _siteSettingsService.GetHomeSettingsAsync(id);
            var categories = _categoryService.GetAll();
            ViewBag.Categories = categories;
             string[] existingCategoriesSettings = settings.value.Split(",");
            List<Category> existingCategories = new List<Category>();
            foreach (var item in existingCategoriesSettings)
            {
                existingCategories.Add(_categoryService.GetById(Guid.Parse(item)));

            }
            ViewBag.existingCategories = existingCategories;
            return View(settings);
        }

        // POST: SiteSettings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HomeSettings(int id, string selectedIds)
        {
            try
            {
                HomePageSettings settings = await _siteSettingsService.GetHomeSettingsAsync(id);
               
                settings.value = selectedIds;
              await _siteSettingsService.Update(settings);
                TempData["MessageType"] = MessageType.SaveSuccess;
                return Redirect($@"~/admin/SiteSettings/HomeSettings?id={id}");
            }
            catch
            {
                TempData["MessageType"] = MessageType.SaveFailed;
                return Redirect($@"~/admin/SiteSettings/HomeSettings?id={id}");
            }
        }

        // GET: SiteSettings/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: SiteSettings/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
