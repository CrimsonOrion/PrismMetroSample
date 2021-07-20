using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.Infrastructure.Models;

namespace PrismMetroSample.Shell.ViewModels.Login
{
    public class CreateAccountViewModel : BindableBase, INavigationAware, IConfirmNavigationRequest
    {

        #region Fields

        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private IRegionNavigationJournal _journal;

        #endregion

        #region Properties

        private string _registeredLoginId;
        public string RegisteredLoginId
        {
            get => _registeredLoginId;
            set => SetProperty(ref _registeredLoginId, value);
        }

        public bool IsUseRequest { get; set; }

        #endregion

        #region Commands

        private DelegateCommand _loginMainContentCommand;
        public DelegateCommand LoginMainContentCommand =>
            _loginMainContentCommand ?? (_loginMainContentCommand = new DelegateCommand(ExecuteLoginMainContentCommand));

        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(ExecuteGoBackCommand));

        private DelegateCommand<object> _verityCommand;
        public DelegateCommand<object> VerityCommand =>
    _verityCommand ?? (_verityCommand = new DelegateCommand<object>(ExecuteVerityCommand));

        #endregion

        #region  Excutes

        private void ExecuteGoBackCommand() => _journal.GoBack();

        private void ExecuteLoginMainContentCommand() => Navigate("LoginMainContent");

        private void ExecuteVerityCommand(object parameter)
        {
            if (!VerityRegister(parameter))
            {
                return;
            }
            IsUseRequest = true;
            string Title = string.Empty;
            _dialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={"Registration was successful"}"), null);
            _journal.GoBack();
        }

        #endregion


        public CreateAccountViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }

        private bool VerityRegister(object parameter)
        {
            if (string.IsNullOrEmpty(RegisteredLoginId))
            {
                MessageBox.Show("LoginId cannot be empty!");
                return false;
            }
            Dictionary<string, PasswordBox> passwords = parameter as Dictionary<string, PasswordBox>;
            string password = passwords["Password"].Password;
            string confimPassword = passwords["ConfirmPassword"].Password;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password cannot be empty!");
                return false;
            }
            if (string.IsNullOrEmpty(confimPassword))
            {
                MessageBox.Show("ConfirmPassword cannot be empty!");
                return false;
            }
            if (password.Trim() != confimPassword.Trim())
            {
                MessageBox.Show("The passwords are inconsistent on both occasions");
                return false;
            }
            GlobalConstants.AllUsers.Add(new User()
            {
                Id = GlobalConstants.AllUsers.Max(t => t.Id) + 1,
                LoginId = RegisteredLoginId,
                Password = password
            });
            return true;
        }


        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                _regionManager.RequestNavigate(RegionNames.LoginContentRegion, navigatePath);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //MessageBox.Show("QuitCreateAccount");
        }

        public void OnNavigatedTo(NavigationContext navigationContext) =>
            //MessageBox.Show("Navigate from Login MainContent to CreateAccount");
            _journal = navigationContext.NavigationService.Journal;

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (!string.IsNullOrEmpty(RegisteredLoginId) && IsUseRequest)
            {
                _dialogService.ShowDialog("AlertDialog", new DialogParameters($"message={"Do I need to sign in with a currently registered user?"}"), r =>
                {
                    if (r.Result == ButtonResult.Yes)
                    {
                        navigationContext.Parameters.Add("loginId", RegisteredLoginId);
                    }
                });
            }
            continuationCallback(true);
            //var result = false;
            //if (MessageBox.Show("Do I need to navigate to the Login MainContent page?", "Navigate?",MessageBoxButton.YesNo) ==MessageBoxResult.Yes)
            //{
            //    result = true;
            //}
            //continuationCallback(result);
        }
    }
}
