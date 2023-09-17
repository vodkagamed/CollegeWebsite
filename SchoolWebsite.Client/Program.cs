using AKSoftware.Localization.MultiLanguages;
using Microsoft.AspNetCore.DataProtection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("D:\\projects\\C#\\InnoTasks\\SchoolWebProject\\SchoolApi"))
    .SetDefaultKeyLifetime(TimeSpan.FromDays(15));
builder.Services.AddLanguageContainerForBlazorServer<EmbeddedResourceKeysProvider>(Assembly.GetExecutingAssembly(), "Resources");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7104") });
builder.Services.AddHttpClient();
builder.Services.AddScoped(typeof(GenericService<>));
builder.Services.AddScoped(typeof(CollegeService));
builder.Services.AddScoped(typeof(CourseService));
builder.Services.AddScoped(typeof(StudentService));
builder.Services.AddScoped(typeof(TeacherService));
builder.Services.AddScoped<DataProtector>();
builder.Services.AddScoped<LogService>();
builder.Services.AddTransient(typeof(ValidationMessages));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
