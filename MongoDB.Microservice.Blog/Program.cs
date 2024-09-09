using Flexerant.MongoMigration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MongoDB.Microservice.Blog.Services;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var mongoDbConnectionString = "";
if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
{
    mongoDbConnectionString = configuration.GetValue<string>("MongoDBSettings:DockerConnectionString")
      ?? throw new Exception("please Define 'MongoDBSettings:DockerConnectionString' in appSettings.json'");
}
else
{
    mongoDbConnectionString = configuration.GetValue<string>("MongoDBSettings:LocalConnectionString") 
        ?? throw new Exception("please Define 'MongoDBSettings:LocalConnectionString' in appSettings.json'");

}

var mongoDbDatabaseName = configuration.GetValue<string>("MongoDBSettings:DatabaseName");

var allowedHosts = new string[] { "http://localhost:5174", "http://localhost:5173", "http://localhost:5022" };

//adding serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

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
builder.Services.AddSwaggerGen(ac =>
    {
        ac.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Blogs Api", Version = "1.0.0" });
        ac.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Scheme = "bearer",
            Description = "Please insert JWT token into field"
        });

        ac.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });

builder.Services.AddSingleton<BlogMongoDbContext>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", op =>
        {
            //op.Authority = "http://localhost:5023/JwtTokenHandler/test";
            //op.Validate();
            op.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false, ValidateIssuer = false, ValidateIssuerSigningKey = false, IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("xl123mw@yahoo.comxl123mw@yahoo.com")) };
            op.RequireHttpsMetadata = false;
        });
builder.Services.AddAuthorization(
    op => op.AddPolicy("BlogsPolicy", policy => policy.RequireClaim("scope", "Blog"))
    );

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
}


if (app.Environment.IsDevelopment())
{
    //adding serilog
    app.UseSerilogRequestLogging();

    //adding swagger
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseMongoMigrations();

// Configure the HTTP request pipeline.
app.UseCors("CORSpolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapSwagger();
app.MapControllers();


app.Run();
