using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using StaffLibrary.Models.Base;
using StaffLibrary.Models;

namespace StaffLibrary.Data
{
	public class StaffInDB: IStaff
	{

		public void AddStaffDetails(Staff addObj)
		{
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();
				string primaryQuery = "Insert into [dbo].[Staff] values ('" + addObj.Name + "', '" + addObj.Phone + "')";
				SqlCommand cmd = new SqlCommand(primaryQuery, connection);
				string secondaryQuery = FormInsertQuery(addObj);
				cmd.ExecuteNonQuery();
				cmd.CommandText = secondaryQuery;
				cmd.ExecuteNonQuery();
			}
		}

		public void UpdateStaffDetails(Staff updateObj)
		{
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();
				string primaryQuery = "Update [dbo].[Staff] set Name = '" + updateObj.Name + "', Phone = '" + updateObj.Phone + "' where staff_id = " + updateObj.Id;
				SqlCommand cmd = new SqlCommand(primaryQuery, connection);
				string secondaryQuery = FormUpdateQuery(updateObj);
				cmd.ExecuteNonQuery();
				cmd.CommandText = secondaryQuery;
				cmd.ExecuteNonQuery();
			}
		}

		public Staff GetStaffById(int staffId)
		{
			Staff findObj = null;

			return findObj;
		}

		public List<Staff> GetAllStaff()
		{
			List<Staff> staffList = new List<Staff>();

			return staffList;
		}

		public void DeleteStaffDetails(Staff deleteObj)
		{
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();
				string query = "Delete from [dbo].[Staff] where staff_id = " + deleteObj.Id;
				SqlCommand cmd = new SqlCommand(query, connection);
				cmd.ExecuteNonQuery();
			}
		}

		public string FormInsertQuery(Staff addObj)
		{
			string query = null;
			if (addObj is Teacher)
			{
				query = "Insert into [dbo].[TeacherStaff] values (SCOPE_IDENTITY(), '" + ((Teacher)addObj).Subject + "')";
			}
			else if (addObj is Admin)
			{
				query = "Insert into [dbo].[AdminStaff] values (SCOPE_IDENTITY(), '" + ((Admin)addObj).Department + "')";
			}
			else if (addObj is Support)
			{
				query = "Insert into [dbo].[SupportStaff] values (SCOPE_IDENTITY()," + ((Support)addObj).Age + ")";
			}

			return query;
		}

		public string FormUpdateQuery(Staff updateObj)
		{
			string query = null;
			if (updateObj is Teacher)
			{
				query = "Update [dbo].[TeacherStaff] set Subject = '" + ((Teacher)updateObj).Subject + "' where staff_id = " + updateObj.Id;
			}
			else if (updateObj is Admin)
			{
				query = "Update [dbo].[AdminStaff] set Department = '" + ((Admin)updateObj).Department + "' where staff_id = " + updateObj.Id;
			}
			else if (updateObj is Support)
			{
				query = "Update [dbo].[SupportStaff] set Age = " + ((Support)updateObj).Age + " where staff_id = " + updateObj.Id;
			}

			return query;
		}
	}
}
