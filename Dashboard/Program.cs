using ApplicationService.ServiceImplementation;
using AutoMapper;
using DomainService.Repo;
using DomainService.UnitOfWork;
using Infrastructure.Context;
using Infrastructure.Mapping;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Dashboard")));
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<Context, Context>();
builder.Services.AddScoped(typeof(IRepo<>), typeof(RepoImplementation<>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

// logic services
builder.Services.AddScoped<ClinicService, ClinicService>();
builder.Services.AddScoped<PatientService, PatientService>();
builder.Services.AddScoped<DoctorService, DoctorService>();
builder.Services.AddScoped<WorkingDayService, WorkingDayService>();
builder.Services.AddScoped<DoctorWorkingDayService, DoctorWorkingDayService>();
builder.Services.AddScoped<PatientAppointmentService, PatientAppointmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.Environment.ContentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
app.UseStaticFiles();
app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
