using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeesPortal.Data;
using EmployeesPortal.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EmployeesPortalContextConnection") ?? throw new InvalidOperationException("Connection string 'EmployeesPortalContextConnection' not found.");

builder.Services.AddDbContext<EmployeesPortalContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<EmployeesPortalUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<EmployeesPortalContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=List}/{id?}");

app.Run();
