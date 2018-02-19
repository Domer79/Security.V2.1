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
    public class RoleManager: BaseManager<RoleViewModel>
    {
        public RoleManager(ISecurity security) : base(security)
        {
        }

        protected override IEnumerable<RoleViewModel> GetItems()
        {
            var roleViewModels = Security.RoleRepository.Get().Select(r => Mapper.Map<RoleViewModel>(r));

            return roleViewModels;
        }

        protected override async Task<IEnumerable<RoleViewModel>> GetItemsAsync()
        {
            var roles = await Security.RoleRepository.GetAsync().ConfigureAwait(false);
            var roleViewModels = roles.Select(r => Mapper.Map<RoleViewModel>(r));

            return roleViewModels;
        }

        protected override RoleViewModel GetNewEmptyItem()
        {
            return Mapper.Map<RoleViewModel>(Security.RoleRepository.CreateEmpty("Новая роль "));
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
