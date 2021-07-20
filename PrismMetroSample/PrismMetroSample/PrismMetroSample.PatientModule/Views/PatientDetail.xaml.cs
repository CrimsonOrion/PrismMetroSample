using MahApps.Metro.Controls;

using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.Infrastructure.Services;

namespace PrismMetroSample.PatientModule.Views
{
    /// <summary>
    /// Interaction logic for PatientDetail
    /// </summary>
    public partial class PatientDetail : Flyout, IFlyoutView
    {
        public PatientDetail() => InitializeComponent();

        public string FlyoutName => FlyoutNames.PatientDetailFlyout;
    }
}
