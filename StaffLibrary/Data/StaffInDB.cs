﻿using System;
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
		public bool AddStaffDetails(Staff addObj)
		{
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();
					
				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = connection;

				if(CheckIfUnique(addObj, "Add"))
				{
					SetAddCmd(cmd, addObj);
					cmd.ExecuteNonQuery();
					connection.Close();
					return true;
				}
				else
				{
					connection.Close();
					return false;
				}					
			}
		}

		public bool UpdateStaffDetails(Staff updateObj)
		{
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();
				
				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = connection;
				if(CheckIfUnique(updateObj, "Update"))
				{
					SetUpdateCmd(cmd, updateObj);
					cmd.ExecuteNonQuery();
					connection.Close();
					return true;
				}
				else
				{
					connection.Close();
					return false;
				}				
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
				cmd.Parameters.AddWithValue("@staffType", staffType.Name);
				cmd.CommandText = "Proc_GetAllByType";

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

		public bool DeleteStaffDetails(Staff deleteObj)
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
			return true;
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

		public bool CheckIfUnique(Staff obj, string operationType)
		{
			bool isUnique;
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();
				string query = null;
				if (operationType == "Add")
				{
					query = "Select * from [dbo].[Staff] where Staff.phone = '" + obj.Phone + "'";
				}
				else if(operationType == "Update")
				{
					query = "Select * from [dbo].[Staff] where Staff.phone = '" + obj.Phone + "' and Staff.staff_id != " + obj.Id;
				}
				SqlCommand cmd = new SqlCommand(query, connection);
				SqlDataReader reader = cmd.ExecuteReader();

				if(reader.Read())
				{
					isUnique = false;
				}
				else
				{
					isUnique = true;
				}

				connection.Close();
			}
			return isUnique;
		}

	}
}
