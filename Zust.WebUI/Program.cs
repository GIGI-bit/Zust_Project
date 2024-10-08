using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zust.Business.Abstrats;
using Zust.Business.Concretes;
using Zust.DataAccess.Abstracts;
using Zust.DataAccess.Concretes;

//using Zust.WebUI.Data;
//using Zust.WebUI.Entities;
using Zust.Entities.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserDal, UserDal>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageService, ImagesService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var con = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<SocialNetworkDbContext>(opt =>
{
    opt.UseSqlServer(con);
});
builder.Services.AddIdentity<CustomIdentityUser, CustomIdentityRole>().AddEntityFrameworkStores<SocialNetworkDbContext>().AddDefaultTokenProviders();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAllOrigins");
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
