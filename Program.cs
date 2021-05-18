using System;
using System.IO;
using StaffApp.Utilities;
using StaffApp.Data;
using System.Configuration;

namespace StaffApp
{
    public enum StaffType
    {
        Teacher = 1,
        Admin,
        Support
    }
    class Program
    {
        static void Main(string[] args)
        {
            char continueChoice;
            bool isChar;
		
			string schoolName = ConfigurationManager.AppSettings["SchoolName"];
			string implementationType = ConfigurationManager.AppSettings["ImplementationType"];


            Console.WriteLine("\nWelcome to {0} staff menu !", schoolName);
            Console.WriteLine("----------------------------------------------");

            do
            {
                int menuChoice;
                var staffObj = (IStaff) Activator.CreateInstance(Type.GetType("StaffApp.Data." + implementationType + ", StaffApp"));

                Utils.OperationsMenu();

                if (int.TryParse(Console.ReadLine(), out menuChoice))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            int staffChoice;
                            Utils.AddMenu();

                            if (int.TryParse(Console.ReadLine(), out staffChoice))
                            {
                                if (Enum.IsDefined(typeof(StaffType), staffChoice))
                                {
                                    staffObj.AddStaffDetails(staffChoice);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice");
                            }
                            break;
                        case 2:
                            int viewChoice;
                            Utils.ViewMenu();
                            if (int.TryParse(Console.ReadLine(), out viewChoice))
                            {
                                staffObj.ViewStaffDetails(viewChoice);
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice");
                            }
                            break;
                        case 3:
                            staffObj.UpdateStaffDetails();
                            break;
                        case 4:
                            staffObj.DeleteStaffDetails();
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }

                Console.WriteLine("\nPress Y to continue...");
                isChar = Char.TryParse(Console.ReadLine().ToLower(), out continueChoice);
            } while (isChar == true && continueChoice == 'y');
        }
    }
}


