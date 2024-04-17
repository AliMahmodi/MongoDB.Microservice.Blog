using Flexerant.MongoMigration;
using MongoDB.Driver;
using MongoDB.Microservice.Blog.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var mongoDbConnectionString = configuration.GetValue<string>("MongoDBSettings:ConnectionString") ?? throw new Exception("please Define 'MongoDBSettings:ConnectionString' in appSettings.json'");
var mongoDbDatabaseName = configuration.GetValue<string>("MongoDBSettings:DatabaseName");

var allowedHosts = new string[] { "http://localhost:5174", "http://localhost:5173", "http://localhost:5022" };


// Add services to the container.
builder.Services.AddMongoMigrations(options =>
{
    IMongoClient mongoClient = new MongoClient(mongoDbConnectionString);
    options.MongoDatabase = mongoClient.GetDatabase(mongoDbDatabaseName);
});



builder.Services.AddControllers();
builder.Services.AddCors(c => c.AddPolicy("CORSpolicy",
    p =>
    p.AllowAnyHeader()
    .AllowAnyMethod()
    //.AllowAnyOrigin()
    .WithOrigins(allowedHosts)
));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<BlogMongoDbContext>();


var app = builder.Build();

app.UseMongoMigrations();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}


// Configure the HTTP request pipeline.
app.UseCors("CORSpolicy");
app.UseAuthorization();

app.MapSwagger();
app.MapControllers();


app.Run();
