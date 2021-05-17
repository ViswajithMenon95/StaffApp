using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Serialization;
using StaffApp.Data;
using StaffApp.Models;
using StaffApp.Models.Base;
using StaffApp.Utilities;

namespace StaffApp.Data
{
    public class StaffInXml : IStaff
    {
		[XmlArray(ElementName ="StaffList")]
		[XmlArrayItem("StaffMember")]
		public static List<Staff> staffList;

		static StaffInXml()
		{
			if (!File.Exists("StaffList.xml"))
			{
				staffList = new List<Staff>();
			}
			else
			{
				XmlReader reader = XmlReader.Create("StaffList.xml");
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
                addObj = new Teacher();

                Utils.AddCommonDetails(addObj);
                Console.WriteLine("Enter the subject");
                ((Teacher)addObj).Subject = Console.ReadLine();
            }

            else if (choice == 2)
            {
                addObj = new Admin();

                Utils.AddCommonDetails(addObj);
                Console.WriteLine("Enter the department");
                ((Admin)addObj).Department = Console.ReadLine();
            }

            else if (choice == 3)
            {
                addObj = new Support();
                int parseInput;

                Utils.AddCommonDetails(addObj);
                Console.WriteLine("Enter the age");

                if (Int32.TryParse(Console.ReadLine(), out parseInput))
                {
                    ((Support)addObj).Age = parseInput;
                }
                else
                {
                    Console.WriteLine("Invalid age");
                }
            }

			staffList.Add(addObj);

			SerializeToXml();
		}


		public void UpdateStaffDetails()
        {
			Staff updateObj = Utils.FindStaff(staffList);

			if (updateObj != null)
			{
				string checkInput;
				Console.WriteLine("Enter the new staff name");
				checkInput = Console.ReadLine();
				if (checkInput != "")
				{
					updateObj.Name = checkInput;
				}

				Console.WriteLine("Enter the new staff phone number");
				checkInput = Console.ReadLine();
				if (checkInput != "")
				{
					updateObj.Phone = checkInput;
				}

				if (updateObj is Teacher)
				{
					Console.WriteLine("Enter the new subject");
					checkInput = Console.ReadLine();
					if (checkInput != "")
					{
						((Teacher)updateObj).Subject = checkInput;
					}
				}
				else if (updateObj is Admin)
				{
					Console.WriteLine("Enter the new department");
					checkInput = Console.ReadLine();
					if (checkInput != "")
					{
						((Admin)updateObj).Department = checkInput;
					}
				}
				else if (updateObj is Support)
				{
					Console.WriteLine("Enter the new age");
					checkInput = Console.ReadLine();

					if (checkInput != "")
					{
						int parseInput;
						if (Int32.TryParse(checkInput, out parseInput))
						{
							((Support)updateObj).Age = parseInput;
						}
						else
						{
							Console.WriteLine("Invalid age");
						}
					}
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
					Utils.DisplayCommonDetails(viewObj);

					if (viewObj is Teacher)
					{
						Console.WriteLine("Subject: {0}\n", ((Teacher)viewObj).Subject);
					}
					else if (viewObj is Admin)
					{
						Console.WriteLine("Department: {0}\n", ((Admin)viewObj).Department);
					}
					else if (viewObj is Support)
					{
						Console.WriteLine("Age: {0}\n", ((Support)viewObj).Age);
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
						Utils.DisplayCommonDetails(viewObj);
						Console.WriteLine("Subject: {0}\n", ((Teacher)viewObj).Subject);
					}
					else if ((viewObj is Admin) && (viewChoice == 3 || viewChoice == 5))
					{
						Utils.DisplayCommonDetails(viewObj);
						Console.WriteLine("Department: {0}\n", ((Admin)viewObj).Department);
					}
					else if ((viewObj is Support) && (viewChoice == 4 || viewChoice == 5))
					{
						Utils.DisplayCommonDetails(viewObj);
						Console.WriteLine("Age: {0}\n", ((Support)viewObj).Age);
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
			XmlWriter writer = XmlWriter.Create("StaffList.xml", settings);
			writeSerializer.Serialize(writer, staffList);
			writer.Close();
		}

	}
}
