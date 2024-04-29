using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MIS_Classroom.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); 

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<tattsContext>(item => item.UseSqlServer(config.GetConnectionString("MIS_ClassroomContext")));

builder.Services.AddSession();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
 
    app.UseHsts();
}

app.UseSession();
app.UseAuthentication(); 


app.UseRouting();

app.UseAuthorization(); 
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
