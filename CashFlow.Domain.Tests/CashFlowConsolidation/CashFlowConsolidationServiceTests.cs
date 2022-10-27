using CashFlow.Domain;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Results;
using CashFlow.Domain.Services;
using Moq;

namespace CashFlow.Domain.Tests.CashFlowConsolidation
{
    [TestClass]
    public class CashFlowConsolidationServiceTests
    {
        private Mock<ICashInRepository> _cashInRepository;
        private Mock<ICashOutRepository> _cashOutRepository;
        private Mock<IConsolidateDayRepository> _consolidateDayRepository;
        private Mock<IConsolidateMonthRepository> _consolidateMonthRepository;
        private Mock<IConsolidateYearRepository> _consolidateYearRepository;

        private List<CashInModel> _cashInModels;
        private List<CashOutModel> _cashOutModels;

        private List<ConsolidateDayModel> _consolidateDayModels;
        private List<ConsolidateMonthModel> _consolidateMonthModels;
        private List<ConsolidateYearModel> _consolidateYearModels;

        public CashFlowConsolidationServiceTests()
        {
            _cashInRepository = new Mock<ICashInRepository>();
            _cashOutRepository = new Mock<ICashOutRepository>();
            _consolidateDayRepository = new Mock<IConsolidateDayRepository>();
            _consolidateMonthRepository = new Mock<IConsolidateMonthRepository>();
            _consolidateYearRepository = new Mock<IConsolidateYearRepository>();

            _cashInModels = new List<CashInModel>();
            _cashInModels.Add(new CashInModel(1, 15.30m, new DateTime(2022, 10, 25)));
            _cashInModels.Add(new CashInModel(1, 11.30m, new DateTime(2022, 10, 25)));
            _cashInModels.Add(new CashInModel(1, 17.30m, new DateTime(2022, 10, 26)));
            _cashInModels.Add(new CashInModel(1, 18.30m, new DateTime(2022, 10, 26)));
            _cashInModels.Add(new CashInModel(1, 9.30m, new DateTime(2022, 10, 27)));
            _cashInModels.Add(new CashInModel(1, 7.30m, new DateTime(2022, 10, 27)));

            _cashOutModels = new List<CashOutModel>();
            _cashOutModels.Add(new CashOutModel(1, 9.30m, new DateTime(2022, 10, 25)));
            _cashOutModels.Add(new CashOutModel(1, 7.30m, new DateTime(2022, 10, 25)));
            _cashOutModels.Add(new CashOutModel(1, 15.30m, new DateTime(2022, 10, 26)));
            _cashOutModels.Add(new CashOutModel(1, 11.30m, new DateTime(2022, 10, 26)));
            _cashOutModels.Add(new CashOutModel(1, 17.30m, new DateTime(2022, 10, 27)));
            _cashOutModels.Add(new CashOutModel(1, 18.30m, new DateTime(2022, 10, 27)));

            _cashInRepository.Setup(c => c.SumActiveAmountByDay(It.IsAny<DateTime>(), It.IsAny<bool>()))
                .ReturnsAsync((DateTime date, bool isActive) => _cashInModels.Where(x => x.IsActive == isActive && x.Date.Date == date.Date).Select(c => c.Amount).Sum());

            _cashOutRepository.Setup(c => c.SumActiveAmountByDay(It.IsAny<DateTime>(), It.IsAny<bool>()))
                .ReturnsAsync((DateTime date, bool isActive) => _cashOutModels.Where(x => x.IsActive == isActive && x.Date.Date == date.Date).Select(c => c.Amount).Sum());

            _consolidateDayModels = new List<ConsolidateDayModel>();
            _consolidateDayRepository.Setup(c => c.Save(It.IsAny<ConsolidateDayModel>()))
                .Callback((ConsolidateDayModel entity) => _consolidateDayModels.Add(entity))
                .ReturnsAsync(true);

            _consolidateDayRepository.Setup(c => c.Get(It.IsAny<DateTime>()))
                .ReturnsAsync((DateTime date) => _consolidateDayModels.FirstOrDefault(x => x.Day == date));

            _consolidateDayRepository.Setup(c => c.SumAmountByMonth(It.IsAny<DateTime>()))
                .ReturnsAsync((DateTime month) => new ConsolidateDayResult()
                {
                    CashInAmout = _consolidateDayModels.Where(x => x.Day.Month == month.Month && x.Day.Year == month.Year)
                        .Select(x => x.CashInAmout).Sum(),
                    CashOutAmout = _consolidateDayModels.Where(x => x.Day.Month == month.Month && x.Day.Year == month.Year)
                        .Select(x => x.CashOutAmout).Sum()
                });

            _consolidateMonthModels = new List<ConsolidateMonthModel>();
            _consolidateMonthRepository.Setup(c => c.Save(It.IsAny<ConsolidateMonthModel>()))
                .Callback((ConsolidateMonthModel entity) => _consolidateMonthModels.Add(entity))
                .ReturnsAsync(true);

            _consolidateMonthRepository.Setup(c => c.Get(It.IsAny<DateTime>()))
                .ReturnsAsync((DateTime date) => _consolidateMonthModels.FirstOrDefault(x => x.Month == date));

            _consolidateMonthRepository.Setup(c => c.SumAmountByYear(It.IsAny<int>()))
                .ReturnsAsync((int year) => new ConsolidateMonthResult()
                {
                    CashInAmout = _consolidateMonthModels.Where(x => x.Month.Year == year).Select(x => x.CashInAmout).Sum(),
                    CashOutAmout = _consolidateMonthModels.Where(x => x.Month.Year == year).Select(x => x.CashOutAmout).Sum(),
                });

            _consolidateYearModels = new List<ConsolidateYearModel>();
            _consolidateYearRepository.Setup(c => c.Save(It.IsAny<ConsolidateYearModel>()))
                .Callback((ConsolidateYearModel entity) => _consolidateYearModels.Add(entity))
                .ReturnsAsync(true);

            _consolidateYearRepository.Setup(c => c.Get(It.IsAny<int>()))
                .ReturnsAsync((int year) => _consolidateYearModels.FirstOrDefault(x => x.Year == year));

        }

        [TestMethod]
        public void ConsolidateDay_when_success()
        {
            var service = new CashFlowConsolidationService(_cashInRepository.Object, _cashOutRepository.Object, _consolidateDayRepository.Object,
                _consolidateMonthRepository.Object, _consolidateYearRepository.Object, Mapper.GetMaps());

            var result = service.ConsolidateDay(new DateTime(2022, 10, 26)).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
            Assert.AreEqual(1, _consolidateDayModels.Count);
            Assert.AreEqual(1, _consolidateMonthModels.Count);
            Assert.AreEqual(1, _consolidateYearModels.Count);
        }

        [TestMethod]
        public void ConsolidateMonth_when_success()
        {
            var service = new CashFlowConsolidationService(_cashInRepository.Object, _cashOutRepository.Object, _consolidateDayRepository.Object,
                _consolidateMonthRepository.Object, _consolidateYearRepository.Object, Mapper.GetMaps());

            _consolidateDayModels.Add(new ConsolidateDayModel(new DateTime(2022, 9, 1), 50, 45));
            _consolidateDayModels.Add(new ConsolidateDayModel(new DateTime(2022, 9, 15), 80, 40));
            _consolidateDayModels.Add(new ConsolidateDayModel(new DateTime(2022, 9, 21), 70, 30));

            var result = service.ConsolidateMonth(new DateTime(2022, 9, 9)).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
            Assert.AreEqual(1, _consolidateMonthModels.Count);
        }

        [TestMethod]
        public void ConsolidateYear_when_success()
        {
            var service = new CashFlowConsolidationService(_cashInRepository.Object, _cashOutRepository.Object, _consolidateDayRepository.Object,
                _consolidateMonthRepository.Object, _consolidateYearRepository.Object, Mapper.GetMaps());

            _consolidateMonthModels.Add(new ConsolidateMonthModel(new DateTime(2022, 8, 5), 50, 45));
            _consolidateMonthModels.Add(new ConsolidateMonthModel(new DateTime(2022, 9, 5), 80, 40));
            _consolidateMonthModels.Add(new ConsolidateMonthModel(new DateTime(2022, 10, 5), 70, 30));

            var result = service.ConsolidateYear(2022).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
            Assert.AreEqual(1, _consolidateYearModels.Count);
        }
    }
}
