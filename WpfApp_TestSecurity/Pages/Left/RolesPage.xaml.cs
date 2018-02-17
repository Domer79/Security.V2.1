using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.Pages.Right;
using WpfApp_TestSecurity.ViewModelManagers;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Pages.Left
{
    /// <summary>
    /// Interaction logic for Roles.xaml
    /// </summary>
    public partial class RolesPage : Page
    {
        private readonly RoleManager _roleManager;
        private readonly AccessSetupPage _accessSetupPage;
        private readonly RoleEditPage _roleEditPage;
        private object _prevSelectedItem;

        public RolesPage(RoleManager roleManager, AccessSetupPage accessSetupPage, RoleEditPage roleEditPage)
        {
            _roleManager = roleManager;
            _accessSetupPage = accessSetupPage;
            _roleEditPage = roleEditPage;
            RoleList = _roleManager.Items;
            DataContext = this;
            DeleteCommand = new RelayCommand(DeleteItem, CanDelete);
            InitializeComponent();
        }

        private void DeleteItem(object obj)
        {
            var roleViewModel = (RoleViewModel)obj;
            if (MessageBox.Show($"Подтвердите удаление пользователя {roleViewModel.Name}", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _roleManager.Items.Remove(roleViewModel);
        }

        private bool CanDelete(object arg)
        {
            return arg != null && _roleList.SelectedItem != null;
        }

        private void AddMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _accessSetupPage._rightFrame.NavigationService.Navigate(_roleEditPage, new RoleViewModel());
        }

        private void _roleList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                _roleList.SelectedItem = _prevSelectedItem;
                return;
            }

            _roleManager.SelectedItem = (RoleViewModel)e.AddedItems[0];
            _prevSelectedItem = e.RemovedItems.Count == 0 ? null : (RoleViewModel)e.RemovedItems[0];
            _accessSetupPage._rightFrame.NavigationService.Navigate(_roleEditPage, _roleManager.SelectedItem);
        }

        public ICommand DeleteCommand { get; set; }

        public ObservableCollection<RoleViewModel> RoleList { get; }
    }
}
