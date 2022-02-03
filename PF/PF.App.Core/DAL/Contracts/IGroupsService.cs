using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL.Contracts
{
    public interface IGroupsService
    {
        delegate Task OnGroupsDataUpdate(IEnumerable<Group> groups);

        void GetGroups(OnGroupsDataUpdate callback, CancellationToken cancellationToken);
        void GetTest(OnGroupsDataUpdate callback);
    }
}