using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.DAL
{
    public class DepartmentRepository
    {
        public bool InsertDepartment(Department department)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Departments (Name, Description) VALUES (@Name, @Description)", con);
                    cmd.Parameters.AddWithValue("@Name", department.Name);
                    cmd.Parameters.AddWithValue("@Description", department.Description);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertDepartment Error: " + ex.Message);
                return false;
            }
        }

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

        public bool UpdateDepartment(int id, string? newName, string? newDescription)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    var updates = new List<string>();
                    var cmd = new SqlCommand();
                    cmd.Connection = con;

                    if (!string.IsNullOrEmpty(newName))
                    {
                        updates.Add("Name = @Name");
                        cmd.Parameters.AddWithValue("@Name", newName);
                    }
                    if (!string.IsNullOrEmpty(newDescription))
                    {
                        updates.Add("Description = @Description");
                        cmd.Parameters.AddWithValue("@Description", newDescription);
                    }

                    if (updates.Count == 0)
                        return false; // Nothing to update

                    cmd.CommandText = $"UPDATE Departments SET {string.Join(", ", updates)} WHERE DepartmentID = @ID";
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
