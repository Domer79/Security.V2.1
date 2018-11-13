using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_TestSecurity.ViewModels
{
    public class PolicyViewModel: BaseViewModel<PolicyViewModel>
    {
        private string _name;

        /// <summary>
        /// Идентификатор объекта в базе данных
        /// </summary>
        public int IdSecObject { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Идентификатор приложения
        /// </summary>
        public int IdApplication { get; set; }

    }
}
