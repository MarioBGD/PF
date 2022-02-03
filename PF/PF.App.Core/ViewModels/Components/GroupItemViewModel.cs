using System.Windows.Input;
using PF.App.Core.DAL.Contracts.Models;

namespace PF.App.Core.ViewModels.Components
{
    public class GroupItemViewModel
    {
        public GroupItemViewModel(Group group, ICommand onTapCommand)
        {
            Name = group.Name;
            Id = group.Id;
            OnTapCommand = onTapCommand;
        }
        
        public string Name { get; }
        public long Id { get; }
        public ICommand OnTapCommand { get; }
    }
}