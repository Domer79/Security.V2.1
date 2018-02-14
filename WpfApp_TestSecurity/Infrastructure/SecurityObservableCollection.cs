using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_TestSecurity.Infrastructure
{
    public class SecurityObservableCollection<T>: ObservableCollection<T>
    {
        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
        }
    }
}
