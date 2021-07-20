using MahApps.Metro.Controls;

using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.Infrastructure.Services;

namespace PrismMetroSample.MedicineModule.Views
{
    /// <summary>
    /// Interaction logic for SearchMedicine
    /// </summary>
    public partial class SearchMedicine : Flyout, IFlyoutView
    {
        public SearchMedicine() => InitializeComponent();

        public string FlyoutName => FlyoutNames.SearchMedicineFlyout;
    }
}
