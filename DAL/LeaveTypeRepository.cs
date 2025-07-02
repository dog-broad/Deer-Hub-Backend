using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.DAL
{
    public class LeaveTypeRepository
    {
        public bool InsertLeaveType(LeaveType leaveType)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO LeaveTypes (Name, Description) VALUES (@Name, @Description)", con);
                    cmd.Parameters.AddWithValue("@Name", leaveType.Name);
                    cmd.Parameters.AddWithValue("@Description", leaveType.Description);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertLeaveType Error: " + ex.Message);
                return false;
            }
        }

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

        public bool UpdateLeaveType(LeaveType leaveType)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE LeaveTypes SET Name = @Name, Description = @Description WHERE LeaveTypeID = @LeaveTypeID", con);
                    cmd.Parameters.AddWithValue("@Name", leaveType.Name);
                    cmd.Parameters.AddWithValue("@Description", leaveType.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@LeaveTypeID", leaveType.LeaveTypeID);
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

        public LeaveType GetLeaveTypeById(int leaveTypeId)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM LeaveTypes WHERE LeaveTypeID = @LeaveTypeID", con);
                    cmd.Parameters.AddWithValue("@LeaveTypeID", leaveTypeId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new LeaveType
                        {
                            LeaveTypeID = (int)reader["LeaveTypeID"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetLeaveTypeById Error: " + ex.Message);
            }
            return null;
        }
    }
}
