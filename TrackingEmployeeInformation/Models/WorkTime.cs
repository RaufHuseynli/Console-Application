using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingEmployeeInformation
{
   public class WorkTime
    {
        public int EmployeeId { get; set; }
        public PersonalInfo Employee { get; set; }
        public int EntryHour { get; set; }
        public int EntryMinute { get; set; }
        public int DepatureHour { get; set; }
        public int DepatureMinute { get; set; }
    }
}
