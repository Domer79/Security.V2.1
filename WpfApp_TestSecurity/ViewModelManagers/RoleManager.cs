using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Security.Model;
using Security.V2.Contracts;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public class RoleManager: ManagerBase<RoleViewModel>
    {
        public RoleManager(ISecurity security) : base(security)
        {
        }

        protected override ObservableCollection<RoleViewModel> GetItems()
        {
            var roleViewModels = new ObservableCollection<RoleViewModel>(Security.RoleRepository.Get().Select(r => Mapper.Map<RoleViewModel>(r)));

            return roleViewModels;
        }

        protected override async Task<ObservableCollection<RoleViewModel>> GetItemsAsync()
        {
            var roles = await Security.RoleRepository.GetAsync().ConfigureAwait(false);
            var roleViewModels = new ObservableCollection<RoleViewModel>(roles.Select(r => Mapper.Map<RoleViewModel>(r)));

            return roleViewModels;
        }

        protected override void SaveAddingItem(RoleViewModel item)
        {
            var role = Security.RoleRepository.Create(Mapper.Map<Role>(item));
            Mapper.Map(role, item);
        }

        protected override void SaveDeletedItem(RoleViewModel item)
        {
            Security.RoleRepository.Remove(item.IdRole);
        }

        protected override void SaveModifiedItem(RoleViewModel item)
        {
            Security.RoleRepository.Update(Mapper.Map<Role>(item));
        }
    }
}
