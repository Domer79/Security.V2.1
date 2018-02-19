using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp_TestSecurity.ViewModelManagers;

namespace WpfApp_TestSecurity.Pages.Right.Controls.RoleControls
{
    /// <summary>
    /// Interaction logic for ProfileControl.xaml
    /// </summary>
    public partial class ProfileControl : UserControl
    {
        private AccessSetupPage _accessSetupPage;
        private RoleManager _roleManager;

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
                _roleManager = IocConfig.Resolve<RoleManager>();
            }
            DataContext = _roleManager;
        }
    }
}
