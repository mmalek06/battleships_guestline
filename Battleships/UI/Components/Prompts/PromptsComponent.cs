using Battleships.IO;

namespace Battleships.UI.Components.Prompts;

internal class PromptsComponent : IPromptsComponent
{
    public int Height => 3;

    private const int PromptHeight = 1;
    private const string InitialMessage = "Choose field or type EXIT below.";
    private const string InvalidMessage = "You entered an invalid value.";
    private const string GameFinishedPlayerWonMessage = "The game ended. You won.";
    
    private readonly IOutputDevice _outputDevice;

    public PromptsComponent(IOutputDevice outputDevice) =>
        _outputDevice = outputDevice;

    public void Display()
    {
        _outputDevice.ClearLine(0, 0);
        _outputDevice.DrawLine(InitialMessage);
        _outputDevice.DrawAt("", top: PromptHeight);
    }

    public void Warn()
    {
        _outputDevice.ClearLine(0, 0);
        _outputDevice.DrawLine($"{InvalidMessage} {InitialMessage}");
    }

    public void Finish()
    {
        _outputDevice.ClearLine(0, 0);
        _outputDevice.DrawLine(GameFinishedPlayerWonMessage);
    }

    public void ClearInput() =>
        _outputDevice.ClearLine(0, PromptHeight);
}