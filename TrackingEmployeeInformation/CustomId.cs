using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingEmployeeInformation
{
    public class CustomId
    {
        public static int CustomIdG()
        {
            Random rnd = new Random();

            int confirmCode = rnd.Next(1, 100);

            Console.WriteLine(confirmCode);

            return confirmCode;
        }
    }
}

