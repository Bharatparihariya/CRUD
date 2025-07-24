using CRUD;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext (PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowExpo",
        policy => policy
            .WithOrigins(
                "http://localhost:8081",           // Optional for browser testing
                "http://172.20.10.2:8081",       // ✅ Expo Dev Server (Metro)
                "http://172.20.10.2:19000",      // ✅ Expo Go app
                "http://172.20.10.2.51:19006"       // ✅ Expo Dev Tools
            )
            .AllowAnyMethod()
            .AllowAnyHeader());
});


var app = builder.Build();

// Enable CORS

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowExpo");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
