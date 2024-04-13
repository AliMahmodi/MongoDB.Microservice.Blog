using Flexerant.MongoMigration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace MongoDB.Microservice.Blog.MongoDB.Migrations
{
    [Migration(1)]
    public class InitMigration : Migration
    {
        public override string Description => "init MongoDB -> Adding Posts Collections";

        public override void Migrate(IMongoDatabase database)
        {

        }

        public override void MigrateAsTransaction(IMongoDatabase database, IClientSessionHandle session)
        {
            var task1 = database.CreateCollectionAsync(session, "Posts");
            var post = database.GetCollection<BlogDetails>("Posts");
            var document = new BlogDetails
            {
                Title = "Post Title",
                Body = "Post Body",
                CreateUserId = 0,
                CreateUserName = "علی محمودی",
                CreateUserImageUrl = "/users/0/profileImage.jpg",
                Id = 0
            };
            post.InsertOne(session, document);
        }
    }
}
