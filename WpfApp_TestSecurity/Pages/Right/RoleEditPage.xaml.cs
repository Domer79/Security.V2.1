using System.Windows.Controls;
using WpfApp_TestSecurity.ViewModelManagers;

namespace WpfApp_TestSecurity.Pages.Right
{
    /// <summary>
    /// Interaction logic for RoleEditPage.xaml
    /// </summary>
    public partial class RoleEditPage : Page
    {
        private readonly RoleManager _roleManager;

        public RoleEditPage(RoleManager roleManager)
        {
            _roleManager = roleManager;
            DataContext = roleManager;
            InitializeComponent();
        }
    }
}
