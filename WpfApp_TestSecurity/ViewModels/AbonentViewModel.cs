using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_TestSecurity.ViewModels
{
    public class AbonentViewModel: BaseViewModel<AbonentViewModel>
    {
        private string _inn;
        private string _shortName;
        private string _fullName;
        private string _agentPhones;
        private string _agent;
        private string _managerFullName;
        private string _phoneNumbers;
        private string _address;

        public string Inn
        {
            get { return _inn; }
            set
            {
                if (value == _inn) return;
                _inn = value;
                OnPropertyChanged();
            }
        }

        public string ShortName
        {
            get { return _shortName; }
            set
            {
                if (value == _shortName) return;
                _shortName = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (value == _fullName) return;
                _fullName = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (value == _address) return;
                _address = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumbers
        {
            get { return _phoneNumbers; }
            set
            {
                if (value == _phoneNumbers) return;
                _phoneNumbers = value;
                OnPropertyChanged();
            }
        }

        public string ManagerFullName
        {
            get { return _managerFullName; }
            set
            {
                if (value == _managerFullName) return;
                _managerFullName = value;
                OnPropertyChanged();
            }
        }

        public string Agent
        {
            get { return _agent; }
            set
            {
                if (value == _agent) return;
                _agent = value;
                OnPropertyChanged();
            }
        }

        public string AgentPhones
        {
            get { return _agentPhones; }
            set
            {
                if (value == _agentPhones) return;
                _agentPhones = value;
                OnPropertyChanged();
            }
        }
    }
}
