using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public static UIManager Instance;

    [Header("Detail Panel UI")]
    public GameObject detailPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI symbolText;
    public TextMeshProUGUI infoText;

    [Header("Follow Mouse Settings")]
    [Tooltip("Positive X/Y moves to Top-Right. Negative moves to Bottom-Left.")]
    public Vector2 panelOffset = new Vector2(20f, -20f);

    private RectTransform _panelRectTransform;
    private CanvasGroup _canvasGroup;

    void Awake() 
    {
        if (Instance == null) Instance = this; 
        else Destroy(gameObject);

        if(detailPanel != null) {
            detailPanel.SetActive(false); 
            _panelRectTransform = detailPanel.GetComponent<RectTransform>();
            
            _canvasGroup = detailPanel.GetComponent<CanvasGroup>();
            if (_canvasGroup == null) {
                _canvasGroup = detailPanel.AddComponent<CanvasGroup>();
            }
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
    }

    private void LateUpdate()
    {
        if (detailPanel != null && detailPanel.activeSelf)
        {
            UpdatePanelPosition();
        }
    }

    private void UpdatePanelPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        _panelRectTransform.position = mousePosition + panelOffset;
    }

    public void ShowDetails(ElementData data) 
    {
        if(detailPanel == null) return;

        detailPanel.SetActive(true);
        UpdatePanelPosition();

        nameText.text = data.elementName;
        symbolText.text = data.symbol;
        infoText.text = $"Atomic Number: {data.atomicNumber}\n" +
                        $"Atomic Mass: {data.atomicMass}\n\n" +
                        $"{data.description}";
    }

    public void ClosePanel() 
    {
        if(detailPanel != null) detailPanel.SetActive(false);
    }
}
