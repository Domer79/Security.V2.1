using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_TestSecurity.AbonentBL.Models
{
    public class Abonent
    {
        public int Id { get; set; }

        public string Inn { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Phones { get; set; }

        public string ManagerFullName { get; set; }

        public string Agent { get; set; }

        public string AgentPhones { get; set; }
    }
}
