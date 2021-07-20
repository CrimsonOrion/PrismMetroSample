using System.Linq;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using PrismMetroSample.Infrastructure;
using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.MedicineModule.Views;

namespace PrismMetroSample.MedicineModule.ViewModels
{

    public class ShowSearchPatientViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;
        private ShowSearchPatient _showSearchPatientView;
        private IRegion _region;

        #endregion

        #region Properties

        private bool _isShow = true;
        public bool IsShow
        {
            get => _isShow = true;
            set
            {
                SetProperty(ref _isShow, value);
                if (_isShow)
                {
                    ActiveShowSearchPatient();
                }
                else
                {
                    DeactiveShowSearchPaitent();
                }
            }
        }

        private IApplicationCommands _applicationCommands;
        public IApplicationCommands ApplicationCommands
        {
            get => _applicationCommands;
            set => SetProperty(ref _applicationCommands, value);
        }

        #endregion

        #region Commands

        private DelegateCommand _showSearchLoadingCommand;
        public DelegateCommand ShowSearchLoadingCommand =>
            _showSearchLoadingCommand ?? (_showSearchLoadingCommand = new DelegateCommand(ExecuteShowSearchLoadingCommand));

        #endregion

        #region  Excutes

        private void ExecuteShowSearchLoadingCommand()
        {
            _region = _regionManager.Regions[RegionNames.ShowSearchPatientRegion];
            _showSearchPatientView = (ShowSearchPatient)_region.Views.Where(t => t.GetType() == typeof(ShowSearchPatient)).FirstOrDefault();
        }

        #endregion


        public ShowSearchPatientViewModel(IApplicationCommands applicationCommands, IRegionManager regionManager)
        {
            ApplicationCommands = applicationCommands;
            _regionManager = regionManager;
        }

        private void ActiveShowSearchPatient()
        {
            if (!_region.ActiveViews.Contains(_showSearchPatientView))
            {
                _region.Add(_showSearchPatientView);
            }
        }

        private async void DeactiveShowSearchPaitent()
        {
            _region.Remove(_showSearchPatientView);
            await Task.Delay(2000);
            IsShow = true;
        }
    }
}
