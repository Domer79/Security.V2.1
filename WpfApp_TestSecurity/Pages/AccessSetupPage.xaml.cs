using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp_TestSecurity.Pages;
using WpfApp_TestSecurity.Pages.Left;

namespace WpfApp_TestSecurity
{
    /// <summary>
    /// Interaction logic for AccessSetup.xaml
    /// </summary>
    public partial class AccessSetupPage : Page
    {
        public AccessSetupPage()
        {
            InitializeComponent();
            Loaded += AccessSetupPage_Loaded;
        }

        private void AccessSetupPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Principal.Identity.IsAuthenticated)
                NavigationService.Navigate(IocConfig.Resolve<LoginPage>());
        }

        private void _userMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _leftFrame.NavigationService.Navigate(IocConfig.Resolve<UsersPage>());
        }

        private void _roleMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _leftFrame.NavigationService.Navigate(IocConfig.Resolve<RolesPage>());
        }
    }
}
