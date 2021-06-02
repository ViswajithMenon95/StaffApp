using StaffLibrary.Models.Base;

namespace StaffLibrary.Models
{
	public class Teacher : Staff
	{
		public string Subject { get; set; }
		public Teacher()
		{
			Type = StaffType.Teacher;
		}
	}
}
