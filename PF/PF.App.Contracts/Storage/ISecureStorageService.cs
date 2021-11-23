using System.Threading.Tasks;

namespace PF.App.Contracts.Storage
{
    public interface ISecureStorageService
    {
        Task<bool> SaveStringAsync(string key, string value);
        
        Task<string> ReadStringAsync(string key, string defaultValue = null);
    }
}