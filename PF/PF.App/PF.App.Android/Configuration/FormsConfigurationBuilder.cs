using DryIoc;
using PF.App.Droid.Composition;

namespace PF.App.Droid.Configuration
{
    public class FormsConfigurationBuilder
    {
        public static void Build()
        {
            var droidContainer = new Container(p => p.WithTrackingDisposableTransients());

            Core.Configuration.CoreConfigurationBuilder.Build(
                droidContainer,
                AndroidFormsComposition.Configure);
        }
    }
}