using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp_TestSecurity.ViewModelManagers;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Pages.Right.Controls.UserControls
{
    /// <summary>
    /// Interaction logic for ProfileControl.xaml
    /// </summary>
    public partial class ProfileControl : UserControl
    {
        private AccessSetupPage _accessSetupPage;
        private UserManager _userManager;

        public ProfileControl()
        {
            InitializeComponent();
            Loaded += ProfileControl_Loaded;
        }

        private void ProfileControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                _accessSetupPage = IocConfig.Resolve<AccessSetupPage>();
                _userManager = IocConfig.Resolve<UserManager>();
            }
            DataContext = _userManager;
        }
    }
}
