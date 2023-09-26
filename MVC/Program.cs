using AppCore.DataAccess.EntityFramework.Bases;
using Business.Services;
using DataAccess.Contexts;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MVC.Settings;

var builder = WebApplication.CreateBuilder(args);




var connectingString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectingString));

builder.Services.AddScoped(typeof(RepoBase<>), typeof(Repo<>));

builder.Services.AddScoped<IVehicleService, VehicleService>();  
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICountryService, CountryService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    

    .AddCookie(config =>
    {
        config.LoginPath = "/Account/Users/Login";
       

        config.AccessDeniedPath = "/Account/Users/AccessDenied";
       

        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        

        config.SlidingExpiration = true;
       
    });

#endregion

#region Session
builder.Services.AddSession(config =>
{
	config.IdleTimeout = TimeSpan.FromMinutes(30);
	config.IOTimeout = Timeout.InfiniteTimeSpan;
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

#region Authentication
app.UseAuthentication(); // sen kimsin?
#endregion

app.UseAuthorization(); // sen iþlem için yetkili misin?

#region Session
app.UseSession();
#endregion

#region AppSettings

var section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());
# endregion


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
