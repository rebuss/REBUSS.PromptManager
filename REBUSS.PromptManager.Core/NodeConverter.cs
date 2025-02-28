using System.Text.Json;
using System.Text.Json.Serialization;
using REBUSS.PromptManager.Core.Model;

namespace REBUSS.PromptManager.Core
{
    public class NodeConverter : JsonConverter<Node>
    {
        public override Node Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = doc.RootElement;

                bool isGroup = root.GetProperty("IsGroup").GetBoolean();
                return isGroup ? GetGroup(root, options) : GetNode(root, options);
            }
        }

        private Node GetNode(JsonElement element, JsonSerializerOptions options)
        {
            var node = new Node();
            foreach (JsonProperty property in element.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "Name":
                        node.Name = property.Value.GetString();
                        break;
                    case "Value":
                        node.Value = property.Value.GetString();
                        break;
                }
            }

            return node;
        }

        private Group GetGroup(JsonElement element, JsonSerializerOptions options)
        {
            var group = new Group(Group.Root);
            foreach (JsonProperty property in element.EnumerateObject())
            {
                switch (property.Name)
                {
                    case "Name":
                        group.Name = property.Value.GetString();
                        break;
                    case "Value":
                        group.Value = property.Value.GetString();
                        break;
                    case "Nodes":
                        var nodes = JsonSerializer.Deserialize<Node[]>(property.Value.GetRawText(), options);
                        group.SetNodes(nodes);
                        break;
                }
            }

            return group;
        }

        public override void Write(Utf8JsonWriter writer, Node value, JsonSerializerOptions options)
        {
            try
            {
                writer.WriteStartObject();

                writer.WriteString("Name", value.Name);
                writer.WriteString("Value", value.Value);
                writer.WriteBoolean("IsGroup", value.IsGroup);

                if (value.Nodes != null && value.Nodes.Count > 0)
                {
                    writer.WritePropertyName("Nodes");
                    JsonSerializer.Serialize(writer, value.Nodes.ToArray(), options);
                }

                writer.WriteEndObject();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}