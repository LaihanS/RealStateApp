
using Banking.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application;
using RealStateApp.Infrastructure.Identity.Entities;
using RealStateApp.Infrastructure.Identity.Seeds;
using RealStateApp.Infrastructure.Persistence;
using RealStateApp.Infrastructure.Shared;
using WebApp.RealStateApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddApplicationInfrastructure();
builder.Services.AddPersistenceRegistration(builder.Configuration);
builder.Services.AddIdentityRegistrationForWeb(builder.Configuration);
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultAgent.SeedAsync(userManager, roleManager);
        await DefaultBasicUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {

    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
