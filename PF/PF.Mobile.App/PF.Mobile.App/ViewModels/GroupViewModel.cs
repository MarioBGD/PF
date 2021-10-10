using PF.DTO.Groups;
using PF.Mobile.App.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PF.Mobile.App.ViewModels
{
    public class GroupViewModel : BaseViewModel
    {
        public GroupViewModel(long groupId)
        {
            DataManager.GetData(DataManager.DataType.Groups, OnGroupDataLoaded, new List<long>{ App.CurrentGroupId });
            //todo pobieranie info
            Task.Run(async () =>
            {
                await SecureStorage.SetAsync("state", groupId.ToString());
            });
        }

        private void OnGroupDataLoaded(object data)
        {
            IEnumerable<GroupDTO> groups = (IEnumerable<GroupDTO>)data;
            var group = groups.FirstOrDefault(p => p.Id == App.CurrentGroupId);

            if (group != null)
            {
                App.CurrentGroup = group;
                AppBarViewModel.Instance.ShowPersonalInfo = false;
                AppBarViewModel.Instance.Header = group.Name;
            }
        }
    }
}
