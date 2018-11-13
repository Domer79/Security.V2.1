using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.Pages;

namespace WpfApp_TestSecurity.ViewModels
{
    public class LoginViewModel: BaseViewModel<LoginViewModel>
    {
        private readonly NavigationService _navigationService;

        public LoginViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new RelayCommand(LogIn, CanLogIn);
        }

        private bool CanLogIn(object arg)
        {
            return !string.IsNullOrWhiteSpace(Login) && arg != null && !string.IsNullOrWhiteSpace(((PasswordBox)arg).Password);
        }

        private void LogIn(object obj)
        {
            Principal.LogIn(Login, ((PasswordBox)obj).Password);
            if (Principal.Identity.IsAuthenticated)
                _navigationService.GoBack();
        }

        public string Login { get; set; }

        public ICommand LoginCommand { get; set; }
    }
}
