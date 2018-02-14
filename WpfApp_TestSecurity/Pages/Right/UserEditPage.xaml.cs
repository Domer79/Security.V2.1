using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using AutoCompare;
using AutoMapper;
using WpfApp_TestSecurity.Annotations;
using WpfApp_TestSecurity.ViewModelManagers;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Pages.Right
{
    /// <summary>
    /// Interaction logic for UserEditPage.xaml
    /// </summary>
    public partial class UserEditPage : PageFunction<UserViewModel>
    {
        private readonly AccessSetupPage _accessSetupPage;
        private readonly UserManager _userManager;
        

        public UserEditPage(AccessSetupPage accessSetupPage, UserManager userManager)
        {
            _accessSetupPage = accessSetupPage;
            _userManager = userManager;
            UserEditDataContext = new UserEditDataContext(_userManager);
            DataContext = UserEditDataContext;
            InitializeComponent();
        }

        public void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Debug.WriteLine(e);
            UserEditDataContext.UserViewModel = (UserViewModel)e.ExtraData;
            _accessSetupPage._rightFrame.NavigationService.LoadCompleted -= NavigationService_LoadCompleted;
        }

        public UserEditDataContext UserEditDataContext { get; set; }
    }

    public class UserEditDataContext: INotifyPropertyChanged
    {
        private readonly UserManager _userManager;
        private UserViewModel _userViewModel;

        public UserEditDataContext(UserManager userManager)
        {
            _userManager = userManager;
            SaveCommand = new RelayCommand(SaveUser, CanSave);
            SetPasswordCommand = new RelayCommand(SetPassword, CanSetPassword);
            SetStatusCommand = new RelayCommand(SetStatus, CanSetStatus);
        }

        private void SetStatus(object obj)
        {
            var model = (UserViewModel)obj;
            _userManager.SetStatus(model, !model.Status);
            model.Status = !model.Status;
        }

        private bool CanSetStatus(object arg)
        {
            return true;
        }

        public RelayCommand SetStatusCommand { get; set; }

        private bool CanSetPassword(object arg)
        {
            var passwordBox = (PasswordBox) arg;
            return passwordBox.Password != string.Empty;
        }

        private void SetPassword(object obj)
        {
            var passwordBox = (PasswordBox)obj;
            _userManager.SetPassword(_userViewModel, passwordBox.Password);
            passwordBox.Password = string.Empty;
        }

        public UserViewModel UserViewModel
        {
            get { return _userViewModel; }
            set
            {
                _userViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand SetPasswordCommand { get; set; }

        private bool CanSave(object arg)
        {
            return arg != null && ((UserViewModel) arg).IsChanged();
        }

        private void SaveUser(object uvm)
        {
            var model = (UserViewModel) uvm;
            var index = _userManager.Users.IndexOf(model);
            if (index == -1)
            {
                _userManager.Users.Add(model);
                return;
            }

            _userManager.Users[index] = model;

            model.Seal();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        [NotNull] private readonly Action<object> _execute;
        [NotNull] private readonly Func<object, bool> _canExecute;

        public RelayCommand([NotNull] Action<object> execute, [NotNull] Func<object, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
