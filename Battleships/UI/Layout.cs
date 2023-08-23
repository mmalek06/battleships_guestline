using Battleships.IO;
using Battleships.UI.Components;

namespace Battleships.UI;

internal class Layout : ILayout
{
    public int Height { get; }

    private const int BottomMargin = 1;
    
    private readonly IReadOnlyList<IComponent> _children;
    private readonly IOutputDevice _outputDevice;

    public Layout(IReadOnlyList<IComponent> children, IOutputDevice outputDevice)
    {
        _children = children;
        _outputDevice = outputDevice;
        Height = children.Sum(c => c.Height);
    }
    
    public void Display()
    {
        _outputDevice.StartDrawing();
        
        var offsetTopSoFar = 0;
        
        foreach (var child in _children)
        {
            _outputDevice.MoveViewport(0, offsetTopSoFar);
            child.Display();

            offsetTopSoFar += child.Height + BottomMargin;
        }
    }

    public void Focus(IComponent component)
    {
        var offsetTopSoFar = 0;

        foreach (var child in _children)
        {
            if (child != component)
                offsetTopSoFar += child.Height + BottomMargin;
            else
                break;
        }

        _outputDevice.MoveViewport(0, offsetTopSoFar);
    }
}