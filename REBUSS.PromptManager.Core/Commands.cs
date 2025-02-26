using REBUSS.PromptManager.Core.Model;

namespace REBUSS.PromptManager.Core
{
    public class Commands
    {
        private readonly Group group;

        public Commands(Group group)
        {
            this.group = group;
        }

        public void AddNewGroup()
        {
            var newGroup = new Group(Group.Root);
            group.AddNode(newGroup);
        }

        public void AddNewPrompt()
        {
            group.AddNewNode();
        }

        public void Remove(Node node)
        {
            group.Nodes.Remove(node);
        }
    }
}
