using Microsoft.AspNetCore.DataProtection;
using SchoolApi.Repos;
using SchoolApi.Data;
using SchoolWebsite.shared;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<CollegeRepo>();
builder.Services.AddScoped<TeachersRepo>();
builder.Services.AddScoped<CoursesRepo>();
builder.Services.AddScoped<StudentsRepo>();
builder.Services.AddScoped<LogRepo>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("D:\\projects\\C#\\InnoTasks\\SchoolWebProject\\SchoolApi"))
    .SetDefaultKeyLifetime(TimeSpan.FromDays(15));
builder.Services.AddSingleton<DataProtector>();
builder.Services.AddCors(x => x.AddPolicy("MyPolicy", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyMethod()));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
