using AutoMapper;
using CashFlow.Common.ExtensionsMethods;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Results;

namespace CashFlow.Domain.Services
{
    public class CashFlowConsolidationService
    {
        private ICashInRepository _cashInRepository;
        private ICashOutRepository _cashOutRepository;
        private IConsolidateDayRepository _consolidateDayRepository;
        private IConsolidateMonthRepository _consolidateMonthRepository;
        private IConsolidateYearRepository _consolidateYearRepository;
        private IMapper _mapper;

        public CashFlowConsolidationService(ICashInRepository cashInRepository, ICashOutRepository cashOutRepository,
            IConsolidateDayRepository consolidateDayRepository, IConsolidateMonthRepository consolidateMonthRepository,
            IConsolidateYearRepository consolidateYearRepository)
        {
            _cashInRepository = cashInRepository;
            _cashOutRepository = cashOutRepository;
            _consolidateDayRepository = consolidateDayRepository;
            _consolidateMonthRepository = consolidateMonthRepository;
            _consolidateYearRepository = consolidateYearRepository;
            _mapper = Mapper.GetMaps();
        }

        public async Task<bool> ConsolidateDay(DateTime date)
        {
            try
            {
                var cashInAmount = await _cashInRepository.SumActiveAmountByDay(date, true);
                var cashOutAmount = await _cashOutRepository.SumActiveAmountByDay(date, true);

                ConsolidateDayModel consolidateDayModel = await _consolidateDayRepository.Get(date);
                if (consolidateDayModel == null)
                {
                    consolidateDayModel = new ConsolidateDayModel(date, cashInAmount, cashOutAmount);
                }
                else
                {
                    consolidateDayModel.Update(cashInAmount, cashOutAmount);
                }

                var result = await _consolidateDayRepository.Save(consolidateDayModel);

                var month = new DateTime(date.Year, date.Month, 1);
                var monthResult = await ConsolidateMonth(month);
                result = (result && monthResult);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetCompleteMessage());

                return false;
            }
        }

        public async Task<bool> ConsolidateMonth(DateTime month)
        {
            try
            {
                var result = true;
                ConsolidateDayResult consolidateDayResult = await _consolidateDayRepository.SumAmountByMonth(month);

                if (consolidateDayResult != null)
                {

                    ConsolidateMonthModel consolidateMonthModel = await _consolidateMonthRepository.Get(month);

                    if (consolidateMonthModel == null)
                    {
                        consolidateMonthModel = new ConsolidateMonthModel(month, consolidateDayResult.CashInAmout, consolidateDayResult.CashOutAmout);
                    }
                    else
                    {
                        consolidateMonthModel.Update(consolidateDayResult.CashInAmout, consolidateDayResult.CashOutAmout);
                    }

                    result = await _consolidateMonthRepository.Save(consolidateMonthModel);
                }

                var yearResult = await ConsolidateYear(month.Year);
                result = (result && yearResult);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetCompleteMessage());

                return false;
            }
        }

        public async Task<bool> ConsolidateYear(int year)
        {
            try
            {
                ConsolidateMonthResult consolidateMonthResult = await _consolidateMonthRepository.SumAmountByYear(year);
                if (consolidateMonthResult == null)
                    return true;

                ConsolidateYearModel consolidateYearModel = await _consolidateYearRepository.Get(year);

                if (consolidateYearModel == null)
                {
                    consolidateYearModel = new ConsolidateYearModel(year, consolidateMonthResult.CashInAmout, consolidateMonthResult.CashOutAmout);
                }
                else
                {
                    consolidateYearModel.Update(consolidateMonthResult.CashInAmout, consolidateMonthResult.CashOutAmout);
                }

                var result = await _consolidateYearRepository.Save(consolidateYearModel);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetCompleteMessage());

                return false;
            }
        }

        public async Task<ConsolidateResult> GetConsolidated(DateTime date)
        {
            ConsolidateResult consolidateResult = new ConsolidateResult();
            var consolidateDay  = await _consolidateDayRepository.Get(date);
            var consolidateMonth = await _consolidateMonthRepository.Get(date);
            var consolidateYear = await _consolidateYearRepository.Get(date.Year);

            consolidateResult.ConsolidateDayResult = _mapper.Map<ConsolidateDayResult>(consolidateDay);
            consolidateResult.ConsolidateMonthResult = _mapper.Map<ConsolidateMonthResult>(consolidateMonth);
            consolidateResult.ConsolidateYearResult = _mapper.Map<ConsolidateYearResult>(consolidateYear);

            return consolidateResult;
        }
    }
}
