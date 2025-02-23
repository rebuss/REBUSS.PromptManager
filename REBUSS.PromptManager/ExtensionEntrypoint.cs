using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;

namespace REBUSS.PromptManager
{
    /// <summary>
    /// Extension entrypoint for the VisualStudio.Extensibility extension.
    /// </summary>
    [VisualStudioContribution]
    internal class ExtensionEntrypoint : Extension
    {
        /// <inheritdoc/>
        public override ExtensionConfiguration ExtensionConfiguration => new()
        {
            Metadata = new(
                    id: "REBUSS.PromptManager.a4ad0cd6-48be-46f7-9197-b680b732b05e",
                    version: this.ExtensionAssemblyVersion,
                    publisherName: "Michał Korbecki",
                    displayName: "Prompt Manager",
                    description: "Enhance your development workflow with the Copilot Prompt Manager extension for Visual Studio. This powerful tool allows you to efficiently manage and customize prompts for Microsoft Copilot, ensuring a seamless and productive coding experience. With Copilot Prompt Manager, you can:\r\n\r\nCreate and Edit Prompts: Easily create new prompts or modify existing ones to suit your specific coding needs.\r\nOrganize Prompts: Categorize and organize your prompts for quick access and better management.\r\nUser-Friendly Interface: Enjoy a clean and intuitive interface designed to streamline your workflow.\r\nBoost your productivity and take full control of your Copilot prompts with the Copilot Prompt Manager extension for Visual Studio."),
        };

        /// <inheritdoc />
        protected override void InitializeServices(IServiceCollection serviceCollection)
        {
            base.InitializeServices(serviceCollection);

            // You can configure dependency injection here by adding services to the serviceCollection.
        }
    }
}
