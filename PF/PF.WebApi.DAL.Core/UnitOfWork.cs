using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly Guid _id;
        protected readonly IDbConnection _connection;
        protected IDbTransaction _transaction;

        public Guid Id => _id;
        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        public UnitOfWork(IDbConnection connection, IsolationLevel il = IsolationLevel.ReadCommitted)
        {
            _id = new Guid();
            _connection = connection;

            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            _transaction = _connection.BeginTransaction(il);
        }

        public UnitOfWork(IsolationLevel il = IsolationLevel.ReadCommitted)
        {
            _id = new Guid();
            _connection = new SqlConnection(Config.DbConnectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction(il);
        }

        public virtual void Commit()
        {
            _transaction.Commit();
            _transaction.Dispose();
        }
        
        /// <summary>
        /// Discard all changes by transaction
        /// </summary>
        public virtual void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public virtual void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            _transaction = null;

            _connection.Dispose();
        }
    }
}
