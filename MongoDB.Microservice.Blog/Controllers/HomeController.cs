using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.Microservice.Blog.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {

        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<List<BlogEntity>> Get(int pageSize = 10, int pageIndex = 1)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var filter = Builders<BlogDetails>.Filter.Empty;
            var blogs = client.GetDatabase("MongoBlog").GetCollection<BlogDetails>("Blogs")
                .Find(Builders<BlogDetails>.Filter.Empty)
                .SortByDescending(e => e.publishDate)
                .Skip((pageIndex - 1) * pageSize)
                .Limit(pageSize)
                .Project(x => new BlogEntity {id = x.id, Title = x.Title, publishDate = x.publishDate })
                .ToListAsync();

            return blogs;
        }

        [HttpGet]
        public Task<BlogDetails> Details(string  id)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var filter = Builders<BlogDetails>.Filter.Empty;
            var blogs = client.GetDatabase("MongoBlog").GetCollection<BlogDetails>("Blogs")
                .Find(Builders<BlogDetails>.Filter.Eq(e=>e.id ,id ))
                .FirstOrDefaultAsync();

            return blogs;
        }
    }
}
