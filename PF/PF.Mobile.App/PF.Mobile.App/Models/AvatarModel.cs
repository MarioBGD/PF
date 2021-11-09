using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.Models
{
    public class AvatarModel
    {
        public delegate void AvatarTap(AvatarModel avatarModel);

        public AvatarModel(AvatarTap tap)
        {
            Tap = tap;
            OnTapCommand = new Command(OnTap);
        }

        public ImageSource AvatarSource { get; set; }
        public string AvatarName { get; set; }
        public bool Selected { get; set; }
        public Command OnTapCommand { get; }


        private AvatarTap Tap;


        private void OnTap()
        {
            Tap.Invoke(this);
        }
    }
}
