using eShop.Application.Catalog.Products;
using eShop.Application.Common;
using eShop.Data.EF;
using eShop.Utilities.Constants;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    WebRootPath = Directory.GetCurrentDirectory(),
});

builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connect to Database
builder.Services.AddDbContext<eShopDbContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString(SystemConstants.MainConnectionString)));

//Register Services
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddScoped<IPublicProductService, PublicProductService>();
builder.Services.AddScoped<IManageProductService, ManageProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

