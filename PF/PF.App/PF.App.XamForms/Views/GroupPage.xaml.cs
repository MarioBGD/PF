﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PF.App.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PF.App.XamForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage
    {
        public GroupPage(GroupViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}