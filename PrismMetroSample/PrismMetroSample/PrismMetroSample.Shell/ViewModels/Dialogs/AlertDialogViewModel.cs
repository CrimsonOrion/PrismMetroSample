using System;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace PrismMetroSample.Shell.ViewModels.Dialogs
{
    public class AlertDialogViewModel : BindableBase, IDialogAware
    {

        #region Fields

        public event Action<IDialogResult> RequestClose;

        #endregion

        #region Properties

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private string _title = "Notification";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        #endregion

        #region Commands

        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(ExecuteCloseDialogCommand));

        #endregion

        #region  Excutes

        private void ExecuteCloseDialogCommand(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.Yes;
            }
            else if (parameter?.ToLower() == "false")
            {
                result = ButtonResult.No;
            }

            RaiseRequestClose(new DialogResult(result));
        }

        #endregion


        public virtual void RaiseRequestClose(IDialogResult dialogResult) => RequestClose?.Invoke(dialogResult);

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters) => Message = parameters.GetValue<string>("message");
    }
}
