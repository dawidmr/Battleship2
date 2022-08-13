using Battleship.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IGridCreator, GridCreator>();
builder.Services.AddScoped<IEngine, Engine>();
builder.Services.AddScoped<ISquareStateTransition, SquareStateTransition>();
builder.Services.AddScoped<IFillStrategy, ShipsVerticalFiller>();
builder.Services.AddScoped<ICoordinatesCreator, CoordinatesCreator>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
