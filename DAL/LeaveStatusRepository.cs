using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Deer_Hub_Backend.DAL;
using LeaveStatus; // Update this namespace if your models are elsewhere

namespace DAL
{
    public class LeaveStatusRepository
    {
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
