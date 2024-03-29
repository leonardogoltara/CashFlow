using CashFlow.Common.Messaging;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Services;
using Moq;

namespace CashFlow.Domain.Tests.CashIn
{
    [TestClass]
    public class CashInServiceTests
    {
        private Mock<ICashInRepository> _mockCashInRepository;
        private Mock<IMessageSender> _messageSender;

        public CashInServiceTests()
        {
            _mockCashInRepository = new Mock<ICashInRepository>();
            _messageSender = new Mock<IMessageSender>();
        }

        [TestMethod]
        public void CashIn_when_success()
        {
            decimal amount = 50.98m;
            DateTime dateTime = DateTime.Now;

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, _messageSender.Object);
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsActive);
            Assert.AreEqual(amount, result.Amount);
            Assert.AreEqual(dateTime, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_when_amount_is_invalid()
        {
            decimal amount = 0m;
            DateTime dateTime = DateTime.Now.AddMinutes(-5);

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, _messageSender.Object);
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Never);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_when_date_is_invalid_minvalue()
        {
            decimal amount = 10m;
            DateTime dateTime = DateTime.MinValue;

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, _messageSender.Object);
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Never);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_when_date_is_invalid_maxvalue()
        {
            decimal amount = 10m;
            DateTime dateTime = DateTime.MaxValue;

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, _messageSender.Object);
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Never);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_when_throw_exception()
        {
            decimal amount = 50.98m;
            DateTime dateTime = DateTime.Now;

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .Callback(() =>
                {
                    throw new Exception("Exce��o de Teste", new Exception("Exce��o Interna de Teste"));
                })
                .ReturnsAsync(true);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, _messageSender.Object);
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_cancelation_when_success()
        {
            Domain.Models.CashInModel cashIn = new Domain.Models.CashInModel(197, 50.98m, DateTime.Now);

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);
            _mockCashInRepository.Setup(r => r.Get(It.IsAny<long>()))
                .ReturnsAsync(cashIn);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, _messageSender.Object);
            var result = service.Cancel(cashIn.Id).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(cashIn.Amount, result.Amount);
            Assert.AreEqual(cashIn.Date, result.Date);
            Assert.IsNotNull(cashIn.CancelationDate);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Once);
        }

        [TestMethod]
        public void CashIn_cancelation_when_not_found()
        {
            long cashInId = 197;
            Domain.Models.CashInModel cashIn = null;

            _mockCashInRepository.Setup(r => r.Get(It.IsAny<long>()))
                .ReturnsAsync(cashIn);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, _messageSender.Object);
            var result = service.Cancel(cashInId).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_cancelation_when_throw_exception_on_get()
        {
            long cashInId = 197;
            Domain.Models.CashInModel cashIn = new Domain.Models.CashInModel(cashInId, 50.98m, DateTime.Now);

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .ReturnsAsync(true);

            _mockCashInRepository.Setup(r => r.Get(It.IsAny<long>()))
                .Callback(() =>
                {
                    throw new Exception("Exce��o de Teste", new Exception("Exce��o Interna de Teste"));
                })
                .ReturnsAsync(cashIn);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, _messageSender.Object);
            var result = service.Cancel(cashInId).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Never);
        }

        [TestMethod]
        public void CashIn_cancelation_when_throw_exception_on_save()
        {
            long cashInId = 197;
            Domain.Models.CashInModel cashIn = new Domain.Models.CashInModel(cashInId, 50.98m, DateTime.Now);

            _mockCashInRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashInModel>()))
                .Callback(() =>
                {
                    throw new Exception("Exce��o de Teste", new Exception("Exce��o Interna de Teste"));
                })
                .ReturnsAsync(true);

            _mockCashInRepository.Setup(r => r.Get(It.IsAny<long>()))
                .ReturnsAsync(cashIn);

            ICashInRepository cashInRepository = _mockCashInRepository.Object;
            var service = new CashInService(cashInRepository, _messageSender.Object);
            var result = service.Cancel(cashInId).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashInRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Once);
            _mockCashInRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashInModel>()), Times.Once);
        }
    }
}