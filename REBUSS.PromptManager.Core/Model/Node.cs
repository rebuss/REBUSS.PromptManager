using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace REBUSS.PromptManager.Core.Model
{
    [DataContract]
    public class Node : NotifyPropertyChangedObject
    {
        private string name;
        private string value;
        private Node parent;

        internal Node()
        {
        }

        public Node(string name, string value) : this(Group.Root, name, value)
        {
        }

        public Node(Node parent) : this(parent, string.Empty, string.Empty)
        {
        }

        public Node(Node parent, string name, string value)
        {
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            Parent = parent;
            Name = name;
            Value = value;
        }

        [DataMember]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        [DataMember]
        public virtual string Value
        {
            get => value;
            set => SetProperty(ref this.value, value);
        }

        [DataMember]
        public virtual bool IsGroup { get; } = false;

        [DataMember]
        public bool IsPrompt => !IsGroup;

        public Node Parent
        {
            get => parent;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                parent = value;
            }
        }

        [DataMember]
        public virtual ObservableCollection<Node> Nodes { get; } = null;
    }
}