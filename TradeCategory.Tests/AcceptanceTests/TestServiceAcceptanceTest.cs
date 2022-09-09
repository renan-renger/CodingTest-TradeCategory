using TradeCategory.Application.Enums;
using TradeCategory.Infrastructure.Services;

namespace TradeCategory.Tests.AcceptanceTests
{
    [TestFixture]
    public class TestServiceAcceptanceTest
    {
        private TradeService _service;

        [SetUp]
        public void Setup()
        {
            _service = new TradeService();
        }

        [Test]
        public void TradeService_AcceptanceTest_Question1()
        {
            DateTime inputPaymentReferenceDate = new(2020, 12, 11); // "12/11/2020"
            var trade1 = _service.CreateTrade("2000000", "Private", "12/29/2025", inputPaymentReferenceDate);
            var trade2 = _service.CreateTrade("400000", "Public", "07/01/2020", inputPaymentReferenceDate);
            var trade3 = _service.CreateTrade("5000000", "Public", "01/02/2024", inputPaymentReferenceDate);
            var trade4 = _service.CreateTrade("3000000", "Public", "10/26/2023", inputPaymentReferenceDate);

            Assert.That(_service.ValidateTrade(trade1), Is.EqualTo(TradeCategoryEnum.HIGHRISK));
            Assert.That(_service.ValidateTrade(trade2), Is.EqualTo(TradeCategoryEnum.EXPIRED));
            Assert.That(_service.ValidateTrade(trade3), Is.EqualTo(TradeCategoryEnum.MEDIUMRISK));
            Assert.That(_service.ValidateTrade(trade4), Is.EqualTo(TradeCategoryEnum.MEDIUMRISK));

        }
    }
}
