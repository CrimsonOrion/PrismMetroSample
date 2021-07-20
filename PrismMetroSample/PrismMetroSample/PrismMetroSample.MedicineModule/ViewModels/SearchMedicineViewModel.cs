using System.Collections.Generic;
using System.Linq;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using PrismMetroSample.Infrastructure.Events;
using PrismMetroSample.Infrastructure.Models;
using PrismMetroSample.Infrastructure.Services;

namespace PrismMetroSample.MedicineModule.ViewModels
{
    public class SearchMedicineViewModel : BindableBase
    {
        #region Fields

        private readonly IMedicineService _medicineSerivce;
        private readonly IEventAggregator _ea;

        #endregion

        #region Properties

        private List<Medicine> _allMedicines;
        public List<Medicine> AllMedicines
        {
            get => _allMedicines;
            set => SetProperty(ref _allMedicines, value);
        }

        private List<Medicine> _currentMedicines;
        public List<Medicine> CurrentMedicines
        {
            get => _currentMedicines;
            set => SetProperty(ref _currentMedicines, value);
        }

        private string _searchCondition;
        public string SearchCondition
        {
            get => _searchCondition;
            set => SetProperty(ref _searchCondition, value);
        }

        #endregion

        #region Commands

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(ExecuteSearchCommand));

        private DelegateCommand<Medicine> _addMedicineCommand;
        public DelegateCommand<Medicine> AddMedicineCommand =>
            _addMedicineCommand ?? (_addMedicineCommand = new DelegateCommand<Medicine>(ExecuteAddMedicineCommand));

        #endregion

        #region  Excutes

        private void ExecuteSearchCommand() => CurrentMedicines = AllMedicines.Where(t => t.Name.Contains(SearchCondition) || t.Type.Contains(SearchCondition)
                                             || t.Unit.Contains(SearchCondition)).ToList();

        private void ExecuteAddMedicineCommand(Medicine currentMedicine) => _ea.GetEvent<MedicineSentEvent>().Publish(currentMedicine);

        #endregion



        public SearchMedicineViewModel(IMedicineService medicineSerivce, IEventAggregator ea)
        {
            _ea = ea;
            _medicineSerivce = medicineSerivce;
            CurrentMedicines = AllMedicines = _medicineSerivce.GetAllMedicines();
        }
    }
}
