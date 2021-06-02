using StaffLibrary.Models.Base;

namespace StaffLibrary.Models
{
	public class Support : Staff
	{
		public int Age { get; set; }
		public Support()
		{
			Type = StaffType.Support;
		}
	}
}
