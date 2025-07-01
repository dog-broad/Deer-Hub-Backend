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

        public bool UpdateUser(int userId, string? username = null, string? email = null, string? passwordHash = null, string? role = null, bool? isActive = null)
        {
            try
            {
                var updates = new List<string>();
                var parameters = new List<SqlParameter>();

                if (username != null)
                {
                    updates.Add("Username = @Username");
                    parameters.Add(new SqlParameter("@Username", username));
                }
                if (email != null)
                {
                    updates.Add("Email = @Email");
                    parameters.Add(new SqlParameter("@Email", email));
                }
                if (passwordHash != null)
                {
                    updates.Add("PasswordHash = @PasswordHash");
                    parameters.Add(new SqlParameter("@PasswordHash", passwordHash));
                }
                if (role != null)
                {
                    updates.Add("Role = @Role");
                    parameters.Add(new SqlParameter("@Role", role));
                }
                if (isActive.HasValue)
                {
                    updates.Add("IsActive = @IsActive");
                    parameters.Add(new SqlParameter("@IsActive", isActive.Value));
                }

                if (updates.Count == 0)
                    return false; // Nothing to update

                updates.Add("ModifiedAt = GETDATE()");

                var setClause = string.Join(", ", updates);
                var query = $"UPDATE Users SET {setClause} WHERE UserID = @UserID";
                parameters.Add(new SqlParameter("@UserID", userId));

                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddRange(parameters.ToArray());
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
    }
}
