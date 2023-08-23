namespace Battleships.Opponents;

internal class RandomPlacementOpponent : IOpponent
{
    private enum Direction
    {
        Horizontal, 
        Vertical
    }
    
    private enum ShipType
    {
        Battleship = 5,
        Destroyer = 4
    }
    
    private readonly int[,] _board;
    private readonly (int, int)[] _neighbors = 
    {
        (0, -1), // above
        (-1, 0), // left
        (1, 0),  // right
        (0, 1)   // below
    };

    public RandomPlacementOpponent() =>
        _board = new int[Constants.BoardSize, Constants.BoardSize];

    public int[,] PlaceShips()
    {
        PlaceShip(ShipType.Battleship, 1);
        PlaceShip(ShipType.Destroyer, 2);

        return CopyBoard();
    }

    public bool TellHit(int left, int top) =>
        _board[left, top] != 0;
    
    private void PlaceShip(ShipType shipType, int count)
    {
        var rand = new Random();

        for (var i = 0; i < count; i++)
        {
            var placed = false;

            while (!placed)
            {
                var startX = rand.Next(Constants.BoardSize);
                var startY = rand.Next(Constants.BoardSize);
                var direction = (Direction)rand.Next(2);

                if (!IsValidPlacement(startX, startY, direction, (int)shipType)) 
                    continue;
                
                for (var j = 0; j < (int)shipType; j++)
                {
                    if (direction == Direction.Horizontal)
                        _board[startX + j, startY] = 1;
                    else
                        _board[startX, startY + j] = 1;
                }

                placed = true;
            }
        }
    }
    
    private bool IsValidPlacement(int startX, int startY, Direction direction, int length)
    {
        for (var move = 0; move < length; move++)
        {
            var x = direction == Direction.Horizontal ? startX + move : startX;
            var y = direction == Direction.Vertical ? startY + move : startY;

            if (OutsideBounds(x, y) || _board[x, y] != 0) 
                return false;

            foreach (var (moveX, moveY) in _neighbors)
            {
                if (!OutsideBounds(x + moveX, y + moveY) && _board[x + moveX, y + moveY] != 0)
                    return false;
            }
        }
        
        return true;
    }

    private bool OutsideBounds(int x, int y) =>
        x is < 0 or >= Constants.BoardSize || y is < 0 or >= Constants.BoardSize;

    private int[,] CopyBoard()
    {
        var rows = _board.GetLength(0);
        var columns = _board.GetLength(1);

        var result = new int[rows, columns];

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
                result[i, j] = _board[i, j];
        }

        return result;
    }
}