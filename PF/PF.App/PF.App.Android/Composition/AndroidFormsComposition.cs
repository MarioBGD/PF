using DryIoc;
using PF.App.XamForms.Composition;

namespace PF.App.Droid.Composition
{
    public static class AndroidFormsComposition
    {
        public static void Configure(IContainer container)
        {
            XamarinFormsComposition.Configure(container);
        }
    }
}