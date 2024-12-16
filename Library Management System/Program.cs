using Library_Management_System.Data;
using Library_Management_System.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;                // Make the session cookie accessible only to HTTP requests
    options.Cookie.IsEssential = true;             // Required for GDPR compliance
});

 

builder.Services.AddMvc();
builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAdminLibrary, AdminLibraryService>();


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
app.UseSession(); // Enable session middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    // pattern: "{controller=Books}/{action=UserIndexBookk}/{id?}");
    pattern: "{controller=AddAdminLibrary}/{action=AdminLibraryCreate}/{id?}");
  //pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
