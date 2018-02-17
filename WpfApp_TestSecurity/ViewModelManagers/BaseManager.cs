using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Security.Model;
using Security.V2.Contracts;
using WpfApp_TestSecurity.Annotations;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public abstract class BaseManager<T>: INotifyPropertyChanged where T: BaseViewModel<T>
    {
        private ObservableCollection<T> _items = new SecurityObservableCollection<T>();
        private T _selectedItem;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseManager(ISecurity security)
        {
            Security = security;
            SaveCommand = new RelayCommand(SaveItem, CanSave);
        }

        public ObservableCollection<T> Items
        {
            get
            {
                if (_items.Count == 0)
                    SetItems();

                return _items;
            }
        }

        public ICommand SaveCommand { get; set; }

        protected ISecurity Security { get; }

        protected abstract IEnumerable<T> GetItems();
        protected abstract Task<IEnumerable<T>> GetItemsAsync();

        private bool CanSave(object arg)
        {
            return arg != null && ((T)arg).IsChanged();
        }

        private void SaveItem(object uvm)
        {
            var model = (T)uvm;
            var index = Items.IndexOf(model);
            if (index == -1)
            {
                Items.Add(model);
                return;
            }

            Items[index] = model;

            model.Seal();
        }

        private async void SetItems()
        {
            _items.CollectionChanged -= ViewModels_CollectionChanged;

            var itemsAsync = GetItemsAsync();
            foreach (var item in (await itemsAsync))
            {
                await Task.Delay(1);
                _items.Add(item);
                item.Seal();
            }

            _items.CollectionChanged += ViewModels_CollectionChanged;
        }

        private void ViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    foreach (var item in e.NewItems)
                    {
                        SaveAddingItem((T)item);
                    }

                    break;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    foreach (var item in e.NewItems)
                    {
                        SaveModifiedItem((T)item);
                    }
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    foreach (var item in e.OldItems)
                    {
                        SaveDeletedItem((T)item);
                    }
                    break;
                }
            }
        }

        protected abstract void SaveDeletedItem(T item);

        protected abstract void SaveModifiedItem(T item);

        protected abstract void SaveAddingItem(T item);

        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(value, _selectedItem)) return;
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}