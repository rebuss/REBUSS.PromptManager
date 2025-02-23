using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.UI;
using REBUSS.PromptManager.Commands;
using REBUSS.PromptManager.Model;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows;

namespace REBUSS.PromptManager.PromptToolWindow
{
    /// <summary>
    /// ViewModel for the PromptToolWindowContent remote user control.
    /// </summary>
    [DataContract]
    internal class PromptToolWindowData : NotifyPropertyChangedObject
    {
        private Node selectedNode;
        private string _name = string.Empty;

        public PromptToolWindowData()
        {
            AddPromptCommand = new AsyncCommand(AddPrompt);
            RemoveSelectedPromptsCommand = new AsyncCommand(RemoveSelectedPrompt);
            CreateGroupCommand = new AsyncCommand(CreateGroup);
            CopyPromptCommand = new AsyncCommand(CopyPrompt);
            SavePromptsCommand = new AsyncCommand(SavePrompts);
            foreach (var node in SettingsManager.LoadSettings())
            {
                Nodes.Add(node);
            }

            CommandStartegy = new PromptCommands(Nodes);
        }

        [DataMember]
        public Node SelectedNode 
        { 
            get => selectedNode;
            set 
            { 
                SetProperty(ref this.selectedNode, value);
                UpdateCommands();
            }
        }

        [DataMember]
        public ICommandStrategy CommandStartegy { get; set; }

        private void UpdateCommands()
        {
            if (SelectedNode?.IsGroup == true)
            {
                CommandStartegy = new GroupCommands(SelectedNode.Nodes);
            }
            else
            {
                CommandStartegy = new PromptCommands(Nodes);
            }
        }

        [DataMember]
        public ObservableCollection<Node> Nodes { get; set; } = new ObservableCollection<Node>();

        [DataMember]
        public string Name
        {
            get => _name;
            set => SetProperty(ref this._name, value);
        }

        [DataMember]
        public AsyncCommand AddPromptCommand { get; }

        [DataMember]
        public AsyncCommand SavePromptsCommand { get; }

        [DataMember]
        public AsyncCommand RemoveSelectedPromptsCommand { get; }

        [DataMember]
        public AsyncCommand CreateGroupCommand { get; }

        [DataMember]
        public AsyncCommand CopyPromptCommand { get; }

        private Task SavePrompts(object? parameter, IClientContext clientContext, CancellationToken cancellationToken)
        {
            SettingsManager.SaveSettings(Nodes.ToArray());
            return Task.CompletedTask;
        }

        private Task AddPrompt(object? parameter, IClientContext clientContext, CancellationToken cancellationToken)
        {
            CommandStartegy.AddNewPrompt();
            return Task.CompletedTask;
        }

        private Task RemoveSelectedPrompt(object? parameter, IClientContext clientContext, CancellationToken cancellationToken)
        {
            CommandStartegy.Remove(SelectedNode);
            return Task.CompletedTask;
        }

        private Task CreateGroup(object? parameter, IClientContext clientContext, CancellationToken cancellationToken)
        {
            CommandStartegy.AddNewGroup();
            return Task.CompletedTask;
        }

        private Task CopyPrompt(object? parameter, IClientContext clientContext, CancellationToken cancellationToken)
        {
            if (SelectedNode != null)
                Clipboard.SetText(SelectedNode.Value);
            return Task.CompletedTask;
        }
    }
}
