using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deer_Hub_Backend.Models
{
    public class LeaveType
    {
        public int LeaveTypeID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}