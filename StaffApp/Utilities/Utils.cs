using System;
using StaffLibrary.Models.Base;


namespace StaffApp.Utilities
{
	public static class Utils
    {
        public static void OperationsMenu(StaffType type)
        {
			string staffType = type.ToString();

            Console.WriteLine("\nEnter your choice:\n");
            Console.WriteLine("\t1 - Add " + staffType + " staff");
            Console.WriteLine("\t2 - View " + staffType + " staff");
            Console.WriteLine("\t3 - Update " + staffType + " staff");
            Console.WriteLine("\t4 - Delete " + staffType + " staff");
        }
        public static void StaffMenu()
        {
            Console.WriteLine("Enter staff type\n");
            Console.WriteLine("\t1 - Teaching");
            Console.WriteLine("\t2 - Administrative");
            Console.WriteLine("\t3 - Support");
        }
        public static void ViewMenu()
        {
            Console.WriteLine("Enter your choice\n");
            Console.WriteLine("\t1 - View by Id");
            Console.WriteLine("\t2 - View All");
        }

        public static void AddCommonDetails(Staff addObj)
        {
            Console.WriteLine("Enter the staff name");
            addObj.Name = Console.ReadLine();
            Console.WriteLine("Enter the staff phone number");
            addObj.Phone = Console.ReadLine();
        }

        public static void DisplayCommonDetails(Staff viewObj)
        {
            Console.WriteLine("Id: {0}", viewObj.Id);
            Console.WriteLine("Name: {0}", viewObj.Name);
            Console.WriteLine("Type: {0}", viewObj.Type);
            Console.WriteLine("Phone: {0}", viewObj.Phone);
        }

    }
}
