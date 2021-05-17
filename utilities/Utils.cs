using System;
using System.Collections.Generic;
using StaffApp.Models.Base;

namespace StaffApp.Utilities
{
    public static class Utils
    {
        public static Staff FindStaff(List<Staff> list)
        {
            Staff findObj = null;
            int findId;

            Console.WriteLine("Enter the Id of the staff member");
            if (Int32.TryParse(Console.ReadLine(), out findId))
            {
                findObj = list.Find(searchObj => searchObj.Id == findId);
            }

            return findObj;
        }
        public static void OperationsMenu()
        {
            Console.WriteLine("\nEnter your choice:\n");
            Console.WriteLine("\t1 - Add");
            Console.WriteLine("\t2 - View");
            Console.WriteLine("\t3 - Update");
            Console.WriteLine("\t4 - Delete");

        }
        public static void AddMenu()
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
            Console.WriteLine("\t2 - View Teaching staff");
            Console.WriteLine("\t3 - View Admin staff");
            Console.WriteLine("\t4 - View Support staff");
            Console.WriteLine("\t5 - View All");

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
