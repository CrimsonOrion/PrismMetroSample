using System.Windows;
using System.Windows.Controls.Primitives;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

using PrismMetroSample.Infrastructure;
using PrismMetroSample.Infrastructure.CustomerRegionAdapters;
using PrismMetroSample.Infrastructure.Services;
using PrismMetroSample.Shell.ViewModels.Dialogs;
using PrismMetroSample.Shell.Views.Dialogs;
using PrismMetroSample.Shell.Views.Login;

using Unity;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Interception.PolicyInjection;

namespace PrismMetroSample.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        protected override void OnStartup(StartupEventArgs e) => base.OnStartup(e);

        protected override Window CreateShell() => Container.Resolve<LoginWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = PrismIocExtensions.GetContainer(containerRegistry);
            container.AddNewExtension<Interception>()//add Extension Aop
                                                     //Sign up for the service and add display blocking
                .RegisterType<IMedicineService, MedicineService>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IPatientService, PatientService>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<PolicyInjectionBehavior>())
                //.RegisterType<IUserService, UserService>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<PolicyInjectionBehavior>())
                .RegisterType<IUserService, UserService>();

            //Register the global command
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
            containerRegistry.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());

            //Sign up for navigation
            containerRegistry.RegisterForNavigation<LoginMainContent>();
            containerRegistry.RegisterForNavigation<CreateAccount>();

            //Register the dialog box
            containerRegistry.RegisterDialog<AlertDialog, AlertDialogViewModel>();
            containerRegistry.RegisterDialog<SuccessDialog, SuccessDialogViewModel>();
            containerRegistry.RegisterDialog<WarningDialog, WarningDialogViewModel>();
            containerRegistry.RegisterDialogWindow<DialogWindow>();
        }

        //protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        //{
        //    moduleCatalog.AddModule<PrismMetroSample.PatientModule.PatientModule>();
        //    var MedicineModuleType = typeof(PrismMetroSample.MedicineModule.MedicineModule);
        //    moduleCatalog.AddModule(new ModuleInfo()
        //    {
        //        ModuleName= MedicineModuleType.Name,
        //        ModuleType=MedicineModuleType.AssemblyQualifiedName,
        //        InitializationMode=InitializationMode.OnDemand
        //    });

        //}

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(UniformGrid), Container.Resolve<UniformGridRegionAdapter>());
        }

        protected override IModuleCatalog CreateModuleCatalog() => new DirectoryModuleCatalog() { ModulePath = @".\Modules" };//return new ConfigurationModuleCatalog();
    }
}
