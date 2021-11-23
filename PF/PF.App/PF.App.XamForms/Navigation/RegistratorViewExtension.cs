using System;
using System.Collections.Concurrent;
using DryIoc;
using Splat;
using Xamarin.Forms;

namespace PF.App.XamForms.Navigation
{
    public static class RegistratorViewExtension
    {
        private static readonly ConcurrentDictionary<Type, Type> _viewViewModelMappings = new ConcurrentDictionary<Type, Type>();

        public static void RegisterView<TView, TViewModel>(this IRegistrator registrator) where TView : View where TViewModel : class
            => RegisterViewOfType<TView, TViewModel>(registrator);
        
        public static void RegisterPage<TPage, TViewModel>(this IRegistrator registrator) where TPage : Page where TViewModel : class 
            => RegisterViewOfType<TPage, TViewModel>(registrator);
        
        public static View GetView<TViewModel>() => GetViewOfType<View, TViewModel>();

        public static Page GetPage<TViewModel>() => GetViewOfType<Page, TViewModel>();

        private static void RegisterViewOfType<TViewType, TViewModel>(IRegistrator registrator) 
            where TViewType : class
            where TViewModel : class
        {
            registrator.Register<TViewType>();
            registrator.Register<TViewModel>(ifAlreadyRegistered: IfAlreadyRegistered.Throw);

            if (!_viewViewModelMappings.ContainsKey(typeof(TViewModel)))
            {
                _viewViewModelMappings[typeof(TViewModel)] = typeof(TViewType);
            }
        }
        
        private static T GetViewOfType<T, TViewModel>() where T : class
        {
            if (_viewViewModelMappings.TryGetValue(typeof(TViewModel), out var viewType))
            {
                if (Locator.Current.GetService(viewType) is T view)
                {
                    return view;
                }

                throw new InvalidOperationException($"{viewType} for viewmodel {typeof(TViewModel)} is not a type of {typeof(T)}");
            }

            throw new InvalidOperationException("No view registered for given view-model.");
        }
    }
}