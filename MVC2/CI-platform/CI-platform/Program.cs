
//using CI_platform.Entities.DataModels;
using CI_platform.Models;

using CI_Platform.Entity.DataModels;
using CI_Platform.Repository.Interface;
using CI_Platform.Repository.Repository;
using Microsoft.EntityFrameworkCore;

//using CI_platform.Repository.Repository;
//using CI_platform.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
//    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
