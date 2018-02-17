using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.Pages.Right;
using WpfApp_TestSecurity.ViewModelManagers;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Pages.Left
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private readonly UserManager _userManager;
        private readonly AccessSetupPage _accessSetupPage;
        private readonly UserEditPage _userEditPage;
        private object _prevSelectedItem;

        public UsersPage(UserManager userManager, AccessSetupPage accessSetupPage, UserEditPage userEditPage)
        {
            InitializeComponent();

            _userManager = userManager;
            _accessSetupPage = accessSetupPage;
            _userEditPage = userEditPage;
            UserList = userManager.Items;
            DataContext = this;
            DeleteCommand = new RelayCommand(DeleteItem, CanDelete);
        }

        private void DeleteItem(object obj)
        {
            var userViewModel = (UserViewModel)obj;
            if (MessageBox.Show($"Подтвердите удаление пользователя {userViewModel.Login}", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _userManager.Items.Remove(userViewModel);
        }

        private bool CanDelete(object arg)
        {
            return arg != null && _userList.SelectedItem != null;
        }

        public ObservableCollection<UserViewModel> UserList { get; }

        private void AddMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _accessSetupPage._rightFrame.NavigationService.Navigate(_userEditPage, new UserViewModel());
        }

        private void _userList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                _userList.SelectedItem = _prevSelectedItem;
                return;
            }

            _userManager.SelectedItem = (UserViewModel)e.AddedItems[0];
            _prevSelectedItem = e.RemovedItems.Count == 0 ? null : (UserViewModel)e.RemovedItems[0];
            _accessSetupPage._rightFrame.NavigationService.Navigate(_userEditPage, _userManager.SelectedItem);
        }

        public ICommand DeleteCommand { get; set; }
    }
}
