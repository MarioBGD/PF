using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using PF.App.Contracts.Components;
using Xamarin.Forms;

namespace PF.App.XamForms.Components
{
    public class DialogsManagerComponent : IDialogsManager
    {
        public Task ShowMessage(string message) => UserDialogs.Instance.AlertAsync(message: message);

        public Task ShowMessage(string title, string message) => UserDialogs.Instance.AlertAsync(message: message, title: title);

        public async Task<string> ShowPrompt(string message, string title, string okButtonText, string cancelButtonText)
        {
            var result = await UserDialogs.Instance.PromptAsync(message: message, title: title, okText: okButtonText, cancelText: cancelButtonText);

            return result.Ok ? result.Text : null;
        }
        
        public void ShowToast(string message) => UserDialogs.Instance.Toast(message);

        public Task<string> ShowList(string[] options) => Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync(null, "Cancel", null, CancellationToken.None, options);
    }
}