using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using AutoMapper;
using Security.Contracts;
using Security.Model;
using WpfApp_TestSecurity.Annotations;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.ViewModels;
using Application = System.Windows.Application;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public abstract class BaseManager<T>: INotifyPropertyChanged where T: BaseViewModel<T>
    {
        private ObservableCollection<T> _items = new SecurityObservableCollection<T>();
        private Action _setItemAction;
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
                Debug.WriteLine("Call Get Items");

                if (_setItemAction == null && _items.Count == 0)
                {
                    _setItemAction = () =>
                    {
                        SetItems();
                        _setItemAction = null;
                    };
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, _setItemAction);
                }
                return _items;
            }
        }

        protected void SetItems()
        {
            _items.CollectionChanged -= ViewModels_CollectionChanged;

            _items.Clear();
            var items = GetItems();
            foreach (var item in items)
            {
                //await Task.Delay(1);
                _items.Add(item);
                item.Seal();
            }

            _items.CollectionChanged += ViewModels_CollectionChanged;
            OnItemsLoadComplete();
        }

        public void CreateEmptyItem()
        {
            var item = GetNewEmptyItem();
            _items.CollectionChanged -= ViewModels_CollectionChanged;
            _items.Add(item);
            var index = _items.IndexOf(item);
            SelectedItem = _items[index];
            _items.CollectionChanged += ViewModels_CollectionChanged;
        }

        protected abstract T GetNewEmptyItem();

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
            SelectedItem = model;

            model.Seal();
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

        public event ItemsLoadCompleteHandler ItemsLoadComplete;

        protected virtual void OnItemsLoadComplete()
        {
            ItemsLoadComplete?.Invoke(this, EventArgs.Empty);
        }
    }

    public delegate void ItemsLoadCompleteHandler(object sender, EventArgs args);
}