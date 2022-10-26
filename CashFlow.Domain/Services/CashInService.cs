using AutoMapper;
using CashFlow.Domain.DTOs;
using CashFlow.Common.ExtensionsMethods;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;

namespace CashFlow.Domain.Services
{
    public class CashInService
    {
        private ICashInRepository _cashInRepository;
        private IMapper _mapper;

        public CashInService(ICashInRepository cashInRepository, IMapper mapper)
        {
            _cashInRepository = cashInRepository;
            _mapper = mapper;
        }

        public async Task<CashInResult> Save(decimal amount, DateTime dateTime)
        {
            CashInResult cashInResult = new CashInResult();

            if (amount < 0)
            {
                cashInResult.SetErrorMessage("O valor da entrada é obrigatório.");
                return cashInResult;
            }

            if (dateTime <= DateTime.MinValue || dateTime >= DateTime.MaxValue)
            {
                cashInResult.SetErrorMessage("A data da entrada está inválida.");
                return cashInResult;
            }

            try
            {
                var cashIn = new CashInModel(amount, dateTime);
                await _cashInRepository.Save(cashIn);
                cashInResult = _mapper.Map<CashInResult>(cashIn);

                return cashInResult;
            }
            catch (Exception ex)
            {
                cashInResult = new CashInResult();
                cashInResult.SetErrorMessage(ex.GetCompleteMessage());

                return cashInResult;
            }
        }

        public async Task<CashInResult> Cancel(int id)
        {
            CashInResult cashInCanceledResult = new CashInResult();

            try
            {
                var cashIn = await _cashInRepository.Get(id);

                if (cashIn == null)
                {
                    cashInCanceledResult.SetErrorMessage("Entrada não encontrada");
                    return cashInCanceledResult;
                }

                cashIn.Cancel();
                await _cashInRepository.Save(cashIn);
                cashInCanceledResult = _mapper.Map<CashInResult>(cashIn);

                return cashInCanceledResult;
            }
            catch (Exception ex)
            {
                cashInCanceledResult = new CashInResult();
                cashInCanceledResult.SetErrorMessage(ex.GetCompleteMessage());

                return cashInCanceledResult;
            }
        }
    }
}
