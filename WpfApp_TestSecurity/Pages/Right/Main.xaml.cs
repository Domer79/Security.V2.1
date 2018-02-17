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
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Pages.Right
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        private readonly UserManager _userManager;

        public Main(UserManager userManager)
        {
            _userManager = userManager;
            DataContext = this;
            InitializeComponent();
        }

        public UserViewModel SelectedItem
        {
            get => _userManager.SelectedItem;
            set => _userManager.SelectedItem = value;
        }
    }
}
