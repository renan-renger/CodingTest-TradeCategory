using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradeCategory.Application.Interfaces;
using Services = TradeCategory.Infrastructure.Services;
using Processors = TradeCategory.Infrastructure.Processors;
using TradeCategory.Domain.Entities;

namespace TradeCategory.Console
{
    public class IoC
    {
        public IHost BuildHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddScoped<ITrade, Trade>()
                    .AddScoped<ITradeService, Services.TradeService>()
                    .AddScoped<ITradeProcessor, Processors.TradeProcessor>()
            )
            .Build();
        }
    }
}
