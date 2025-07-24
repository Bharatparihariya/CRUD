using CRUD;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Configure PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔧 Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔧 Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowExpo", policy =>
        policy.WithOrigins(
                "http://localhost:8081",
                "http://172.20.10.2:8081",
                "http://172.20.10.2:19000",
                "http://172.20.10.51:19006" // ⚠️ typo fixed from .2.51 to .51
            )
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// ✅ Use Swagger always (not just in Development)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD API V1");
    c.RoutePrefix = string.Empty; // show Swagger at root
});

// ✅ Use dynamic port for Render.com
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// Enable middleware
app.UseCors("AllowExpo");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
