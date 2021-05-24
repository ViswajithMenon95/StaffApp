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

		public void AddStaffDetails(Staff addObj)
		{
			staffList.Add(addObj);

			SerializeToJson();
		}

		public void UpdateStaffDetails(Staff updateObj)
		{
			SerializeToJson();
		}

		public Staff GetStaffById(int staffId, Type staffType)
		{
			Staff findObj = staffList.Find(searchObj => searchObj.Id == staffId);

			return findObj;
		}

		public List<Staff> GetAllStaff(Type staffType)
		{
			return staffList;
		}

		public void DeleteStaffDetails(Staff deleteObj)
		{
			staffList.Remove(deleteObj);

			SerializeToJson();
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
