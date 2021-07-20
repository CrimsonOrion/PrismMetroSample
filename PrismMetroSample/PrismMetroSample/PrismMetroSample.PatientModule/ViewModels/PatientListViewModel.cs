using System.Collections.Generic;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using PrismMetroSample.Infrastructure;
using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.Infrastructure.Events;
using PrismMetroSample.Infrastructure.Models;
using PrismMetroSample.Infrastructure.Services;
using PrismMetroSample.PatientModule.Views;

namespace PrismMetroSample.PatientModule.ViewModels
{
    public class PatientListViewModel : BindableBase, IRegionMemberLifetime
    {

        #region Fields

        private readonly IEventAggregator _ea;
        private readonly IRegionManager _regionManager;
        private readonly IPatientService _patientService;
        private readonly IRegion _region;
        private readonly PatientList _patientListView;

        #endregion

        #region Properties

        private IApplicationCommands _applicationCommands;
        public IApplicationCommands ApplicationCommands
        {
            get => _applicationCommands;
            set => SetProperty(ref _applicationCommands, value);
        }

        private List<Patient> _allPatients;
        public List<Patient> AllPatients
        {
            get => _allPatients;
            set => SetProperty(ref _allPatients, value);
        }


        public bool KeepAlive => true;

        #endregion

        #region Commands

        private DelegateCommand<Patient> _mouseDoubleClickCommand;
        public DelegateCommand<Patient> MouseDoubleClickCommand =>
            _mouseDoubleClickCommand ?? (_mouseDoubleClickCommand = new DelegateCommand<Patient>(ExecuteMouseDoubleClickCommand));

        #endregion

        #region  Excutes

        /// <summary>
        /// DataGrid double-clicks the button command method
        /// </summary>
        private void ExecuteMouseDoubleClickCommand(Patient patient)
        {
            ApplicationCommands.ShowCommand.Execute(FlyoutNames.PatientDetailFlyout);//Open the form
            _ea.GetEvent<PatientSentEvent>().Publish(patient);//Publish the message
        }

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public PatientListViewModel(IPatientService patientService, IEventAggregator ea, IApplicationCommands applicationCommands)
        {
            _ea = ea;
            ApplicationCommands = applicationCommands;
            _patientService = patientService;
            AllPatients = _patientService.GetAllPatients();
        }

    }
}
