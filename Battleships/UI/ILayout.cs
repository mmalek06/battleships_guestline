using Battleships.UI.Components;

namespace Battleships.UI;

internal interface ILayout : IComponent
{
    void Focus(IComponent component);
}