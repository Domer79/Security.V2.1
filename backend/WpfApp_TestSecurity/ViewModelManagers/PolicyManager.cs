using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using Security.Contracts;
using Security.Model;
using WpfApp_TestSecurity.Dialogs;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public class PolicyManager: BaseManager<PolicyViewModel>
    {
        private readonly RoleManager _roleManager;

        public PolicyManager(ISecurity security, RoleManager roleManager) : base(security)
        {
            _roleManager = roleManager;
            _roleManager.PropertyChanged += _roleManager_PropertyChanged;
            AddPolicyCommand = new RelayCommand(AddPolicy, CanAddPolicy);
            DeletePolicyCommand = new RelayCommand(DeletePolicy, CanDeletePolicy);
        }

        public ICommand AddPolicyCommand { get; set; }

        public ICommand DeletePolicyCommand { get; set; }

        private void AddPolicy(object obj)
        {
            var exceptPolicies = Security.GrantRepository.GetExceptRoleGrant(_roleManager.SelectedItem.Name)
                .Select(_ => Mapper.Map<PolicyViewModel>(_));

            if (exceptPolicies.SelectItemsDialogShow(out var selectedPolicies))
            {
                foreach (var policy in selectedPolicies)
                {
                    Items.Add((PolicyViewModel) policy);
                }
            }
        }

        private bool CanAddPolicy(object arg)
        {
            return _roleManager.SelectedItem != null;
        }

        private void DeletePolicy(object obj)
        {
            var policyViewModel = (PolicyViewModel)obj;
            if (MessageBox.Show($"Подтвердите удаление политики {policyViewModel.Name}", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Items.Remove(policyViewModel);
        }

        private bool CanDeletePolicy(object arg)
        {
            return arg != null;
        }

        private void _roleManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
                SetItems();
        }

        protected override PolicyViewModel GetNewEmptyItem()
        {
            throw new NotSupportedException();
        }

        protected override IEnumerable<PolicyViewModel> GetItems()
        {
            if (_roleManager?.SelectedItem == null)
                return new PolicyViewModel[] { };

            var items = Security.GrantRepository.GetRoleGrants(_roleManager.SelectedItem.Name);
            return items.Select(_ => Mapper.Map<PolicyViewModel>(_));
        }

        protected override async Task<IEnumerable<PolicyViewModel>> GetItemsAsync()
        {
            if (_roleManager?.SelectedItem == null)
                return new PolicyViewModel[] { };

            var items = await Security.GrantRepository.GetRoleGrantsAsync(_roleManager.SelectedItem.Name).ConfigureAwait(false);
            return items.Select(_ => Mapper.Map<PolicyViewModel>(_));
        }

        protected override void SaveDeletedItem(PolicyViewModel item)
        {
            Security.GrantRepository.RemoveGrant(_roleManager.SelectedItem.Name, item.Name);
        }

        protected override void SaveModifiedItem(PolicyViewModel item)
        {
            throw new NotSupportedException();
        }

        protected override void SaveAddingItem(PolicyViewModel item)
        {
            Security.GrantRepository.SetGrant(_roleManager.SelectedItem.Name, item.Name);
        }
    }
}
