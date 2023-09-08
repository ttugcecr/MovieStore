using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using MovieStore.BusinessLayer.Abstract;
using MovieStore.BusinessLayer.Concrete;
using MovieStore.DataAccessLayer.Abstract;
using MovieStore.DataAccessLayer.Concrete;
using MovieStore.DataAccessLayer.EntityFramework;
using MovieStore.EntityLayer.Concrete;
using MovieStore.ShopApp.WebUI.FakePaymentService;
using MovieStore.ShopApp.WebUI.Models;
using MovieStore.ShopApp.WebUI.SendMail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.





builder.Services.AddDbContext<Context>();

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomerIdentityValidator>().AddDefaultTokenProviders();

//builder.Services.AddDefaultIdentity<AppUser>().AddRoleManager<IdentityRole>().AddErrorDescriber<CustomerIdentityValidator>().AddEntityFrameworkStores<Context>();


//builder.Services.Configure<IdentityOptions>(options =>
//{
//    //password bölumu
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequireNonAlphanumeric = true;


//    //Lockout bölümü
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.AllowedForNewUsers = true;

//    // options.User.AllowedUserNameCharecters="";
//    options.User.RequireUniqueEmail = true;
//    options.SignIn.RequireConfirmedEmail = true;
//    options.SignIn.RequireConfirmedPhoneNumber = false;

//});
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/account/login";
//    options.LogoutPath = "/account/logout";
//    options.AccessDeniedPath = "/account/accessdenied";
//    options.SlidingExpiration = true;//bu sure verýyor býze ve ýslem yapmsak cýkýs yapýyor.ýslem yaparsak erýlen sure sýfýrlanýr bastan baslar
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);//yukardaký sure ýle ýstenýldgýnde oynanablýr

//    options.Cookie = new CookieBuilder
//    {
//        HttpOnly = true,
//        Name = ".ShopAppUygulama.Security.Cookie",
//        SameSite = SameSiteMode.Strict

//    };
//});

builder.Services.AddHttpClient();
builder.Services.AddScoped<IFakePaymentService, FakePaymentManager>();

builder.Services.AddScoped<ICategoryDAL, EFCategoryDAL>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IProductDAL, EFProductDAL>();
builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddScoped<ICartDAL, EFCartDAL>();
builder.Services.AddScoped<ICartService, CartManager>();

builder.Services.AddScoped<IOrderDal, EFOrderDal>();
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddScoped<ISendEMail, SendEMail>();



builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.LoginPath = "/Account/Login/";
});



builder.Services.AddControllersWithViews();
 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
