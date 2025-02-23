using Microsoft.VisualStudio.Extensibility.UI;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace REBUSS.PromptManager.PromptToolWindow
{
    /// <summary>
    /// A remote user control to use as tool window UI content.
    /// </summary>
    public class PromptToolWindowContent : RemoteUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromptToolWindowContent" /> class.
        /// </summary>
        public PromptToolWindowContent()
            : base(dataContext: new PromptToolWindowData())
        {
        }
    }
}
