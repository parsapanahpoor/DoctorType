using DoctorType.Application.SiteServices;
using DoctorType.Data.DbContext;
using DoctorType.IoC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

#region Service

#region mvc

builder.Services.AddControllersWithViews();

builder.Services.AddMvc();

#endregion

#region Session

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

#endregion

#region PWA

builder.Services.AddProgressiveWebApp("/Manifest/manifest.json");

#endregion

#region Add DBContext

builder.Services.AddDbContext<DoctorTypeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DoctorTypeConnectionString"));
});

#endregion

#region Data Protection

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory() + "\\wwwroot\\Auth\\"))
    .SetApplicationName("DoctorType")
    .SetDefaultKeyLifetime(TimeSpan.FromDays(30));

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})

// Add Cookie settings
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
    });

#endregion

#region Encoder

builder.Services.AddSingleton<HtmlEncoder>(
    HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

#endregion

#region Register Services

DependencyContainer.RegisterServices(builder.Services);

#endregion

#region SignalR

builder.Services.AddSignalR();

#endregion

#endregion

#region MiddleWares

var app = builder.Build();

app.UseDeveloperExceptionPage();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

SiteCurrentContext.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion
