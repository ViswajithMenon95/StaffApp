using System.Collections.Generic;
using StaffLibrary.Models.Base;

namespace StaffLibrary.Data
{
	public class StaffInMemory : IStaff
	{
		private static List<Staff> staffList = new List<Staff>();

		public void AddStaffDetails(Staff addObj)
		{
			staffList.Add(addObj);
		}

		public void UpdateStaffDetails(Staff updateObj)
		{

		}

		public Staff GetStaffById(int staffId)
		{
			Staff findObj = staffList.Find(searchObj => searchObj.Id == staffId);

			return findObj;
		}

		public List<Staff> GetAllStaff()
		{
			return staffList;
		}

		public void DeleteStaffDetails(Staff deleteObj)
		{
			staffList.Remove(deleteObj);
		}

		public int GetMaxId()
		{
			int maxId;

			if (staffList.Count == 0)
			{
				maxId = 0;
			}
			else
			{
				int maxIndex = staffList.Count - 1;
				maxId = staffList[maxIndex].Id;
			}
			return maxId;
		}

	}
}
