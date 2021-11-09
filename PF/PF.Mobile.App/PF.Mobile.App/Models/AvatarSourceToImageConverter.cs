using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PF.Mobile.App.Models
{
    public static class AvatarSourceToImageConverter
    {
        public static ImageSource Convert(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                source = "av1";
            }

            return ImageSource.FromResource("PF.Mobile.App.Resources.Avatars." + source + ".png");
        }
    }
}
