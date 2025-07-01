using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.DAL
{
    public class EmployeeRepository
    {
        public bool InsertEmployee(Employee emp)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    string sql = @"INSERT INTO Employees (UserID, FullName, DepartmentID, DateOfJoining, PhoneNumber) 
                                   VALUES (@UserID, @FullName, @DepartmentID, @DateOfJoining, @PhoneNumber)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@UserID", emp.UserID);
                    cmd.Parameters.AddWithValue("@FullName", emp.FullName);
                    cmd.Parameters.AddWithValue("@DepartmentID", emp.DepartmentID);
                    cmd.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber ?? (object)DBNull.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertEmployee Error: " + ex.Message);
                return false;
            }
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    string sql = "SELECT * FROM Employees WHERE IsActive = 1";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeID = (int)reader["EmployeeID"],
                            UserID = (int)reader["UserID"],
                            FullName = reader["FullName"].ToString(),
                            DepartmentID = (int)reader["DepartmentID"],
                            DateOfJoining = (DateTime)reader["DateOfJoining"],
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            IsActive = (bool)reader["IsActive"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            ModifiedAt = reader["ModifiedAt"] as DateTime?
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllEmployees Error: " + ex.Message);
            }

            return employees;
        }

        public bool UpdateEmployee(int employeeId, string? fullName = null, int? departmentId = null, DateTime? dateOfJoining = null)
        {
            try
            {
                var updates = new List<string>();
                var parameters = new List<SqlParameter>();

                if (fullName != null)
                {
                    updates.Add("FullName = @FullName");
                    parameters.Add(new SqlParameter("@FullName", fullName));
                }
                if (departmentId.HasValue)
                {
                    updates.Add("DepartmentID = @DepartmentID");
                    parameters.Add(new SqlParameter("@DepartmentID", departmentId.Value));
                }
                if (dateOfJoining.HasValue)
                {
                    updates.Add("DateOfJoining = @DateOfJoining");
                    parameters.Add(new SqlParameter("@DateOfJoining", dateOfJoining.Value));
                }

                if (updates.Count == 0)
                    return false; // Nothing to update

                updates.Add("ModifiedAt = GETDATE()");

                string sql = $"UPDATE Employees SET {string.Join(", ", updates)} WHERE EmployeeID = @EmployeeID";
                parameters.Add(new SqlParameter("@EmployeeID", employeeId));

                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddRange(parameters.ToArray());
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateEmployee Error: " + ex.Message);
                return false;
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    string sql = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteEmployee Error: " + ex.Message);
                return false;
            }
        }

        public Employee GetEmployeeById(int employeeId)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    string sql = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID AND IsActive = 1";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Employee
                        {
                            EmployeeID = (int)reader["EmployeeID"],
                            UserID = (int)reader["UserID"],
                            FullName = reader["FullName"].ToString(),
                            DepartmentID = (int)reader["DepartmentID"],
                            DateOfJoining = (DateTime)reader["DateOfJoining"],
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            IsActive = (bool)reader["IsActive"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            ModifiedAt = reader["ModifiedAt"] as DateTime?
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetEmployeeById Error: " + ex.Message);
            }
            return null; // Not found
        }
    }
}
