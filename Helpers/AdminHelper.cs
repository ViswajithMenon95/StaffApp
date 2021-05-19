using System;
using StaffApp.Models;
using StaffApp.Utilities;
using StaffApp.Data;

namespace StaffApp.Helpers
{
	class AdminHelper
	{
		public static void AddAdminDetails(IStaff staffObj)
		{
			Admin addObj = new Admin();

			Utils.AddCommonDetails(addObj);

			int newId = staffObj.GetMaxId() + 1;
			addObj.Id = newId;

			Console.WriteLine("Enter the department");
			addObj.Department = Console.ReadLine();

			staffObj.AddStaffDetails( addObj );
		}

		public static void UpdateAdminDetails( Admin updateObj )
		{
			string checkInput;
			Console.WriteLine("Enter the new staff name");
			checkInput = Console.ReadLine();
			if (!string.IsNullOrWhiteSpace(checkInput))
			{
				updateObj.Name = checkInput;
			}

			Console.WriteLine("Enter the new staff phone number");
			checkInput = Console.ReadLine();
			if (!string.IsNullOrWhiteSpace(checkInput))
			{
				updateObj.Phone = checkInput;
			}

			Console.WriteLine("Enter the new department");
			checkInput = Console.ReadLine();
			if (!string.IsNullOrWhiteSpace(checkInput))
			{
				updateObj.Department = checkInput;
			}
		}

		public static void ViewAdminDetails(Admin viewObj)
		{
			Utils.DisplayCommonDetails(viewObj);
			Console.WriteLine("Department: {0}\n", viewObj.Department);
		}
	}
}
