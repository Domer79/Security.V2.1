using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp_TestSecurity.Annotations;
using WpfApp_TestSecurity.Infrastructure;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Dialogs
{
    /// <summary>
    /// Interaction logic for SelectItemsDialog.xaml
    /// </summary>
    public partial class SelectItemsDialog : Window, INotifyPropertyChanged
    {
        private IEnumerable<BaseViewModel> _items;

        public SelectItemsDialog()
        {
            InitializeComponent();
            DataContext = this;
            CancelCommand = new RelayCommand(o => DialogResult = false, o => true);
            OkCommand = new RelayCommand(o => DialogResult = true, CanOk);
        }

        private bool CanOk(object arg)
        {
            var itemsCount = ItemList.SelectedItems?.Count;
            return itemsCount != null && itemsCount > 0;
        }

        public ICommand OkCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public IEnumerable<BaseViewModel> Items
        {
            get { return _items; }
            set
            {
                if (Equals(value, _items))
                    return;

                _items = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<BaseViewModel> SelectedItems
        {
            get { return ItemList.SelectedItems.OfType<BaseViewModel>(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
