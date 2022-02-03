using AutoMapper;
using PF.DTO.Expenses;
using PF.WebApi.DAL.Core;
using PF.WebApi.DAL.Core.Repositories;
using PF.WebApi.DAL.Entities.Procedures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PF.WebApi.BLL.Contracts;

namespace PF.WebApi.BLL.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;

        public PaymentService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<BalanceDTO>> GetBalances(List<long> usersId, Nullable<long> groupId)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var paymentsRepo = new PaymentsRepository(uow);
                IEnumerable<BalanceData> balances;

                if (groupId.HasValue)
                {
                    balances = await paymentsRepo.GetBalancesInGroup(usersId, groupId.Value);
                }
                else
                {
                    balances = await paymentsRepo.GetBalances(usersId);
                }
                IEnumerable<BalanceDTO> balancesDTO = _mapper.Map<IEnumerable<BalanceDTO>>(balances);

                return balancesDTO;
            }
        }
    }
}
