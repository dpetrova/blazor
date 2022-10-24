using BlazorMovies.Client.Repository;
using BlazorMovies.Server;
using BlazorMovies.Server.Helpers;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//add configuration providers
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// allows both to access and to set up the config (in appsettings.json)
//ConfigurationManager configuration = builder.Configuration; 
//IWebHostEnvironment environment = builder.Environment;

// REGISTER SERVICES HERE

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//help us to serialize entities that have circular references
builder.Services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

//configure automapper (to map values of objects and updated only the values that have changed)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//for store files in Azure Storage
//builder.Services.AddScoped<IFileStorageService, AzureStorageService>();

//for store files in local machine
builder.Services.AddScoped<IFileStorageService, LocalStorageService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
//IConfiguration configuration = app.Configuration;
//IWebHostEnvironment environment = app.Environment;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// REGISTER MIDDLEWARE HERE

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
