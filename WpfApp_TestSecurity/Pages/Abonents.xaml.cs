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

namespace WpfApp_TestSecurity.Pages
{
    /// <summary>
    /// Interaction logic for Abonents.xaml
    /// </summary>
    public partial class Abonents : Page
    {
        private readonly AbonentManager _abonentManager;

        public Abonents(AbonentManager abonentManager)
        {
            _abonentManager = abonentManager;
            InitializeComponent();
            DataContext = _abonentManager;
            Loaded += Abonents_Loaded;
        }

        private void Abonents_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Principal.Identity.IsAuthenticated)
            {
                NavigationService.Navigate(IocConfig.Resolve<LoginPage>());
                return;
            }

            if (!Principal.CheckAccess(Policy.AbonentsPage))
            {
                NavigationService.Navigate(IocConfig.Resolve<AccessDeniedPage>());
                return;
            }
        }
    }
}
