using System;
using System.Collections.Generic;
using StaffLibrary.Models.Base;
using StaffLibrary.Models;

namespace StaffLibrary.Data
{
	public interface IStaff
	{
		bool AddStaffDetails(Staff addObj);
		bool UpdateStaffDetails(Staff updateObj);
		Staff GetStaffById(int findId, StaffType type);
		List<Staff> GetAllStaff(StaffType type);
		bool DeleteStaffDetails(Staff deleteObj);
	}
}
