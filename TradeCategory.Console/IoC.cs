using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradeCategory.Application.Interfaces;
using Infra = TradeCategory.Infrastructure;

namespace TradeCategory.Console
{
    public class IoC
    {
        public IHost BuildHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddScoped<ITrade, Infra.Entities.Trade>()
                    .AddScoped<ITradeService, Infra.Services.TradeService>()
                    .AddScoped<ITradeProcessor, Infra.Processors.TradeProcessor>()
            )
            .Build();
        }
    }
}
