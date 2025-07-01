using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Deer_Hub_Backend.DAL;
using Deer_Hub_Backend.Models;
namespace DAL
{
    public class LeaveStatusRepository
    {
        public bool InsertLeaveStatus(LeaveStatus status)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO LeaveStatuses (StatusName) VALUES (@StatusName)", con);
                    cmd.Parameters.AddWithValue("@StatusName", status.StatusName);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertLeaveStatus Error: " + ex.Message);
                return false;
            }
        }

        public List<LeaveStatus> GetAllStatuses()
        {
            var statuses = new List<LeaveStatus>();
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM LeaveStatuses", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        statuses.Add(new LeaveStatus
                        {
                            StatusID = (int)reader["StatusID"],
                            StatusName = reader["StatusName"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllStatuses Error: " + ex.Message);
            }
            return statuses;
        }

        public bool UpdateStatus(int id, string statusName)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE LeaveStatuses SET StatusName = @StatusName WHERE StatusID = @ID", con);
                    cmd.Parameters.AddWithValue("@StatusName", statusName);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateStatus Error: " + ex.Message);
                return false;
            }
        }

        public bool DeleteStatus(int id)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM LeaveStatuses WHERE StatusID = @ID", con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteStatus Error: " + ex.Message);
                return false;
            }
        }
    }
}
