using UnityEngine;

namespace PanicLab.Player
{
    public class ChemicalHolder : MonoBehaviour
    {
        public static ChemicalHolder Instance;

        [Header("Held Substance")]
        [SerializeField] private ElementData currentElement;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void SetChemical(ElementData data)
        {
            currentElement = data;
            Debug.Log($"<color=cyan>Player is now holding:</color> {data.elementName} ({data.symbol})");
            // You can add logic here to update UI or player model visuals
        }

        public ElementData GetCurrentChemical()
        {
            return currentElement;
        }

        public bool HasChemical()
        {
            return currentElement != null;
        }

        public void ClearChemical()
        {
            currentElement = null;
            Debug.Log("Chemical holder cleared.");
        }
    }
}
