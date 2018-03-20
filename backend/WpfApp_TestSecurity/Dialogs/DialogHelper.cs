using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp_TestSecurity.Annotations;
using WpfApp_TestSecurity.ViewModels;

namespace WpfApp_TestSecurity.Dialogs
{
    public static class DialogHelper
    {
        public static bool SelectItemsDialogShow(this IEnumerable<BaseViewModel> source,
            out IEnumerable<BaseViewModel> selectedItems)
        {
            selectedItems = null;
            var dialog = new SelectItemsDialog();
            dialog.Items = source;
            if (dialog.ShowDialog().Value)
            {
                selectedItems = dialog.SelectedItems.ToArray();
                return true;
            }

            return false;
        }

        public static bool EditAbonentDialogShow(ref AbonentViewModel abonent)
        {
            var dialog = new AbonentEditor();
            dialog.Abonent = abonent ?? new AbonentViewModel();
            if (dialog.ShowDialog().Value)
            {
                abonent = dialog.Abonent;
                return true;
            }

            return false;
        }
    }
}
