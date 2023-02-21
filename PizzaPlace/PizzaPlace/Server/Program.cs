using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaPlace.Server;

var builder = WebApplication.CreateBuilder(args);

// allows both to access and to set up the config (in appsettings.json)
// ConfigurationManager configuration = builder.Configuration;

//add configuration providers
//builder.Configuration
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //from json file
//    .AddCommandLine(args); //from command line arguments

/* Add services to the container. */

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//tells ASP.NET Core that you will be using the PizzaPlaceDbContext and that you will be storing it in SQL Server (look up the connection string for the database in configuration)
builder.Services.AddDbContext<PizzaPlaceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PizzaDb")));


var app = builder.Build();

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

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
// method that is used for hosting your services
app.MapControllers();
// method takes care of the Blazor client project
app.MapFallbackToFile("index.html");

app.Run();
