using AutoMapper;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Results;
using System;

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
            IConsolidateYearRepository consolidateYearRepository, IMapper mapper)
        {
            _cashInRepository = cashInRepository;
            _cashOutRepository = cashOutRepository;
            _consolidateDayRepository = consolidateDayRepository;
            _consolidateMonthRepository = consolidateMonthRepository;
            _consolidateYearRepository = consolidateYearRepository;
            _mapper = mapper;
        }

        public async Task<bool> ConsolidateDay(DateTime dateTime)
        {
            var cashInAmount = await _cashInRepository.SumActiveAmountByDay(dateTime, true);
            var cashOutAmount = await _cashOutRepository.SumActiveAmountByDay(dateTime, true);

            ConsolidateDayModel consolidateDayModel = await _consolidateDayRepository.Get(dateTime);
            if (consolidateDayModel == null)
            {
                consolidateDayModel = new ConsolidateDayModel(dateTime, cashInAmount, cashOutAmount);
            }
            else
            {
                consolidateDayModel.Update(cashInAmount, cashOutAmount);
            }

            var result = await _consolidateDayRepository.Save(consolidateDayModel);

            var month = new DateTime(dateTime.Year, dateTime.Month, 1);
            var monthResult = await ConsolidateMonth(month);
            result = (result && monthResult);

            return result;
        }

        public async Task<bool> ConsolidateMonth(DateTime month)
        {
            ConsolidateDayResult consolidateDayResult = await _consolidateDayRepository.SumAmountByMonth(month);
            ConsolidateMonthModel consolidateMonthModel = await _consolidateMonthRepository.Get(month);

            if (consolidateMonthModel == null)
            {
                consolidateMonthModel = new ConsolidateMonthModel(month, consolidateDayResult.CashInAmout, consolidateDayResult.CashOutAmout);
            }
            else
            {
                consolidateMonthModel.Update(consolidateDayResult.CashInAmout, consolidateDayResult.CashOutAmout);
            }

            var result = await _consolidateMonthRepository.Save(consolidateMonthModel);

            var yearResult = await ConsolidateYear(month.Year);
            result = (result && yearResult);

            return result;
        }

        public async Task<bool> ConsolidateYear(int year)
        {
            try
            {
                ConsolidateMonthResult consolidateMonthResult = await _consolidateMonthRepository.SumAmountByYear(year);
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
            catch (Exception)
            {
                return false;
            }
        }
    }
}
