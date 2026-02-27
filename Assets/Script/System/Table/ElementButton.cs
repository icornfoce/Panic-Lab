using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ElementButton : MonoBehaviour {
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

    public void OnClick() {
        UIManager.Instance.ShowDetails(data);
    }
}