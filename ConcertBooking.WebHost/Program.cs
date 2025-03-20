using ConcertBooking.Repositories.Implementations;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TechnologyKeeda.ConcertBooking.Repositories;
using Microsoft.AspNetCore.Identity;
using ConcertBooking.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ConcertBooking.WebHost"))
);


 builder.Services.AddIdentity<ApplicationUser,IdentityRole>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.AddScoped<IDbInitial,DbInitial>();
builder.Services.AddScoped<IVenueRepo,VenueRepo>();
builder.Services.AddScoped<IConcertRepo,ConcertRepo>();
builder.Services.AddScoped<IArtistRepo,ArtistRepo>();
builder.Services.AddScoped<IUtilityRepo,UtilityRepo>();
builder.Services.AddScoped<ITicketRepo,TicketRepo>();
builder.Services.AddScoped<IEmailSender,EmailSender>();
builder.Services.AddScoped<IBookingRepo,BookingRepo>();

builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddRazorPages();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

DataSeeding();

void DataSeeding()
{
    using (var scope=app.Services.CreateScope())
    {
        var _dpRepo = scope.ServiceProvider.GetRequiredService <IDbInitial>();
        _dpRepo.Seed();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
