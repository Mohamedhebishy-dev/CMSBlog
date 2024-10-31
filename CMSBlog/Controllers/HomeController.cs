using Bl.Contracts;
using CMSBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMSBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;
        public HomeController(ILogger<HomeController> logger, IArticleService articleService) : base()
        {
            _logger = logger;
            _articleService = articleService;
        }

        public IActionResult Index()
        {
       var Articles = _articleService.GetAll();
            return View(Articles);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
