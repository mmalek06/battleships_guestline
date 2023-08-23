namespace Battleships.IO;

internal class ConsoleOutputDevice : IOutputDevice
{
    private int _viewportLeft;
    private int _viewportTop;
    
    public IOutputDevice StartDrawing()
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);

        _viewportTop = 0;
        _viewportLeft = 0;

        return this;
    }

    public IOutputDevice MoveViewport(int left, int top)
    {
        Console.SetCursorPosition(left, top);
        
        _viewportLeft = left;
        _viewportTop = top;

        return this;
    }

    public IOutputDevice DrawLine(string contents)
    {
        Console.SetCursorPosition(_viewportLeft, Console.GetCursorPosition().Top);
        Console.WriteLine(contents);
        
        return this;
    }

    public IOutputDevice DrawLines(string contents, int timesRepeat, int left = 0, int top = 0)
    {
        for (var i= 0; i < timesRepeat; i++)
            DrawAt(contents, left, top + i);

        return this;
    }
    
    public IOutputDevice DrawAt(string contents, int left = 0, int top = 0)
    {
        Console.SetCursorPosition(_viewportLeft + left, _viewportTop + top);
        Console.Write(contents);

        return this;
    }
    
    public IOutputDevice DrawAt(char contents, int left = 0, int top = 0)
    {
        Console.SetCursorPosition(_viewportLeft + left, _viewportTop + top);
        Console.Write(contents);

        return this;
    }

    public IOutputDevice ClearLine(int left, int top)
    {
        Console.SetCursorPosition(_viewportLeft + left, _viewportTop + top);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(_viewportLeft + left, _viewportTop + top);
        
        return this;
    }
}