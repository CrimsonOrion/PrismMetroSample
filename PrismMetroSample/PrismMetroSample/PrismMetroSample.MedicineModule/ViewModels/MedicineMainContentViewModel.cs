using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using PrismMetroSample.Infrastructure.Events;
using PrismMetroSample.Infrastructure.Models;
using PrismMetroSample.Infrastructure.Services;

namespace PrismMetroSample.MedicineModule.ViewModels
{
    public class MedicineMainContentViewModel : BindableBase, IActiveAware
    {
        #region Fields

        private readonly IMedicineService _medicineSerivce;
        private readonly IEventAggregator _ea;
        private readonly IDialogService _dialogService;

        public event EventHandler IsActiveChanged;

        #endregion

        #region Properties


        private ObservableCollection<Medicine> _allMedicines = new ObservableCollection<Medicine>();

        public ObservableCollection<Medicine> AllMedicines
        {
            get => _allMedicines;
            set => _allMedicines = value;
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                if (_isActive)
                {
                    _dialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={"The view is activated"}"), null);
                }
                else
                {
                    _dialogService.ShowDialog("WarningDialog", new DialogParameters($"message={"The view is invalid"}"), null);
                }
                IsActiveChanged?.Invoke(this, new EventArgs());
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _loadCommand;
        public DelegateCommand LoadCommand =>
            _loadCommand ?? (_loadCommand = new DelegateCommand(ExecuteLoadCommand));

        private void ExecuteLoadCommand() =>
            //TaskExtension for async void Command 
            ALongTask().Await(completedCallback: () =>
            {
                AllMedicines.AddRange(_medicineSerivce.GetAllMedicines());
            }, errorCallback: null, configureAwait: true);

        private async Task ALongTask()
        {
            await Task.Delay(3000);//Simulation takes time to operate
            Debug.WriteLine("Time-consuming operations are complete");
        }

        #endregion

        #region  Excutes



        #endregion



        public MedicineMainContentViewModel(IMedicineService medicineSerivce, IEventAggregator ea, IDialogService dialogService)
        {
            _medicineSerivce = medicineSerivce;
            _ea = ea;
            _dialogService = dialogService;
            _ea.GetEvent<MedicineSentEvent>().Subscribe(MedicineMessageReceived);//Subscribe to events
            AllMedicines = new ObservableCollection<Medicine>();
        }

        /// <summary>
        /// The event message accepts the function
        /// </summary>
        private void MedicineMessageReceived(Medicine medicine) => AllMedicines?.Add(medicine);
    }
}
