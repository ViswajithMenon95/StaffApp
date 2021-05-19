﻿using System.Collections.Generic;
using StaffApp.Models.Base;

namespace StaffApp.Data
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
