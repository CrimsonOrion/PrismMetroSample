using System.Linq;
using System.Windows.Controls;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.Infrastructure.Models;
using PrismMetroSample.Shell.Views;
using PrismMetroSample.Shell.Views.Login;

namespace PrismMetroSample.Shell.ViewModels.Login
{
    public class LoginMainContentViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        #region Fields

        private IRegionNavigationJournal _journal;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        #endregion

        #region Properties

        private bool _isCanExecute;
        public bool IsCanExecute
        {
            get => _isCanExecute;
            set => SetProperty(ref _isCanExecute, value);
        }

        private User _currentUser = new();
        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public bool KeepAlive => true;

        #endregion

        #region Commands

        private DelegateCommand _createAccountCommand;
        public DelegateCommand CreateAccountCommand =>
            _createAccountCommand ?? (_createAccountCommand = new DelegateCommand(ExecuteCreateAccountCommand));

        private DelegateCommand _goForwardCommand;
        public DelegateCommand GoForwardCommand =>
            _goForwardCommand ?? (_goForwardCommand = new DelegateCommand(ExecuteGoForwardCommand));

        private DelegateCommand<PasswordBox> _loginCommand;
        public DelegateCommand<PasswordBox> LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand<PasswordBox>(ExecuteLoginCommand, CanExecuteGoForwardCommand));

        #endregion

        #region  Excutes

        private void ExecuteCreateAccountCommand() => Navigate("CreateAccount");

        private void ExecuteLoginCommand(PasswordBox passwordBox)
        {
            if (string.IsNullOrEmpty(CurrentUser.LoginId))
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"LoginId cannot be empty!"}"), null);
                return;
            }
            CurrentUser.Password = passwordBox.Password;
            if (string.IsNullOrEmpty(CurrentUser.Password))
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"Password cannot be empty!"}"), null);
                return;
            }
            else if (GlobalConstants.AllUsers.Where(t => t.LoginId == CurrentUser.LoginId && t.Password == CurrentUser.Password).Count() == 0)
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"LoginId or Password error!"}"), null);
                return;
            }
            ShellSwitcher.Switch<LoginWindow, MainWindow>();
        }

        private void ExecuteGoForwardCommand() => _journal.GoForward();

        #endregion

        public LoginMainContentViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                _regionManager.RequestNavigate(RegionNames.LoginContentRegion, navigatePath);
            }
        }



        private bool CanExecuteGoForwardCommand(PasswordBox passwordBox)
        {
            IsCanExecute = _journal != null && _journal.CanGoForward;
            return true;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //MessageBox.Show("Exited Login MainContent");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //MessageBox.Show("Navigate from CreateAccount to Login MainContent");
            _journal = navigationContext.NavigationService.Journal;

            string loginId = navigationContext.Parameters["loginId"] as string;
            if (loginId != null)
            {
                CurrentUser = new User() { LoginId = loginId };
            }
            LoginCommand.RaiseCanExecuteChanged();
        }
    }
}
