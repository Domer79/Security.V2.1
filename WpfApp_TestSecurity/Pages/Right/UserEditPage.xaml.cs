using System.Diagnostics;
using System.Windows.Navigation;
using WpfApp_TestSecurity.ViewModelManagers;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Pages.Right
{
    /// <summary>
    /// Interaction logic for UserEditPage.xaml
    /// </summary>
    public partial class UserEditPage : PageFunction<UserViewModel>
    {
        private readonly AccessSetupPage _accessSetupPage;
        private readonly UserManager _userManager;
        

        public UserEditPage(AccessSetupPage accessSetupPage, UserManager userManager)
        {
            _accessSetupPage = accessSetupPage;
            _userManager = userManager;
            DataContext = userManager;
            InitializeComponent();
        }
    }
}
