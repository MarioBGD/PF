using System.Threading.Tasks;

namespace PF.WebApi.DAL.Entities
{
    public interface IAuthRepository : IRepository<AuthEntity>
    {
        Task<AuthEntity> GetUserByLogin(string login);
    }
}