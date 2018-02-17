using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using AutoMapper;
using Security.Model;
using Security.V2.Contracts;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public class UserManager : BaseManager<UserViewModel>
    {
        public UserManager(ISecurity security) : base(security)
        {
            SetPasswordCommand = new RelayCommand(SetPassword, CanSetPassword);
            SetStatusCommand = new RelayCommand(SetStatus, CanSetStatus);
        }

        public ICommand SetStatusCommand { get; set; }
        public ICommand SetPasswordCommand { get; set; }

        private void SetStatus(object obj)
        {
            var model = (UserViewModel)obj;
            SetStatus(model, !model.Status);
            model.Status = !model.Status;
        }

        private bool CanSetStatus(object arg)
        {
            return true;
        }

        private bool CanSetPassword(object arg)
        {
            var passwordBox = (PasswordBox)arg;
            return passwordBox.Password != string.Empty;
        }

        private void SetPassword(object obj)
        {
            var passwordBox = (PasswordBox)obj;
            SetPassword(SelectedItem, passwordBox.Password);
            passwordBox.Password = string.Empty;
        }

        protected override IEnumerable<UserViewModel> GetItems()
        {
            var userViewModels = Security.UserRepository.Get().Select(u => Mapper.Map<UserViewModel>(u));

            return userViewModels;
        }

        protected override async Task<IEnumerable<UserViewModel>> GetItemsAsync()
        {
            var users = await Security.UserRepository.GetAsync().ConfigureAwait(false);
            var userViewModels = users.Select(u => Mapper.Map<UserViewModel>(u));

            return userViewModels;
        }

        protected override void SaveDeletedItem(UserViewModel item)
        {
            Security.UserRepository.Remove(item.IdMember);
        }

        protected override void SaveModifiedItem(UserViewModel item)
        {
            Security.UserRepository.Update(Mapper.Map<User>(item));
        }

        protected override void SaveAddingItem(UserViewModel item)
        {
            item.DateCreated = DateTime.Now;
            var user = Security.UserRepository.Create(Mapper.Map<User>(item));
            Mapper.Map(user, item);
        }

        public void SetPassword(UserViewModel model, string password)
        {
            if (!Security.SetPassword(model.Login, password))
                throw new InvalidOperationException("Не удалось установить пароль!");
        }

        public void SetStatus(UserViewModel model, bool status)
        {
            Security.UserRepository.SetStatus(model.Login, status);
        }
    }
}
