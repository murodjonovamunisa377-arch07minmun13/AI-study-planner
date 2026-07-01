// See https://aka.ms/new-console-template for more information

using AIStudyPlanner.Data;
using AIStudyPlanner.Repositories;
using AIStudyPlanner.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Inject Infrastructure Components
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=StudyPlanner.db"));

// Map Architectural Contracts for Clean Inversion of Control (IoC)
builder.Services.AddScoped<IStudyPlanRepository, StudyPlanRepository>();
builder.Services.AddTransient<IAISchedulingService, AISchedulingService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
