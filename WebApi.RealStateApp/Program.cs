using Banking.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application;
using RealStateApp.Infrastructure.Identity.Entities;
using RealStateApp.Infrastructure.Identity.Seeds;
using RealStateApp.Infrastructure.Persistence;
using RealStateApp.Infrastructure.Shared;
using RealStateApp.WebApi.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddApplicationInfrastructure();
builder.Services.AddPersistenceRegistration(builder.Configuration);
builder.Services.AddIdentityRegistrationForApi(builder.Configuration);
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
    options.SuppressMapClientErrors = true;
});
builder.Services.AddHealthChecks();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultAdministrador.SeedAsync(userManager, roleManager);
        await DefaultDesarrollador.SeedAsync(userManager, roleManager);

    }
    catch (Exception ex)
    {

    }
}

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/health");
app.UseSession();

app.MapControllers();

app.Run();
