using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrackingEmployeeInformation
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public int EmployeeNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfStart { get; set; }
        public decimal SalaryRate { get; set; }
        public string Position { get; set; }
        public int MontlyWorkingMinute { get; set; }

    }
}
