using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PF.DTO.Expenses;

namespace PF.WebApi.BLL.Contracts
{
    public interface IPaymentService
    {
        public Task<IEnumerable<BalanceDTO>> GetBalances(List<long> usersId, Nullable<long> groupId);
    }
}
