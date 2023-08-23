using Battleships.Game;
using Battleships.IO;
using Battleships.Opponents;
using Battleships.UI;
using Battleships.UI.Components;
using Battleships.UI.Components.Board;
using Battleships.UI.Components.Prompts;

var outputDevice = new ConsoleOutputDevice();
var inputDevice = new ConsoleInputDevice();
var description = new GameDescriptionComponent(outputDevice);
var board = new BoardComponent(outputDevice);
var prompts = new PromptsComponent(outputDevice);
var components = new IComponent[]
{
    description,
    board,
    prompts
};
var layout = new Layout(components, outputDevice);
var opponent = new RandomPlacementOpponent();
var eventHandlers = new EventHandlers(layout, board, prompts);
var game = new GameController(opponent, layout, inputDevice, eventHandlers);

game.Play();
