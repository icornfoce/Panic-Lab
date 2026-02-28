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
        Vector2 targetPos = mousePosition + panelOffset;

        // Clamp to screen boundaries
        float panelWidth = _panelRectTransform.rect.width * _panelRectTransform.lossyScale.x;
        float panelHeight = _panelRectTransform.rect.height * _panelRectTransform.lossyScale.y;

        // Calculate screen limits based on pivot (assuming pivot is usually middle or top-left)
        // Here we'll just use the screen width/height and panel dimensions
        float minX = panelWidth * _panelRectTransform.pivot.x;
        float maxX = Screen.width - (panelWidth * (1 - _panelRectTransform.pivot.x));
        float minY = panelHeight * _panelRectTransform.pivot.y;
        float maxY = Screen.height - (panelHeight * (1 - _panelRectTransform.pivot.y));

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        _panelRectTransform.position = targetPos;
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
