using System.IO;
using System.Text.Json;

namespace Deer_Hub_Backend.AppConfig
{
    public static class ConfigurationManager
    {
        private static JsonElement _root;

        static ConfigurationManager()
        {
            var json = File.ReadAllText("appsettings.json");
            _root = JsonDocument.Parse(json).RootElement;
        }

        public static string GetConnectionString(string name)
        {
            if (_root.TryGetProperty("ConnectionStrings", out var connSection) &&
                connSection.TryGetProperty(name, out var connValue))
            {
                return connValue.GetString();
            }

            throw new KeyNotFoundException($"Connection string '{name}' not found.");
        }
    }
}
