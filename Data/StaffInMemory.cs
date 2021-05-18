using System;
using System.Collections.Generic;
using StaffApp.Models;
using StaffApp.Models.Base;
using StaffApp.Utilities;
using StaffApp.Helpers;


namespace StaffApp.Data
{
    public class StaffInMemory : IStaff
    {
        private static List<Staff> staffList = new List<Staff>();
		public void AddStaffDetails(int choice)
		{
			Staff addObj = null;

			if (choice == 1)
			{
				addObj = TeacherHelper.AddTeacherDetails();
			}

			else if (choice == 2)
			{
				addObj = AdminHelper.AddAdminDetails();
			}

			else if (choice == 3)
			{
				addObj = SupportHelper.AddSupportDetails();
			}

			staffList.Add(addObj);
		}

		public void UpdateStaffDetails()
		{
			Staff updateObj = Utils.FindStaff(staffList);

			if (updateObj != null)
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
			}
			else
			{
				Console.WriteLine("Not found");
			}

		}

		public void ViewStaffDetails(int viewChoice)
		{
			if (viewChoice == 1)
			{
				Staff viewObj = Utils.FindStaff(staffList);

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

		public void DeleteStaffDetails()
        {
            Staff deleteObj = Utils.FindStaff(staffList);

            if (deleteObj != null)
            {
                staffList.Remove(deleteObj);
            }
            else
            {
                Console.WriteLine("Not found");
            }

        }
    }

}