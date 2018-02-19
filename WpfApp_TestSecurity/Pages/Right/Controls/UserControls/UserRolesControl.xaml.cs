using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfApp_TestSecurity.Annotations;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.ViewModelManagers;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Pages.Right.Controls.UserControls
{
    /// <summary>
    /// Interaction logic for UserRolesControl.xaml
    /// </summary>
    public partial class UserRolesControl : UserControl
    {
        private UserRoleManager _userRoleManager;

        public UserRolesControl()
        {
            InitializeComponent();
            _userRoleManager = IocConfig.Resolve<UserRoleManager>();
            DataContext = _userRoleManager;
        }
    }
}
