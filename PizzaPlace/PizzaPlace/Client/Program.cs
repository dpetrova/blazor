using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PizzaPlace.Client;
using PizzaPlace.Client.Services;
using PizzaPlace.Client.Shared;
using PizzaPlace.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after" );

/* configure dependency injection */

// Transient objects are always different; a new instance is provided to every component and every service
// Scoped objects are the same for a user’s connection, but different across different users and connections (the same instance is scoped to your browser's tab)
// Singleton objects are the same for every object and every request


//builder.Services.AddTransient<IMenuService, HardCodedMenuService>();
builder.Services.AddTransient<IMenuService, MenuService>();
//builder.Services.AddTransient<IOrderService, ConsoleOrderService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//configure dependency injection to inject the State instance as a singleton
builder.Services.AddSingleton<State>();

// services doing interoperability with JavaScript
builder.Services.AddSingleton<ILocalStorage, LocalStorage>();

await builder.Build().RunAsync();
