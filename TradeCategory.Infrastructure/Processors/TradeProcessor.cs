using TermConsole = System.Console;
using System.Globalization;
using TradeCategory.Application.Interfaces;

namespace TradeCategory.Infrastructure.Processors
{
    public class TradeProcessor : ITradeProcessor
    {
        private readonly ITradeService _service;
        public TradeProcessor(ITradeService tradeService)
        {
            _service = tradeService;
        }

        public void ProcessTrades()
        {
            var referenceDateString = TermConsole.ReadLine(); //Reading reference date
            if (!DateTime.TryParseExact(referenceDateString, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime referenceDate)) // Parsing from fixed MM/dd/YYYY into system format (i.e: dd/MM/YYYY for local machine)
                throw new InvalidDataException("Invalid reference date supplied. Date must be in format \"MM/dd/yyyy\" and must be valid.");

            var tradeAmountString = TermConsole.ReadLine(); //Reading how many trades will be inputted
            if (!int.TryParse(tradeAmountString, out int tradeAmount))
                throw new InvalidDataException("Invalid trade amount. Value must be integer and positive");

            var trades = new List<ITrade>();

            for (int i = 0; i < tradeAmount; i++)
            {
                var rawTradeData = TermConsole.ReadLine();
                if (string.IsNullOrWhiteSpace(rawTradeData))
                    throw new InvalidDataException("Trade data is invalid. Review it and try again. Format is \"<Value> <Client Sector> <Next Pending Payment>\"");

                var tradeData = rawTradeData.Split(); // As per https://docs.microsoft.com/en-us/dotnet/api/system.string.split?view=net-6.0, calling .Split() defaults to splitting by whitespace

                trades.Add(_service.CreateTrade(tradeData[0], tradeData[1], tradeData[2], referenceDate));
            }

            foreach (var trade in trades)
            {
                var evaluatedTrade = _service.ValidateTrade(trade);
                if (evaluatedTrade != null)
                    TermConsole.WriteLine(evaluatedTrade);
            }
        }
    }
}
