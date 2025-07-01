using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.DAL
{
    public class DepartmentRepository
    {
        public List<Department> GetAllDepartments()
        {
            var departments = new List<Department>();
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Departments", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            DepartmentID = (int)reader["DepartmentID"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllDepartments Error: " + ex.Message);
            }
            return departments;
        }

        public bool UpdateDepartment(int id, string newName, string newDescription)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Departments SET Name = @Name, Description = @Description WHERE DepartmentID = @ID", con);
                    cmd.Parameters.AddWithValue("@Name", newName);
                    cmd.Parameters.AddWithValue("@Description", newDescription);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateDepartment Error: " + ex.Message);
                return false;
            }
        }

        public bool DeleteDepartment(int id)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Departments WHERE DepartmentID = @ID", con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteDepartment Error: " + ex.Message);
                return false;
            }
        }
    }
}
