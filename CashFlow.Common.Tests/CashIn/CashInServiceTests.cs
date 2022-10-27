using CashFlow.Domain;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Services;
using Moq;

namespace CashFlow.Common.Tests.CashIn
{
    [TestClass]
    public class CashInServiceTests
    {
        private Mock<ICashInRepository> _mockCashInRepository;

        public CashInServiceTests()
        {
            _mockCashInRepository = new Mock<ICashInRepository>();
        }

        [TestMethod]
        public void CashIn_when_success()
        {
            decimal amount = 50.98m;
            DateTime dateTime = DateTime.Now;

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsActive);
            Assert.AreEqual(amount, result.Amount);
            Assert.AreEqual(dateTime, result.DateTime);
            Assert.IsNull(result.CancelationDate);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_when_amount_is_invalid()
        {
            decimal amount = 0m;
            DateTime dateTime = DateTime.Now.AddMinutes(-5);

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.DateTime);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Never);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_when_date_is_invalid_minvalue()
        {
            decimal amount = 10m;
            DateTime dateTime = DateTime.MinValue;

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.DateTime);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Never);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_when_date_is_invalid_maxvalue()
        {
            decimal amount = 10m;
            DateTime dateTime = DateTime.MaxValue;

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.DateTime);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Never);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_when_throw_exception()
        {
            decimal amount = 50.98m;
            DateTime dateTime = DateTime.Now;

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .Callback(() =>
                {
                    throw new Exception("Exceção de Teste", new Exception("Exceção Interna de Teste"));
                })
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.DateTime);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_cancelation_when_success()
        {
            Domain.Models.CashInModel cashIn = new Domain.Models.CashInModel(197, 50.98m, DateTime.Now);

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);
            _mockCashInRepository.Setup(r => r.Get(It.IsAny<int>()))
                .ReturnsAsync(cashIn);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, Mapper.GetMaps());
            var result = service.Cancel(cashIn.Id).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(cashIn.Amount, result.Amount);
            Assert.AreEqual(cashIn.DateTime, result.DateTime);
            Assert.IsNotNull(cashIn.CancelationDate);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Once);
        }

        [TestMethod]
        public void CashIn_cancelation_when_not_found()
        {
            int cashInId = 197;
            Domain.Models.CashInModel cashIn = null;

            _mockCashInRepository.Setup(r => r.Get(It.IsAny<int>()))
                .ReturnsAsync(cashIn);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, Mapper.GetMaps());
            var result = service.Cancel(cashInId).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.DateTime);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_cancelation_when_throw_exception_on_get()
        {
            int cashInId = 197;
            Domain.Models.CashInModel cashIn = new Domain.Models.CashInModel(cashInId, 50.98m, DateTime.Now);

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            _mockCashInRepository.Setup(r => r.Get(It.IsAny<int>()))
                .Callback(() =>
                {
                    throw new Exception("Exceção de Teste", new Exception("Exceção Interna de Teste"));
                })
                .ReturnsAsync(cashIn);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, Mapper.GetMaps());
            var result = service.Cancel(cashInId).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.DateTime);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_cancelation_when_throw_exception_on_save()
        {
            int cashInId = 197;
            Domain.Models.CashInModel cashIn = new Domain.Models.CashInModel(cashInId, 50.98m, DateTime.Now);

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .Callback(() =>
                {
                    throw new Exception("Exceção de Teste", new Exception("Exceção Interna de Teste"));
                })
                .ReturnsAsync(true);

            _mockCashInRepository.Setup(r => r.Get(It.IsAny<int>()))
                .ReturnsAsync(cashIn);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, Mapper.GetMaps());
            var result = service.Cancel(cashInId).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.DateTime);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<int>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Once);
        }
    }
}