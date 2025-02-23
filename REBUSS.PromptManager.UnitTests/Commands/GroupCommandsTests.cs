using REBUSS.PromptManager.Commands;
using REBUSS.PromptManager.Model;
using System.Collections.ObjectModel;

namespace REBUSS.PromptManager.UnitTests.Commands
{
    public class GroupCommandsTests
    {
        private GroupCommands systemUnderTests;

        public GroupCommandsTests()
        {
            systemUnderTests = new GroupCommands(new ObservableCollection<Node>());
        }

        [Fact]
        public void AddNewPrompt_AddsPromptToNodes()
        {
            var nodes = new ObservableCollection<Node>();
            var systemUnderTests = new GroupCommands(nodes);

            systemUnderTests.AddNewPrompt();

            Assert.Single(nodes);
            Assert.True(nodes[0].IsPrompt);
        }

        [Fact]
        public void AddNewGroup()
        {
            var nodes = new ObservableCollection<Node>();
            var systemUnderTests = new GroupCommands(nodes);

            systemUnderTests.AddNewGroup();

            Assert.Single(nodes);
            Assert.True(nodes[0].IsGroup);
        }

        [Fact]
        public void RemoveGroup_RemovesGroupAndAllSubNodes()
        {
            var nodes = new ObservableCollection<Node>();
            var group = Node.NewGroup();
            group.Nodes.Add(Node.NewPrompt());
            group.Nodes.Add(Node.NewPrompt());
            nodes.Add(group);
            nodes.Add(Node.NewPrompt());
            nodes.Add(Node.NewGroup());
            systemUnderTests = new GroupCommands(nodes);

            systemUnderTests.Remove(group);

            Assert.Equal(2, nodes.Count);
        }
    }
}
