using REBUSS.PromptManager.Model;
using System.Text.Json;

namespace REBUSS.PromptManager
{
    internal class SettingsManager
    {
        private static string settingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "REBUSS.PromptManager",
            "settings.json");

        public static void SaveSettings(Node[] nodes)
        {
            string directory = Path.GetDirectoryName(settingsPath);
            if (!Directory.Exists(directory)) 
            { 
                Directory.CreateDirectory(directory);
            }

            string json = JsonSerializer.Serialize(nodes);
            File.WriteAllText(settingsPath, json);
        }

        public static Node[] LoadSettings()
        {
            if (!File.Exists(settingsPath)) 
            {
                return Array.Empty<Node>();
            }

            string json = File.ReadAllText(settingsPath);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new NodeConverter());
            return JsonSerializer.Deserialize<Node[]>(json) ?? Array.Empty<Node>();
        }
    }
}
