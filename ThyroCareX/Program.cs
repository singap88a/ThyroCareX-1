using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using ThyroCareX.Core;
using ThyroCareX.Infrastructure;
using ThyroCareX.Infrastructure.Context;
using ThyroCareX.Service;
using Microsoft.AspNetCore.Identity;
using ThyroCareX.Data.Healpers;
using ThyroCareX.Data.Models.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});


// Add Serilog
builder.Host.UseSerilog((context, services, configuration) =>
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console()
        .WriteTo.File("./logs/webhook-.log", rollingInterval: RollingInterval.Day));

#region Dependancy Injection 
builder.Services.AddInfrastructureDependencies()
                .AddServiceDependencies()
                .AddCoreDependencies()
                .AddServiceRegisteration(builder.Configuration); ;
#endregion

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//var CORS = "_cors";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: CORS,
//                      policy =>
//                      {
//                          policy.AllowAnyHeader();
//                          policy.AllowAnyMethod();
//                          policy.AllowAnyOrigin();
//                      });
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = ""; // عشان تفتح Swagger على الرابط الأساسي
});


app.UseHttpsRedirection();
//app.Use(async (context, next) =>
//{
//    var host = context.Request.Host.Host;
//    var validHosts = builder.Configuration.GetSection("Stripe:WebhookHosts").Get<string[]>();
//    if (!validHosts.Any(h => host.EndsWith(h)))
//        context.Response.StatusCode = 403;
//    else
//        await next();
//});
app.UseRouting();
//app.UseCors(CORS);
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

#region Seeding
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    await RoleSeeder.SeedAsync(userManager, roleManager);
}
#endregion

app.MapControllers();

app.Run();
