using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Security.V2.Contracts;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public class UserRolesManager: BaseManager<RoleViewModel>
    {
        private readonly UserManager _userManager;

        public UserRolesManager(ISecurity security, UserManager userManager) : base(security)
        {
            _userManager = userManager;
        }

        protected override IEnumerable<RoleViewModel> GetItems()
        {
            var items = Security.MemberRoleRepository.GetRolesByIdMember(_userManager.SelectedItem.IdMember);
            return items.Select(_ => Mapper.Map<RoleViewModel>(_));
        }

        protected override async Task<IEnumerable<RoleViewModel>> GetItemsAsync()
        {
            var items = await Security.MemberRoleRepository.GetRolesByIdMemberAsync(_userManager.SelectedItem.IdMember);
            return items.Select(_ => Mapper.Map<RoleViewModel>(_));
        }

        protected override void SaveDeletedItem(RoleViewModel item)
        {
            Security.MemberRoleRepository.DeleteRolesFromMember(new []{item.IdRole}, _userManager.SelectedItem.IdMember);
        }

        protected override void SaveModifiedItem(RoleViewModel item)
        {
            throw new NotSupportedException();
        }

        protected override void SaveAddingItem(RoleViewModel item)
        {
            Security.MemberRoleRepository.AddRolesToMember(new[] { item.IdRole }, _userManager.SelectedItem.IdMember);
        }
    }
}
