using BlazorMovies.Client;
using BlazorMovies.Client.Auth;
using BlazorMovies.Client.Helpers;
using BlazorMovies.Client.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
//register repositories
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//configure 3rd party js sweetalert2 library service
builder.Services.AddScoped<IDisplayMessage, DisplayMessage>();

//setup installed Tewr.Blazor.Filereader library
builder.Services.AddFileReaderService(options => options.InitializeOnFirstCall = true);

/* setup authentication and authorization services */
builder.Services.AddAuthorizationCore();
//builder.Services.AddScoped<AuthenticationStateProvider, DummyAthenticationStateProvider>(); //dummy for test
//builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(); //real

//when we are going to use a same instance for both services:
//first we need to register the instance:
builder.Services.AddScoped<JWTAuthenticationStateProvider>();
//second use the instance in both services:
builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(
    provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
);
builder.Services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(
    provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
);


await builder.Build().RunAsync();
