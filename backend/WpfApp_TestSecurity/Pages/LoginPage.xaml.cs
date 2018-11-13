using System.Windows.Controls;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            Loaded += LoginPage_Loaded;
        }

        private void LoginPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new LoginViewModel(NavigationService);
        }
    }
}
