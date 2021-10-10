using Dapper;
using PF.WebApi.Entities;
using PF.WebApi.Entities.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.DAL.Repositories
{
    public class PaymentsRepository : Repository<PaymentEntity>
    {
        public PaymentsRepository(IUnitOfWork uow) : base(uow.Connection, uow.Transaction, "Payments")
        {

        }

        public async Task<IEnumerable<PaymentEntity>> GetPaymentsOfUser(long userId, IEnumerable<long> expensesIds = null)
        {
            string query = $"SELECT * FROM {TableName} WHERE UserId = {userId}";

            if (expensesIds != null)
            {
                query += $" AND ExpenseId IN ({string.Join(',', expensesIds)})";
            }

            return await Connection.QueryAsync<PaymentEntity>(query, null, Transaction);
        }

        public async Task<IEnumerable<PaymentEntity>> GetPayments(IEnumerable<long> expensesIds)
        {
            string query = $"SELECT * FROM {TableName} WHERE ExpenseId IN ({string.Join(',', expensesIds)})";
            return await Connection.QueryAsync<PaymentEntity>(query, null, Transaction);
        }

        public async Task<IEnumerable<BalanceData>> GetBalances(IEnumerable<long> usersIds)
        {
            string query = $"SELECT UserId AS UserId, SUM(Amount) AS Balance FROM {TableName} WHERE UserId IN ({string.Join(',', usersIds)}) Group by UserId";
            return await Connection.QueryAsync<BalanceData>(query, null, Transaction);
        }

        public async Task<IEnumerable<BalanceData>> GetBalancesInGroup(IEnumerable<long> usersIds, long groupId)
        {
            string query = $"SELECT UserId AS UserId, SUM(Amount) AS Balance FROM {TableName} where ExpenseId in (SELECT Id from Expenses where GroupId = {groupId}) AND UserId IN ({string.Join(',', usersIds)}) Group by UserId";
            return await Connection.QueryAsync<BalanceData>(query, null, Transaction);
        }
    }
}
