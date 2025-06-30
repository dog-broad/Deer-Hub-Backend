using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LeaveType; // Adjust if your LeaveType model is in a different namespace

namespace Deer_Hub_Backend.DAL
{
    public class LeaveTypeRepository
    {
        public List<LeaveType> GetAllLeaveTypes()
        {
            var types = new List<LeaveType>();
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM LeaveTypes", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        types.Add(new LeaveType
                        {
                            LeaveTypeID = (int)reader["LeaveTypeID"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllLeaveTypes Error: " + ex.Message);
            }
            return types;
        }

        public bool UpdateLeaveType(int id, string name, string description)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE LeaveTypes SET Name = @Name, Description = @Description WHERE LeaveTypeID = @ID", con);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateLeaveType Error: " + ex.Message);
                return false;
            }
        }

        public bool DeleteLeaveType(int id)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM LeaveTypes WHERE LeaveTypeID = @ID", con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteLeaveType Error: " + ex.Message);
                return false;
            }
        }
    }
}
