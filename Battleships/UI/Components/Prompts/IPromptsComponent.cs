namespace Battleships.UI.Components.Prompts;

internal interface IPromptsComponent : IComponent
{
    void Warn();

    void Finish();

    void ClearInput();
}
