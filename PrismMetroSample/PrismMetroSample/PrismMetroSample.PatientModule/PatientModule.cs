using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.PatientModule.Views;

namespace PrismMetroSample.PatientModule
{
    public class PatientModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();

            //PatientList
            //regionManager.RegisterViewWithRegion(RegionNames.PatientListRegion, typeof(PatientList));
            //PatientDetail-Flyout
            regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(PatientDetail));


        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
