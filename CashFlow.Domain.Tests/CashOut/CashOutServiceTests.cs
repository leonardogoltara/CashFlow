using CashFlow.Domain;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Services;
using Moq;

namespace CashFlow.Domain.Tests.CashOut
{
    [TestClass]
    public class CashOutServiceTests
    {
        private Mock<ICashOutRepository> _mockCashOutRepository;

        public CashOutServiceTests()
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
            Assert.AreEqual(dateTime, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Once);
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public void CashOut_when_amount_is_invalid()
        {
            decimal amount = 0m;
            DateTime dateTime = DateTime.Now.AddMinutes(-5);

            _mockCashOutRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashOutModel>()))
                .ReturnsAsync(true);

            ICashOutRepository cashOutRepository = _mockCashOutRepository.Object;
            var service = new CashOutService(cashOutRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Never);
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public void CashOut_when_date_is_invalid_minvalue()
        {
            decimal amount = 10m;
            DateTime dateTime = DateTime.MinValue;

            _mockCashOutRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashOutModel>()))
                .ReturnsAsync(true);

            ICashOutRepository cashOutRepository = _mockCashOutRepository.Object;
            var service = new CashOutService(cashOutRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Never);
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public void CashOut_when_date_is_invalid_maxvalue()
        {
            decimal amount = 10m;
            DateTime dateTime = DateTime.MaxValue;

            _mockCashOutRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashOutModel>()))
                .ReturnsAsync(true);

            ICashOutRepository cashOutRepository = _mockCashOutRepository.Object;
            var service = new CashOutService(cashOutRepository, Mapper.GetMaps());
            var result = service.Save(amount, dateTime).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Never);
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Never);
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
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Once);
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public void CashOut_cancelation_when_success()
        {
            Domain.Models.CashOutModel cashOut = new Domain.Models.CashOutModel(197, 50.98m, DateTime.Now);

            _mockCashOutRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashOutModel>()))
                .ReturnsAsync(true);
            _mockCashOutRepository.Setup(r => r.Get(It.IsAny<long>()))
                .ReturnsAsync(cashOut);

            ICashOutRepository cashOutRepository = _mockCashOutRepository.Object;
            var service = new CashOutService(cashOutRepository, Mapper.GetMaps());
            var result = service.Cancel(cashOut.Id).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(cashOut.Amount, result.Amount);
            Assert.AreEqual(cashOut.Date, result.Date);
            Assert.IsNotNull(cashOut.CancelationDate);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Once);
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Once);
        }

        [TestMethod]
        public void CashOut_cancelation_when_not_found()
        {
            long cashOutId = 197;
            Domain.Models.CashOutModel cashOut = null;

            _mockCashOutRepository.Setup(r => r.Get(It.IsAny<long>()))
                .ReturnsAsync(cashOut);

            ICashOutRepository cashOutRepository = _mockCashOutRepository.Object;
            var service = new CashOutService(cashOutRepository, Mapper.GetMaps());
            var result = service.Cancel(cashOutId).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Once);
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Never);
        }

        [TestMethod]
        public void CashOut_cancelation_when_throw_exception_on_get()
        {
            long cashOutId = 197;
            Domain.Models.CashOutModel cashOut = new Domain.Models.CashOutModel(cashOutId, 50.98m, DateTime.Now);

            _mockCashOutRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashOutModel>()))
                .ReturnsAsync(true);

            _mockCashOutRepository.Setup(r => r.Get(It.IsAny<long>()))
                .Callback(() =>
                {
                    throw new Exception("Exceção de Teste", new Exception("Exceção Interna de Teste"));
                })
                .ReturnsAsync(cashOut);

            ICashOutRepository cashOutRepository = _mockCashOutRepository.Object;
            var service = new CashOutService(cashOutRepository, Mapper.GetMaps());
            var result = service.Cancel(cashOutId).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Once);
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Never);
        }

        [TestMethod]
        public void CashOut_cancelation_when_throw_exception_on_save()
        {
            long cashOutId = 197;
            Domain.Models.CashOutModel cashOut = new Domain.Models.CashOutModel(cashOutId, 50.98m, DateTime.Now);

            _mockCashOutRepository.Setup(r => r.Save(It.IsAny<Domain.Models.CashOutModel>()))
                .Callback(() =>
                {
                    throw new Exception("Exceção de Teste", new Exception("Exceção Interna de Teste"));
                })
                .ReturnsAsync(true);

            _mockCashOutRepository.Setup(r => r.Get(It.IsAny<long>()))
                .ReturnsAsync(cashOut);

            ICashOutRepository cashOutRepository = _mockCashOutRepository.Object;
            var service = new CashOutService(cashOutRepository, Mapper.GetMaps());
            var result = service.Cancel(cashOutId).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsActive);
            Assert.AreEqual(0, result.Amount);
            Assert.AreEqual(DateTime.MinValue, result.Date);
            Assert.IsNull(result.CancelationDate);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage));
            _mockCashOutRepository.Verify(c => c.Get(It.IsAny<long>()), Times.Once);
            _mockCashOutRepository.Verify(c => c.Save(It.IsAny<Domain.Models.CashOutModel>()), Times.Once);
        }
    }
}
