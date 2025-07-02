using System;
using System.Collections.Generic;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.Validators
{
    public static class LeaveValidator
    {
        // Rule 1: StartDate should be before or equal to EndDate
        public static bool IsDateRangeValid(DateTime start, DateTime end)
        {
            return start <= end;
        }

        // Rule 2: Employee must have joined at least 30 days ago
        public static bool IsEligibleForLeave(Employee emp, DateTime leaveStart)
        {
            return (leaveStart - emp.DateOfJoining).TotalDays >= 30;
        }

        // Rule 3: New leave request should not overlap with existing leaves
        public static bool IsOverlapping(DateTime newStart, DateTime newEnd, List<LeaveRequest> existingLeaves)
        {
            foreach (var leave in existingLeaves)
            {
                if (!leave.IsDeleted && (
                    (newStart <= leave.EndDate && newEnd >= leave.StartDate)
                ))
                {
                    return true;
                }
            }
            return false;
        }

        // Rule 4: Prevent leave requests in the past
        public static bool IsFutureDate(DateTime date)
        {
            return date.Date >= DateTime.Today;
        }

        // Rule 5: Reason should not be empty or too short
        public static bool IsReasonValid(string? reason)
        {
            return !string.IsNullOrWhiteSpace(reason) && reason.Trim().Length >= 5;
        }

        // Rule 6: Leave duration limit (optional: let's say max 30 days at once)
        public static bool IsWithinMaxLeaveDays(DateTime start, DateTime end, int maxDays = 30)
        {
            return (end - start).TotalDays + 1 <= maxDays;
        }
    }
}