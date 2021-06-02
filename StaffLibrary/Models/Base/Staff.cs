using System.Xml.Serialization;

namespace StaffLibrary.Models.Base
{
	[XmlInclude(typeof(Teacher)), XmlInclude(typeof(Admin)), XmlInclude(typeof(Support))]
	public abstract class Staff
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public StaffType Type { get; set; }
		public string Phone { get; set; }

	}
}
