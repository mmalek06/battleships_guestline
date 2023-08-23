namespace Battleships.UI.Components;

internal interface IComponent
{
    int Height { get; }
    
    void Display();
}