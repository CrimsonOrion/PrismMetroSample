using System;
using System.Windows;

using Prism.Services.Dialogs;

using PrismMetroSample.Infrastructure.Helpers;

namespace PrismMetroSample.Shell.Views.Dialogs
{
    /// <summary>
    /// The interactive logic of DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window, IDialogWindow
    {
        public DialogWindow() => InitializeComponent();

        protected override void OnSourceInitialized(EventArgs e) => WindowHelp.RemoveIcon(this);

        public IDialogResult Result { get; set; }
    }
}
