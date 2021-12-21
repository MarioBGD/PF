using System;
using System.Threading.Tasks;

namespace PF.App.Contracts
{
    public interface IUiThreadInvoker
    {
        //Task InvokeOnUIThreadAsync(Task task);
        public Task InvokeOnUIThreadAsync(Action action);
    }
}