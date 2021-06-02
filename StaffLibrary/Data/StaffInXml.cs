using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using StaffLibrary.Models;
using StaffLibrary.Models.Base;

namespace StaffLibrary.Data
{
	public class StaffInXml : IStaff
	{
		public static List<Staff> staffList;
		public static string fileName;

		static StaffInXml()
		{
			fileName = ConfigurationManager.AppSettings["XmlFileName"];

			if (!File.Exists(fileName))
			{
				staffList = new List<Staff>();
			}
			else
			{
				XmlReader reader = XmlReader.Create(fileName);
				XmlSerializer readSerializer = new XmlSerializer(typeof(List<Staff>));
				staffList = (List<Staff>)readSerializer.Deserialize(reader);
				reader.Close();
			}
		}

		public bool AddStaffDetails(Staff addObj)
		{
			if(CheckIfUnique(addObj, "Add"))
			{
				staffList.Add(addObj);
				SerializeToXml();

				return true;
			}
			else
			{
				return false;
			}				
		}

		public bool UpdateStaffDetails(Staff updateObj)
		{
			SerializeToXml();

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

			SerializeToXml();

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

		public void SerializeToXml()
		{
			XmlSerializer writeSerializer = new XmlSerializer(typeof(List<Staff>));
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			XmlWriter writer = XmlWriter.Create(fileName, settings);
			writeSerializer.Serialize(writer, staffList);
			writer.Close();
		}

		public bool CheckIfUnique(Staff obj, string operationType)
		{
			bool isUnique;
			Predicate<Staff> searchPredicate = null;

			if(operationType == "Add")
			{
				searchPredicate = searchObj => searchObj.Phone == obj.Phone;
			}
			else if(operationType == "Update")
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
