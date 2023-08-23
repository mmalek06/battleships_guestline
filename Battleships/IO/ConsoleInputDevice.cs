namespace Battleships.IO;

internal class ConsoleInputDevice : IInputDevice
{
    public string GetInput()
    {
        var input = Console.ReadLine();

        return string.IsNullOrEmpty(input) ? string.Empty : input;
    }
}