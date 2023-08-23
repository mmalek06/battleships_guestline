namespace Battleships.UI.Components.Board;

internal readonly record struct MarkData(
    bool IsHit,
    int Left,
    int Right);