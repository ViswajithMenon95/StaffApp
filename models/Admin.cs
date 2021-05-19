using StaffApp.Models.Base;

namespace StaffApp.Models
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
