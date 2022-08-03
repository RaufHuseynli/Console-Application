using System;
using System.Threading;
using TrackingEmployeeInformation.Managers;
using TrackingEmployeeInformation.Models;

namespace TrackingEmployeeInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=========================== Kadr idaretme proqrami ===========================");

            menu();
        }
        public static void menu()
        {
            Console.WriteLine("1.Bir işçinin melumatlarının gösterilmesi.");
            Thread.Sleep(200);
            Console.WriteLine("2.Bir işçinin melumatlarının gösterilmesi ve hemin ay iş melumatların gösterilmesi.");
            Thread.Sleep(200);
            Console.WriteLine("3.Müeyyen bir ünvana göre işçilerin siyahısının görüntülenmesi.");
            Thread.Sleep(200);
            Console.WriteLine("4.İşe qebul olunan işçilerin sayının iller üzre bölgüsünün görüntülenmesi.");
            Thread.Sleep(200);
            Console.WriteLine("5. İşe gec gelen işçilerin siyahısı.");
            Thread.Sleep(200);
            Console.WriteLine("6.Müeyyen bir günün elave iş qeydlerinin sadalanması.");
            Thread.Sleep(200);
            Console.WriteLine("7.Yeni işçinin elave edilmesi");
            Thread.Sleep(200);
            Console.WriteLine("8.İşçi melumatlarının yenilenmesi");
            Thread.Sleep(200);
            Console.WriteLine("9.Müeyyen bir günün iş qeydlerinin elave edilmesi");
            Thread.Sleep(200);
            Console.WriteLine("10.İşçi melumatlarının dosyalardan silinmesi");
            Thread.Sleep(200);
            Console.WriteLine("11.Çıxış");
            Thread.Sleep(200);
            Console.Write("Daxil et: ");
            int secim = int.Parse(Console.ReadLine());
            switch (secim)
            {
                case 1:
                    Console.Write("Isci nömresini giriniz: ");
                    int nomre = Convert.ToInt32(Console.ReadLine());
                    EmployeeManager.EmployeeReader(nomre);
                    menu();
                    break;
                case 2:
                    Console.Write("Iscinin id'si daxil edin:  ");
                    int nomree = Convert.ToInt32(Console.ReadLine());
                    EmployeeManager.EmployeeReader(nomree);
                    
                    menu();
                    break;
                case 3:
                    Console.Write("İşçi adresini girin: ");
                    string adress = Console.ReadLine();
                    EmployeeManager.FindEmployeebyPosition();
                    menu();
                    break;
                case 4:

                    menu();
                    break;
                case 5:
                    WorkTimeManager.LateWorkerInfo();
                    menu();
                    break;
                case 6:
                    EmployeeManager.SelectWorkerbyYear();
                    menu();
                    break;
                case 7:
                    string iscontune = "he";
                    do
                    {
                        EmployeeManager.EmployeeAdder();
                        Console.WriteLine("Davam etmek isteyirsiniz? he / yox");
                        iscontune = Console.ReadLine().ToLower();
                    } while (iscontune == "he");
                    
                    menu();
                    break;
                case 8:
                    Console.Write("Melumatini deyişmek istediyiniz işçinin işçi nömresini giriniz : ");
                    int nomreee = Convert.ToInt32(Console.ReadLine());

                    EmployeeManager.EmployeeUpdate();
                    menu();
                    break;
                case 9:
                    return;
                case 10:
                    Console.Write("Melumatını silmek istediyiniz işçinin işçi nömresini girin: ");
                    int nomreeee = Convert.ToInt32(Console.ReadLine());
                    EmployeeManager.EmployeeDelete(nomreeee);
                    menu();
                    break;
                case 11:
                    WorkTimeManager.AddToDatabaseoFWorktime();
                    break;
                default:
                    break;
            }
        }
    }
}
