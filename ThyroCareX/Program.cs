using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ThyroCareX.Core;
using ThyroCareX.Infrastructure;
using ThyroCareX.Infrastructure.Context;
using ThyroCareX.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});
#region Dependancy Injection 
builder.Services.AddInfrastructureDependencies()
                .AddServiceDependencies()
                .AddCoreDependencies();
#endregion

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catalog API",
        Version = "v1",
        Description = "Catalog API for E-Commerce Application",
        Contact = new OpenApiContact()
        {
            Name = "Ahmed Ehab",
            Email = "ahmedehabahmed31@gmail.com"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = ""; // عشان تفتح Swagger على الرابط الأساسي
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
