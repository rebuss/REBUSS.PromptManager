using Microsoft.VisualStudio.Extensibility.UI;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace REBUSS.PromptManager.Model
{
    [JsonConverter(typeof(NodeConverter))]
    [DataContract]
    public class Node : NotifyPropertyChangedObject
    {
        private string name;
        private string value;
        private ObservableCollection<Node> nodes = new ObservableCollection<Node>();

        public static Node NewPrompt()
        {
            return new Node(false);
        }

        public static Node NewGroup()
        {
            return new Node(true);
        }

        public Node() : this(false) { }

        public Node(bool group)
        {
            IsGroup = group;
            IsPrompt = !group;
        }

        [DataMember]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        [DataMember]
        public string Value
        {
            get => value;
            set => SetProperty(ref this.value, value);
        }

        [DataMember]
        public bool IsPrompt { get; }

        [DataMember]
        public bool IsGroup { get; }

        [DataMember]
        public ObservableCollection<Node> Nodes 
        { 
            get => nodes; 
            set => SetProperty(ref nodes, value); 
        }
    }
}