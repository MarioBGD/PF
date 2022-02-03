using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL.Contracts
{
    public interface IUserInfo
    {
        Task<User> GetMyUserData();
        Task<User> GetUserData(long id);
    }
}