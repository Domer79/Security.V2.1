using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp_TestSecurity.Annotations;

namespace WpfApp_TestSecurity.ViewModels
{
    public class RoleViewModel: BaseViewModel<RoleViewModel>
    {
        private string _name;
        private string _description;

        /// <summary>
        /// Идентификатор роли в БД
        /// </summary>
        public int IdRole { get; set; }

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
        /// Описание
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public int IdApplication { get; set; }
    }
}
