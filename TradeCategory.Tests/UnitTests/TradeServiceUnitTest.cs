using TradeCategory.Application.Enums;
using TradeCategory.Infrastructure.Services;

namespace TradeCategory.Tests.UnitTests
{
    [TestFixture]
    public class TradeServiceUnitTest
    {
        private TradeService _service;
        [SetUp]
        public void Setup()
        {
            _service = new TradeService();
        }

        [Test]
        public void TradeService_CreateTrade_CorrectData()
        {
            string inputValue = "2000000";
            string inputClientSector = "Private";
            string inputNextPaymentDate = "12/29/2025";
            DateTime inputPaymentReferenceDate = new(2020, 12, 11); // "12/11/2020"

            var trade = _service.CreateTrade(inputValue, inputClientSector, inputNextPaymentDate, inputPaymentReferenceDate);

            Assert.That(trade.Value.ToString(), Is.EqualTo(inputValue));
            Assert.That(trade.ClientSector.ToString(), Is.EqualTo(inputClientSector));
            Assert.That(trade.NextPaymentDate.ToString("MM/dd/yyyy"), Is.EqualTo(inputNextPaymentDate));
            Assert.That(trade.PaymentReferenceDate, Is.EqualTo(inputPaymentReferenceDate));
        }

        [Test]
        public void TradeService_CreateTrade_IncorrectValue()
        {
            string expectedExceptionMessage = "\"Value\" part of trade data is invalid. Value must be numeric.";
            string inputValue = "a";
            string inputClientSector = "Private";
            string inputNextPaymentDate = "12/29/2025";
            DateTime inputPaymentReferenceDate = new(2020, 12, 11); // "12/11/2020"

            Assert.That(() => _service.CreateTrade(inputValue, inputClientSector, inputNextPaymentDate, inputPaymentReferenceDate),
                Throws.ArgumentException.With.Message.EqualTo(expectedExceptionMessage));
        }

        [Test]
        public void TradeService_CreateTrade_IncorrectClientSector()
        {
            string expectedExceptionMessage = "Client Sector informed doesn't match either \"Private\" or \"Public\"";
            string inputValue = "2000000";
            string inputClientSector = "a";
            string inputNextPaymentDate = "12/29/2025";
            DateTime inputPaymentReferenceDate = new(2020, 12, 11); // "12/11/2020"

            var trade = _service.CreateTrade(inputValue, inputClientSector, inputNextPaymentDate, inputPaymentReferenceDate);

            Assert.That(() => _service.ValidateTrade(trade),
                Throws.ArgumentException.With.Message.EqualTo(expectedExceptionMessage));
        }

        [Test]
        public void TradeService_CreateTrade_IncorrectNextPaymentDate()
        {
            string expectedExceptionMessage = "\"Next Payment Date\" part of trade data is invalid. Date must be in format \"MM/dd/yyyy\" and must be valid.";
            string inputValue = "2000000";
            string inputClientSector = "Private";
            string inputNextPaymentDate = "a";
            DateTime inputPaymentReferenceDate = new(2020, 12, 11); // "12/11/2020"

            Assert.That(() => _service.CreateTrade(inputValue, inputClientSector, inputNextPaymentDate, inputPaymentReferenceDate),
                Throws.ArgumentException.With.Message.EqualTo(expectedExceptionMessage));
        }

        [Test]
        public void TradeService_ValidateTrade_HighRisk()
        {
            string inputValue = "2000000";
            string inputClientSector = "Private";
            string inputNextPaymentDate = "12/29/2025";
            DateTime inputPaymentReferenceDate = new(2020, 12, 11); // "12/11/2020"

            var trade = _service.CreateTrade(inputValue, inputClientSector, inputNextPaymentDate, inputPaymentReferenceDate);
            var riskAssessment = _service.ValidateTrade(trade);
            Assert.That(riskAssessment, Is.Not.Null);
            Assert.That(riskAssessment, Is.EqualTo(TradeCategoryEnum.HIGHRISK));
        }

        [Test]
        public void TradeService_ValidateTrade_MediumRisk()
        {
            string inputValue = "5000000";
            string inputClientSector = "Public";
            string inputNextPaymentDate = "01/02/2024";
            DateTime inputPaymentReferenceDate = new(2020, 12, 11); // "12/11/2020"

            var trade = _service.CreateTrade(inputValue, inputClientSector, inputNextPaymentDate, inputPaymentReferenceDate);
            var riskAssessment = _service.ValidateTrade(trade);
            Assert.That(riskAssessment, Is.Not.Null);
            Assert.That(riskAssessment, Is.EqualTo(TradeCategoryEnum.MEDIUMRISK));
        }

        [Test]
        public void TradeService_ValidateTrade_NoRisk()
        {
            string inputValue = "500000";
            string inputClientSector = "Private";
            string inputNextPaymentDate = "12/29/2025";
            DateTime inputPaymentReferenceDate = new(2020, 12, 11); // "12/11/2020"

            var trade = _service.CreateTrade(inputValue, inputClientSector, inputNextPaymentDate, inputPaymentReferenceDate);
            var riskAssessment = _service.ValidateTrade(trade);
            Assert.That(riskAssessment, Is.Null);
        }

        [Test]
        public void TradeService_ValidateTrade_IsExpired()
        {
            string inputValue = "400000";
            string inputClientSector = "Public";
            string inputNextPaymentDate = "07/01/2020";
            DateTime inputPaymentReferenceDate = new(2020, 12, 11); // "12/11/2020"

            var trade = _service.CreateTrade(inputValue, inputClientSector, inputNextPaymentDate, inputPaymentReferenceDate);
            var riskAssessment = _service.ValidateTrade(trade);
            Assert.That(riskAssessment, Is.Not.Null);
            Assert.That(riskAssessment, Is.EqualTo(TradeCategoryEnum.EXPIRED));
        }
    }
}