using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Microservice.Blog.MongoDbExtensions;
using MongoDB.Microservice.Blog.Services;
using System;
using System.Diagnostics;
using System.Threading;

namespace MongoDB.Microservice.Blog.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {


        private readonly ILogger<HomeController> _logger;
        private readonly BlogMongoDbContext _db;
        private readonly IConfiguration _config;
        private readonly string postsCollectionName;


        public HomeController(ILogger<HomeController> logger, BlogMongoDbContext db, IConfiguration config)
        {
            _logger = logger;
            _db = db;
            _config = config;
            postsCollectionName = _config.GetValue<string>("MongoDBSettings:PostsCollectionName");
        }

        [HttpGet]
        //public async Task<IReadOnlyList<BlogDetails>> Get(int pageSize = 10, int pageIndex = 1)
        public async Task<List<BlogDetails>> Get(int pageSize = 10, int pageIndex = 1)
        {
            var db = _db.GetDatabase();

            var sort = Builders<BlogDetails>.Sort.Descending(x => x.Id);
            var filter = Builders<BlogDetails>.Filter.Empty;

            //var t=Stopwatch.StartNew();

            //var data= await db.GetCollection<BlogDetails>(postsCollectionName).AggregateByPageAsync(filter, sort,pageIndex,pageSize);
            //t.Stop();
            //var ttt= t.ElapsedMilliseconds;

            //t.Restart();

            var blogs = await db.GetCollection<BlogDetails>(postsCollectionName)
                .Find(Builders<BlogDetails>.Filter.Empty)
                .Sort(sort)
                .Skip((pageIndex - 1) * pageSize)
                .Limit(pageSize)
                //.Project(x => new BlogEntity { id = x._id, Title = x.Title, publishDate = x.publishDate })
                .ToListAsync();

            //t.Stop();
            //var tttt = t.ElapsedMilliseconds;

            return blogs;
            //return data.data;
        }

        [HttpGet]
        public async Task<BlogDetails> Details(int id)
        {
            try
            {
            var db = _db.GetDatabase();
            var filter = Builders<BlogDetails>.Filter.Eq(e => e.Id, id);
            var blogs = await db.GetCollection<BlogDetails>(postsCollectionName)
                .Find(filter)
                .FirstOrDefaultAsync();

            return blogs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
