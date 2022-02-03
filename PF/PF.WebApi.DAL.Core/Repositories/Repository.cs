using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PF.WebApi.DAL.Entities;

namespace PF.WebApi.DAL.Core.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entities.BaseEntity
    {
        internal string TableName { get; }
        internal IDbConnection Connection { get; }
        internal IDbTransaction Transaction { get; }

        protected Repository(IDbConnection connection, IDbTransaction transaction, string tableName)
        {
            Connection = connection;
            Transaction = transaction;
            TableName = tableName;
        }

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        public async Task<T> Get(long id)
        {
            var result = await Connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {TableName} WHERE Id=@Id", new { Id = id }, Transaction);
            if (result == null)
                throw new KeyNotFoundException($"{TableName} with id [{id}] could not be found.");

            return result;
        }

        [Obsolete]
        public async Task<IEnumerable<T>> GetAll()
        {
            return await Connection.QueryAsync<T>($"SELECT * FROM {TableName}", null, Transaction);
        }

        public async Task<IEnumerable<T>> GetRange(IEnumerable<long> ids)
        {
            string query = $"SELECT * FROM {TableName} WHERE Id IN ({string.Join(", ", ids)})";
            return await Connection.QueryAsync<T>(query, null, Transaction);
        }


        public async Task<long> Add(T entity, bool withId = false)
        {
            entity.CreatedDate = DateTime.Now;

            var insertQuery = GenerateInsertQuery(withId);

            return await Connection.ExecuteScalarAsync<long>(insertQuery, entity, Transaction);
        }

        public async Task<long> AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
            }

            var inserted = 0;
            var query = GenerateInsertQuery();

            inserted += await Connection.ExecuteAsync(query, entities, Transaction);


            return inserted;
        }


        public async Task<long> Remove(long id)
        {
            return await Connection.ExecuteAsync($"DELETE FROM {TableName} WHERE Id=@Id", new { Id = id }, Transaction);
        }

        public async Task<long> Update(T entity)
        {
            var updateQuery = GenerateUpdateQuery();

            return await Connection.ExecuteAsync(updateQuery, entity, Transaction);
        }


        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        private string GenerateInsertQuery(bool withId = false)
        {
            var insertQuery = new StringBuilder();

            if (withId)
                insertQuery.Append($"SET IDENTITY_INSERT {TableName} ON ");

            insertQuery.Append($"INSERT INTO {TableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => {
                if (withId || prop != "Id")
                    insertQuery.Append($"[{prop}],");
            });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => {
                if (withId || prop != "Id")
                    insertQuery.Append($"@{prop},");
            });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            if (withId)
                insertQuery.Append($" SET IDENTITY_INSERT {TableName} ON");

            insertQuery.Append($" SELECT CAST(SCOPE_IDENTITY() as bigint)");

            return insertQuery.ToString();
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {TableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id") && !property.Equals("CreatedDate") && !property.Equals("CreatedUserId"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }
    }
}
