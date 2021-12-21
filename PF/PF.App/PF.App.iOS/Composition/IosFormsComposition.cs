using DryIoc;
using PF.App.XamForms.Composition;

namespace PF.App.iOS.Composition
{
    public class IosFormsComposition
    {
        public static void Configure(IContainer container)
        {
            XamarinFormsComposition.Configure(container);
        }
    }
}