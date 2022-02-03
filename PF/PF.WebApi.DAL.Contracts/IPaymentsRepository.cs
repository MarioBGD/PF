using System.Collections.Generic;
using System.Threading.Tasks;
using PF.WebApi.DAL.Entities.Procedures;

namespace PF.WebApi.DAL.Entities
{
    public interface IPaymentsRepository : IRepository<PaymentEntity>
    {
        Task<IEnumerable<PaymentEntity>> GetPaymentsOfUser(long userId, IEnumerable<long> expensesIds = null);
        Task<IEnumerable<PaymentEntity>> GetPayments(IEnumerable<long> expensesIds);
        Task<IEnumerable<BalanceData>> GetBalances(IEnumerable<long> usersIds);
        Task<IEnumerable<BalanceData>> GetBalancesInGroup(IEnumerable<long> usersIds, long groupId);
    }
}