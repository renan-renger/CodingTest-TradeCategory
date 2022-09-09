// No Main() required due to using .net 6 - Simplified console declaration

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradeCategory.Application.Interfaces;
using TradeCategory.Console;

using IHost host = new IoC().BuildHost(args);

Run(host.Services);

await host.RunAsync();


static void Run(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider serviceProvider = serviceScope.ServiceProvider;

    ITradeProcessor tradeProcessor = serviceProvider.GetRequiredService<ITradeProcessor>();
    tradeProcessor.ProcessTrades();
}