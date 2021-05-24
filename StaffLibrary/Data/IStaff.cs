using System;
using System.Collections.Generic;
using StaffLibrary.Models.Base;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffLibrary.Data
{
	public enum StaffType
	{
		Teacher = 1,
		Admin,
		Support
	}
	public interface IStaff
	{
		void AddStaffDetails(Staff addObj);
		void UpdateStaffDetails(Staff updateObj);
		Staff GetStaffById(int findId, Type staffType);
		List<Staff> GetAllStaff(Type staffType);
		void DeleteStaffDetails(Staff deleteObj);
	}
}
