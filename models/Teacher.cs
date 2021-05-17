using System;
using StaffApp.Models.Base;

namespace StaffApp.Models
{
    public class Teacher : Staff
    {
        public string Subject { get; set; }
        public Teacher()
        {
            Type = "Teaching";
        }
    }
}
