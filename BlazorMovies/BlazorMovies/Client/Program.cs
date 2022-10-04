using BlazorMovies.Client;
using BlazorMovies.Client.Helpers;
using BlazorMovies.Client.Repository;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tewr.Blazor.FileReader;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//configure services in the dipendency injection (DI) system
//that way we will be able to request instances of these objects through DI (use @inject in components)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); //Scoped: lives within a context (e.g. during an HTTP request)
builder.Services.AddSingleton<SingletonService>(); //Singleton: single instance of the service is created
builder.Services.AddTransient<TransientService>(); //Transient: different instances of the service are created each time that service is requested
builder.Services.AddTransient<IRepository, RepositoryInMemory>(); //if IRepository is requested, then the DI system will reply with the instance of RepositoryInMemory class

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();

//setup installed Tewr.Blazor.Filereader library
builder.Services.AddFileReaderService(options => options.InitializeOnFirstCall = true);

await builder.Build().RunAsync();
