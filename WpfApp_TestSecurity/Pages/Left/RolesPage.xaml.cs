using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Roles.xaml
    /// </summary>
    public partial class RolesPage : Page, INotifyPropertyChanged
    {
        private readonly RoleManager _roleManager;
        private readonly AccessSetupPage _accessSetupPage;
        private readonly RoleEditPage _roleEditPage;

        public RolesPage(RoleManager roleManager, AccessSetupPage accessSetupPage, RoleEditPage roleEditPage)
        {
            _roleManager = roleManager;
            _roleManager.PropertyChanged += _roleManager_PropertyChanged;
            _accessSetupPage = accessSetupPage;
            _roleEditPage = roleEditPage;
            RoleList = _roleManager.Items;
            DataContext = this;
            DeleteCommand = new RelayCommand(DeleteItem, CanDelete);
            InitializeComponent();
        }

        private void _roleManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
                OnPropertyChanged("SelectedItem");
        }

        private void DeleteItem(object obj)
        {
            var roleViewModel = (RoleViewModel)obj;
            if (MessageBox.Show($"Подтвердите удаление роли {roleViewModel.Name}", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _roleManager.Items.Remove(roleViewModel);
        }

        private bool CanDelete(object arg)
        {
            return arg != null && _roleList.SelectedItem != null;
        }

        private void AddMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _roleManager.CreateEmptyItem();
            _accessSetupPage._rightFrame.NavigationService.Navigate(_roleEditPage);
        }

        public ICommand DeleteCommand { get; set; }

        public ObservableCollection<RoleViewModel> RoleList { get; }

        public RoleViewModel SelectedItem
        {
            get { return _roleManager.SelectedItem; }
            set
            {
                if (Equals(value, _roleManager.SelectedItem)) return;
                _roleManager.SelectedItem = value;
                OnPropertyChanged();
                _accessSetupPage._rightFrame.NavigationService.Navigate(_roleEditPage);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
