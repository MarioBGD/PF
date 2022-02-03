using System.Collections.Generic;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Entities
{
    public interface IGroupCurrenciesRepository : IRepository<GroupCurrencyEntity>
    {
        Task<IEnumerable<GroupCurrencyEntity>> GetCurrenciesOfGroup(long groupId);
    }
}