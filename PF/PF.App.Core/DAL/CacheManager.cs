using System.Collections.Generic;
using System.Threading.Tasks;
using PF.App.Contracts.Storage;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL
{
    public class CacheManager : ICacheManager
    {
        private readonly ICacheStorage _cacheStorage;
        
        public CacheManager(ICacheStorage cacheStorage)
        {
            _cacheStorage = cacheStorage;
        }
        
        public Task SaveGroups(IEnumerable<Group> groups)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetGroups()
        {
            throw new System.NotImplementedException();
        }
    }
}