using System.Collections.ObjectModel;
using System.Linq;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using PrismMetroSample.Infrastructure.Events;
using PrismMetroSample.Infrastructure.Models;
using PrismMetroSample.Infrastructure.Services;

namespace PrismMetroSample.PatientModule.ViewModels
{
    public class PatientDetailViewModel : BindableBase
    {

        #region Fields

        private readonly IMedicineService _medicineService;
        private readonly IEventAggregator _ea;

        #endregion

        #region Properties

        private Patient _currentPatient;
        public Patient CurrentPatient
        {
            get => _currentPatient;
            set => SetProperty(ref _currentPatient, value);
        }

        private ObservableCollection<Medicine> _medicines;
        public ObservableCollection<Medicine> Medicines
        {
            get => _medicines;
            set => SetProperty(ref _medicines, value);
        }

        #endregion

        #region Commands

        private DelegateCommand _cancelSubscribeCommand;
        public DelegateCommand CancelSubscribeCommand =>
            _cancelSubscribeCommand ?? (_cancelSubscribeCommand = new DelegateCommand(ExecuteCancelSubscribeCommand));

        #endregion

        #region  Excutes

        private void ExecuteCancelSubscribeCommand() => _ea.GetEvent<MedicineSentEvent>().Unsubscribe(MedicineMessageReceived);

        #endregion



        public PatientDetailViewModel(IEventAggregator ea, IMedicineService medicineSerivce)
        {
            _medicineService = medicineSerivce;
            _ea = ea;
            _ea.GetEvent<PatientSentEvent>().Subscribe(PatientMessageReceived);
            _ea.GetEvent<MedicineSentEvent>().Subscribe(MedicineMessageReceived, ThreadOption.PublisherThread, false,
                medicine => medicine.Name == "Angelica" || medicine.Name == "Joan Yulu");
        }

        /// <summary>
        /// Accept the event message function
        /// </summary>
        private void MedicineMessageReceived(Medicine medicine) => Medicines?.Add(medicine);

        private void PatientMessageReceived(Patient patient)
        {
            CurrentPatient = patient;
            Medicines = new ObservableCollection<Medicine>(_medicineService.GetRecipesByPatientId(CurrentPatient.Id).FirstOrDefault().Medicines);
        }
    }
}
