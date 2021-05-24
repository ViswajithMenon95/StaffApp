using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
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

		public void AddStaffDetails(Staff addObj)
		{
			staffList.Add(addObj);

			SerializeToXml();
		}

		public void UpdateStaffDetails(Staff updateObj)
		{
			SerializeToXml();
		}

		public Staff GetStaffById(int staffId)
		{
			Staff findObj = staffList.Find(searchObj => searchObj.Id == staffId);

			return findObj;
		}

		public List<Staff> GetAllStaff()
		{
			return staffList;
		}

		public void DeleteStaffDetails(Staff deleteObj)
		{
			staffList.Remove(deleteObj);

			SerializeToXml();
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

	}
}
