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
using WpfApp_TestSecurity.ViewModelManagers;

namespace WpfApp_TestSecurity.Pages.Right.Controls.RoleControls
{
    /// <summary>
    /// Interaction logic for PolicyControl.xaml
    /// </summary>
    public partial class PolicyControl : UserControl
    {
        private PolicyManager _policyManager;

        public PolicyControl()
        {
            InitializeComponent();
            _policyManager = IocConfig.Resolve<PolicyManager>();
            DataContext = _policyManager;
        }

    }
}
