using System;
using System.Collections.Generic;
using StaffLibrary.Data;
using StaffLibrary.Models.Base;
using StaffLibrary.Models;

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
			else if (staffObj is StaffInDB)
				maxId = 0;


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

		public static void UpdateDetails(StaffType type, IStaff staffObj)
		{
			Staff updateObj = null;
			int findId;

			Type staffType = null;

			if (type == StaffType.Teacher)
			{
				staffType = typeof(Teacher);
			}
			else if (type == StaffType.Admin)
			{
				staffType = typeof(Admin);
			}
			else if (type == StaffType.Support)
			{
				staffType = typeof(Support);
			}

			Console.WriteLine("Enter the Id of the staff member");
			if (int.TryParse(Console.ReadLine(), out findId))
			{
				updateObj = staffObj.GetStaffById(findId, staffType);
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

				staffObj.UpdateStaffDetails(updateObj);
			}
			else
			{
				Console.WriteLine("Not found");
			}

		}

		public static void ViewDetails(StaffType type, int viewChoice, IStaff staffObj)
		{
			Type staffType = null;

			if (type == StaffType.Teacher)
			{
				staffType = typeof(Teacher);
			}
			else if (type == StaffType.Admin)
			{
				staffType = typeof(Admin);
			}
			else if (type == StaffType.Support)
			{
				staffType = typeof(Support);
			}

			if (viewChoice == 1)
			{
				int findId;
				Staff viewObj = null;

				Console.WriteLine("Enter the Id of the staff member");
				if (int.TryParse(Console.ReadLine(), out findId))
				{
					viewObj = staffObj.GetStaffById(findId, staffType);
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
				
				List<Staff> staffList = staffObj.GetAllStaff(staffType);
				
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

		public static void DeleteDetails(StaffType type, IStaff staffObj)
		{
			Staff deleteObj = null;
			int findId;

			Type staffType = null;

			if (type == StaffType.Teacher)
			{
				staffType = typeof(Teacher);
			}
			else if (type == StaffType.Admin)
			{
				staffType = typeof(Admin);
			}
			else if (type == StaffType.Support)
			{
				staffType = typeof(Support);
			}

			Console.WriteLine("Enter the Id of the staff member");
			if (int.TryParse(Console.ReadLine(), out findId))
			{
				deleteObj = staffObj.GetStaffById(findId, staffType);
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
