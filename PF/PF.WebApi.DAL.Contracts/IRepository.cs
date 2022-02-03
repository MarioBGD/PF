using System.Collections.Generic;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Entities
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Get(long id);
        Task<IEnumerable<T>> GetRange(IEnumerable<long> ids);
        Task<long> Add(T entity, bool withId = false);
        Task<long> AddRange(IEnumerable<T> entities);
        Task<long> Remove(long id);
        Task<long> Update(T entity);
    }
}