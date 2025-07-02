using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Deer_Hub_Backend.Models;

namespace Deer_Hub_Backend.DAL
{
    public class UserRepository
    {
        public bool InsertUser(User user)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Users (Username, Email, PasswordHash, Role, IsActive, CreatedAt) " +
                        "VALUES (@Username, @Email, @PasswordHash, @Role, @IsActive, GETDATE())", con);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertUser Error: " + ex.Message);
                return false;
            }
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE IsActive = 1", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserID = (int)reader["UserID"],
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString(),
                            IsActive = (bool)reader["IsActive"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            ModifiedAt = reader["ModifiedAt"] as DateTime?
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllUsers Error: " + ex.Message);
            }
            return users;
        }

        public bool UpdateUser(User user)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Role = @Role, IsActive = @IsActive, ModifiedAt = GETDATE() WHERE UserID = @UserID", con);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                    cmd.Parameters.AddWithValue("@UserID", user.UserID);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateUser Error: " + ex.Message);
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserID = @UserID", con);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteUser Error: " + ex.Message);
                return false;
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE UserID = @UserID", con);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserID = (int)reader["UserID"],
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString(),
                            IsActive = (bool)reader["IsActive"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            ModifiedAt = reader["ModifiedAt"] as DateTime?
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetUserById Error: " + ex.Message);
            }
            return null;
        }
    }
}
