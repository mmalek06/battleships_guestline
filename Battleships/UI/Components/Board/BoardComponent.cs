using Battleships.IO;

namespace Battleships.UI.Components.Board;

internal class BoardComponent : IBoardComponent
{
    public int Height => Constants.BoardSize + HorizontalBorderHeight + HorizontalBorderHeight + ColumnNamesHeight;
    
    private const int ColumnNamesHeight = 1;
    private const int HorizontalBorderHeight = 1;
    private const int VerticalLineWidth = 1;
    
    private readonly IOutputDevice _outputDevice;
    private readonly int _offsetLeft;

    public BoardComponent(IOutputDevice outputDevice)
    {
        _outputDevice = outputDevice;
        _offsetLeft = Constants.BoardSize.ToString().Length + VerticalLineWidth + 1; // +1 for 1 space margin
    }

    public void Display()
    {
        _outputDevice
            .DrawAt(GetColumnNamesLine(), left: _offsetLeft + VerticalLineWidth)
            .DrawAt(GetHorizontalBorderLine(), left: _offsetLeft, top: ColumnNamesHeight)
            .DrawLines(GetGridInnerLine(), Constants.BoardSize, left: _offsetLeft, top: ColumnNamesHeight + HorizontalBorderHeight)
            .DrawAt(GetHorizontalBorderLine(), left: _offsetLeft, top: Constants.BoardSize + ColumnNamesHeight + HorizontalBorderHeight);
        DrawRowNames();
    }

    public void Mark(MarkData markData)
    {
        var (isHit, left, top) = markData;
        var topWithOffset = top + ColumnNamesHeight + HorizontalBorderHeight;
        var leftWithOffset = left + _offsetLeft + VerticalLineWidth;
        var marker = isHit ? Constants.HitMark : Constants.MissMark;
        
        _outputDevice.DrawAt(marker, leftWithOffset, topWithOffset);
    }

    private string GetColumnNamesLine() =>
        string.Join(string.Empty, Enumerable.Range(0, Constants.BoardSize).Select(n => (char)(Constants.FirstCharacterCode + n)));
    
    private string GetHorizontalBorderLine() =>
        new('#', Constants.BoardSize + 2 * VerticalLineWidth);

    private string GetGridInnerLine() =>
        $"#{new string(' ', Constants.BoardSize)}#";
    
    private void DrawRowNames()
    {
        const int left = 0;
        const int topOccupiedSpace = ColumnNamesHeight + HorizontalBorderHeight;
        
        for (var row = 0; row < Constants.BoardSize; row++)
        {
            var contents = (row + 1).ToString();

            _outputDevice.DrawAt(contents, left, row + topOccupiedSpace);
        }
    }
}