using Flexerant.MongoMigration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace MongoDB.Microservice.Blog.MongoDB.Migrations
{
    [Migration(1)]
    public class InitMigration : Migration
    {

        private readonly IConfiguration _config;
        private readonly string postsCollectionName;

        public InitMigration(IConfiguration config)
        {
            _config = config;
            postsCollectionName = _config.GetValue<string>("MongoDBSettings:PostsCollectionName") ?? throw new Exception("MongoDBSettings:PostsCollectionName not defined in appSettings.json");
        }


        public override string Description => "init MongoDB -> Adding Posts Collections";

        public override void Migrate(IMongoDatabase database)
        {

        }

        public override void MigrateAsTransaction(IMongoDatabase database, IClientSessionHandle session)
        {
            var post = database.GetCollection<BlogDetails>(postsCollectionName);

            var names = new List<string>() { "علی", "رضا", "حسین", "محمد" };
            var families = new List<string>() { "محمودی", "رضایی", "حسینی", "محمدی", "فارسی" };

            var docs = new List<BlogDetails>();
            var random = new Random();
            for (int i = 0; i < 5000; i++)
            {
                var document = new BlogDetails
                {
                    Title = $"عنوان پست {families[random.Next(families.Count)]}",
                    Body = $"بدنه پست {families[random.Next(families.Count)]}  {families[random.Next(families.Count)]} {names[random.Next(names.Count)]} ",
                    CreateUserId = 0,
                    PublishDate = DateTime.UtcNow,
                    CreateUserName = names[random.Next(names.Count)] + " " + families[random.Next(families.Count)],
                    CreateUserMedias = new List<Media>
                    {
                       new Media {  Url =  $"/users/{i}/{MediaSize.Tumbnill.ToString()}/profileImage.jpg" , MediaSize= MediaSize.Tumbnill , MediaType = MediaType.Image },
                       new Media {  Url =  $"/users/{i}/{MediaSize.FullSize.ToString()}/profileImage.jpg" , MediaSize= MediaSize.FullSize , MediaType = MediaType.Image }
                    },
                    PostType = (PostType)random.Next(6),
                    Tags = new List<string>() 
                    {
                        names[random.Next(names.Count)] ,
                        families[random.Next(families.Count)]
                    },
                    Id = i
                };
                docs.Add(document);
            }

            post.InsertMany(session, docs);
        }
    }
}
