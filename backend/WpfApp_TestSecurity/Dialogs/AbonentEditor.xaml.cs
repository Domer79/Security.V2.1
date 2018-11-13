using System.Windows;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Dialogs
{
    /// <summary>
    /// Interaction logic for AbonentEditor.xaml
    /// </summary>
    public partial class AbonentEditor : Window
    {
        public AbonentEditor()
        {
            InitializeComponent();
            Loaded += AbonentEditor_Loaded;
        }

        private void AbonentEditor_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Abonent;
        }

        public AbonentViewModel Abonent { get; set; }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
