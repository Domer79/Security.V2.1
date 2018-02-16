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
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public class UserManager : ManagerBase<UserViewModel>
    {
        public UserManager(ISecurity security) : base(security)
        {
        }

        protected override ObservableCollection<UserViewModel> GetItems()
        {
            var userViewModels = new ObservableCollection<UserViewModel>(Security.UserRepository.Get().Select(u => Mapper.Map<UserViewModel>(u)));

            return userViewModels;
        }

        protected override async Task<ObservableCollection<UserViewModel>> GetItemsAsync()
        {
            var users = await Security.UserRepository.GetAsync().ConfigureAwait(false);
            var userViewModels = new ObservableCollection<UserViewModel>(users.Select(u => Mapper.Map<UserViewModel>(u)));

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
