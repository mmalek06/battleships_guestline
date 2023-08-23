using Battleships.Game;
using Battleships.UI;
using Battleships.UI.Components.Board;
using Battleships.UI.Components.Prompts;

namespace Battleships.UnitTests;

public class EventHandlersTests
{
    private readonly Mock<ILayout> _layoutMock;
    private readonly Mock<IBoardComponent> _boardMock;
    private readonly Mock<IPromptsComponent> _promptsMock;
    private readonly EventHandlers _handler;

    public EventHandlersTests()
    {
        _layoutMock = new();
        _boardMock = new();
        _promptsMock = new();
        _handler = new(_layoutMock.Object, _boardMock.Object, _promptsMock.Object);
    }

    [Fact]
    public void OnValidTargetChosen_ShouldCallExpectedMethods()
    {
        var testData = new MarkData();

        _handler.OnValidTargetChosen(testData);

        _layoutMock.Verify(m => m.Focus(_boardMock.Object), Times.Once());
        _boardMock.Verify(m => m.Mark(testData), Times.Once());
        _layoutMock.Verify(m => m.Focus(_promptsMock.Object), Times.Once());
        _promptsMock.Verify(m => m.Display(), Times.Once());
        _promptsMock.Verify(m => m.ClearInput(), Times.Once());
    }

    [Fact]
    public void OnInvalidTargetChosen_ShouldCallExpectedMethods()
    {
        _handler.OnInvalidTargetChosen();

        _layoutMock.Verify(m => m.Focus(_promptsMock.Object), Times.Once());
        _promptsMock.Verify(m => m.Warn(), Times.Once());
        _promptsMock.Verify(m => m.ClearInput(), Times.Once());
    }

    [Fact]
    public void OnGameCompleted_ShouldCallExpectedMethods()
    {
        _handler.OnGameCompleted();

        _layoutMock.Verify(m => m.Focus(_promptsMock.Object), Times.Once());
        _promptsMock.Verify(m => m.Finish(), Times.Once());
        _promptsMock.Verify(m => m.ClearInput(), Times.Once());
    }
}