using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PanicLab.Player;

public class ElementButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public ElementData data;
    public TextMeshProUGUI symbolText;
    public TextMeshProUGUI numberText;
    public RawImage background;

    void Start() {
        if (data != null) UpdateUI();
    }

    public void UpdateUI() {
        symbolText.text = data.symbol;
        numberText.text = data.atomicNumber.ToString();
        background.color = data.categoryColor;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (UIManager.Instance != null && data != null) {
            UIManager.Instance.ShowDetails(data);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (UIManager.Instance != null) {
            UIManager.Instance.ClosePanel();
        }
    }

    public void OnClick() {
        if (ChemicalHolder.Instance != null && data != null) {
            ChemicalHolder.Instance.SetChemical(data);
        }
    }
}