using AutoMapper;
using PF.DTO.Expenses;
using PF.WebApi.DAL;
using PF.WebApi.DAL.Repositories;
using PF.WebApi.Entities.Procedures;
using PF.WebApi.Infrastructure.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.BLL.Services
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
