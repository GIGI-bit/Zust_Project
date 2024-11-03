using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zust.Business.Abstrats;
using Zust.Business.Concretes;
using Zust.DataAccess.Abstracts;
using Zust.DataAccess.Concretes;

//using Zust.WebUI.Data;
//using Zust.WebUI.Entities;
using Zust.Entities.Models;
using Zust.WebUI.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserDal, UserDal>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageService, ImagesService>();
builder.Services.AddScoped<IPostDal, PostDal>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentDal,CommentDal>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFriendRequestDal, FriendRequestDal>();
builder.Services.AddScoped<IFriendRequestService, FriendRequestService>();
builder.Services.AddScoped<IFriendDal, FriendDal>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IMessageDal, MessageDal>();
builder.Services.AddScoped<IMessageService,MessageSevice>();
builder.Services.AddScoped<IChatDal, ChatDal>();
builder.Services.AddScoped<IChatService, ChatService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var con = builder.Configuration.GetConnectionString("Production");
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

app.MapHub<ChatHub>("/chathub");


app.Run();
