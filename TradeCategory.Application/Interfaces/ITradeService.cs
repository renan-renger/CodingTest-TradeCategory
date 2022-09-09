using TradeCategory.Application.Enums;

namespace TradeCategory.Application.Interfaces
{
    public interface ITradeService
    {
        public ITrade CreateTrade(string _value, string _clientSector, string _nextPaymentDate, DateTime _paymentReferenceDate);
        public ITrade CreateTrade(double _value, string _clientSector, DateTime _nextPaymentDate, DateTime _paymentReferenceDate);
        public TradeCategoryEnum? ValidateTrade(ITrade trade);
    }
}
