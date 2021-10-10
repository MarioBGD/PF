using PF.Mobile.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PF.Mobile.App.ViewModels.Popups
{
    public class ComboBoxPopupViewModel : BaseViewModel
    {
        public ObservableCollection<ComboBoxItemModel> Items { get; set; }

        public ComboBoxPopupViewModel(IEnumerable<ComboBoxItemModel> items)
        {
            Items = new ObservableCollection<ComboBoxItemModel>();

            if (items != null)
            {
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
        }
    }
}
