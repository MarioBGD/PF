using DryIoc;
using PF.App.Contracts.Navigation;
using PF.App.Core.Composition;
using PF.App.XamForms.Navigation;
using DryIoc;
using System;
using System.Collections.Generic;
using System.Text;

namespace PF.App.XamForms.Composition
{
    public static class XamarinFormsComposition
    {
        public static void Configure(IRegistrator registrator)
        {
            CoreComposition.Configure(registrator);

            registrator.Register<IFormsNavigationProvider, FormsNavigationProvider>(Reuse.Singleton);
            registrator.Register<INavigationService, NavigationService>(Reuse.Singleton);
        }
    }
}
