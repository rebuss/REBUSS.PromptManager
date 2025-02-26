using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace REBUSS.PromptManager.Core.Model
{
    [DataContract]
    public class Group : Node
    {
        private static readonly Lazy<Group> _rootInstance = new Lazy<Group>(() => new Group());
        public static Group Root => _rootInstance.Value;

        protected readonly ObservableCollection<Node> _nodes = new ObservableCollection<Node>();

        private Group()
        {
            Name = "Root";
        }

        public Group(Group parent, string name = "")
        {
            Parent = parent;
            Name = name;
        }

        [DataMember]
        public override bool IsGroup { get; } = true;

        [DataMember]
        public override ObservableCollection<Node> Nodes { get => _nodes; }

        [DataMember]
        public override string Value { get => null; set { } }

        public void AddNewNode()
        {
            var node = new Node(this);
            AddNode(node);
        }

        public void AddNode(Node node)
        {
            node.Parent = this;
            _nodes.Add(node);
        }

        public void SetNodes(Node[] nodes)
        {
            _nodes.Clear();
            foreach (var node in nodes)
            {
                AddNode(node);
            }
        }
    }
}
