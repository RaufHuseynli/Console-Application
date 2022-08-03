using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackingEmployeeInformation.DAL;
using System.Data.SqlClient;

namespace TrackingEmployeeInformation.Models
{
    public class EmployeeManager
    {

        static List<PersonalInfo> employeeList = new List<PersonalInfo>();
        static List<WorkTime> WorkTimeList = new List<WorkTime>();
        static string DataSource = @"Data Source=RAUF\SQLEXPRESS;Initial Catalog=EmployeeTrackingManagments;Integrated Security=True";
        private static string connectiondata;

        public static void EmployeeAdder()
        {



            PersonalInfo person = new PersonalInfo();
            Console.WriteLine("Isci ID si tetbiq olunur ...");
            Thread.Sleep(500);
            person.EmployeeNumber = CustomId.CustomIdG();
            Console.WriteLine("Yeni isci ID si : ");

            Console.Write("Isci adini daxil edin : ");
            person.Name = Console.ReadLine();

            Console.Write("Isci soyadini daxil edin : ");
            person.Surname = Console.ReadLine();

            Console.Write("Iscinin hal-hazirdaki unvanin daxil edin : ");
            person.Position = Console.ReadLine();

            Console.Write("Isci ise baslama tarixini daxil edin : (Il/Ay/Gun)");
            person.DateOfStart = DateTime.Now;



            person.MontlyWorkingMinute = 0;

            Console.Write("Emsali daxil edin: ");

            person.SalaryRate = Convert.ToDecimal(Console.ReadLine());


            Console.WriteLine("Isci elave olunur .....");


            employeeList.Add(person);




            SqlConnection sqlConnection = new SqlConnection(DataSource);
            sqlConnection.Open();
            string query = $"INSERT INTO[dbo].[tblEmployee] ([EmployeeNumber],[Name],[Surname],[DateofStart],[Position],[SalaryRate], [WorkingMinutebyMonth])VALUES(@EmployeeNumber, @Name, @Surname, @DateofStart, @Position, @SalaryRate, @WorkingMinutebyMonth );";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@EmployeeNumber", person.EmployeeNumber);
            sqlCommand.Parameters.AddWithValue("@Name", person.Name);
            sqlCommand.Parameters.AddWithValue("@Surname", person.Surname);
            sqlCommand.Parameters.AddWithValue("@DateofStart", person.DateOfStart);
            sqlCommand.Parameters.AddWithValue("@Position", person.Position);
            sqlCommand.Parameters.AddWithValue("@SalaryRate", person.SalaryRate);
            sqlCommand.Parameters.AddWithValue("@WorkingMinutebyMonth", person.MontlyWorkingMinute);
            sqlCommand.ExecuteNonQuery();


            sqlConnection.Close();






        }

        public static void EmployeeReader(int EmployeeId)
        {
            SqlDataReader sqlDataReader;
            SqlConnection sqlConnection = new SqlConnection(DataSource);
            sqlConnection.Open();
            string query = $"select EmployeeNumber, Name, Surname, tblEmployee.DateofStart, Position, SalaryRate, WorkingMinutebyMonth, WorkTime.EntryHour, WorkTime.EntryMinutes, WorkTime.DepartureHour, WorkTime.DepartureMinutes from [dbo].[tblEmployee] left join WorkTime on tblEmployee.EmployeeNumber = WorkTime.PersonalNumber where EmployeeNumber ={EmployeeId} and WorkTime.EntryHour IS NOT NULL and WorkTime.EntryMinutes IS NOT NULL and WorkTime.DepartureHour IS NOT NULL AND WorkTime.DepartureMinutes IS NOT NULL;";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                PersonalInfo personal = new PersonalInfo();
                WorkTime worktime = new WorkTime(); 

                personal.EmployeeNumber = (int)sqlDataReader.GetValue(0);
                personal.Name = (string)sqlDataReader.GetValue(1);
                personal.Surname = (string)sqlDataReader.GetValue(2);
                personal.DateOfStart = Convert.ToDateTime(sqlDataReader.GetValue(3));
                personal.Position = (string)sqlDataReader.GetValue(4);
                personal.SalaryRate = (decimal)sqlDataReader.GetValue(5);
                personal.MontlyWorkingMinute = (int)sqlDataReader.GetValue(6);
                worktime.EntryHour = (int)sqlDataReader.GetValue(7);
                worktime.EntryMinute = (int)sqlDataReader.GetValue(8);
                worktime.DepatureHour = (int)sqlDataReader.GetValue(9);
                worktime.DepatureMinute = (int)sqlDataReader.GetValue(10);
                WorkTimeList.Add(worktime);
                employeeList.Add(personal);
            }

            for (int i = 0; i < employeeList.Count; i++)
            {


                Console.WriteLine($"Isci nomresi: {employeeList[i].EmployeeNumber}");
                Console.WriteLine($"Isci adi: {employeeList[i].Name}");
                Console.WriteLine($"Isci soyadi: {employeeList[i].Surname}");
                Console.WriteLine($"Isci unvani: {employeeList[i].Position}");
                Console.WriteLine($"Isr bawlama tarixi: {employeeList[i].DateOfStart}");
                Console.WriteLine($"Iwcinin emek haqqi emsali: {employeeList[i].SalaryRate}");
                Console.WriteLine($"Iwcinin bir ay erzinde islediyi deqiqe : {employeeList[i].MontlyWorkingMinute}");
                Console.WriteLine($"Iwcinin giriw saati: {WorkTimeList[i].EntryHour}");
                Console.WriteLine($"Iwcinin giriw deqiqesi: {WorkTimeList[i].EntryMinute}");
                Console.WriteLine($"Iwcinin cixis saati: {WorkTimeList[i].DepatureHour}");
                Console.WriteLine($"Iwcinin cixis deqiqesi: {WorkTimeList[i].DepatureMinute}");

            }


        }


        public static void EmployeeReader(string Adress)
        {
            SqlDataReader sqlDataReader;
            SqlConnection sqlConnection = new SqlConnection(DataSource);
            sqlConnection.Open();
            string query = $"select EmployeeNumber, Name, Surname,tblEmployee.DateofStart, Position, SalaryRate, WorkingMinutebyMonth, WorkTime.EntryHour, WorkTime.EntryMinutes, WorkTime.DepartureHour, WorkTime.DepartureMinutes, Adress from [dbo].[tblEmployee] left join WorkTime on tblEmployee.EmployeeNumber = WorkTime.PersonalNumber where WorkTime.EntryHour IS NOT NULL and WorkTime.EntryMinutes IS NOT NULL and WorkTime.DepartureHour IS NOT NULL AND WorkTime.DepartureMinutes IS NOT NULL and  Adress = '{Adress}';";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                PersonalInfo personal = new PersonalInfo();
                WorkTime worktime = new WorkTime();

                personal.EmployeeNumber = (int)sqlDataReader.GetValue(0);
                personal.Name = (string)sqlDataReader.GetValue(1);
                personal.Surname = (string)sqlDataReader.GetValue(2);
                personal.DateOfStart = Convert.ToDateTime(sqlDataReader.GetValue(3));
                personal.Position = (string)sqlDataReader.GetValue(4);
                personal.SalaryRate = (decimal)sqlDataReader.GetValue(5);
                personal.MontlyWorkingMinute = (int)sqlDataReader.GetValue(6);
                worktime.EntryHour = (int)sqlDataReader.GetValue(7);
                worktime.EntryMinute = (int)sqlDataReader.GetValue(8);
                worktime.DepatureHour = (int)sqlDataReader.GetValue(9);
                worktime.DepatureMinute = (int)sqlDataReader.GetValue(10);
                WorkTimeList.Add(worktime);
                employeeList.Add(personal);
            }

         

            var employees=employeeList.OrderByDescending(p => p.SalaryRate).ToList();
            for (int i = 0; i < employees.Count; i++)
            {

                Console.WriteLine("=================================================");
                Console.WriteLine($"Isci nomresi: {employeeList[i].EmployeeNumber}");
                Console.WriteLine($"Isci adi: {employeeList[i].Name}");
                Console.WriteLine($"Isci soyadi: {employeeList[i].Surname}");
                Console.WriteLine($"Isci unvani: {employeeList[i].Position}");
                Console.WriteLine($"Isr bawlama tarixi: {employeeList[i].DateOfStart}");
                Console.WriteLine($"Iwcinin emek haqqi emsali: {employeeList[i].SalaryRate}");
                Console.WriteLine($"Iwcinin bir ay erzinde islediyi deqiqe : {employeeList[i].MontlyWorkingMinute}");
                Console.WriteLine($"Iwcinin giriw saati: {WorkTimeList[i].EntryHour}");
                Console.WriteLine($"Iwcinin giriw deqiqesi: {WorkTimeList[i].EntryMinute}");
                Console.WriteLine($"Iwcinin cixis saati: {WorkTimeList[i].DepatureHour}");
                Console.WriteLine($"Iwcinin cixis deqiqesi: {WorkTimeList[i].DepatureMinute}");
                Console.WriteLine("=================================================");

            }
        }
        public static void EmployeeDelete(int EmployeeNumber)
        {
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (employeeList[i].EmployeeNumber == EmployeeNumber)
                {
                    employeeList.Remove(employeeList[i]);
                }
            }
        }


        public static void EmployeeUpdate()
        {
            Console.Write("Deyiwiklik ucun iwci nomresi daxil edin:");
            int EmployeeNumber = Convert.ToInt32(Console.ReadLine());
            SqlConnection sqlconnection = new SqlConnection(DataSource);
            sqlconnection.Open();
            Console.WriteLine("Deyiweceyiniz yeni adi daxil edin: ");
            string ad = Console.ReadLine();
            Console.WriteLine("Deyiweceyiniz yeni saheni daxil edin: ");
            string sahe = Console.ReadLine();
            string updateQuery = $"Update [dbo].[tblEmployee] set Name =  '{ad}', Position {sahe}' where EmployeeNumber = {EmployeeNumber};";
            SqlCommand updateCommand = new SqlCommand(updateQuery, sqlconnection);

            updateCommand.ExecuteNonQuery();
            sqlconnection.Close();
     
    
          
        }

        public static void SelectWorkerbyYear()
        {
            Console.Write("Axtariw ucun il daxil edin: ");
            int year = Convert.ToInt32(Console.ReadLine());

            SqlDataReader sqlDataReader;
            SqlConnection sqlConnection = new SqlConnection(DataSource);
            sqlConnection.Open();
            string query = $"select EmployeeNumber, Name, Surname,tblEmployee.DateofStart, Position, SalaryRate from [dbo].[tblEmployee] where DateofStart like  '{year}%'";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                PersonalInfo personal = new PersonalInfo();
                WorkTime worktime = new WorkTime();

                personal.EmployeeNumber = (int)sqlDataReader.GetValue(0);
                personal.Name = (string)sqlDataReader.GetValue(1);
                personal.Surname = (string)sqlDataReader.GetValue(2);
                personal.DateOfStart = Convert.ToDateTime(sqlDataReader.GetValue(3));
                personal.Position = (string)sqlDataReader.GetValue(4);
                personal.SalaryRate = (decimal)sqlDataReader.GetValue(5);
                employeeList.Add(personal);
            }
            foreach (var item in employeeList)
            {
             
            }
        }


        public static void FindEmployeebyPosition()
        {
            Console.Write("Axtariw ucun is sahesi daxil edin: ");
            string sahe = Console.ReadLine();
            SqlDataReader sqlDataReader;
            SqlConnection sqlConnection = new SqlConnection(DataSource);
            sqlConnection.Open();
            string query = $"select EmployeeNumber, Name, Surname,tblEmployee.DateofStart, Position, SalaryRate from [dbo].[tblEmployee] where Position = '{sahe}'";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();


            while (sqlDataReader.Read())
            {
                Console.WriteLine("================================================");
                Console.WriteLine("ID : " + sqlDataReader.GetValue(0));
                Console.WriteLine("Adi :" + sqlDataReader.GetValue(1));
                Console.WriteLine("Soyadi :" + sqlDataReader.GetValue(2));
                Console.WriteLine("Ise baslama tarixi :" + sqlDataReader.GetValue(3));
                Console.WriteLine("Adresi :" + sqlDataReader.GetValue(4));
                Console.WriteLine("================================================");
            }
            sqlConnection.Close();



        }
    }
    }

