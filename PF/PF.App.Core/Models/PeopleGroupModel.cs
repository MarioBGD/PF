using System.Collections.Generic;
using System.Collections.ObjectModel;
using PF.App.Core.ViewModels;

namespace PF.App.Core.Models
{
    public class PeopleGroup : ObservableCollection<PersonComponentViewModel>
    {
        public string GroupName { get; }

        public PeopleGroup(string name)
        {
            GroupName = name;
        }

        public PeopleGroup(string name, IEnumerable<PersonComponentViewModel> source) : base (source)
        {
            GroupName = name;
        }
    }
}