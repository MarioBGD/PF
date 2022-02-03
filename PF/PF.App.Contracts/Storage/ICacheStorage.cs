using System.Collections.Generic;
using System.Threading.Tasks;

namespace PF.App.Contracts.Storage
{
    public interface ICacheStorage
    {
        Task<TReturnDataType> GetData<TReturnDataType>(string key);
        Task SaveData<TData>(TData data, string key);
        bool IsDataExpired(string key);
    }
}