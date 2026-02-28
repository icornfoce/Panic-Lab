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

    [Header("Hover Settings")]
    public float hoverScale = 1.15f;
    public float smoothSpeed = 15f;

    private Vector3 _targetScale = Vector3.one;

    void Start() {
        if (data != null) UpdateUI();
        _targetScale = Vector3.one;
    }

    void Update() {
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.deltaTime * smoothSpeed);
    }

    public void UpdateUI() {
        symbolText.text = data.symbol;
        symbolText.color = data.symbolColor;
        numberText.text = data.atomicNumber.ToString();
        background.color = data.categoryColor;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _targetScale = Vector3.one * hoverScale;
        if (UIManager.Instance != null && data != null) {
            UIManager.Instance.ShowDetails(data);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        _targetScale = Vector3.one;
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