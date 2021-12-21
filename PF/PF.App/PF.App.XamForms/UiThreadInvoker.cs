using System;
using System.Threading.Tasks;
using PF.App.Contracts;
using Xamarin.Forms;

namespace PF.App.XamForms
{
    public class UiThreadInvoker : IUiThreadInvoker
    {
        public UiThreadInvoker()
        {
            var hc = this.GetHashCode();
        }
        
        //TODO: normalize this
        public Task InvokeOnUIThreadAsync(Task task) => Device.InvokeOnMainThreadAsync(async () => await task);
        public Task InvokeOnUIThreadAsync(Action action) => Device.InvokeOnMainThreadAsync(action);
    }
}