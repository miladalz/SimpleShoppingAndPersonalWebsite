using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Products.FacadPattern;
using MyWeb.Common.Roles;
using MyWeb.Persistence.Contexts;
using MyWeb.Application.Services.Users.FacadPattern;
using MyWeb.Application.Services.Orders.FacadPattern;
using MyWeb.Application.Services.HomePages.FacadPattern;
using MyWeb.Application.Services.Common.FacadPattern;
using MyWeb.Application.Services.Finances.FacadPattern;
using MyWeb.Application.Services.Carts.FacadPattern;
using MyWeb.Application.Services.Personals.FacadPattern;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
    options.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
    options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/Authentication/Signin");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
    options.AccessDeniedPath= new PathString("/Authentication/Signin");
});


builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();

//FacadeInject
builder.Services.AddScoped<IUserFacad, UserFacad>();
builder.Services.AddScoped<IProductFacad, ProductFacad>();
builder.Services.AddScoped<IOrderFacad, OrderFacad>();
builder.Services.AddScoped<IHomePageFacad, HomePageFacad>();
builder.Services.AddScoped<ICommonFacad, CommonFacad>();
builder.Services.AddScoped<IFinancesFacad, FinancesFacad>();
builder.Services.AddScoped<ICartsFacad, CartsFacad>();
builder.Services.AddScoped<IPersonalFacad, PersonalFacad>();

string contectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=MyWebDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(contectionString));
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
                   name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
