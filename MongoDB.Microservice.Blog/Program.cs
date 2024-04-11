using MongoDB.Microservice.Blog.Services;

var builder = WebApplication.CreateBuilder(args);

//var configuration = builder.Configuration;
//var mongoDbConnectionString = configuration.GetValue<string>("MongoDBSettings:ConnectionString");
//var mongoDbDatabaseName = configuration.GetValue<string>("MongoDBSettings:DatabaseName");
var allowedHosts = new string[] { "http://localhost:5173","http://localhost:5022" };


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(c => c.AddPolicy("CORSpolicy", 
    p => 
    p.AllowAnyHeader()
    .AllowAnyMethod()
    //.AllowAnyOrigin()
    .WithOrigins(allowedHosts)
));


builder.Services.AddSingleton<BlogMongoDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CORSpolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
