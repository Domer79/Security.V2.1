using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private UserViewModel _selectedItem;
        private UserViewModel _prevSelectedItem;

        public UsersPage(UserManager userManager, AccessSetupPage accessSetupPage, UserEditPage userEditPage)
        {
            InitializeComponent();

            _userManager = userManager;
            _accessSetupPage = accessSetupPage;
            _userEditPage = userEditPage;
            UserList = userManager.Users;
            DataContext = this;
        }

        public ObservableCollection<UserViewModel> UserList { get; }

        private void AddMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _accessSetupPage._rightFrame.NavigationService.LoadCompleted +=
                _userEditPage.NavigationService_LoadCompleted;
            _accessSetupPage._rightFrame.NavigationService.Navigate(_userEditPage, new UserViewModel());
        }

        private void _userList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                _userList.SelectedItem = _prevSelectedItem;
                return;
            }
            _selectedItem = (UserViewModel) e.AddedItems[0];
            _prevSelectedItem = e.RemovedItems.Count == 0 ? null : (UserViewModel)e.RemovedItems[0];
            _accessSetupPage._rightFrame.NavigationService.LoadCompleted +=
                _userEditPage.NavigationService_LoadCompleted;
            _accessSetupPage._rightFrame.NavigationService.Navigate(_userEditPage, _selectedItem);
        }

        private void DeleteMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Подтвердите удаление пользователя {_selectedItem.Login}", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _userManager.Users.Remove(_selectedItem);
        }
    }
}
