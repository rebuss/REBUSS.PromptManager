using REBUSS.PromptManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REBUSS.PromptManager.Commands
{
    public interface ICommandStrategy
    {
        void AddNewPrompt();

        void AddNewGroup();

        void Remove(Node node);
    }
}
