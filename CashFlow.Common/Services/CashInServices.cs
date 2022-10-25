using CashFlow.Common.DTOs;
using CashFlow.Common.Models;
using CashFlow.Common.Repository;

namespace CashFlow.Common.Services
{
    public class CashInServices
    {
        private ICashInRepository _cashInRepository;

        public CashInServices(ICashInRepository cashInRepository)
        {
            _cashInRepository = cashInRepository;
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

            var cashIn = new CashIn(amount, dateTime);
            _cashInRepository.Save(cashIn);

            return cashInResult;
        }
    }
}
