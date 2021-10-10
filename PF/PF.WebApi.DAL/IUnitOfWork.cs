using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        public Guid Id { get; }
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; }
        public void Commit();
        public void Rollback();
    }
}
