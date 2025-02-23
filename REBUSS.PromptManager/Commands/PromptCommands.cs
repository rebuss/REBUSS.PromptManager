using REBUSS.PromptManager.Model;
using System.Collections.ObjectModel;

namespace REBUSS.PromptManager.Commands
{
    public class PromptCommands : CommandStrategyBase
    {
        public PromptCommands(ObservableCollection<Node> nodes) : base(nodes)
        {
        }

        public override void Remove(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
