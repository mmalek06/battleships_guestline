using Battleships.IO;

namespace Battleships.UI.Components;

internal class GameDescriptionComponent : IComponent
{
    public int Height => _lines.Count;

    private readonly IReadOnlyCollection<string> _lines = new[]
    {
        $"{Constants.HitMark} marks a hit.",
        $"{Constants.MissMark} marks a miss.",
        "You choose your targets by typing in the column ",
        "name and row name, for example B6.",
        "Enter EXIT to terminate the program."
    };
    
    private readonly IOutputDevice _outputDevice;

    public GameDescriptionComponent(IOutputDevice outputDevice) =>
        _outputDevice = outputDevice;

    public void Display()
    {
        foreach (var line in _lines)
            _outputDevice.DrawLine(line);
    }
}