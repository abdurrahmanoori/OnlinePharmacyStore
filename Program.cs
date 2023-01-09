using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlinePharmacyStore.DataAccess.Data;
using OnlinePharmacyStore.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



var connectionString = builder.Configuration.GetConnectionString("DBCS");
builder.Services.AddDbContext<AppDbContext>(option =>
option.UseSqlServer(connectionString));

var app = builder.Build();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>();



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
               pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
               );
app.Run();



















//services.AddRazorPages().AddRazorRuntimeCompilation();//Run Razor page code quickly.
//                                                      //First install RazorPage.Runtimecompilation.


//services.AddDbContext<AppDbContext>(option =>
//option.UseSqlServer(Configuration.GetConnectionString("DBCS")));



////services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
////.AddEntityFrameworkStores<AppDbContext>();


//services.AddIdentity<ApplicationUser, IdentityRole>()
//.AddEntityFrameworkStores<AppDbContext>();

//services.AddControllersWithViews();

//services.AddMvc(config =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//                     .RequireAuthenticatedUser()
//                     .Build();
//    config.Filters.Add(new AuthorizeFilter(policy));
//});


//services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/Identity/Account/Login";  //in your case /Account/Login
//    options.LogoutPath = "/Identity/Account/logout";
//    options.AccessDeniedPath = "/Visitor/Error/AccessDenied";// In case of access denied.
//});

//services.AddAuthentication().AddCookie();

//services.Configure<IdentityOptions>(option =>//Override indentity default password ruls.
//{
//    option.Password.RequireDigit = false;
//    option.Password.RequiredLength = 1;
//    option.Password.RequireLowercase = false;
//    option.Password.RequireNonAlphanumeric = false;
//    option.Password.RequireUppercase = false;
//});
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//{


//    //app.Run(context => { throw new Exception("Thestt"); });
//    if (env.IsDevelopment())
//    {
//        app.UseDeveloperExceptionPage();

//    }
//    else
//    {
//        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

//        app.UseStatusCodePagesWithRedirects("Error/{0}");
//        app.UseExceptionHandler("/Visitor/Error/Error");
//        app.UseHsts();
//    }
//    app.UseHttpsRedirection();
//    app.UseStaticFiles();

//    app.UseRouting();



//    app.UseAuthentication();
//    app.UseAuthorization();

//    //app.UseMvc(routes =>
//    //{
//    //    routes.MapRoute(
//    //      name: "areas",
//    //      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    //    );
//    //});

//    app.UseEndpoints(endpoints =>
//    {

//        //    endpoints.MapAreaControllerRoute(
//        //name: "myVisitor",
//        //areaName: "Visitor",
//        //pattern: "Visitor/{controller=Home}/{action=Index}/{id?}");

//        endpoints.MapControllerRoute(
//            name: "default",
//            pattern: "{Area=Visitor}/{controller=Home}/{action=Index}/{id?}");


//    });
//    ;