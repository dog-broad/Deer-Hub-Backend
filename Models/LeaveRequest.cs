using System;

namespace LeaveRequestBackend.Models
{
    public class LeaveRequest
    {
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }
        public int StatusID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public DateTime RequestedAt { get; set; }
        public int? ApprovedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}