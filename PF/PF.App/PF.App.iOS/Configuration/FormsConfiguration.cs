using DryIoc;
using PF.App.Core.Configuration;
using PF.App.iOS.Composition;
using PF.App.XamForms.Composition;

namespace PF.App.iOS.Configuration
{
    public class FormsConfiguration
    {
        public static void Build()
        {
            var iOSContainer = new Container(r => r.WithoutFastExpressionCompiler().WithTrackingDisposableTransients());
            CoreConfigurationBuilder.Build(iOSContainer, IosFormsComposition.Configure);
        }
    }
}