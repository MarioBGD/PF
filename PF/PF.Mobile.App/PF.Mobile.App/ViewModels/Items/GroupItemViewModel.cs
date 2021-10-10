using PF.DTO.Groups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels.Items
{
    public class GroupItemViewModel : BaseViewModel
    {
        private string name;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
       
        public long GroupId { get; set; }


        public delegate Task GroupButtonClickDel(GroupItemViewModel person);
        public GroupButtonClickDel OnItemTapped;

        public Command TapCommand { get; }


        public GroupItemViewModel(long id,
            GroupButtonClickDel onItemTapped)
        {
            GroupId = id;
            OnItemTapped = onItemTapped;

            TapCommand = new Command(async () => await OnItemTapped?.Invoke(this), () => !IsBusy);
        }

        public void UpdateData(GroupDTO data)
        {
            Name = data.Name;
        }
    }
}
