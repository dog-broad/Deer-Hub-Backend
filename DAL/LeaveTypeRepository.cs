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

        public bool UpdateLeaveType(int id, string? name, string? description)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    var updates = new List<string>();
                    var cmd = new SqlCommand();
                    cmd.Connection = con;

                    if (name != null)
                    {
                        updates.Add("Name = @Name");
                        cmd.Parameters.AddWithValue("@Name", name);
                    }
                    if (description != null)
                    {
                        updates.Add("Description = @Description");
                        cmd.Parameters.AddWithValue("@Description", description);
                    }

                    if (updates.Count == 0)
                        return false; // Nothing to update

                    string updateClause = string.Join(", ", updates);
                    cmd.CommandText = $"UPDATE LeaveTypes SET {updateClause} WHERE LeaveTypeID = @ID";
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
