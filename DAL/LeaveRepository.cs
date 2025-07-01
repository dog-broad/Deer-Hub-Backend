using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.DAL
{
    public class LeaveRepository
    {
        public List<LeaveRequest> GetAllLeaveRequests()
        {
            var requests = new List<LeaveRequest>();
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    string sql = "SELECT * FROM LeaveRequests WHERE IsDeleted = 0";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        requests.Add(MapLeaveRequest(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllLeaveRequests Error: " + ex.Message);
            }
            return requests;
        }

        public bool UpdateLeaveStatus(int leaveId, int statusId)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    string sql = "UPDATE LeaveRequests SET StatusID = @StatusID, ModifiedAt = GETDATE() WHERE LeaveID = @LeaveID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@StatusID", statusId);
                    cmd.Parameters.AddWithValue("@LeaveID", leaveId);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateLeaveStatus Error: " + ex.Message);
                return false;
            }
        }

        public bool DeleteLeaveRequest(int leaveId)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    string sql = "UPDATE LeaveRequests SET IsDeleted = 1, ModifiedAt = GETDATE() WHERE LeaveID = @LeaveID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@LeaveID", leaveId);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteLeaveRequest Error: " + ex.Message);
                return false;
            }
        }

        public List<LeaveRequest> GetLeavesByType(int leaveTypeId)
        {
            var leaves = new List<LeaveRequest>();
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    string sql = "SELECT * FROM LeaveRequests WHERE LeaveTypeID = @LeaveTypeID AND IsDeleted = 0";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@LeaveTypeID", leaveTypeId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        leaves.Add(MapLeaveRequest(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetLeavesByType Error: " + ex.Message);
            }
            return leaves;
        }

        public List<LeaveRequest> GetLeavesByStatus(int statusId)
        {
            var leaves = new List<LeaveRequest>();
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    string sql = "SELECT * FROM LeaveRequests WHERE StatusID = @StatusID AND IsDeleted = 0";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@StatusID", statusId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        leaves.Add(MapLeaveRequest(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetLeavesByStatus Error: " + ex.Message);
            }
            return leaves;
        }

        // Common mapping logic to reduce duplication
        private LeaveRequest MapLeaveRequest(SqlDataReader reader)
        {
            return new LeaveRequest
            {
                LeaveID = (int)reader["LeaveID"],
                EmployeeID = (int)reader["EmployeeID"],
                LeaveTypeID = (int)reader["LeaveTypeID"],
                StatusID = (int)reader["StatusID"],
                StartDate = (DateTime)reader["StartDate"],
                EndDate = (DateTime)reader["EndDate"],
                Reason = reader["Reason"].ToString(),
                RequestedAt = (DateTime)reader["RequestedAt"],
                ApprovedBy = reader["ApprovedBy"] as int?,
                IsDeleted = (bool)reader["IsDeleted"],
                ModifiedAt = reader["ModifiedAt"] as DateTime?
            };
        }
    }
}
