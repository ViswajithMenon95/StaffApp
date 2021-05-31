using System;
using System.Collections.Generic;
using StaffLibrary.Models.Base;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffLibrary.Data
{
	public interface IStaff
	{
		bool AddStaffDetails(Staff addObj);
		bool UpdateStaffDetails(Staff updateObj);
		Staff GetStaffById(int findId, Type staffType);
		List<Staff> GetAllStaff(Type staffType);
		bool DeleteStaffDetails(Staff deleteObj);
	}
}
