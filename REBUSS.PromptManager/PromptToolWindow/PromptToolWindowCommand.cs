using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace REBUSS.PromptManager.PromptToolWindow
{
    /// <summary>
    /// A command for showing a tool window.
    /// </summary>
    [VisualStudioContribution]
    public class PromptToolWindowCommand : Command
    {
        /// <inheritdoc />
        public override CommandConfiguration CommandConfiguration => new(displayName: "Show Prompt Manager")
        {
            // Use this object initializer to set optional parameters for the command. The required parameter,
            // displayName, is set above. To localize the displayName, add an entry in .vsextension\string-resources.json
            // and reference it here by passing "%REBUSS.PromptManager.PromptToolWindow.PromptToolWindowCommand.DisplayName%" as a constructor parameter.
            Placements = [CommandPlacement.KnownPlacements.ExtensionsMenu],
            Icon = new(ImageMoniker.KnownValues.OnPremisesDatabase, IconSettings.IconAndText),
        };

        /// <inheritdoc />
        public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
        {
            await this.Extensibility.Shell().ShowToolWindowAsync<PromptToolWindow>(activate: true, cancellationToken);
        }
    }
}
