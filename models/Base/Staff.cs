using System;
using System.Xml.Serialization;

namespace StaffApp.Models.Base
{
	[XmlInclude(typeof( Teacher )), XmlInclude(typeof( Admin )), XmlInclude(typeof( Support ))]
	public abstract class Staff
	{
		private static int idCount = 0;
		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Phone { get; set; }

		public Staff()
		{
			idCount++;
			Id = idCount;
		}

		public virtual void AddStaffDetails()
		{
			Console.WriteLine("Enter the staff name");
			Name = Console.ReadLine();
			Console.WriteLine("Enter the staff phone number");
			Phone = Console.ReadLine();
		}
		public virtual void UpdateStaffDetails()
		{
			string checkInput;
			Console.WriteLine("Enter the new staff name");
			checkInput = Console.ReadLine();
			if (checkInput != "")
			{
				Name = checkInput;
			}

			Console.WriteLine("Enter the new staff phone number");
			checkInput = Console.ReadLine();
			if (checkInput != "")
			{
				Phone = checkInput;
			}
		}
		public virtual void ViewStaffDetails()
		{
			Console.WriteLine("Id: {0}", Id);
			Console.WriteLine("Name: {0}", Name);
			Console.WriteLine("Type: {0}", Type);
			Console.WriteLine("Phone: {0}", Phone);
		}
	}
}