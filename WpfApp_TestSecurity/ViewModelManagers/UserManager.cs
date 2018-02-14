using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Security.Model;
using Security.V2.Contracts;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public class UserManager
    {
        private readonly ISecurity _security;
        private ObservableCollection<UserViewModel> _users = new SecurityObservableCollection<UserViewModel>();

        public UserManager(ISecurity security)
        {
            _security = security;
        }

        public ObservableCollection<UserViewModel> Users
        {
            get
            {
                if (_users.Count == 0)
                    SetUsers();

                return _users;
            }
        }

        private ObservableCollection<UserViewModel> GetUsers()
        {
            var userViewModels = new ObservableCollection<UserViewModel>(_security.UserRepository.Get().Select(u => Mapper.Map<UserViewModel>(u)));

            return userViewModels;
        }

        private async Task<ObservableCollection<UserViewModel>> GetUsersAsync()
        {
            var users = await _security.UserRepository.GetAsync().ConfigureAwait(false);
            var userViewModels = new ObservableCollection<UserViewModel>(users.Select(u => Mapper.Map<UserViewModel>(u)));

            return userViewModels;
        }

        private void InitObservable<T>(ObservableCollection<T> viewModels)
        {
            viewModels.CollectionChanged += ViewModels_CollectionChanged;
        }

        private void ViewModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    foreach (var item in e.NewItems)
                    {
                        ((UserViewModel)item).DateCreated = DateTime.Now;
                        var user = _security.UserRepository.CreateAsync(Mapper.Map<User>(item)).GetAwaiter().GetResult();
                        Mapper.Map(user, item);
                    }

                    break;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    foreach (var item in e.NewItems)
                    {
                        _security.UserRepository.UpdateAsync(Mapper.Map<User>(item)).Wait();
                    }
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    foreach (var item in e.OldItems)
                    {
                        _security.UserRepository.RemoveAsync(((UserViewModel) item).IdMember).Wait();
                    }
                    break;
                }
            }
        }

        public void SetPassword(UserViewModel model, string password)
        {
            if (!_security.SetPassword(model.Login, password))
                throw new InvalidOperationException("Не удалось установить пароль!");
        }

        public async void SetUsers()
        {
            var users = GetUsersAsync();
            foreach (var userModel in (await users))
            {
                await Task.Delay(10);
                _users.Add(userModel);
                userModel.Seal();
            }
            InitObservable(_users);
        }

        public void SetStatus(UserViewModel model, bool status)
        {
            _security.UserRepository.SetStatus(model.Login, status);
        }
    }
}
