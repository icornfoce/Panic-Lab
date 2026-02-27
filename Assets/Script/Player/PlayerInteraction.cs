using UnityEngine;
using UnityEngine.InputSystem;

namespace PanicLab.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [SerializeField] private float interactionDistance = 3f;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private Transform cameraTransform;

        private void Update()
        {
            // Optional: Raycast every frame to detect objects for UI feedback
            CheckForInteractable();
        }

        public void OnInteract(InputValue value)
        {
            if (value.isPressed)
            {
                PerformInteraction();
            }
        }

        private void PerformInteraction()
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }

        private void CheckForInteractable()
        {
            // This can be used to show "Press F to Interct" UI
            // For now, we just perform the logic on press
        }
    }
}
