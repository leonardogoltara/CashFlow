﻿using AutoMapper;
using CashFlow.Common.ExtensionsMethods;
using CashFlow.Common.JsonHelper;
using CashFlow.Common.Messaging;
using CashFlow.Domain.DTOs;
using CashFlow.Domain.Messaging;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;

namespace CashFlow.Domain.Services
{
    public class CashOutService
    {
        private ICashOutRepository _cashOutRepository;
        private IMessageSender _messageSender;
        private IMapper _mapper;

        public CashOutService(ICashOutRepository cashOutRepository, IMessageSender messageSender)
        {
            _cashOutRepository = cashOutRepository;
            _mapper = Mapper.GetMaps();
            _messageSender = messageSender;
        }

        public async Task<CashOutResult> Save(decimal amount, DateTime date)
        {
            CashOutResult cashOutResult = new CashOutResult();

            if (amount <= 0)
            {
                cashOutResult.SetErrorMessage("O valor da saída é obrigatório.");
                return cashOutResult;
            }

            if (date <= DateTime.MinValue || date >= DateTime.MaxValue)
            {
                cashOutResult.SetErrorMessage("A data da saída está inválida.");
                return cashOutResult;
            }

            try
            {
                var cashOut = new CashOutModel(amount, date);
                await _cashOutRepository.Save(cashOut);
                cashOutResult = _mapper.Map<CashOutResult>(cashOut);

                MessageModel message = new MessageModel()
                {
                    RoutingKey = typeof(CashOutModel).Name,
                    Body = JsonUtils.Serialize(cashOutResult)
                };
                await _messageSender.Send(message);

                return cashOutResult;
            }
            catch (Exception ex)
            {
                cashOutResult = new CashOutResult();
                cashOutResult.SetErrorMessage(ex.GetCompleteMessage());

                return cashOutResult;
            }

        }

        public async Task<CashOutResult> Cancel(long id)
        {
            CashOutResult cashOutCanceledResult = new CashOutResult();

            try
            {
                var cashOut = await _cashOutRepository.Get(id);

                if (cashOut == null)
                {
                    cashOutCanceledResult.SetErrorMessage("Saída não encontrada");
                    return cashOutCanceledResult;
                }

                cashOut.Cancel();
                await _cashOutRepository.Save(cashOut);
                cashOutCanceledResult = _mapper.Map<CashOutResult>(cashOut);

                MessageModel message = new MessageModel()
                {
                    RoutingKey = typeof(CashOutModel).Name,
                    Body = JsonUtils.Serialize(cashOutCanceledResult)
                };
                await _messageSender.Send(message);

                return cashOutCanceledResult;
            }
            catch (Exception ex)
            {
                cashOutCanceledResult = new CashOutResult();
                cashOutCanceledResult.SetErrorMessage(ex.GetCompleteMessage());

                return cashOutCanceledResult;
            }
        }
    }
}
