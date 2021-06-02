using System;
using System.Collections.Generic;
using StaffLibrary.Models;
using StaffLibrary.Models.Base;

namespace StaffLibrary.Data
{
	public class StaffInMemory : IStaff
	{
		private static List<Staff> staffList = new List<Staff>();

		public bool AddStaffDetails(Staff addObj)
		{
			if(CheckIfUnique(addObj, "Add"))
			{
				staffList.Add(addObj);
				return true;
			}
			else
			{
				return false;
			}		
		}

		public bool UpdateStaffDetails(Staff updateObj)
		{
			return true;
		}

		public Staff GetStaffById(int staffId, StaffType type)
		{
			Staff findObj = staffList.Find(searchObj => searchObj.Id == staffId && searchObj.Type == type);

			return findObj;
		}

		public List<Staff> GetAllStaff(StaffType type)
		{
			List<Staff> typeList = staffList.FindAll(searchObj => searchObj.Type == type);
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

		public bool CheckIfUnique(Staff obj, string operationType)
		{
			bool isUnique;
			Predicate<Staff> searchPredicate = null;

			if (operationType == "Add")
			{
				searchPredicate = searchObj => searchObj.Phone == obj.Phone;
			}
			else if (operationType == "Update")
			{
				searchPredicate = searchObj => (searchObj.Phone == obj.Phone && searchObj.Id != obj.Id);
			}

			if (staffList.Exists(searchPredicate))
			{
				isUnique = false;
			}
			else
			{
				isUnique = true;
			}

			return isUnique;
		}

	}
}
