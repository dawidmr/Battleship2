using Battleship.Client;
using Battleship.Client.Game;
using Battleship.Client.Interfaces;
using Battleship.Game.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IGridCreator, GridCreator>();
builder.Services.AddScoped<IEngine, Engine>();
builder.Services.AddScoped<IGameConfigurator, GameConfigurator>();
builder.Services.AddScoped<ISquareStateTransition, SquareStateTransition>();
builder.Services.AddScoped<IFillStrategy, ShipsVerticalFiller>();
builder.Services.AddScoped<ICoordinatesInterpreter, CoordinatesInterpreter>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
