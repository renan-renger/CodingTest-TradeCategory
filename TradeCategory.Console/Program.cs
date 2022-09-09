// No Main() required due to using .net 6 - Simplified console declaration

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradeCategory.Application.Interfaces;
using Infra = TradeCategory.Infrastructure;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddScoped<ITrade, Infra.Entities.Trade>()
        .AddScoped<ITradeService, Infra.Services.TradeService>()
        .AddScoped<ITradeProcessor, Infra.Processors.TradeProcessor>()
    )
    .Build();

Run(host.Services);

await host.RunAsync();


static void Run(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider serviceProvider = serviceScope.ServiceProvider;

    ITradeProcessor tradeProcessor = serviceProvider.GetRequiredService<ITradeProcessor>();
    tradeProcessor.ProcessTrades();
}