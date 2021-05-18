using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Configuration;
using StaffApp.Models;
using StaffApp.Models.Base;
using StaffApp.Utilities;
using StaffApp.Helpers;

namespace StaffApp.Data
{
    public class StaffInXml : IStaff
    {
		[XmlArray]
		[XmlArrayItem(ElementName = "StaffMember" ,Type = typeof(Teacher)), XmlArrayItem(ElementName = "StaffMember", Type = typeof(Admin)), XmlArrayItem(ElementName = "StaffMember", Type = typeof(Support))]
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
		public void AddStaffDetails(int choice)
        {
            Staff addObj = null;

            if (choice == 1)
            {
				addObj = TeacherHelper.AddTeacherDetails();
            }

            else if (choice == 2)
            {
				addObj = AdminHelper.AddAdminDetails();
            }

            else if (choice == 3)
            {
				addObj = SupportHelper.AddSupportDetails();
            }

			staffList.Add(addObj);

			SerializeToXml();
		}


		public void UpdateStaffDetails()
        {
			Staff updateObj = Utils.FindStaff(staffList);

			if (updateObj != null)
			{
				if (updateObj is Teacher)
				{
					TeacherHelper.UpdateTeacherDetails((Teacher)updateObj);
				}
				else if (updateObj is Admin)
				{
					AdminHelper.UpdateAdminDetails((Admin)updateObj);
				}
				else if (updateObj is Support)
				{
					SupportHelper.UpdateSupportDetails((Support)updateObj);
				}
			}
			else
			{
				Console.WriteLine("Not found");
			}

			SerializeToXml();
		}

		public void ViewStaffDetails(int viewChoice)
		{
			if (viewChoice == 1)
			{
				Staff viewObj = Utils.FindStaff(staffList);

				if (viewObj != null)
				{

					if (viewObj is Teacher)
					{
						TeacherHelper.ViewTeacherDetails((Teacher)viewObj);
					}
					else if (viewObj is Admin)
					{
						AdminHelper.ViewAdminDetails((Admin)viewObj);
					}
					else if (viewObj is Support)
					{
						SupportHelper.ViewSupportDetails((Support)viewObj);
					}
				}
				else
				{
					Console.WriteLine("Not found");
				}
			}
			else if (1 < viewChoice && viewChoice < 6)
			{
				foreach (Staff viewObj in staffList)
				{
					if ((viewObj is Teacher) && (viewChoice == 2 || viewChoice == 5))
					{
						TeacherHelper.ViewTeacherDetails((Teacher)viewObj);
					}
					else if ((viewObj is Admin) && (viewChoice == 3 || viewChoice == 5))
					{
						AdminHelper.ViewAdminDetails((Admin)viewObj);
					}
					else if ((viewObj is Support) && (viewChoice == 4 || viewChoice == 5))
					{
						SupportHelper.ViewSupportDetails((Support)viewObj);
					}
				}
			}
			else
			{
				Console.WriteLine("Invalid choice");
			}
		}

		public void DeleteStaffDetails()
		{
			Staff deleteObj = Utils.FindStaff(staffList);

			if (deleteObj != null)
			{
				staffList.Remove(deleteObj);
			}
			else
			{
				Console.WriteLine("Not found");
			}

			SerializeToXml();

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
