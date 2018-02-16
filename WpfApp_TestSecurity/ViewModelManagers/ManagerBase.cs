using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using AutoMapper;
using Security.Model;
using Security.V2.Contracts;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.ViewModelManagers
{
    public abstract class ManagerBase<T> where T: ViewModelBase<T>
    {
        private ObservableCollection<T> _items = new SecurityObservableCollection<T>();

        public ManagerBase(ISecurity security)
        {
            Security = security;
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

        protected ISecurity Security { get; }

        protected abstract ObservableCollection<T> GetItems();
        protected abstract Task<ObservableCollection<T>> GetItemsAsync();

        private void InitObservable(ObservableCollection<T> viewModels)
        {
            viewModels.CollectionChanged += ViewModels_CollectionChanged;
        }

        private async void SetItems()
        {
            var itemsAsync = GetItemsAsync();
            foreach (var item in (await itemsAsync))
            {
                await Task.Delay(1);
                _items.Add(item);
                item.Seal();
            }
            InitObservable(_items);
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
    }
}