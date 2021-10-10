using PF.DTO.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.Infrastructure.Interfaces.IServices
{
    public interface IPaymentService
    {
        public Task<IEnumerable<BalanceDTO>> GetBalances(List<long> usersId, Nullable<long> groupId);
    }
}
