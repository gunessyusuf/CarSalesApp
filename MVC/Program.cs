using AppCore.DataAccess.EntityFramework.Bases;
using Business.Services;
using DataAccess.Contexts;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);




var connectingString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectingString));

builder.Services.AddScoped(typeof(RepoBase<>), typeof(Repo<>));

builder.Services.AddScoped<IVehicleService, VehicleService>();  
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IModelService, ModelService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
