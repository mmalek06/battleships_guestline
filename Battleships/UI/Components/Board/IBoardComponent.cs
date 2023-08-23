namespace Battleships.UI.Components.Board;

internal interface IBoardComponent : IComponent
{
    void Mark(MarkData markData);
}