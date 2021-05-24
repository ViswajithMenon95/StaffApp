using System;
using StaffApp.Utilities;
using StaffLibrary.Data;
using System.Configuration;
using StaffApp.Helpers;

namespace StaffApp
{
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
                var staffObj = (IStaff) Activator.CreateInstance(Type.GetType("StaffLibrary.Data." + implementationType + ", StaffLibrary"));
				int staffChoice;
				Utils.StaffMenu();

				if (int.TryParse(Console.ReadLine(), out staffChoice))
				{
					if (Enum.IsDefined(typeof(StaffType), staffChoice))
					{
						int menuChoice;
						Utils.OperationsMenu();

						if (int.TryParse(Console.ReadLine(), out menuChoice))
						{
							switch (menuChoice)
							{
								case 1:
									StaffHelper.AddDetails((StaffType)staffChoice, staffObj);
									break;
								case 2:
									int viewChoice;
									Utils.ViewMenu();
									if (int.TryParse(Console.ReadLine(), out viewChoice))
									{
										StaffHelper.ViewDetails((StaffType)staffChoice, viewChoice, staffObj);
									}
									else
									{
										Console.WriteLine("Invalid choice");
									}
									break;
								case 3:
									StaffHelper.UpdateDetails((StaffType)staffChoice, staffObj);
									break;
								case 4:
									StaffHelper.DeleteDetails((StaffType)staffChoice, staffObj);
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
				
                Console.WriteLine("\nPress Y to continue...");
                isChar = Char.TryParse(Console.ReadLine().ToLower(), out continueChoice);
            } while (isChar == true && continueChoice == 'y');
        }
    }
}


