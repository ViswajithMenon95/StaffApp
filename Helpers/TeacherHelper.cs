using System;
using StaffApp.Models;
using StaffApp.Utilities;
using StaffApp.Data;

namespace StaffApp.Helpers
{
	class TeacherHelper
	{
		public static void AddTeacherDetails(IStaff staffObj)
		{
			Teacher addObj = new Teacher();

			Utils.AddCommonDetails(addObj);

			int newId = staffObj.GetMaxId() + 1;
			addObj.Id = newId;

			Console.WriteLine("Enter the subject");
			addObj.Subject = Console.ReadLine();

			staffObj.AddStaffDetails(addObj);
		} 

		public static void UpdateTeacherDetails( Teacher updateObj )
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

			Console.WriteLine("Enter the new subject");
			checkInput = Console.ReadLine();
			if (!string.IsNullOrWhiteSpace(checkInput))
			{
				updateObj.Subject = checkInput;
			}
		}

		public static void ViewTeacherDetails( Teacher viewObj )
		{
			Utils.DisplayCommonDetails(viewObj);
			Console.WriteLine("Subject: {0}\n", viewObj.Subject);
		}
	}
}
