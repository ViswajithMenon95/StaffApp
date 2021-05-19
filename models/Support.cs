using StaffApp.Models.Base;

namespace StaffApp.Models
{
    public class Support : Staff
    {
        public int Age { get; set; }
        public Support()
        {
            Type = "Support";
        }
    }
}
