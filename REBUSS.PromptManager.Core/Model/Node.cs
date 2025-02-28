using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace REBUSS.PromptManager.Core.Model
{
    [DataContract]
    public class Node : NotifyPropertyChangedObject
    {
        private bool isSelected = false;
        private string name;
        private Node parent;
        private string value;

        public Node()
        {
        }

        public Node(string name, string value) : this(Group.Root, name, value)
        {
        }

        public Node(Node parent) : this(parent, "New Prompt", string.Empty)
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
        public bool IsEditing { get; set; } = false;

        [DataMember]
        public virtual bool IsGroup { get; } = false;

        [DataMember]
        public bool IsPrompt => !IsGroup;

        [DataMember]
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        [DataMember]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        [DataMember]
        public virtual ObservableCollection<Node> Nodes { get; } = null;

        [IgnoreDataMember]
        public Node Parent
        {
            get => parent;
            set { parent = value ?? Group.Root; }
        }

        [DataMember]
        public virtual string Value
        {
            get => value;
            set => SetProperty(ref this.value, value);
        }
    }
}