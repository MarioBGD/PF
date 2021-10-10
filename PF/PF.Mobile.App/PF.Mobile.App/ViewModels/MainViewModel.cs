using PF.DTO.Users;
using PF.Mobile.App.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PF.Mobile.App.ViewModels
{
     public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            AppBarViewModel.Instance.Refresh();
        }
        //private string barName;
        //private string barBalance;
        //private ImageSource profilePicture;
        
        //public string BarName
        //{
        //    get => barName;
        //    set => SetProperty(ref barName, value);
        //}
        //public string BarBalance
        //{
        //    get => barBalance;
        //    set => SetProperty(ref barBalance, value);
        //}
        //public ImageSource ProfilePicture
        //{
        //    get => profilePicture;
        //    set => SetProperty(ref profilePicture, value);
        //}

        //public Command ProfileCommand { get; }

        //public MainViewModel()
        //{
        //    ProfileCommand = new Command(async ()=> await OnProfileClick(), () => !IsBusy);

        //    DataManager.Init();

        //    //Common.Identicon.NIdenticon.Init();
        //    //ProfilePicture = ImageSource.FromStream(() =>
        //    //{
                
        //    //    using (MemoryStream ms = new MemoryStream())
        //    //    {
        //    //        var bitmap = Common.Identicon.NIdenticon.Generate(23345);
        //    //        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    //        return ms;
        //    //    }
        //    //});
           
        //    //Common.Identicon.NIdenticon.Generate(23345);

        //    DataManager.GetData(DataManager.DataType.MyUser, OnMyUserDataUpdate);
        //}

        //public void OnMyUserDataUpdate(object data)
        //{
        //    try
        //    {
        //        UserDTO userDTO = (UserDTO)data;
        //        BarName = userDTO.Name;
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        //public async Task OnProfileClick()
        //{
        //    IsBusy = true;

        //    string[] buttons = new string[] { "Log out" };

        //    var choice = await Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync(
        //        null, "Cancel", null, System.Threading.CancellationToken.None, buttons);

        //    if (!string.IsNullOrEmpty(choice))
        //    {
        //        if (choice == "Log out")
        //        {
        //            await App.LogOut();
        //        }
        //    }

        //    IsBusy = false;
        //}
    }
}
