using REBUSS.PromptManager.Core.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace REBUSS.PromptManager.Core
{
    public class SettingsManager
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

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            options.Converters.Add(new NodeConverter());

            string json = JsonSerializer.Serialize(nodes, options);
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
            try
            {
                return JsonSerializer.Deserialize<Node[]>(json, options) ?? Array.Empty<Node>();
            }
            catch (Exception)
            {
                return Array.Empty<Node>();
            }
        }
    }
}
