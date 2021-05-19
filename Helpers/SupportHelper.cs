﻿using System;
using StaffApp.Models;
using StaffApp.Utilities;
using StaffApp.Data;

namespace StaffApp.Helpers
{
	class SupportHelper
	{
		public static void AddSupportDetails( IStaff staffObj )
		{
			Support addObj = new Support();;
			int parseInput;

			Utils.AddCommonDetails(addObj);

			int newId = staffObj.GetMaxId() + 1;
			addObj.Id = newId;

			Console.WriteLine("Enter the age");

			if (int.TryParse(Console.ReadLine(), out parseInput))
			{
				addObj.Age = parseInput;
			}
			else
			{
				Console.WriteLine("Invalid age");
			}

			staffObj.AddStaffDetails(addObj);
		}

		public static void UpdateSupportDetails( Support updateObj )
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

			Console.WriteLine("Enter the new age");
			checkInput = Console.ReadLine();
			if (!string.IsNullOrWhiteSpace(checkInput))
			{
				int parseInput;
				if (int.TryParse(checkInput, out parseInput))
				{
					updateObj.Age = parseInput;
				}
				else
				{
					Console.WriteLine("Invalid age");
				}
			}
		}

		public static void ViewSupportDetails(Support viewObj)
		{
			Utils.DisplayCommonDetails(viewObj);
			Console.WriteLine("Age: {0}\n", viewObj.Age);
		}
	}
}
