using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.DAL.Contracts
{
    public interface IDataManager
    {
        void Init(); 
        public delegate void DataCallback(object data);

        void GetData(DataType dataType, DataCallback callback, object args = null, bool highPrio = false);
    }
}