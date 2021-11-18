using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PF.App.XamForms.Navigation
{
    public interface IFormsNavigationProvider
    {
        INavigation Navigation { get; set; }
    }

    public class FormsNavigationProvider : IFormsNavigationProvider
    {
        private INavigation _navigation;

        public INavigation Navigation
        {
            get => _navigation ?? throw new InvalidOperationException("You need set naviagtion first");
            set => _navigation = value;
        }
    }
}
