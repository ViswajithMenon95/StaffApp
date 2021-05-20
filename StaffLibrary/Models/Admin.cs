using StaffLibrary.Models.Base;

namespace StaffLibrary.Models
{
	public class Admin : Staff
	{
		public string Department { get; set; }
		public Admin()
		{
			Type = "Administrative";
		}
	}
}
