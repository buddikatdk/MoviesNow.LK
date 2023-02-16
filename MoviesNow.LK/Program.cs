using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesNow.LK.Data;
using MoviesNow.LK.Data.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MoviesNowConnection")));
builder.Services.AddIdentity<User, Role>(option =>
{
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequiredLength = 3;
    option.Password.RequireUppercase = false;
    option.Password.RequiredUniqueChars = 0;
    option.Password.RequireNonAlphanumeric = false;
    option.SignIn.RequireConfirmedEmail = true;

}).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
