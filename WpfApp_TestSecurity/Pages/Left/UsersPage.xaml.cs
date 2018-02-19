using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp_TestSecurity.Annotations;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.Pages.Right;
using WpfApp_TestSecurity.ViewModelManagers;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Pages.Left
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class UsersPage : Page, INotifyPropertyChanged
    {
        private readonly UserManager _userManager;
        private readonly AccessSetupPage _accessSetupPage;
        private readonly UserEditPage _userEditPage;

        public UsersPage(UserManager userManager, AccessSetupPage accessSetupPage, UserEditPage userEditPage)
        {
            InitializeComponent();

            _userManager = userManager;
            _userManager.PropertyChanged += _userManager_PropertyChanged;
            _userManager.ItemsLoadComplete += _userManager_ItemsLoadComplete            ;
            _accessSetupPage = accessSetupPage;
            _userEditPage = userEditPage;
            UserList = userManager.Items;
            DataContext = this;
            DeleteCommand = new RelayCommand(DeleteItem, CanDelete);
        }

        private void _userManager_ItemsLoadComplete(object sender, EventArgs args)
        {
            SelectedItem = UserList[0];
        }

        private void _userManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedItem")
                OnPropertyChanged("SelectedItem");
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

        public UserViewModel SelectedItem
        {
            get { return _userManager.SelectedItem; }
            set
            {
                if (Equals(value, _userManager.SelectedItem)) return;
                _userManager.SelectedItem = value;
                OnPropertyChanged();
                _accessSetupPage._rightFrame.NavigationService.Navigate(_userEditPage);
            }
        }

        private void AddMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _userManager.CreateEmptyItem();
            _accessSetupPage._rightFrame.NavigationService.Navigate(_userEditPage);
        }

        public ICommand DeleteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
