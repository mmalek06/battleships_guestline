using Battleships.UI;
using Battleships.UI.Components.Board;
using Battleships.UI.Components.Prompts;

namespace Battleships.Game;

internal class EventHandlers : IEventHandlers
{
    private readonly ILayout _layout;
    private readonly IBoardComponent _board;
    private readonly IPromptsComponent _prompts;

    public EventHandlers(ILayout layout, IBoardComponent board, IPromptsComponent prompts)
    {
        _layout = layout;
        _board = board;
        _prompts = prompts;
    }

    public void OnValidTargetChosen(MarkData markData)
    {
        _layout.Focus(_board);
        _board.Mark(markData);
        _layout.Focus(_prompts);
        _prompts.Display();
        _prompts.ClearInput();
    }

    public void OnInvalidTargetChosen()
    {
        _layout.Focus(_prompts);
        _prompts.Warn();
        _prompts.ClearInput();
    }

    public void OnGameCompleted()
    {
        _layout.Focus(_prompts);
        _prompts.Finish();
        _prompts.ClearInput();
    }
}
    