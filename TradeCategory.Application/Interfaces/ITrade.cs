using TradeCategory.Application.Enums;

namespace TradeCategory.Application.Interfaces
{
    public interface ITrade
    {
        /// <summary>
        /// Indicates the transaction amount in dollars
        /// </summary>
        public double Value { get; init; }
        /// <summary>
        /// Indicates the client's sector which can be "Public" or "Private"
        /// </summary>
        public string ClientSector { get; init; }
        /// <summary>
        /// Parsing ClientSector into an enum for better handling during validation
        /// </summary>
        public ClientSectorEnum ClientSectorParsedEnum { get; }
        /// <summary>
        /// Indicates when the next payment from the client to the bank is expected
        /// </summary>
        public DateTime NextPaymentDate { get; init; }
        /// <summary>
        /// Reference date for payment expiry calculation, inputted by the user
        /// </summary>
        public DateTime PaymentReferenceDate { get; init; }
    }
}