namespace PanicLab.Player
{
    public interface IInteractable
    {
        string InteractionPrompt { get; }
        void Interact();
    }
}
