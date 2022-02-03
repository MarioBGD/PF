using System.Threading.Tasks;
using PF.App.Contracts.Storage;
using Xamarin.Essentials;

namespace PF.App.XamForms.Storage
{
    internal class SecureStorageService : ISecureStorageService
    {
        public async Task<bool> SaveStringAsync(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
            return true;
        }

        public async Task<string> ReadStringAsync(string key, string defaultValue = null)
        { 
            var res = await SecureStorage.GetAsync(key);
            return string.IsNullOrEmpty(res) ? defaultValue : res;
        }
    }
}