using System.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;
using StaffLibrary.Models;
using StaffLibrary.Models.Base;


namespace StaffLibrary.Data
{
	public class StaffInJson : IStaff
	{
		public static List<Staff> staffList;
		public static string fileName;

		static StaffInJson()
		{
			fileName = ConfigurationManager.AppSettings["JsonFileName"];

			if (!File.Exists(fileName))
			{
				staffList = new List<Staff>();
			}
			else
			{
				JsonSerializerSettings settings = new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.All,
					Formatting = Formatting.Indented
				};
				StreamReader reader = File.OpenText(fileName);
				JsonSerializer readSerializer = JsonSerializer.Create(settings);
				staffList = (List<Staff>)readSerializer.Deserialize(reader, typeof(List<Staff>));
				reader.Close();
			}
		}

		public bool AddStaffDetails(Staff addObj)
		{
			if(CheckIfUnique(addObj, "Add"))
			{
				staffList.Add(addObj);
				SerializeToJson();

				return true;
			}
			else
			{
				return false;
			}			
		}

		public bool UpdateStaffDetails(Staff updateObj)
		{
			SerializeToJson();

			return true;
		}

		public Staff GetStaffById(int staffId, StaffType type)
		{
			Staff findObj = staffList.Find(searchObj => searchObj.Id == staffId && searchObj.Type == type);

			return findObj;
		}

		public List<Staff> GetAllStaff(StaffType type)
		{
			List<Staff> typeList = staffList.FindAll(searchObj => searchObj.Type == type);
			return typeList;
		}

		public bool DeleteStaffDetails(Staff deleteObj)
		{
			staffList.Remove(deleteObj);

			SerializeToJson();

			return true;
		}

		public int GetMaxId()
		{
			int maxId;

			if (staffList.Count == 0)
			{
				maxId = 0;
			}
			else
			{
				int maxIndex = staffList.Count - 1;
				maxId = staffList[maxIndex].Id;
			}
			return maxId;
		}

		public void SerializeToJson()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All,
				Formatting = Formatting.Indented
			};

			StreamWriter writer = File.CreateText(fileName);
			JsonSerializer writeSerializer = JsonSerializer.Create(settings);
			writeSerializer.Serialize(writer, staffList);
			writer.Close();
		}

		public bool CheckIfUnique(Staff obj, string operationType)
		{
			bool isUnique;
			Predicate<Staff> searchPredicate = null;

			if (operationType == "Add")
			{
				searchPredicate = searchObj => searchObj.Phone == obj.Phone;
			}
			else if (operationType == "Update")
			{
				searchPredicate = searchObj => (searchObj.Phone == obj.Phone && searchObj.Id != obj.Id);
			}

			if (staffList.Exists(searchPredicate))
			{
				isUnique = false;
			}
			else
			{
				isUnique = true;
			}

			return isUnique;
		}
	}
}
