using Microsoft.AspNetCore.Authorization;
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
    [Authorize("BlogsPolicy")]
    [Authorize(Roles ="User")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly BlogMongoDbContext _db;
        private readonly IConfiguration _config;
        private readonly string postsCollectionName;

        public BlogController(ILogger<BlogController> logger, BlogMongoDbContext db, IConfiguration config)
        {
            _logger = logger;
            _db = db;
            _config = config;
            postsCollectionName = _config.GetValue<string>("MongoDBSettings:PostsCollectionName") ?? throw new Exception("MongoDBSettings:PostsCollectionName not defined in appSettings.json");
        }

        [Route("[controller]/[action]")]
        [HttpGet]
        //public async Task<IReadOnlyList<BlogDetails>> Get(int pageSize = 10, int pageIndex = 1)
        public async Task<List<BlogModel>> GetPagedAsync(int pageSize = 10, int pageIndex = 1, CancellationToken cancellationToken = default)
        {
            try
            {
                var totalTime = Stopwatch.StartNew();

                if (pageIndex < 1 || pageIndex > 5000000 || pageSize >= 50 || pageSize < 1 || pageIndex * pageSize >= 5000000)
                {
                    return new List<BlogModel>();
                }

                var db = _db.GetDatabase();

                var sort = Builders<BlogDetails>.Sort.Descending(x => x.Id);
                var filter = Builders<BlogDetails>.Filter.Empty;

                var t = Stopwatch.StartNew();

                //var data = await db.GetCollection<BlogDetails>(postsCollectionName).AggregateByPageAsync(filter, sort, pageIndex, pageSize);
                t.Stop();
                var ttt = t.ElapsedMilliseconds;

                //await Task.Delay(5000, cancellationToken);

                t.Restart();

                var blogs = await db.GetCollection<BlogDetails>(postsCollectionName)
                    .Find(Builders<BlogDetails>.Filter.Empty)
                    .Sort(sort)
                    .Skip((pageIndex - 1) * pageSize)
                    .Limit(pageSize)
                    .Project(x => new BlogModel { Id = x.Id, Title = x.Title, Body = x.Body , PublishDate = x.PublishDate })
                    .ToListAsync(cancellationToken);

                foreach (var blog in blogs)
                {
                    blog.PublishDateStr = blog.PublishDate?.DateInDeatilWithTimePersian();
                }
                t.Stop();
                var tttt = t.ElapsedMilliseconds;
                //await Task.Delay(5000, cancellationToken);
                totalTime.Stop();
                
                _logger.LogInformation("method1 : {ttt} ms , method2 : {tttt} ms , totalTime : {totalTime}",ttt,tttt, totalTime.ElapsedMilliseconds );
                return blogs;
                //return data.data;
            }
            catch
            {
                if (cancellationToken.IsCancellationRequested == true)
                    return new List<BlogModel>();
                else
                    throw;
            }
        }

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<BlogDetails> Post(int id)
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

        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<bool> CreateAsync(BlogDetails blog)
        {
            var db = _db.GetDatabase();
            //db.GetCollection<>
            return false;
        }
        

    }
}
