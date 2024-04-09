var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(c => c.AddPolicy("CORSpolicy", 
    p => 
    p.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:5173")
));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CORSpolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
