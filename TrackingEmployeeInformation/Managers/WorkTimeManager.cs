using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrackingEmployeeInformation.Managers
{
    public class WorkTimeManager
    {
        static List<PersonalInfo> personalInfos = new List<PersonalInfo>();
        static List<WorkTime> worktimeList = new List<WorkTime>();

        static string DataSource = @"Data Source=RAUF\SQLEXPRESS;Initial Catalog=EmployeeTrackingManagments;Integrated Security=True";
        private static string connectiondata;

        public static void WorkTimeAdder()
        {

            string iscontun = "he";

            do
            {

                WorkTime worktime = new WorkTime();
                
                Console.WriteLine("Isci ID si tetbiq olunur ...");
                Thread.Sleep(500);
                worktime.EmployeeId = CustomId.CustomIdG();
                Console.WriteLine("Yeni isci ID si : ");


                Console.Write("Giriw saatini daxil edin : ");
                worktime.EntryHour = Convert.ToInt32(Console.ReadLine());
                Console.Write("Giriw deqiqesini daxil edin : ");
                worktime.EntryMinute = Convert.ToInt32(Console.ReadLine());
                Console.Write("Cixiw saatini daxil edin : ");
                worktime.EntryHour = Convert.ToInt32(Console.ReadLine());
                Console.Write("Cixiw deqiqesini daxil edin : ");
                worktime.DepatureMinute = Convert.ToInt32(Console.ReadLine());

                worktimeList.Add(worktime);
                AddToDatabaseoFWorktime();

                Console.WriteLine("Davam etmek isteyirsiniz? : ");

                iscontun = Console.ReadLine();

            } while (iscontun == "he");

        }
        public static void AddToDatabaseoFWorktime()
        {
            WorkTime Employeer = new WorkTime();
            SqlConnection sqlConnection = new SqlConnection(DataSource);
            sqlConnection.Open();
            string query = $"INSERT INTO[dbo].[Worktime] ([Id],[PersonalNumber],[EntryHour],[EntryMinutes],[DepartureHour],[DepartureMinutes])VALUES(@Id,@PersonalNumber,@EntryHour,@EntryMinutes,@DepartureHour,@DepartureMinutes);";
            Employeer.EmployeeId = Convert.ToInt32( Console.ReadLine());
            Employeer.EntryHour = Convert.ToInt32(Console.ReadLine());
            
            Employeer.EntryMinute = Convert.ToInt32(Console.ReadLine());
            Employeer.DepatureHour = Convert.ToInt32(Console.ReadLine());
            Employeer.DepatureMinute = Convert.ToInt32(Console.ReadLine());

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", Employeer.EmployeeId);
            sqlCommand.Parameters.AddWithValue("@PersonalNumber", Employeer.EmployeeId);
            sqlCommand.Parameters.AddWithValue("@EntryHour", Employeer.EntryHour);
            sqlCommand.Parameters.AddWithValue("@EntryMinutes", Employeer.EntryMinute);
            sqlCommand.Parameters.AddWithValue("@DepartureHour", Employeer.DepatureHour);
            sqlCommand.Parameters.AddWithValue("@DepartureMinutes", Employeer.DepatureMinute );

            



            sqlCommand.ExecuteNonQuery();


            sqlConnection.Close();


        }
        //public static void WorkTimeReader(int employeeNumber)
        //{
        //    WorkTime Employeer = new WorkTime();
        //    for (int i = 0; i < worktimeList.Count; i++)
        //    {
        //        if (worktimeList[i].EmployeeId == employeeNumber)
        //        {
        //            Console.WriteLine($"Iwci nomresi: {Employeer.EmployeeId}");
        //            Console.WriteLine($"Iwcinin giriw saati: {Employeer.EntryHour}");
        //            Console.WriteLine($"Iwcinin giriw deqiqesi: {Employeer.EntryMinute}");
        //            Console.WriteLine($"Iwcinin cixiw saati: {Employeer.DepatureHour}");
        //            Console.WriteLine($"Iwcinin cixiw  deqiqesi: {Employeer.DepatureMinute}");
        //        }
        //    }
        //}

        public static void WorkTimeDelete(int EmployeeNumber)
        {
            for (int i = 0; i < worktimeList.Count; i++)
            {
                if (worktimeList[i].EmployeeId == EmployeeNumber)
                {
                    worktimeList.Remove(worktimeList[i]);
                }
            }
        }
        public static void LateWorkerInfo()
        {

            SqlConnection sqlcon = new SqlConnection(connectiondata);
            SqlDataReader sqlDataReader;
            sqlcon.Open();
            string query = "select PersonalNumber, EntryHour, EntryMinutes from [dbo].[WorkTime] where EntryHour > 9 or (EntryHour = 9 and EntryMinutes>0)";
            SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                WorkTime workTime = new WorkTime();
                workTime.EmployeeId = (int)sqlDataReader.GetValue(0);
                workTime.EntryHour = (int)sqlDataReader.GetValue(1);
                workTime.EntryMinute = (int)sqlDataReader.GetValue(2);

                worktimeList.Add(workTime);
            }
            SqlDataReader sqlDataReaders;
            foreach (var item in worktimeList)
            {
                string employeeQuery = $"select EmployeeNumber, Name, Surname, tblEmployee.DateofStart, Position, SalaryRate, WorkingMinutebyMonth from [dbo].[tblEmployee] where EmployeeNumber ={item.EmployeeId};";
                SqlCommand sqlCommandEmployee = new SqlCommand(employeeQuery, sqlcon);
                sqlDataReaders = sqlCommandEmployee.ExecuteReader();
                Console.WriteLine($"Employee number : {(int)sqlDataReader.GetValue(0)}");
            }
            sqlcon.Close();
        }
        public static void OutfHourInfo()
        {

            foreach (var item in worktimeList)
            {
                if (item.DepatureHour > 18 || (item.DepatureHour == 18 && item.DepatureMinute > 0))
                {
                    Console.WriteLine($"Ici Nomresi: {item.EmployeeId} Adi {item.Employee.Name} Soyadi {item.Employee.Surname}");
                }
            }


        }
    

    }

}
