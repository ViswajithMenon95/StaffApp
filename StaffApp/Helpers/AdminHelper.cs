using System;
using StaffLibrary.Models;
using StaffApp.Utilities;

namespace StaffApp.Helpers
{
	class AdminHelper
	{
		public static Admin AddAdminDetails(int maxId)
		{
			Admin addObj = new Admin();

			Utils.AddCommonDetails(addObj);

			int newId = maxId + 1;
			addObj.Id = newId;

			Console.WriteLine("Enter the department");
			addObj.Department = Console.ReadLine();

			return addObj;
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
