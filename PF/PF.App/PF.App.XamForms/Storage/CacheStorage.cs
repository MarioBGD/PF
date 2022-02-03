using System;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using MonkeyCache.FileStore;
using PF.App.Contracts.Storage;

namespace PF.App.XamForms.Storage
{
    internal class CacheStorage : ICacheStorage
    {
        private const string _appStorageKey = "PF";

        public CacheStorage()
        {
            Barrel.ApplicationId = _appStorageKey;
        }

        public async Task<TReturnDataType> GetData<TReturnDataType>(string key)
        {
            await Task.Yield();
            try
            {

                var result = Barrel.Current.Get<TReturnDataType>(key);
                return result;
            }
            catch (Exception e)
            {
                var a = e;
            }

            return default;
        }

        public async Task SaveData<TData>(TData data, string key)
        {
            await Task.Yield();
            Barrel.Current.Add(key, data, TimeSpan.FromDays(14));
        }

        public bool IsDataExpired(string key) => Barrel.Current.IsExpired(key);
    }
}