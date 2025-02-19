﻿using System;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace PrismMetroSample.Shell.ViewModels.Dialogs
{
    public class SuccessDialogViewModel : BindableBase, IDialogAware
    {

        #region Fields

        public event Action<IDialogResult> RequestClose;

        #endregion

        #region Properties

        private string _title = "Notification";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        #endregion

        #region Commands

        private DelegateCommand _closeDialogCommand;
        public DelegateCommand CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand(ExecuteCloseDialogCommand));

        #endregion

        #region  Excutes

        private async void ExecuteCloseDialogCommand()
        {
            ButtonResult result = ButtonResult.No;
            await RaiseRequestClose(new DialogResult(result));
        }

        #endregion


        public virtual async Task RaiseRequestClose(IDialogResult dialogResult)
        {
            await Task.Delay(500);
            RequestClose?.Invoke(dialogResult);
        }


        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters) => Message = parameters.GetValue<string>("message");
    }
}
