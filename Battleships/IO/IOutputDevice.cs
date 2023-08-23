namespace Battleships.IO;

internal interface IOutputDevice
{
    IOutputDevice StartDrawing();

    IOutputDevice MoveViewport(int left, int top);

    IOutputDevice DrawLine(string contents);

    IOutputDevice DrawLines(string contents, int timesRepeat, int left = 0, int top = 0);
    
    IOutputDevice DrawAt(string contents, int left = 0, int top = 0);
    
    IOutputDevice DrawAt(char contents, int left = 0, int top = 0);

    IOutputDevice ClearLine(int left, int top);
}