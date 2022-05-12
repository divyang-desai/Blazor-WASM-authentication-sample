using Blazored.LocalStorage;
using JwtAuth.Client;
using JwtAuth.Client.AuthProviders;
using JwtAuth.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
var configureClient = void (HttpClient client) => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>("AuthClient", configureClient);
builder.Services.AddHttpClient<IHttpService, HttpService>("HttpClient", configureClient);

await builder.Build().RunAsync();
