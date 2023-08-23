using System.Text.RegularExpressions;
using Battleships.IO;
using Battleships.Opponents;
using Battleships.UI.Components;

namespace Battleships.Game;

internal partial class GameController
{
    private const string ExitCommand = "EXIT";
    
    private readonly IOpponent _opponent;
    private readonly IComponent _layout;
    private readonly IInputDevice _inputDevice;
    private readonly IEventHandlers _eventHandlers;

    [GeneratedRegex("^[A-J]([1-9]|10)$", RegexOptions.IgnoreCase)]
    private static partial Regex MyRegex();
    
    private int[,] _board = new int[0, 0];
    private int _occupiedFields;
    
    public GameController(
        IOpponent opponent, 
        IComponent layout, 
        IInputDevice inputDevice, 
        IEventHandlers eventHandlers)
    {
        _opponent = opponent;
        _layout = layout;
        _inputDevice = inputDevice;
        _eventHandlers = eventHandlers;
    }
    
    public void Play()
    {
        _board = _opponent.PlaceShips();
        _occupiedFields = CountOccupiedFields();
        
        _layout.Display();

        var hits = 0;
        
        while (true)
        {
            var command = _inputDevice.GetInput().ToUpper().Trim();
            
            if (command == ExitCommand)
                return;
            if (!IsCommandValid(command))
                _eventHandlers.OnInvalidTargetChosen();
            else
            {
                var (left, top) = Translate(command);
                var isHit = _opponent.TellHit(left, top);

                if (isHit)
                    hits++;
                
                _eventHandlers.OnValidTargetChosen(new(isHit, left, top));

                if (hits != _occupiedFields) 
                    continue;
                
                _eventHandlers.OnGameCompleted();

                return;
            }
        }
    }

    /// <summary>
    /// For the base case of a 10x10 board the below regex will do, but should the requirements change,
    /// say, to allow the user to change the board size, also the logic here should change.
    /// I guess that would be a subject of review - should we still use a regexp like:
    /// $"^[A-{(char)(65 + maxColumns - 1)}]{{1,{maxColumns}}}[1-{boardSize}]{{1,2}}$"; - btw. I haven't tested if it works
    /// Or should we rather just try to parse the string parts and validate it with a series of ifs. 
    /// </summary>
    private bool IsCommandValid(string input) =>
        MyRegex().IsMatch(input);

    private int CountOccupiedFields()
    {
        var rows = _board.GetLength(0);
        var columns = _board.GetLength(1);
        var occupiedCount = 0;
        
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                if (_board[i, j] != 0)
                    occupiedCount++;
            }
        }

        return occupiedCount;
    }
    
    private static (int Left, int Top) Translate(string input)
    {
        var colName = input[0];
        var col = colName - Constants.FirstCharacterCode;
        var row = int.Parse(input[1..]) - 1;

        return (col, row);
    }
}