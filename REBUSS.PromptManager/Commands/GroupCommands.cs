using REBUSS.PromptManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REBUSS.PromptManager.Commands
{
    public class GroupCommands : CommandStrategyBase
    {
        public GroupCommands(ObservableCollection<Node> nodes) : base(nodes)
        {
        }

        public override void Remove(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
