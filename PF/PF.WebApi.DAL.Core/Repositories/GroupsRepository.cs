using Dapper;
using PF.WebApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Core.Repositories
{
    public class GroupsRepository : Repository<GroupEntity>, IGroupsRepository
    {
        public GroupsRepository(IUnitOfWork uow) : base(uow.Connection, uow.Transaction, "Groups")
        {

        }
    }
}
