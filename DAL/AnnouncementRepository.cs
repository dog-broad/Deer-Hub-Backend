using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Announcement;

namespace Deer_Hub_Backend.DAL
{
    internal class AnnouncementRepository
    {
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

        public bool UpdateAnnouncement(int id, string title, string description)
        {
            try
            {
                using (var con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Announcements SET Title = @Title, Description = @Description WHERE AnnouncementID = @ID", con);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
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
