using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using REBUSS.PromptManager.Model;

public class NodeConverter : JsonConverter<Node>
{
    public override Node Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            JsonElement root = doc.RootElement;

            bool isGroup = root.GetProperty("IsGroup").GetBoolean();
            Node node = isGroup ? Node.NewGroup() : Node.NewPrompt();

            foreach (JsonProperty property in root.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "Name":
                        node.Name = property.Value.GetString();
                        break;
                    case "Value":
                        node.Value = property.Value.GetString();
                        break;
                    case "Nodes":
                        node.Nodes = JsonSerializer.Deserialize<ObservableCollection<Node>>(property.Value.GetRawText(), options);
                        break;
                }
            }

            return node;
        }
    }

    public override void Write(Utf8JsonWriter writer, Node value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value);
    }
}