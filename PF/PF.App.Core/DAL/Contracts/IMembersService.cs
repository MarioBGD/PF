using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL.Contracts
{
    public interface IMembersService
    {
        delegate Task OnGroupsDataUpdate(IEnumerable<Member> groups);

        void GetMembers(OnGroupsDataUpdate callback, CancellationToken cancellationToken);
        void GetTest(OnGroupsDataUpdate callback);
    }
}