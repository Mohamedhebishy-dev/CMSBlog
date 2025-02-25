using Microsoft.AspNetCore.Mvc;
using Bl.Contracts;
using Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
      private readonly  IArticleService _ArticleService;
        
        public ArticleController(IArticleService articleService)
        {
            _ArticleService = articleService;
        }



        // GET: api/<ArticleController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult <IEnumerable<Article>>> GetAll ()
        {
            var articles =await _ArticleService.GetAllAsync();
            return Ok(articles);
        }



        //// GET: api/<ArticleController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2","0000000" };
        //}

        // GET api/<ArticleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ArticleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
