using UnityEngine;

namespace PanicLab.Player
{
    public class TestInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string prompt = "Press F to Test";

        public string InteractionPrompt => prompt;

        public void Interact()
        {
            Debug.Log("<color=green>Interaction Successful!</color> You interacted with " + gameObject.name);
        }
    }
}
