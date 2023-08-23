using Battleships.UI.Components.Board;

namespace Battleships.Game;

internal interface IEventHandlers
{
    void OnValidTargetChosen(MarkData markData);

    void OnInvalidTargetChosen();

    void OnGameCompleted();
}