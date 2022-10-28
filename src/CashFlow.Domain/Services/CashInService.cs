using AutoMapper;
using CashFlow.Domain.DTOs;
using CashFlow.Common.ExtensionsMethods;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;
using CashFlow.Common.Messaging;
using CashFlow.Domain.Messaging;
using System.Text.Json.Nodes;

namespace CashFlow.Domain.Services
{
    public class CashInService
    {
        private ICashInRepository _cashInRepository;
        private IMessageSender _messageSender;
        private IMapper _mapper;

        public CashInService(ICashInRepository cashInRepository, IMessageSender messageSender)
        {
            _cashInRepository = cashInRepository;
            _mapper = Mapper.GetMaps();
            _messageSender = messageSender;
        }

        public async Task<CashInResult> Save(decimal amount, DateTime date)
        {
            CashInResult cashInResult = new CashInResult();

            if (amount <= 0)
            {
                cashInResult.SetErrorMessage("O valor da entrada é obrigatório.");
                return cashInResult;
            }

            if (date <= DateTime.MinValue || date >= DateTime.MaxValue)
            {
                cashInResult.SetErrorMessage("A data da entrada está inválida.");
                return cashInResult;
            }

            try
            {
                var cashIn = new CashInModel(amount, date);
                await _cashInRepository.Save(cashIn);
                cashInResult = _mapper.Map<CashInResult>(cashIn);

                MessageModel message = new MessageModel()
                {
                    Body = cashIn.Date
                };
                await _messageSender.Send(message);

                return cashInResult;
            }
            catch (Exception ex)
            {
                cashInResult = new CashInResult();
                cashInResult.SetErrorMessage(ex.GetCompleteMessage());

                return cashInResult;
            }
        }

        public async Task<CashInResult> Cancel(long id)
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

                MessageModel message = new MessageModel()
                {
                    Body = cashIn.Date
                };
                await _messageSender.Send(message);

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
