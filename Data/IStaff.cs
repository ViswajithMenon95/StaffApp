using System;
using StaffApp.Models.Base;
using StaffApp.Models;

namespace StaffApp.Data
{
    public interface IStaff
    {
        void AddStaffDetails(int staffChoice);
        void UpdateStaffDetails();
        void ViewStaffDetails(int viewChoice);
        void DeleteStaffDetails();
    }
}
