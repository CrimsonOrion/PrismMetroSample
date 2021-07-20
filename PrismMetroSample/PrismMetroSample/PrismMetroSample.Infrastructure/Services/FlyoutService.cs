using System.Linq;
using System.Windows.Input;

using MahApps.Metro.Controls;

using Prism.Commands;
using Prism.Regions;

using PrismMetroSample.Infrastructure.Constants;

namespace PrismMetroSample.Infrastructure.Services
{
    public class FlyoutService : IFlyoutService
    {
        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommands _applicationCommands;

        public ICommand ShowFlyoutCommand { get; private set; }
        public FlyoutService(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            _regionManager = regionManager;
            _applicationCommands = applicationCommands;

            ShowFlyoutCommand = new DelegateCommand<string>(ShowFlyout);
            //Register subcommands to global composite commands
            _applicationCommands.ShowCommand.RegisterCommand(ShowFlyoutCommand);

        }
        public void ShowFlyout(string flyoutName)
        {
            IRegion region = _regionManager.Regions[RegionNames.FlyoutRegion];

            if (region != null)
            {
                Flyout flyout = region.Views.Where(v => v is IFlyoutView && ((IFlyoutView)v).FlyoutName.Equals(flyoutName)).FirstOrDefault() as Flyout;

                if (flyout != null)
                {
                    flyout.IsOpen = !flyout.IsOpen;
                }
            }
        }
    }
}
