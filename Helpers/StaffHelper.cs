using System;
using System.Collections.Generic;
using StaffApp.Data;
using StaffApp.Models.Base;
using StaffApp.Models;

namespace StaffApp.Helpers
{
	class StaffHelper
	{
		public static void AddDetails( StaffType type, IStaff staffObj)
		{
			Staff addObj = null;
			int maxId = 0;

			if (staffObj is StaffInMemory)
				maxId = ((StaffInMemory)staffObj).GetMaxId();
			else if (staffObj is StaffInXml)
				maxId = ((StaffInXml)staffObj).GetMaxId();
			else if (staffObj is StaffInJson)
				maxId = ((StaffInJson)staffObj).GetMaxId();


			if ( type == StaffType.Teacher )
			{
				addObj = TeacherHelper.AddTeacherDetails(maxId);
			}

			else if ( type == StaffType.Admin )
			{
				addObj = AdminHelper.AddAdminDetails(maxId);
			}

			else if ( type == StaffType.Support )
			{
				addObj = SupportHelper.AddSupportDetails(maxId);
			}

			staffObj.AddStaffDetails(addObj);
		}

		public static void UpdateDetails(IStaff staffObj)
		{
			Staff updateObj = null;
			int findId;

			Console.WriteLine("Enter the Id of the staff member");
			if (int.TryParse(Console.ReadLine(), out findId))
			{
				updateObj = staffObj.GetStaffById(findId);
			}

			if(updateObj!=null)
			{
				if (updateObj is Teacher)
				{
					TeacherHelper.UpdateTeacherDetails((Teacher)updateObj);
				}
				else if (updateObj is Admin)
				{
					AdminHelper.UpdateAdminDetails((Admin)updateObj);
				}
				else if (updateObj is Support)
				{
					SupportHelper.UpdateSupportDetails((Support)updateObj);
				}

				staffObj.UpdateStaffDetails();
			}
			else
			{
				Console.WriteLine("Not found");
			}

		}

		public static void ViewDetails(int viewChoice, IStaff staffObj)
		{
			if (viewChoice == 1)
			{
				Staff viewObj = null;
				int findId;

				Console.WriteLine("Enter the Id of the staff member");
				if (int.TryParse(Console.ReadLine(), out findId))
				{
					viewObj = staffObj.GetStaffById(findId);
				}
				if (viewObj != null)
				{
					if (viewObj is Teacher)
					{
						TeacherHelper.ViewTeacherDetails((Teacher)viewObj);
					}
					else if (viewObj is Admin)
					{
						AdminHelper.ViewAdminDetails((Admin)viewObj);
					}
					else if (viewObj is Support)
					{
						SupportHelper.ViewSupportDetails((Support)viewObj);
					}
				}
				else
				{
					Console.WriteLine("Not found");
				}
			}
			else if (1 < viewChoice && viewChoice < 6)
			{
				List<Staff> staffList = staffObj.GetAllStaff();
				
				foreach (Staff viewObj in staffList)
				{
					if ((viewObj is Teacher) && (viewChoice == 2 || viewChoice == 5))
					{
						TeacherHelper.ViewTeacherDetails((Teacher)viewObj);
					}
					else if ((viewObj is Admin) && (viewChoice == 3 || viewChoice == 5))
					{
						AdminHelper.ViewAdminDetails((Admin)viewObj);
					}
					else if ((viewObj is Support) && (viewChoice == 4 || viewChoice == 5))
					{
						SupportHelper.ViewSupportDetails((Support)viewObj);
					}
				}
			}
			else
			{
				Console.WriteLine("Invalid choice");
			}
		}

		public static void DeleteDetails(IStaff staffObj)
		{
			Staff deleteObj = null;
			int findId;

			Console.WriteLine("Enter the Id of the staff member");
			if (int.TryParse(Console.ReadLine(), out findId))
			{
				deleteObj = staffObj.GetStaffById(findId);
			}

			if (deleteObj != null)
			{
				staffObj.DeleteStaffDetails(deleteObj);
			}
			else
			{
				Console.WriteLine("Not found");
			}
		}
	}
}
