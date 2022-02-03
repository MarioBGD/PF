using System.Collections.Generic;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL.Contracts
{
    public interface ICacheManager
    {
        Task SaveGroups(IEnumerable<Group> groups);
        Task<IEnumerable<Group>> GetGroups();
    }
}