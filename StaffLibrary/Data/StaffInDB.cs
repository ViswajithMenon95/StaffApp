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
					
				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = connection;
				SetAddCmd(cmd, addObj);

				cmd.ExecuteNonQuery();
				connection.Close();
			}
		}

		public void UpdateStaffDetails(Staff updateObj)
		{
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();
				
				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = connection;
				SetUpdateCmd(cmd, updateObj);

				cmd.ExecuteNonQuery();
				connection.Close();
			}
		}

		public Staff GetStaffById(int staffId, Type staffType)
		{
			Staff findObj = null;
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = connection;
				SetGetCmd(cmd, staffId, staffType, false);

				SqlDataReader reader = cmd.ExecuteReader();

				if (reader.Read())
				{
					findObj = CreateObject(staffType, reader);
				}
				else
				{
					findObj = null;
				}

				reader.Close();
				connection.Close();
			}

			return findObj;
		}

		public List<Staff> GetAllStaff(Type staffType)
		{
			List<Staff> staffList = new List<Staff>();

			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = connection;
				SetGetCmd(cmd, 0, staffType, true);

				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					Staff listObj = CreateObject(staffType, reader);

					staffList.Add(listObj);
				}
				reader.Close();
				connection.Close();
			}

			return staffList;
		}

		public void DeleteStaffDetails(Staff deleteObj)
		{
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();
				
				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = connection;
				cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = deleteObj.Id;
				cmd.CommandText = "Proc_StaffDelete";

				cmd.ExecuteNonQuery();
				connection.Close();
			}
		}

		public void SetAddCmd(SqlCommand cmd, Staff addObj)
		{
			cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 50).Value = addObj.Name;
			cmd.Parameters.Add("@phone", System.Data.SqlDbType.VarChar, 50).Value = addObj.Phone;

            if (addObj is Teacher)
			{
				cmd.Parameters.Add("@subject", System.Data.SqlDbType.VarChar, 50).Value = ((Teacher)addObj).Subject;
				cmd.CommandText = "Proc_TeacherStaffAdd";
			}
			else if (addObj is Admin)
			{
				cmd.Parameters.Add("@department", System.Data.SqlDbType.VarChar, 50).Value = ((Admin)addObj).Department;
				cmd.CommandText = "Proc_AdminStaffAdd";
			}
			else if (addObj is Support)
			{
				cmd.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = ((Support)addObj).Age;
				cmd.CommandText = "Proc_SupportStaffAdd";
			}

		}

		public void SetUpdateCmd(SqlCommand cmd, Staff updateObj)
		{
			cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = updateObj.Id;
			cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 50).Value = updateObj.Name;
			cmd.Parameters.Add("@phone", System.Data.SqlDbType.VarChar, 50).Value = updateObj.Phone;

			if (updateObj is Teacher)
			{
				cmd.Parameters.Add("@subject", System.Data.SqlDbType.VarChar, 50).Value = ((Teacher)updateObj).Subject;
				cmd.CommandText = "Proc_TeacherStaffUpdate";
			}
			else if (updateObj is Admin)
			{
				cmd.Parameters.Add("@department", System.Data.SqlDbType.VarChar, 50).Value = ((Admin)updateObj).Department;
				cmd.CommandText = "Proc_AdminStaffUpdate";
			}
			else if (updateObj is Support)
			{
				cmd.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = ((Support)updateObj).Age;
				cmd.CommandText = "Proc_SupportStaffUpdate";
			}

		}

		public void SetGetCmd(SqlCommand cmd, int staffId, Type staffType, bool displayAll)
		{
			cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = staffId;
			cmd.Parameters.Add("@displayAll", System.Data.SqlDbType.Bit).Value = displayAll;

			if (staffType == typeof(Teacher))
			{
				cmd.CommandText = "Proc_TeacherStaffGet";
			}
			else if(staffType == typeof(Admin))
            {
				cmd.CommandText = "Proc_AdminStaffGet";
			}
			else if(staffType == typeof(Support))
			{
				cmd.CommandText = "Proc_SupportStaffGet";
			}
		}

		public Staff CreateObject(Type staffType, SqlDataReader reader)
		{
			Staff findObj = null;

			if (staffType == typeof(Teacher))
			{
				findObj = new Teacher();
				((Teacher)findObj).Subject = (string)reader[3];
			}
			else if (staffType == typeof(Admin))
			{
				findObj = new Admin();
				((Admin)findObj).Department = (string)reader[3];
			}
			else if (staffType == typeof(Support))
			{
				findObj = new Support();
				((Support)findObj).Age = (int)reader[3];
			}

			findObj.Id = (int)reader[0];
			findObj.Name = (string)reader[1];
			findObj.Phone = (string)reader[2];

			return findObj;
		}

	}
}
