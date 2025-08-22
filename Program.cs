using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Data;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudentEnrolmentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EnrolmentConnection")));
builder.Services.AddMvc();

// Register repositories
builder.Services.AddScoped<IStudent, StudentRepo>();
builder.Services.AddScoped<ICourse, CourseRepo>();
builder.Services.AddScoped<IEnrolments, EnrolmentRepo>();

/*
builder.Services.AddTransient<IStudent, StudentRepo>();
builder.Services.AddTransient<ICourse, CourseRepo>();
builder.Services.AddTransient<IEnrolments, EnrolmentRepo>();*/

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
