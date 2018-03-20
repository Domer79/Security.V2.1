using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using Security.Contracts;
using WpfApp_TestSecurity.AbonentBL.Interfaces;
using WpfApp_TestSecurity.AbonentBL.Models;
using WpfApp_TestSecurity.Dialogs;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public class AbonentManager: BaseManager<AbonentViewModel>
    {
        private readonly IAbonentRepository _repository;

        public AbonentManager(ISecurity security, IAbonentRepository repository) : base(security)
        {
            _repository = repository;
            AddCommand = new RelayCommand(Add, CanAdd);
            EditCommand = new RelayCommand(Update, CanUpdate);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            PrintCommand = new RelayCommand(Print, CanPrint);
        }

        public ICommand AddCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand PrintCommand { get; set; }

        private void Add(object obj)
        {
            var abonent = GetNewEmptyItem();
            Items.Add(abonent);
        }

        private bool CanAdd(object arg)
        {
            return Principal.CheckAccess(Policy.AddAbonent);
        }

        private void Update(object obj)
        {
            var abonent = (AbonentViewModel) obj;
            var index = Items.IndexOf(abonent);
            if (DialogHelper.EditAbonentDialogShow(ref abonent))
            {
                Items[index] = abonent;
            }
        }

        private bool CanUpdate(object arg)
        {
            return arg != null && Principal.CheckAccess(Policy.EditAbonent);
        }

        private void Delete(object obj)
        {
            var abonent = (AbonentViewModel)obj;
            if (MessageBox.Show($"Подтвердите удаление абонента {abonent.FullName}", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Items.Remove(abonent);
        }

        private bool CanDelete(object arg)
        {
            return arg != null && Principal.CheckAccess(Policy.DeleteAbonent);
        }

        private void Print(object obj)
        {
            MessageBox.Show(MainWindow.Self, "Список отправлен на печать", "Информация");
        }

        private bool CanPrint(object arg)
        {
            return Principal.CheckAccess(Policy.PrintAbonentList);
        }

        protected override AbonentViewModel GetNewEmptyItem()
        {
            var abonent = new AbonentViewModel();
            if (DialogHelper.EditAbonentDialogShow(ref abonent))
                return abonent;

            return null;
        }

        protected override IEnumerable<AbonentViewModel> GetItems()
        {
            return _repository.Get().Select(_ => Mapper.Map<AbonentViewModel>(_)).ToArray();
        }

        protected override Task<IEnumerable<AbonentViewModel>> GetItemsAsync()
        {
            throw new NotSupportedException();
        }

        protected override void SaveDeletedItem(AbonentViewModel item)
        {
            _repository.Remove(item.Id);
        }

        protected override void SaveModifiedItem(AbonentViewModel item)
        {
            _repository.Update(Mapper.Map<Abonent>(item));
        }

        protected override void SaveAddingItem(AbonentViewModel item)
        {
            var abonent = _repository.Create(Mapper.Map<Abonent>(item));
            Mapper.Map(abonent, item);
        }
    }
}
