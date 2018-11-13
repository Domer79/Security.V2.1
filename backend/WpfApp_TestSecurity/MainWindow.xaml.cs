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
using NLog;
using WpfApp_TestSecurity.Pages;

namespace WpfApp_TestSecurity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();
        private static Frame _mainFrame;
        private static MainWindow _self;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainFrame = _frame;
                _self = this;
                _frame.Navigate(IocConfig.Resolve<Abonents>());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _frame.NavigationService.Navigate(IocConfig.Resolve<AccessSetupPage>());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        private void AbonentMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _frame.NavigationService.Navigate(IocConfig.Resolve<Abonents>());
        }

        public static Frame MainFrame
        {
            get { return _mainFrame; }
        }

        public static MainWindow Self => _self;
    }
}
