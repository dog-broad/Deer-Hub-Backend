using Deer_Hub_Backend.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Deer_Hub_Backend.DAL
{
    public class AnnouncementRepository
    {
        public bool InsertAnnouncement(Announcement announcement)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Announcements (Title, Description, PostedBy, PostedAt, IsVisible) VALUES (@Title, @Description, @PostedBy, @PostedAt, @IsVisible)", con);
                    cmd.Parameters.AddWithValue("@Title", announcement.Title);
                    cmd.Parameters.AddWithValue("@Description", announcement.Description);
                    cmd.Parameters.AddWithValue("@PostedBy", announcement.PostedBy);
                    cmd.Parameters.AddWithValue("@PostedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IsVisible", announcement.IsVisible);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddAnnouncement Error: " + ex.Message);
                return false;
            }
        }

        public List<Announcement> GetAllAnnouncements()
        {
            var announcements = new List<Announcement>();
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Announcements WHERE IsVisible = 1", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        announcements.Add(new Announcement
                        {
                            AnnouncementID = (int)reader["AnnouncementID"],
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            PostedBy = (int)reader["PostedBy"],
                            PostedAt = (DateTime)reader["PostedAt"],
                            IsVisible = (bool)reader["IsVisible"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllAnnouncements Error: " + ex.Message);
            }
            return announcements;
        }

        public bool UpdateAnnouncement(int id, string? title = null, string? description = null)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    var updates = new List<string>();
                    var cmd = new SqlCommand();
                    cmd.Connection = con;

                    if (title != null)
                    {
                        updates.Add("Title = @Title");
                        cmd.Parameters.AddWithValue("@Title", title);
                    }
                    if (description != null)
                    {
                        updates.Add("Description = @Description");
                        cmd.Parameters.AddWithValue("@Description", description);
                    }

                    if (updates.Count == 0)
                        return false; // Nothing to update

                    cmd.CommandText = $"UPDATE Announcements SET {string.Join(", ", updates)} WHERE AnnouncementID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateAnnouncement Error: " + ex.Message);
                return false;
            }
        }

        public bool DeleteAnnouncement(int id)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Announcements WHERE AnnouncementID = @ID", con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteAnnouncement Error: " + ex.Message);
                return false;
            }
        }
    }
}
