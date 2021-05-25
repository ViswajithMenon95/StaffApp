using System;
using System.Configuration;
using System.Data;
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
				SetGetByIdCmd(cmd, staffId, staffType);

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
				SetGetAllCmd(cmd, staffType);

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
			DataTable staffTable = new DataTable();
			staffTable.Columns.Add("name");
			staffTable.Columns.Add("phone");

			if (addObj is Teacher)
			{
				staffTable.Columns.Add("subject");
				staffTable.Rows.Add(addObj.Name, addObj.Phone, ((Teacher)addObj).Subject);
				cmd.Parameters.Add("@teacherDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_TeacherStaffAdd";
			}
			else if (addObj is Admin)
			{
				staffTable.Columns.Add("department");
				staffTable.Rows.Add(addObj.Name, addObj.Phone, ((Admin)addObj).Department);
				cmd.Parameters.Add("@adminDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_AdminStaffAdd";
			}
			else if (addObj is Support)
			{
				staffTable.Columns.Add("age");
				staffTable.Rows.Add(addObj.Name, addObj.Phone, ((Support)addObj).Age);
				cmd.Parameters.Add("@supportDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_SupportStaffAdd";
			}
		}

		public void SetUpdateCmd(SqlCommand cmd, Staff updateObj)
		{
			cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = updateObj.Id;
			DataTable staffTable = new DataTable();
			staffTable.Columns.Add("name");
			staffTable.Columns.Add("phone");

			if (updateObj is Teacher)
			{
				staffTable.Columns.Add("subject");
				staffTable.Rows.Add(updateObj.Name, updateObj.Phone, ((Teacher)updateObj).Subject);
				cmd.Parameters.Add("@teacherDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_TeacherStaffUpdate";
			}
			else if (updateObj is Admin)
			{
				staffTable.Columns.Add("department");
				staffTable.Rows.Add(updateObj.Name, updateObj.Phone, ((Admin)updateObj).Department);
				cmd.Parameters.Add("@adminDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_AdminStaffUpdate";
			}
			else if (updateObj is Support)
			{
				staffTable.Columns.Add("age");
				staffTable.Rows.Add(updateObj.Name, updateObj.Phone, ((Support)updateObj).Age);
				cmd.Parameters.Add("@supportDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_SupportStaffUpdate";
			}

		}

		public void SetGetByIdCmd(SqlCommand cmd, int staffId, Type staffType)
		{
			cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = staffId;

			if (staffType == typeof(Teacher))
			{
				cmd.CommandText = "Proc_TeacherStaffGetById";
			}
			else if(staffType == typeof(Admin))
            {
				cmd.CommandText = "Proc_AdminStaffGetById";
			}
			else if(staffType == typeof(Support))
			{
				cmd.CommandText = "Proc_SupportStaffGetById";
			}
		}

		public void SetGetAllCmd(SqlCommand cmd, Type staffType)
		{
			if (staffType == typeof(Teacher))
			{
				cmd.CommandText = "Proc_TeacherStaffGetAll";
			}
			else if (staffType == typeof(Admin))
			{
				cmd.CommandText = "Proc_AdminStaffGetAll";
			}
			else if (staffType == typeof(Support))
			{
				cmd.CommandText = "Proc_SupportStaffGetAll";
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
