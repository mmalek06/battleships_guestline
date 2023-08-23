using Battleships.Opponents;
using FluentAssertions;

namespace Battleships.UnitTests;

/// <summary>
/// Game algorithms are notoriously hard to test and parts of the below code on their own are an
/// algorithm that could be unit tested. However, I didn't want to get too deep into the rabbit hole,
/// and so I thought that saying just that will suffice as an explanation.
/// </summary>
public class RandomPlacementOpponentTests
{
    private const int ExpectedOccupiedCellsCount = 4 + 4 + 5;
    
    [Fact]
    public void Placement_ShouldResultInNoOverlap()
    {
        var opponent = new RandomPlacementOpponent();
        var board = opponent.PlaceShips();
        var flatBoard = board.Cast<int>();
        var occupiedCellsCount = flatBoard.Count(cell => cell != 0);
        var occupiedFields = GetOccupiedFields(board);
        var measurements = MeasureNonOverlappingNonIntersectingLines(occupiedFields).Order().ToList();
        
        occupiedCellsCount.Should().Be(ExpectedOccupiedCellsCount);
        measurements.Should().BeEquivalentTo(new[] { 4, 4, 5 });
    }

    [Fact]
    public void HitDetection_ShouldReturnTrueForOccupiedCells()
    {
        var hitCounter = 0;
        var opponent = new RandomPlacementOpponent();
        
        opponent.PlaceShips();

        for (var i = 0; i < 10; i++)
        {
            for (var j = 0; j < 10; j++)
            {
                if (opponent.TellHit(i, j))
                    hitCounter++;
            }
        }

        hitCounter.Should().Be(ExpectedOccupiedCellsCount);
    }

    private static IReadOnlyCollection<(int y, int x)> GetOccupiedFields(int[,] board)
    {
        var allOccupiedFields = new HashSet<(int, int)>();

        for (var row = 0; row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)            
            {
                if (board[row, col] == 1)
                    allOccupiedFields.Add((row, col));
            }
        }

        return allOccupiedFields;
    }

    private static IEnumerable<int> MeasureNonOverlappingNonIntersectingLines(IEnumerable<(int, int)> ones)
    {
        var sortedTuples = ones.OrderBy(t => t.Item1).ThenBy(t => t.Item2).ToList();
        var visited = new HashSet<(int, int)>();
        var lineLengths = new List<int>();

        foreach (var tuple in sortedTuples)
        {
            if (visited.Contains(tuple)) 
                continue;

            var length = 0;
            var current = tuple;
            
            while (sortedTuples.Contains(current))
            {
                visited.Add(current);
                
                length++;
                current = (current.Item1, current.Item2 + 1);
            }

            if (length > 1)
                lineLengths.Add(length);

            length = 0;
            current = tuple;
            
            while (sortedTuples.Contains(current))
            {
                visited.Add(current);
                
                length++;
                current = (current.Item1 + 1, current.Item2);
            }

            if (length > 1)
                lineLengths.Add(length);
        }

        return lineLengths;
    }
}