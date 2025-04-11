using Microsoft.EntityFrameworkCore;
using blog_core;
using blog_core.Data;
using blog_core.interfaces;
using blog_core.services;
using my_blog_service;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register DbContext and other services
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddSingleton<TokenService>();
//builder.Services.AddSession();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IBlogDataService, BlogDataService>();
builder.Services.AddScoped<IBlogService, BlogService>();

// JWT Service config
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// OAuth 2.0 configuration

    /*services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = "https://your-auth-server";
        options.Audience = "your-api";
    });*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseMiddleware<ApiKeyMiddleware>();
// app.UseMiddleware<BasicAuthMiddleware>();



app.UseRouting(); // Enables routing
app.UseAuthorization(); // Optional, if you have auth
app.MapControllers();

app.UseHttpsRedirection();

app.Run();
