using System;
using System.Collections.Generic;
using StaffLibrary.Models.Base;

namespace StaffLibrary.Data
{
	public class StaffInMemory : IStaff
	{
		private static List<Staff> staffList = new List<Staff>();

		public bool AddStaffDetails(Staff addObj)
		{
			staffList.Add(addObj);
			return true;
		}

		public bool UpdateStaffDetails(Staff updateObj)
		{
			return true;
		}

		public Staff GetStaffById(int staffId, Type staffType)
		{
			Staff findObj = staffList.Find(searchObj => searchObj.Id == staffId && searchObj.GetType() == staffType);

			return findObj;
		}

		public List<Staff> GetAllStaff(Type staffType)
		{
			List<Staff> typeList = staffList.FindAll(searchObj => searchObj.GetType() == staffType);
			return typeList;
		}

		public bool DeleteStaffDetails(Staff deleteObj)
		{
			staffList.Remove(deleteObj);

			return true;
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
