using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniversityStudentApi1.Repositories.Abstract;
using UniversityStudentApi1.Repositories.Concrete;
using UniversityStudentApi1.Data;
using UniversityStudentApi1.Data;
using UniversityStudentApi1.Repositories.Abstract;
using UniversityStudentApi1.Repositories.Concrete;
using Repositories.Abstract;
using Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddHttpClient<DataService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Initialize data using a scoped service provider
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    await dataService.FetchAndStoreStudentDataAsync();
    await dataService.FetchAndStoreUniversityDataAsync();
}

app.Run();
