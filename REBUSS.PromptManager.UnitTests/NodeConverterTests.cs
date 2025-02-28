using System.Text.Json;
using REBUSS.PromptManager.Core.Model;
using REBUSS.PromptManager.Core;
using System.Text.Json.Serialization;

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
            Assert.IsType<Node>(result);
            Assert.NotNull(result);
            Assert.Equal("TestNode", result.Name);
            Assert.Equal("TestValue", result.Value);
            Assert.False(result.IsGroup);
            Assert.Null(result.Nodes);
        }

        [Fact]
        public void Read_ShouldDeserializeGroupNodeCorrectly()
        {
            // Arrange
            string json = "[{\"Name\":\"TestGroup\",\"Value\":null,\"IsGroup\":true}]";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new NodeConverter());

            // Act
            Node[] result = JsonSerializer.Deserialize<Node[]>(json, options);

            // Assert
            Assert.IsType<Group>(result[0]);
            Assert.NotNull(result[0]);
            Assert.Equal("TestGroup", result[0].Name);
            Assert.Null(result[0].Value);
            Assert.True(result[0].IsGroup);
            Assert.Empty(result[0].Nodes);
        }

        [Fact]
        public void ReadShouldDeserializeNestedGroupNodeCorrectly()
        {
            string json = "{\"Name\":\"TestGroup\",\"Value\":\"\",\"IsGroup\":true,\"Nodes\":[{\"Name\":\"TestGroup1\",\"Value\":\"\",\"IsGroup\":true,\"Nodes\":[{\"Name\":\"TestNode\",\"Value\":\"TestValue\",\"IsGroup\":false,\"Nodes\":[]}]}]}";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new NodeConverter());

            Node result = JsonSerializer.Deserialize<Node>(json, options);

            Assert.IsType<Group>(result);
            Assert.NotNull(result);
            Assert.Equal("TestGroup", result.Name);
            Assert.Null(result.Value);
            Assert.True(result.IsGroup);
            Assert.Single(result.Nodes);
            Assert.IsType<Group>(result.Nodes[0]);
            Assert.Equal("TestGroup1", result.Nodes[0].Name);
            Assert.Null(result.Nodes[0].Value);
            Assert.True(result.Nodes[0].IsGroup);
            Assert.Single(result.Nodes[0].Nodes);
            Assert.IsType<Node>(result.Nodes[0].Nodes[0]);
            Assert.Equal("TestNode", result.Nodes[0].Nodes[0].Name);
            Assert.Equal("TestValue", result.Nodes[0].Nodes[0].Value);
            Assert.False(result.Nodes[0].Nodes[0].IsGroup);
            Assert.Null(result.Nodes[0].Nodes[0].Nodes);
        }

        [Fact]
        public void Write_ShouldSerializeNodeCorrectly()
        {
            // Arrange
            var node = new Node(Group.Root);
            node.Name = "TestNode";
            node.Value = "TestValue";
            var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve };
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
            var node = new Node(Group.Root);
            node.Name = "TestGroup";
            var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve };
            // Act
            string json = JsonSerializer.Serialize(node, options);

            // Assert
            Assert.Contains("\"Name\":\"TestGroup\"", json);
            Assert.Contains("\"IsGroup\":true", json);
        }
    }
}
