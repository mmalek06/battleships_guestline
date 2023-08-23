namespace Battleships.Opponents;

internal interface IOpponent
{
    int[,] PlaceShips();

    bool TellHit(int left, int top);
}