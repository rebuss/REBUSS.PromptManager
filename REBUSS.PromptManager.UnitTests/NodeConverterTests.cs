using System.Text.Json;
using System.Collections.ObjectModel;
using REBUSS.PromptManager.Model;
using Xunit;

namespace REBUSS.PromptManager.UnitTests
{
    public class NodeConverterTests
    {
        [Fact]
        public void Read_ShouldDeserializeNodeCorrectly()
        {
            // Arrange
            string json = "{\"Name\":\"TestNode\",\"Value\":\"TestValue\",\"IsGroup\":false,\"Nodes\":[]}";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new NodeConverter());

            // Act
            Node result = JsonSerializer.Deserialize<Node>(json, options);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestNode", result.Name);
            Assert.Equal("TestValue", result.Value);
            Assert.False(result.IsGroup);
            Assert.True(result.IsPrompt);
            Assert.Empty(result.Nodes);
        }

        [Fact]
        public void Read_ShouldDeserializeGroupNodeCorrectly()
        {
            // Arrange
            string json = "{\"Name\":\"TestGroup\",\"Value\":\"\",\"IsGroup\":true,\"Nodes\":[]}";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new NodeConverter());

            // Act
            Node result = JsonSerializer.Deserialize<Node>(json, options);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestGroup", result.Name);
            Assert.Equal("", result.Value);
            Assert.True(result.IsGroup);
            Assert.False(result.IsPrompt);
            Assert.Empty(result.Nodes);
        }

        [Fact]
        public void Write_ShouldSerializeNodeCorrectly()
        {
            // Arrange
            var node = Node.NewPrompt();
            node.Name = "TestNode";
            node.Value = "TestValue";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new NodeConverter());

            // Act
            string json = JsonSerializer.Serialize(node, options);

            // Assert
            Assert.Contains("\"Name\":\"TestNode\"", json);
            Assert.Contains("\"Value\":\"TestValue\"", json);
            Assert.Contains("\"IsGroup\":false", json);
        }

        [Fact]
        public void Write_ShouldSerializeGroupNodeCorrectly()
        {
            // Arrange
            var node = Node.NewGroup();
            node.Name = "TestGroup";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new NodeConverter());

            // Act
            string json = JsonSerializer.Serialize(node, options);

            // Assert
            Assert.Contains("\"Name\":\"TestGroup\"", json);
            Assert.Contains("\"IsGroup\":true", json);
        }
    }
}
