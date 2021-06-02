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
		public bool AddStaffDetails(Staff addObj)
		{
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();
					
				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = connection;

				if(CheckIfUnique(addObj))
				{
					SetAddCmd(cmd, addObj);
					cmd.ExecuteNonQuery();
					return true;
				}
				else
				{
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
				if(CheckIfUnique(updateObj))
				{
					SetUpdateCmd(cmd, updateObj);
					cmd.ExecuteNonQuery();
					return true;
				}
				else
				{
					return false;
				}				
			}
		}

		public Staff GetStaffById(int staffId, StaffType type)
		{
			Staff findObj = null;
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = connection;
				cmd.Parameters.AddWithValue("@id", staffId);
				cmd.Parameters.AddWithValue("@staffType", (int)type);
				cmd.CommandText = "Proc_GetById";

				SqlDataReader reader = cmd.ExecuteReader();

				if (reader.Read())
				{
					findObj = CreateObject(type, reader);
				}
				else
				{
					findObj = null;
				}

				reader.Close();
			}

			return findObj;
		}

		public List<Staff> GetAllStaff(StaffType type)
		{
			List<Staff> staffList = new List<Staff>();

			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();

				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = connection;
				cmd.Parameters.AddWithValue("@staffType", (int)type);
				cmd.CommandText = "Proc_GetByType";

				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					Staff listObj = CreateObject(type, reader);

					staffList.Add(listObj);
				}
				reader.Close();
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
			}
			return true;
		}

		public void SetAddCmd(SqlCommand cmd, Staff addObj)
		{
			DataTable staffTable = new DataTable();
			staffTable.Columns.Add("name");
			staffTable.Columns.Add("phone");
			staffTable.Columns.Add("type");
	
			if (addObj.Type == StaffType.Teacher)
			{
				staffTable.Columns.Add("subject");
				staffTable.Rows.Add(addObj.Name, addObj.Phone, ((Teacher)addObj).Subject, (int)addObj.Type);
				cmd.Parameters.Add("@teacherDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_TeacherStaffAdd";
			}
			else if (addObj.Type == StaffType.Admin)
			{
				staffTable.Columns.Add("department");
				staffTable.Rows.Add(addObj.Name, addObj.Phone, ((Admin)addObj).Department, (int)addObj.Type);
				cmd.Parameters.Add("@adminDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_AdminStaffAdd";
			}
			else if (addObj.Type == StaffType.Support)
			{
				staffTable.Columns.Add("age");
				staffTable.Rows.Add(addObj.Name, addObj.Phone, ((Support)addObj).Age, (int)addObj.Type);
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
			staffTable.Columns.Add("type");

			if (updateObj.Type ==  StaffType.Teacher)
			{
				staffTable.Columns.Add("subject");
				staffTable.Rows.Add(updateObj.Name, updateObj.Phone, ((Teacher)updateObj).Subject, (int)updateObj.Type);
				cmd.Parameters.Add("@teacherDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_TeacherStaffUpdate";
			}
			else if (updateObj.Type == StaffType.Admin)
			{
				staffTable.Columns.Add("department");
				staffTable.Rows.Add(updateObj.Name, updateObj.Phone, ((Admin)updateObj).Department, (int)updateObj.Type);
				cmd.Parameters.Add("@adminDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_AdminStaffUpdate";
			}
			else if (updateObj.Type == StaffType.Support)
			{
				staffTable.Columns.Add("age");
				staffTable.Rows.Add(updateObj.Name, updateObj.Phone, ((Support)updateObj).Age, (int)updateObj.Type);
				cmd.Parameters.Add("@supportDetails", System.Data.SqlDbType.Structured).Value = staffTable;
				cmd.CommandText = "Proc_SupportStaffUpdate";
			}

		}

		public Staff CreateObject(StaffType type, SqlDataReader reader)
		{
			Staff staff = null;

			if (type == StaffType.Teacher)
			{
				staff = new Teacher();
				((Teacher)staff).Subject = (string)reader["subject"];
			}
			else if (type == StaffType.Admin)
			{
				staff = new Admin();
				((Admin)staff).Department = (string)reader["department"];
			}
			else if (type == StaffType.Support)
			{
				staff = new Support();
				((Support)staff).Age = (int)reader["age"];
			}

			staff.Id = (int)reader["staff_id"];
			staff.Name = (string)reader["name"];
			staff.Phone = (string)reader["phone"];

			return staff;
		}

		public bool CheckIfUnique(Staff obj)
		{
			bool isUnique;
			string connString = ConfigurationManager.AppSettings["ConnString"];
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();
				
				SqlCommand cmd = new SqlCommand();
				cmd.Parameters.AddWithValue("@id", obj.Id);
				cmd.Parameters.AddWithValue("@phone", obj.Phone);

				SqlParameter outParameter = new SqlParameter
				{
					ParameterName = "@isUnique",
					SqlDbType = SqlDbType.Bit,
					Direction = ParameterDirection.Output
				};
				cmd.Parameters.Add(outParameter);
				cmd.Connection = connection;
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "Proc_CheckUniquePhone";
		
				cmd.ExecuteNonQuery();
				isUnique = (bool)outParameter.Value;
			}
			return isUnique;
		}

	}
}
