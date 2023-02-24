using HouseRenting.Core.Contracts;
using HouseRenting.Core.Services;
using HouseRenting.Data;
using HouseRenting.Data.Entities;
using HouseRenting.Web.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HouseRentingDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", opt =>
    {
        opt.AllowAnyOrigin();
        opt.AllowAnyMethod();
    });
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
       {
           options.SignIn.RequireConfirmedAccount = false;
           options.Password.RequireLowercase = false;
           options.Password.RequireNonAlphanumeric = false;
           options.Password.RequireUppercase = false;
           options.Password.RequireDigit = false;
       })
       .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HouseRentingDbContext>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});


builder.Services.AddTransient<IHouseService, HouseService>();
builder.Services.AddTransient<IAgentService, AgentService>();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{

    //app.UseExceptionHandler("/Home/Error/500");
    //app.UseExceptionHandler("/Home/Error?statusCode={0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("all");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "House Details",
        pattern: "{controller=Houses}/{action=Details}/{id}/{information}");
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

app.SeedAdmin();
app.Run();
