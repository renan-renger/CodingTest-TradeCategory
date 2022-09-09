using System.Globalization;
using TradeCategory.Application.Enums;
using TradeCategory.Application.Interfaces;
using TradeCategory.Domain.Entities;

namespace TradeCategory.Infrastructure.Services
{
    public class TradeService : ITradeService
    {
        private const int TradeExpirationDays = 30; //Not exposing this yet, can be done via Trade constructor is desired.

        public ITrade CreateTrade(string _value, string _clientSector, string _nextPaymentDate, DateTime _paymentReferenceDate)
        {
            if (!double.TryParse(_value, out var _tempValue))
                throw new ArgumentException("\"Value\" part of trade data is invalid. Value must be numeric.");

            if (!DateTime.TryParseExact(_nextPaymentDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _tempNextPaymentDate))
                throw new InvalidDataException("\"Next Payment Date\" part of trade data is invalid. Date must be in format \"MM/dd/yyyy\" and must be valid.");

            return new Trade(_tempValue, _clientSector, _tempNextPaymentDate, _paymentReferenceDate);
        }

        public ITrade CreateTrade(double _value, string _clientSector, DateTime _nextPaymentDate, DateTime _paymentReferenceDate)
        {
            return new Trade(_value, _clientSector, _nextPaymentDate, _paymentReferenceDate);
        }

        /// <summary>
        /// Validates a trade based on several criteria
        /// </summary>
        /// <returns></returns>
        public TradeCategoryEnum? ValidateTrade(ITrade trade)
        {
            return IsTradeExpired(trade) ?? RiskAssessment(trade);
        }

        /// <summary>
        /// Checks is trade is expired or not, based on PaymentReferenceDate (user input) and a const expiration date
        /// </summary>
        private TradeCategoryEnum? IsTradeExpired(ITrade trade)
        {
            var expiry = trade.PaymentReferenceDate.AddDays(-TradeExpirationDays);
                switch (DateTime.Compare(trade.NextPaymentDate, expiry))
                {
                    case < 0:
                        return TradeCategoryEnum.EXPIRED;
                    default:
                        return null;
                }
}

        /// <summary>
        /// Checks trade value and client's sector for risk associated with trade
        /// </summary>
        private TradeCategoryEnum? RiskAssessment(ITrade trade)
        {
            if (trade.Value <= 1000000)
                    return null;
                switch (trade.ClientSectorParsedEnum)
                {
                    case ClientSectorEnum.Private:
                        return TradeCategoryEnum.HIGHRISK;
                    case ClientSectorEnum.Public:
                        return TradeCategoryEnum.MEDIUMRISK;
                    default:
                        return null;
                }
}
    }
}
