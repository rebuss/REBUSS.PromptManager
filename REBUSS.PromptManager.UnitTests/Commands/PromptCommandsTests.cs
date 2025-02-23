using REBUSS.PromptManager.Commands;
using REBUSS.PromptManager.Model;
using System.Collections.ObjectModel;

namespace REBUSS.PromptManager.UnitTests
{
    public class PromptCommandsTests
    {
        private PromptCommands systemUnderTests;

        public PromptCommandsTests()
        {
            systemUnderTests = new PromptCommands(new ObservableCollection<Node>());
        }

        [Fact]
        public void AddNewPrompt_AddsPromptToNodes()
        {
            var nodes = new ObservableCollection<Node>();
            var systemUnderTests = new PromptCommands(nodes);

            systemUnderTests.AddNewPrompt();

            Assert.Single(nodes);
            Assert.True(nodes[0].IsPrompt);
        }

        [Fact]
        public void AddNewGroup()
        {
            var nodes = new ObservableCollection<Node>();
            var systemUnderTests = new PromptCommands(nodes);

            systemUnderTests.AddNewGroup();

            Assert.Single(nodes);
            Assert.True(nodes[0].IsGroup);
        }

        [Fact]
        public void RemovePrompt_RemovesPrompt()
        {
            var nodes = new ObservableCollection<Node>();
            var node = Node.NewPrompt();
            nodes.Add(node);
            systemUnderTests = new PromptCommands(nodes);

            systemUnderTests.Remove(node);

            Assert.Equal(0, nodes.Count);
        }
    }
}