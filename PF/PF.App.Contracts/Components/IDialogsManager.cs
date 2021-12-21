using System.Threading.Tasks;

namespace PF.App.Contracts.Components
{
    public interface IDialogsManager
    {
        Task ShowMessage(string message);
        Task ShowMessage(string title, string message);
        Task<string> ShowPrompt(string message, string title, string okButtonText, string cancelButtonText);
        void ShowToast(string message);
        Task<string> ShowList(string[] options);
    }
}