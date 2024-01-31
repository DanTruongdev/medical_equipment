using MedicalEquipmentWeb.Data;
using MedicalEquipmentWeb.Models;
using MedicalEquipmentWeb.Services;
using MedicalEquipmentWeb.Services.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;
var defaultConnection = config.GetConnectionString("DefaultConnection");
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
//Dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
 opts.UseLazyLoadingProxies().UseMySql(defaultConnection, ServerVersion.AutoDetect(defaultConnection)));
//Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
   {
     options.LoginPath = "/Authentication/Login";
     options.LogoutPath = "/Authentication/Logout";
     options.ExpireTimeSpan = TimeSpan.FromDays(3);      
   })
  .AddGoogle(googleOptions =>
  {
      googleOptions.ClientId = config["GoogleAuthentication:ClientID"];
      googleOptions.ClientSecret = config["GoogleAuthentication:ClientSecret"];
  });


//Services
var emailConfig = config.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
