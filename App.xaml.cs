using FastReportsTest.Dialogs;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FastReportsTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private string ConnectionString = new SqlConnectionStringBuilder()
        {
            DataSource = @".\SQLEXPRESS",
            Encrypt = false,
            InitialCatalog = "FastReportsTest",
            IntegratedSecurity = true,
            PersistSecurityInfo = false
        }.ToString();


        protected override void RegisterTypes(IContainerRegistry registry)
        {
            // Register SERVICES
            // Like: registry.RegisterSingleton<SERVICE>();
            // OR registry.RegisterSingleton<SERVICE>(() => new SERVICE(string parameter))
            // OR registry.RegisterSingleton<SERVICE>(() =>
            // new SERVICE(string parameter,
            //             Container.Resolve<DEPENDENT_REGISTERED_SERVICE>())

            // Register VIEWS/VIEWMODELS
            // Like: registry.RegisterForNavigation<VIEW, VIEWMODEL>();
            registry.RegisterForNavigation<Content, ContentVM>();

            // Register DIALOGS
            // Like: registry.RegisterDialog<DIALOGVIEW, DIALOGVIEWMODEL>();
            registry.RegisterDialog<ReportPreviewer, ReportPreviewerVM>("ReportDialog");

        }

        protected override Window CreateShell() { return Container.Resolve<MainWindow>(); }

        protected override void Initialize()
        {
            base.Initialize();

            // Disable Touch Devices on Prism dialog controls
            //https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/disable-the-realtimestylus-for-wpf-applications?view=netframeworkdesktop-4.8
            DisableWPFTabletSupport();

            // Make sure there is only one instance of the App running
            CheckApplicationInstance();

            var regionManger = Container.Resolve<IRegionManager>();
            regionManger.RequestNavigate("Content", nameof(Content));
        }

        private void CheckApplicationInstance()
        {
            Process process = Process.GetCurrentProcess();
            int count = Process.GetProcesses().Where(a => a.ProcessName == process.ProcessName).Count();

            if (count > 1)
            {
                MessageBox.Show("Application Already Running!");
                App.Current.Shutdown();
            }
        }

        private static void DisableWPFTabletSupport()
        {
            // Get a collection of the tablet devices for this window.
            TabletDeviceCollection devices = System.Windows.Input.Tablet.TabletDevices;

            // Get the Type of InputManager
            Type imputManagerType = typeof(System.Windows.Input.InputManager);

            // Call the StylusLogic method on the InputManager.Current instance.
            object stylusLogic = imputManagerType.InvokeMember("StylusLogic",
                System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, System.Windows.Input.InputManager.Current, null);

            if (stylusLogic != null)
            {
                // Get the type of the stylusLogic returned from the call to StylusLogic.
                Type stylusLogicType = stylusLogic.GetType();

                // Loop until there are no more devices to remove.
                while (devices.Count > 0)
                {
                    // Remove the first tablet device in the devices collection
                    stylusLogicType.InvokeMember("OnTabletRemoved",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                        null, stylusLogic, new object[] { (uint)0 });
                }
            }
        }
    }
}
