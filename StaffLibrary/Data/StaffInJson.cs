using System.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;
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
			staffList.Add(addObj);

			SerializeToJson();

			return true;
		}

		public bool UpdateStaffDetails(Staff updateObj)
		{
			SerializeToJson();

			return true;
		}

		public Staff GetStaffById(int staffId, Type staffType)
		{
			Staff findObj = staffList.Find(searchObj => searchObj.Id == staffId && searchObj.GetType() == staffType);

			return findObj;
		}

		public List<Staff> GetAllStaff(Type staffType)
		{
			List<Staff> typeList = staffList.FindAll(searchObj => searchObj.GetType() == staffType);
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
	}
}
