using CashFlow.Domain;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Services;
using Moq;

namespace CashFlow.Common.Tests.CashIn
{
    [TestClass]
    public class CashInServicesTests
    {
        Mock<ICashInRepository> _mockCashInRepository;

        public CashInServicesTests()
        {
            _mockCashInRepository = new Mock<ICashInRepository>();
        }

        [TestMethod]
        public void CashIn_when_success()
        {
            decimal amount = 13.97m;
            DateTime dateTime = DateTime.Now.AddMinutes(-5);

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsActive);
            Assert.AreEqual(amount, result.Amount);
            Assert.AreEqual(dateTime, result.DateTime);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_when_amount_is_invalid()
        {

        }

        [TestMethod]
        public void CashIn_when_date_is_invalid_minvalue()
        {

        }

        [TestMethod]
        public void CashIn_when_date_is_invalid_maxvalue()
        {

        }

        [TestMethod]
        public void CashIn_when_throw_exception()
        {

        }

        [TestMethod]
        public void CashIn_cancelation_when_success()
        {

        }


        [TestMethod]
        public void CashIn_cancelation_when_not_found()
        {

        }

        [TestMethod]
        public void CashIn_cancelation_when_throw_exception()
        {

        }
    }
}