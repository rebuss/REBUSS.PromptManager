using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.UI;
using REBUSS.PromptManager.Core;
using REBUSS.PromptManager.Core.Model;
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
        private string _name = string.Empty;
        private Node selectedNode;

        public PromptToolWindowData()
        {
            AddPromptCommand = new AsyncCommand(AddPrompt);
            RemoveSelectedPromptsCommand = new AsyncCommand(RemoveSelectedPrompt);
            CreateGroupCommand = new AsyncCommand(CreateGroup);
            CopyPromptCommand = new AsyncCommand(CopyPrompt);
            SavePromptsCommand = new AsyncCommand(SavePrompts);
            SelectedItemChangedCommand = new AsyncCommand((parameter, clientContext, cancellationToken) =>
            {
                SelectedNode = (Node)parameter;
                return Task.CompletedTask;
            });
            UnselectAllCommand = new AsyncCommand(UnselectAll);
            Group.Root.SetNodes(SettingsManager.LoadSettings());
            Nodes = Group.Root.Nodes;
            Commands = new Commands(Group.Root);
        }

        [DataMember]
        public AsyncCommand AddPromptCommand { get; }

        [DataMember]
        public Commands Commands { get; set; }

        [DataMember]
        public AsyncCommand CopyPromptCommand { get; }

        [DataMember]
        public AsyncCommand CreateGroupCommand { get; }

        [DataMember]
        public string Name
        {
            get => _name;
            set => SetProperty(ref this._name, value);
        }

        [DataMember]
        public ObservableCollection<Node> Nodes { get; set; }

        [DataMember]
        public AsyncCommand RemoveSelectedPromptsCommand { get; }

        [DataMember]
        public AsyncCommand SavePromptsCommand { get; }

        [DataMember]
        public AsyncCommand SelectedItemChangedCommand { get; }

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
        public AsyncCommand UnselectAllCommand { get; }

        private Task AddPrompt(object? parameter, IClientContext clientContext, CancellationToken cancellationToken)
        {
            Commands.AddNewPrompt();
            return Task.CompletedTask;
        }

        private Task CopyPrompt(object? parameter, IClientContext clientContext, CancellationToken cancellationToken)
        {
            if (SelectedNode != null)
            {
                var thread = new Thread(() => Clipboard.SetText(SelectedNode.Value));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
            return Task.CompletedTask;
        }

        private Task CreateGroup(object? parameter, IClientContext clientContext, CancellationToken cancellationToken)
        {
            Commands.AddNewGroup();
            return Task.CompletedTask;
        }

        private Task RemoveSelectedPrompt(object? parameter, IClientContext clientContext, CancellationToken cancellationToken)
        {
            Commands.Remove(SelectedNode);
            return Task.CompletedTask;
        }

        private Task SavePrompts(object? parameter, IClientContext clientContext, CancellationToken cancellationToken)
        {
            SettingsManager.SaveSettings(Nodes.ToArray());
            return Task.CompletedTask;
        }

        private async Task UnselectAll(object? arg1, CancellationToken token)
        {
            if (SelectedNode != null)
            {
                SelectedNode.IsSelected = false;
            }

            SelectedNode = null;
        }

        private void UpdateCommands()
        {
            if (SelectedNode is Group)
            {
                Commands = new Commands((Group)SelectedNode);
            }
            else
            {
                Commands = new Commands(Group.Root);
            }
        }
    }
}