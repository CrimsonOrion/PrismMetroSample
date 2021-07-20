using System.Linq;

using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.MedicineModule.Views;
using PrismMetroSample.PatientModule.Views;
using PrismMetroSample.Shell.Views.RegionAdapterViews;

namespace PrismMetroSample.Shell.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields

        private readonly IModuleManager _moduleManager;
        private readonly IDialogService _dialogService;
        private IRegion _patientListRegion;
        private IRegion _medicineListRegion;
        private PatientList _patientListView;
        private MedicineMainContent _medicineMainContentView;

        #endregion

        #region Properties

        public IRegionManager RegionManager { get; }

        private bool _isCanExecute = false;
        public bool IsCanExecute
        {
            get => _isCanExecute;
            set => SetProperty(ref _isCanExecute, value);
        }

        #endregion

        #region Commands

        private DelegateCommand _loadingCommand;
        public DelegateCommand LoadingCommand =>
            _loadingCommand ?? (_loadingCommand = new DelegateCommand(ExecuteLoadingCommand));

        private DelegateCommand _activePatientListCommand;
        public DelegateCommand ActivePatientListCommand =>
            _activePatientListCommand ?? (_activePatientListCommand = new DelegateCommand(ExecuteActivePatientListCommand));

        private DelegateCommand _deactivePatientListCommand;
        public DelegateCommand DeactivePatientListCommand =>
            _deactivePatientListCommand ?? (_deactivePatientListCommand = new DelegateCommand(ExecuteDeactivePatientListCommand));

        private DelegateCommand _activeMedicineListCommand;
        public DelegateCommand ActiveMedicineListCommand =>
            _activeMedicineListCommand ?? (_activeMedicineListCommand = new DelegateCommand(ExecuteActiveMedicineListCommand).ObservesCanExecute(() => IsCanExecute));

        private DelegateCommand _deactiveMedicineListCommand;
        public DelegateCommand DeactiveMedicineListCommand =>
            _deactiveMedicineListCommand ?? (_deactiveMedicineListCommand = new DelegateCommand(ExecuteDeactiveMedicineListCommand).ObservesCanExecute(() => IsCanExecute));


        private DelegateCommand _loadMedicineModuleCommand;
        public DelegateCommand LoadMedicineModuleCommand =>
            _loadMedicineModuleCommand ?? (_loadMedicineModuleCommand = new DelegateCommand(ExecuteLoadMedicineModuleCommand));

        #endregion

        #region  Executes

        private void ExecuteLoadingCommand()
        {

            _patientListRegion = RegionManager.Regions[RegionNames.PatientListRegion];
            _patientListView = ContainerLocator.Current.Resolve<PatientList>();
            _patientListRegion.Add(_patientListView);

            IRegion uniformContentRegion = RegionManager.Regions["UniformContentRegion"];
            RegionAdapterView1 regionAdapterView1 = ContainerLocator.Current.Resolve<RegionAdapterView1>();
            uniformContentRegion.Add(regionAdapterView1);
            RegionAdapterView2 regionAdapterView2 = ContainerLocator.Current.Resolve<RegionAdapterView2>();
            uniformContentRegion.Add(regionAdapterView2);

            _medicineListRegion = RegionManager.Regions[RegionNames.MedicineMainContentRegion];
        }

        private void ExecuteDeactiveMedicineListCommand() => _medicineListRegion.Deactivate(_medicineMainContentView);

        private void ExecuteActiveMedicineListCommand() => _medicineListRegion.Activate(_medicineMainContentView);

        private void ExecuteLoadMedicineModuleCommand()
        {
            _moduleManager.LoadModule("MedicineModule");
            _medicineMainContentView = (MedicineMainContent)_medicineListRegion.Views.Where(t => t.GetType() == typeof(MedicineMainContent)).FirstOrDefault();
            IsCanExecute = true;
        }

        private void ExecuteDeactivePatientListCommand() => _patientListRegion.Deactivate(_patientListView);

        private void ExecuteActivePatientListCommand() => _patientListRegion.Activate(_patientListView);

        #endregion

        public MainWindowViewModel(IModuleManager moduleManager, IRegionManager regionManager, IDialogService dialogService)
        {
            _moduleManager = moduleManager;
            RegionManager = regionManager;
            _dialogService = dialogService;
            _moduleManager.LoadModuleCompleted += _moduleManager_LoadModuleCompleted;
        }


        private void _moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e) => _dialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={e.ModuleInfo.ModuleName + " module is loaded"}"), null);


    }
}
