using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DryIoc;
using PF.App.Droid.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PF.App.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            FormsConfigurationBuilder.Build();
        }
    }
}