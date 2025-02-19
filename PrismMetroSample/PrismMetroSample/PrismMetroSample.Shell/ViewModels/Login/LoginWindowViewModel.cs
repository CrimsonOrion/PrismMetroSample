﻿using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.Infrastructure.Services;

namespace PrismMetroSample.Shell.ViewModels.Login
{
    public class LoginWindowViewModel : BindableBase
    {

        #region Fields

        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;

        #endregion

        #region Properties


        #endregion

        #region Commands

        private DelegateCommand _loginLoadingCommand;
        public DelegateCommand LoginLoadingCommand =>
            _loginLoadingCommand ?? (_loginLoadingCommand = new DelegateCommand(ExecuteLoginLoadingCommand));

        #endregion

        #region  Excutes

        private void ExecuteLoginLoadingCommand()
        {
            //_regionManager.RequestNavigate(RegionNames.LoginContentRegion, "LoginMainContent");
            IRegion region = _regionManager.Regions[RegionNames.LoginContentRegion];
            region.RequestNavigate("LoginMainContent", NavigationCompelted);
            GlobalConstants.AllUsers = _userService.GetAllUsers();
        }

        public async Task TestTask() => await Task.Run(() =>
                                      {
                                          Thread.Sleep(6000);
                                          Debug.WriteLine("test");
                                      });

        #endregion


        public LoginWindowViewModel(IRegionManager regionManager, IUserService userService, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _userService = userService;
            _dialogService = dialogService;
        }



        private void NavigationCompelted(NavigationResult result)
        {
            if (result.Result == true)
            {
                Thread.Sleep(1000);
                _dialogService.Show("SuccessDialog", new DialogParameters($"message={"Navigation to the Login MainContent page was successful"}"), null);
            }
            else
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"Navigation to the Login MainContent page failed"}"), null);
            }
        }


    }
}
