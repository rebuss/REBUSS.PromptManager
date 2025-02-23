using REBUSS.PromptManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REBUSS.PromptManager.Commands
{
    public abstract class CommandStrategyBase : ICommandStrategy
    {
        protected readonly ObservableCollection<Node> _nodes;
        protected CommandStrategyBase(ObservableCollection<Node> nodes)
        {
            _nodes = nodes;
        }

        public void AddNewPrompt()
        {
            _nodes.Add(Node.NewPrompt());
        }

        public void AddNewGroup()
        {
            _nodes.Add(Node.NewGroup());
        }

        public abstract void Remove(Node node);
    }
}
