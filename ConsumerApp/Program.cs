using ConsumerApp;
using Infrastructure.ConsoleAction;
using Infrastructure.MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Abstractions;
using Services.Implementations;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<StartApp>();
builder.Services.AddScoped<IMassTransitHelper, MassTransitHelper>();
builder.Services.AddScoped<IConsumerService, ConsumerService>();
builder.Services.AddScoped<IConsoleAction, ConsoleAction>();


using IHost host = builder.Build();

await host.RunAsync();
