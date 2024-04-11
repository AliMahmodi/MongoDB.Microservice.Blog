using MongoDB.Driver;

namespace MongoDB.Microservice.Blog.Services
{
    public class BlogMongoDbContext
    {
        private readonly IConfiguration _configuration;
        public BlogMongoDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMongoDatabase GetDatabase(string? connectionString = null, string? dbName = null)
        {
            var currentConnectionString = connectionString ?? _configuration.GetValue<string>("MongoDBSettings:ConnectionString");
            var currentDbName = dbName ?? _configuration.GetValue<string>("MongoDBSettings:DatabaseName");
            var client = new MongoClient(currentConnectionString);
            var db = client.GetDatabase(currentDbName);
            return db;
        }



    }
}
