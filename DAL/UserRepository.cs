using UserUser; // Make sure your User model is in this namespace
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Deer_Hub_Backend.DAL
{
    public class UserRepository
    {
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

        public bool UpdateUserEmail(int userId, string newEmail)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Users SET Email = @Email, ModifiedAt = GETDATE() WHERE UserID = @UserID", con);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateUserEmail Error: " + ex.Message);
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
