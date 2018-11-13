using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using Security.Contracts;
using WpfApp_TestSecurity.Dialogs;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public class UserRoleManager: BaseManager<RoleViewModel>
    {
        private readonly UserManager _userManager;

        public UserRoleManager(ISecurity security, UserManager userManager) : base(security)
        {
            _userManager = userManager;
            _userManager.PropertyChanged += _userManager_PropertyChanged;
            AddRoleCommand = new RelayCommand(AddRole, CanAddRole);
            DeleteRoleCommand = new RelayCommand(DeleteRole, CanDeleteRole);
        }

        private void AddRole(object obj)
        {
            var exceptRoles = Security.MemberRoleRepository.GetExceptRoles(_userManager.SelectedItem.IdMember).Select(_ => Mapper.Map<RoleViewModel>(_));

            if (exceptRoles.SelectItemsDialogShow(out var selectedRoles))
            {
                foreach (var item in selectedRoles)
                {
                    Items.Add((RoleViewModel) item);
                }
            }
        }

        private bool CanAddRole(object arg)
        {
            return _userManager.SelectedItem != null;
        }

        private void DeleteRole(object obj)
        {
            var roleViewModel = (RoleViewModel)obj;
            if (MessageBox.Show($"Подтвердите удаление роли {roleViewModel.Name}", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Items.Remove(roleViewModel);
        }

        private bool CanDeleteRole(object arg)
        {
            return arg != null;
        }

        public ICommand DeleteRoleCommand { get; set; }

        public ICommand AddRoleCommand { get; set; }

        private void _userManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
                SetItems();
        }

        protected override IEnumerable<RoleViewModel> GetItems()
        {
            if (_userManager?.SelectedItem == null)
                return new RoleViewModel[] { };

            var items = Security.MemberRoleRepository.GetRoles(_userManager.SelectedItem.IdMember);
            return items.Select(_ => Mapper.Map<RoleViewModel>(_));
        }

        protected override async Task<IEnumerable<RoleViewModel>> GetItemsAsync()
        {
            if (_userManager?.SelectedItem == null)
                return new RoleViewModel[] { };

            var items = await Security.MemberRoleRepository.GetRolesAsync(_userManager.SelectedItem.IdMember).ConfigureAwait(false);
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

        protected override RoleViewModel GetNewEmptyItem()
        {
            throw new NotSupportedException();
        }
    }
}
