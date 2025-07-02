using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deer_Hub_Backend.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public int DepartmentID { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

