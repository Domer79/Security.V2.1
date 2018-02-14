using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp_TestSecurity.Pages.Left;
using WpfApp_TestSecurity.Pages.Right;
using WpfApp_TestSecurity.ViewModels;

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
        }

        private void _userMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _leftFrame.NavigationService.Navigate(IocConfig.Resolve<UsersPage>());
        }

    }
}
