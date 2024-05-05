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
            //var currentConnectionString = connectionString ?? _configuration.GetValue<string>("MongoDBSettings:ConnectionString");
            var host = "";
            var port = 27017;

            var currentDbName = dbName ?? _configuration.GetValue<string>("MongoDBSettings:DatabaseName");



            if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
            {
                host = _configuration.GetValue<string>("MongoDBSettings:DockerMongoServerAddress");
                port = _configuration.GetValue<int>("MongoDBSettings:DockerMongoServerPort");
            }
            else
            {
                host = _configuration.GetValue<string>("MongoDBSettings:LocalMongoServerAddress");
                port = _configuration.GetValue<int>("MongoDBSettings:LocalMongoServerPort");
            }

            //var client = new MongoClient(currentConnectionString);
            var client = new MongoClient
                (
                    new MongoClientSettings
                    {
                        Server = new MongoServerAddress(host, port),
                        MaxConnecting = 99999999,
                        MaxConnectionPoolSize = 9999999,
                    }
                );

            var db = client.GetDatabase(currentDbName);
            return db;
        }



    }
}
