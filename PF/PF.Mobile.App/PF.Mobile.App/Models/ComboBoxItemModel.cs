using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.Models
{
    public class ComboBoxItemModel
    {
        public object Value { get; set; }
        public string Text { get; set; }

        public delegate Task OnItemSelected(ComboBoxItemModel item);
        private OnItemSelected onItemSelected;

        public Command TapCommand { get; set; }

        public ComboBoxItemModel(OnItemSelected onItemSelected)
        {
            this.onItemSelected = onItemSelected;
            TapCommand = new Command(async () => await onItemSelected?.Invoke(this));
        }
    }
}
