using System.Threading.Tasks;

namespace PF.WebApi.DAL.Entities
{
    public interface IUsersRepository : IRepository<UserEntity>
    {
        Task<UserEntity> GetByLogin(string login);
    }
}