using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PF.App.Contracts.Storage;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL
{
    public class GroupsService : IGroupsService
    {
        private readonly ICacheStorage _cacheStorage;
        private readonly IDataGetterService _dataGetterService;
        private const string _groupsStorageKey = "GROUPS";
        
        public GroupsService(ICacheStorage cacheStorage, IDataGetterService dataGetterService)
        {
            _cacheStorage = cacheStorage;
            _dataGetterService = dataGetterService;
        }

        public void GetTest(IGroupsService.OnGroupsDataUpdate callback)
        {
            var groups = new Group[]
            {
                new Group {Id = 1, Name = "Group1"},
                new Group {Id = 2, Name = "Group2"},
                new Group {Id = 3, Name = "Group3"},
                new Group {Id = 4, Name = "Group4"},
                new Group {Id = 5, Name = "Group5"},
                new Group {Id = 6, Name = "Group6"},
                new Group {Id = 7, Name = "Group7"},
                new Group {Id = 8, Name = "Group8"},
                new Group {Id = 6, Name = "Group9"},
                new Group {Id = 7, Name = "Group10"},
                new Group {Id = 8, Name = "Group11"},
            };

            callback?.Invoke(groups);
        }
        
        public void GetGroups(IGroupsService.OnGroupsDataUpdate callback, CancellationToken cancellationToken)
        {
            Task.Run(async () => await GetGroupsAsync(callback, cancellationToken));
        }

        private async Task GetGroupsAsync(IGroupsService.OnGroupsDataUpdate callback, CancellationToken cancellationToken)
        {
            bool cacheExpired = _cacheStorage.IsDataExpired(_groupsStorageKey);

            if (!cacheExpired)
            {
                var cachedGroups = await _cacheStorage.GetData<List<Group>>(_groupsStorageKey);

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                if (cachedGroups.Any())
                {
                    await callback?.Invoke(cachedGroups);
                }
            }

            var onlineGroupsDto = await _dataGetterService.GetAllMyGroups();
            
            //TODO: check is there a diff

            var groups = onlineGroupsDto.Select(x =>
                new Group
                {
                    Id = x.Id,
                    Name = x.Name
                });

            if (!cancellationToken.IsCancellationRequested)
            {
                await callback?.Invoke(groups);
            }
            
            await _cacheStorage.SaveData(groups.ToList(), _groupsStorageKey);
        }
    }
}