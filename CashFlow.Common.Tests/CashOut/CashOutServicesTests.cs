using CashFlow.Domain;
using CashFlow.Domain.DTOs;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Services;
using Moq;

namespace CashFlow.Common.Tests.CashOut
{
    [TestClass]
    public class CashOutServicesTests
    {
        Mock<ICashOutRepository> _mockCashOutRepository;

        public CashOutServicesTests()
        {
            _mockCashOutRepository = new Mock<ICashOutRepository>();
        }

        [TestMethod]
        public void CashOut_when_success()
        {
            decimal amount = 50.98m;
            DateTime dateTime = DateTime.Now;

            _mockCashOutRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashOutModel>()))
                .ReturnsAsync(true);

            ICashOutRepository cashOutRepository = _mockCashOutRepository.Object;
            var service = new CashOutService(cashOutRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsActive);
            Assert.AreEqual(amount, result.Amount);
            Assert.AreEqual(dateTime, result.DateTime);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Once);
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void CashOut_when_amount_is_invalid()
        {

        }

        [TestMethod]
        public void CashOut_when_date_is_invalid_minvalue()
        {

        }

        [TestMethod]
        public void CashOut_when_date_is_invalid_maxvalue()
        {

        }

        [TestMethod]
        public void CashOut_when_throw_exception()
        {
            decimal amount = 50.98m;
            DateTime dateTime = DateTime.Now;

            _mockCashOutRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashOutModel>()))
                .Callback(() =>
                {
                    throw new Exception("Exceção de Teste", new Exception("Exceção Interna de Teste"));
                })
                .ReturnsAsync(true);

            ICashOutRepository cashOutRepository = _mockCashOutRepository.Object;
            var service = new CashOutService(cashOutRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.DateTime);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Once);
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void CashOut_cancelation_when_success()
        {

        }


        [TestMethod]
        public void CashOut_cancelation_when_not_found()
        {

        }

        [TestMethod]
        public void CashOut_cancelation_when_throw_exception()
        {

        }
    }
}
