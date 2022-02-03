using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL
{
    public class MembersService : IMembersService
    {
        public void GetMembers(IMembersService.OnGroupsDataUpdate callback, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void GetTest(IMembersService.OnGroupsDataUpdate callback)
        {
            var members = new []
            {
                new Member {Id = 1, Name = "Test Member 1", Balance = 12.34m},
                new Member {Id = 2, Name = "Test Member 2", Balance = 127.34m},
                new Member {Id = 3, Name = "Test Member 3", Balance = 122.34m},
                new Member {Id = 4, Name = "Test Member 4", Balance = -212.34m},
                new Member {Id = 5, Name = "Test Member 5", Balance = -12.34m},
                new Member {Id = 6, Name = "Test Member 6", Balance = 3m}
            };

            callback?.Invoke(members);
        }
    }
}