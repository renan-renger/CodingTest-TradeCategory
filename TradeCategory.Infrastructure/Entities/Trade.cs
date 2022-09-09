using TradeCategory.Application.Enums;
using TradeCategory.Application.Interfaces;

namespace TradeCategory.Infrastructure.Entities
{
    public class Trade : ITrade
    {
        public Trade(double _value, string _clientSector, DateTime _nextPaymentDate, DateTime _paymentReferenceDate)
        {
            Value = _value;
            ClientSector = _clientSector;
            NextPaymentDate = _nextPaymentDate;
            PaymentReferenceDate = _paymentReferenceDate;
        }

        public double Value { get; init; }
        public string ClientSector { get; init; }
        public DateTime NextPaymentDate { get; init; }
        public DateTime PaymentReferenceDate { get; init; }
        public ClientSectorEnum ClientSectorParsedEnum
        {
            get
            {
                if (Enum.TryParse<ClientSectorEnum>(ClientSector, true, out var clientSectorEnum))
                    return clientSectorEnum;
                throw
                    new InvalidOperationException("Client Sector informed doesn't match either \"Private\" or \"Public\"");
            }
        }
    }
}