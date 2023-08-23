using Battleships.Game;
using Battleships.IO;
using Battleships.Opponents;
using Battleships.UI;
using Battleships.UI.Components.Board;

namespace Battleships.UnitTests;

public class GameControllerTests
{
    private const string ExitCommand = "EXIT";

    private readonly Mock<IOpponent> _opponentMock;
    private readonly Mock<ILayout> _layoutMock;
    private readonly Mock<IInputDevice> _inputMock;
    private readonly Mock<IEventHandlers> _eventHandlerMock;
    private readonly GameController _controller;

    public GameControllerTests()
    {
        _opponentMock = new();
        _layoutMock = new();
        _inputMock = new();
        _eventHandlerMock = new();
        _controller = new GameController(
            _opponentMock.Object, 
            _layoutMock.Object, 
            _inputMock.Object,
            _eventHandlerMock.Object);
    }

    [Fact]
    public void Play_ShouldExit_WhenExitCommandGiven()
    {
        _inputMock.SetupSequence(x => x.GetInput()).Returns(ExitCommand);
        _controller.Play();

        _eventHandlerMock.Verify(x => x.OnValidTargetChosen(It.IsAny<MarkData>()), Times.Never);
        _eventHandlerMock.Verify(x => x.OnInvalidTargetChosen(), Times.Never);
        _eventHandlerMock.Verify(x => x.OnGameCompleted(), Times.Never);
    }
    
    [Fact]
    public void Play_ShouldHandleInvalidTarget_WhenInvalidCommandGiven()
    {
        _inputMock.SetupSequence(x => x.GetInput())
            .Returns("INVALID")
            .Returns(ExitCommand);
        _controller.Play();

        _eventHandlerMock.Verify(x => x.OnInvalidTargetChosen(), Times.Once);
    }
    
    [Fact]
    public void Play_ShouldHandleHit_ThenExit()
    {
        _opponentMock.Setup(x => x.TellHit(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
        _inputMock.SetupSequence(x => x.GetInput())
            .Returns("A1")
            .Returns(ExitCommand);
        _controller.Play();

        _eventHandlerMock.Verify(x => x.OnValidTargetChosen(It.IsAny<MarkData>()), Times.Once);
    }
    
    [Fact]
    public void Play_ShouldEndGame_AfterAllFieldsHit()
    {
        int[,] mockBoard = {
            {1, 0},
            {1, 0}
        };

        _opponentMock.Setup(x => x.PlaceShips()).Returns(mockBoard);
        _opponentMock.Setup(x => x.TellHit(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
        _inputMock.SetupSequence(x => x.GetInput())
            .Returns("A1")
            .Returns("A2")
            .Returns(ExitCommand);
        _controller.Play();

        _eventHandlerMock.Verify(x => x.OnValidTargetChosen(It.IsAny<MarkData>()), Times.Exactly(2));
        _eventHandlerMock.Verify(x => x.OnGameCompleted(), Times.Once);
    }
}