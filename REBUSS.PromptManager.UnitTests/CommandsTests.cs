using REBUSS.PromptManager.Core;
using REBUSS.PromptManager.Core.Model;

namespace REBUSS.PromptManager.UnitTests
{
    public class CommandsTests
    {
        private Core.Commands systemUnderTests;

        public CommandsTests()
        {
            Group.Root.Nodes.Clear();
            systemUnderTests = new Commands(Group.Root);
        }

        [Fact]
        public void AddNewPrompt_AddsPromptToNodes()
        {
            var systemUnderTests = new Commands(Group.Root);

            systemUnderTests.AddNewPrompt();

            Assert.Single(Group.Root.Nodes);
            Assert.IsNotType<Group>(Group.Root.Nodes[0]);
            Assert.IsType<Node>(Group.Root.Nodes[0]);
        }

        [Fact]
        public void AddNewGroup()
        {
            var systemUnderTests = new Commands(Group.Root);

            systemUnderTests.AddNewGroup();

            Assert.Single(Group.Root.Nodes);
            Assert.IsType<Group>(Group.Root.Nodes[0]);
        }

        [Fact]
        public void RemoveGroup_RemovesGroupAndAllSubNodes()
        {
            var group = new Group(Group.Root);
            group.AddNewNode();
            group.AddNewNode();
            var newGroup = new Group(Group.Root);
            Group.Root.AddNode(group);
            Group.Root.AddNode(newGroup);
            Group.Root.AddNewNode();

            systemUnderTests = new Commands(Group.Root);

            systemUnderTests.Remove(group);

            Assert.Equal(2, Group.Root.Nodes.Count);
            Assert.DoesNotContain(group, Group.Root.Nodes);
            Assert.Contains(newGroup, Group.Root.Nodes);
        }
    }
}
