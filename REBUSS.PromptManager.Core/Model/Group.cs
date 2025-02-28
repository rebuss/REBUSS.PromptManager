using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace REBUSS.PromptManager.Core.Model
{
    [DataContract]
    public class Group : Node
    {
        protected readonly ObservableCollection<Node> _nodes = new ObservableCollection<Node>();
        private static readonly Lazy<Group> _rootInstance = new Lazy<Group>(() => new Group("Root"));
        private bool _isExpanded;

        public Group()
        {
        }

        public Group(string name)
        {
            Name = name;
        }

        public Group(Group parent, string name = "New Group") : this(name)
        {
            Parent = parent;
        }

        public static Group Root => _rootInstance.Value;

        [DataMember]
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        [DataMember]
        public override bool IsGroup { get; } = true;

        [DataMember]
        public override ObservableCollection<Node> Nodes { get => _nodes; }

        [DataMember]
        public override string Value
        { get => null; set { } }

        public void AddNewNode()
        {
            var node = new Node(this);
            AddNode(node);
        }

        public void AddNode(Node node)
        {
            node.Parent = this;
            _nodes.Add(node);
            IsExpanded = true;
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