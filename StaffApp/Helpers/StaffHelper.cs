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

			if (type == StaffType.Teacher)
			{
				addObj = TeacherHelper.AddTeacherDetails(maxId);
			}

			else if (type == StaffType.Admin)
			{
				addObj = AdminHelper.AddAdminDetails(maxId);
			}

			else if (type == StaffType.Support)
			{
				addObj = SupportHelper.AddSupportDetails(maxId);
			}

			bool operationResult = staffObj.AddStaffDetails(addObj);

			if(operationResult)
			{
				Console.WriteLine("Added");
			}
			else
			{
				Console.WriteLine("Not added");
			}
		}

		public static void UpdateDetails(StaffType type, IStaff staffObj)
		{
			Staff updateObj = null;
			int findId;

			Console.WriteLine("Enter the Id of the staff member");
			if (int.TryParse(Console.ReadLine(), out findId))
			{
				updateObj = staffObj.GetStaffById(findId, type);
			}

			if(updateObj!=null)
			{
				if (updateObj.Type == StaffType.Teacher)
				{
					TeacherHelper.UpdateTeacherDetails((Teacher)updateObj);
				}
				else if (updateObj.Type == StaffType.Admin)
				{
					AdminHelper.UpdateAdminDetails((Admin)updateObj);
				}
				else if (updateObj.Type == StaffType.Support)
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
			if (viewChoice == 1)
			{
				int findId;
				Staff viewObj = null;

				Console.WriteLine("Enter the Id of the staff member");
				if (int.TryParse(Console.ReadLine(), out findId))
				{
					viewObj = staffObj.GetStaffById(findId, type);
				}
				if (viewObj != null)
				{
					if (viewObj.Type == StaffType.Teacher)
					{
						TeacherHelper.ViewTeacherDetails((Teacher)viewObj);
					}
					else if (viewObj.Type == StaffType.Admin)
					{
						AdminHelper.ViewAdminDetails((Admin)viewObj);
					}
					else if (viewObj.Type == StaffType.Support)
					{
						SupportHelper.ViewSupportDetails((Support)viewObj);
					}
				}
				else
				{
					Console.WriteLine("Not found");
				}
			}
			else if (viewChoice == 2)
			{
				List<Staff> staffList = staffObj.GetAllStaff(type);

				if (staffList.Count != 0)
				{
					if (type == StaffType.Teacher)
					{
						foreach (Staff viewObj in staffList)
						{
							TeacherHelper.ViewTeacherDetails((Teacher)viewObj);
						}
					}
					else if (type == StaffType.Admin)
					{
						foreach (Staff viewObj in staffList)
						{
							AdminHelper.ViewAdminDetails((Admin)viewObj);
						}
					}
					else if (type == StaffType.Support)
					{
						foreach (Staff viewObj in staffList)
						{
							SupportHelper.ViewSupportDetails((Support)viewObj);
						}
					}
				}	
				else
				{
					Console.WriteLine("List is empty");
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

			Console.WriteLine("Enter the Id of the staff member");
			if (int.TryParse(Console.ReadLine(), out findId))
			{
				deleteObj = staffObj.GetStaffById(findId, type);
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
