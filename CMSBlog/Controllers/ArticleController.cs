using Bl.Contracts;
using CMSBlog.Models;
using Domain;
using Domain.Reposotry;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CMSBlog.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticleController(IArticleService articleService, ICommentService commentService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _articleService = articleService;
            _commentService = commentService;
            _signInManager = signInManager;
            _userManager = userManager;

        }


        public IActionResult Index()
        {
           var Articles = _articleService.GetAll();
            return View(Articles);
        }
        [HttpGet("article/{url}")]
        public IActionResult Details(string url)
        {

          ArticleVModel model = new ArticleVModel();
           model.Article = _articleService.GetByUrl(url);
            model.comments = _commentService.GetCommentsByArticle(model.Article.ArticleId);
            model.NewComment = new CommentViewModel() { ArticleUrl = url, ArticleId =model.Article.ArticleId};
            return View(model);
        }
        public  IActionResult AddComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment();
                comment.ArticleId=model.ArticleId;
                comment.CreatedDate = DateTime.Now;
                comment.Email = model.Email;
                comment.Name = model.Name;
                comment.CommentContent = model.CommentContent;
               if (_signInManager.IsSignedIn(User))
                {
                    var id = Guid.Parse(_userManager.GetUserId(User));
                    comment.CreatedBy = id;
                }
                _commentService.Create(comment);
         
            }

            return Redirect("~/article/" + Uri.EscapeDataString(model.ArticleUrl));
        }
    }
}
