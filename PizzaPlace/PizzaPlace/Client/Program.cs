using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PizzaPlace.Client;
using PizzaPlace.Client.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after" );

/* configure dependency injection */

// Transient objects are always different; a new instance is provided to every component and every service
// Scoped objects are the same for a user’s connection, but different across different users and connections (the same instance is scoped to your browser's tab)
// Singleton objects are the same for every object and every request

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<IMenuService, HardCodedMenuService>();
builder.Services.AddTransient<IOrderService, ConsoleOrderService>();

await builder.Build().RunAsync();
