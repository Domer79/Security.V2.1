using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using NLog;
using Security.Contracts;

namespace WpfApp_TestSecurity
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public App()
        {
            MapperMappings.Map();
            IocConfig.Configure();
            Compare.Config();

            var security = IocConfig.Resolve<ISecurity>();
            security.Config.RegisterSecurityObjects("IPPS.LightingSystem", Policies.GetAllPolicies());

            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Error(e.Exception);
        }
    }
}
