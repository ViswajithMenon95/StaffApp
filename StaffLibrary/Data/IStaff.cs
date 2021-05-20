﻿using System;
using System.Collections.Generic;
using StaffLibrary.Models.Base;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffLibrary.Data
{
	public interface IStaff
	{
		void AddStaffDetails(Staff addObj);
		void UpdateStaffDetails();
		Staff GetStaffById(int findId);
		List<Staff> GetAllStaff();
		void DeleteStaffDetails(Staff deleteObj);
	}
}