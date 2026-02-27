using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public static UIManager Instance; // ทำให้ Script อื่นเรียกใช้ง่ายๆ

    [Header("Detail Panel UI")]
    public GameObject detailPanel; // หน้าต่าง Pop-up
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI symbolText;
    public TextMeshProUGUI infoText;

    void Awake() 
    {
        // สร้าง Singleton เพื่อให้เรียกใช้ผ่าน UIManager.Instance ได้เลย
        if (Instance == null) Instance = this; 
        else Destroy(gameObject);

        if(detailPanel != null) detailPanel.SetActive(false); 
    }

    public void ShowDetails(ElementData data) 
    {
        if(detailPanel == null) return;

        detailPanel.SetActive(true);
        nameText.text = data.elementName;
        symbolText.text = data.symbol;
        infoText.text = $"Atomic Number: {data.atomicNumber}\n" +
                        $"Atomic Mass: {data.atomicMass}\n\n" +
                        $"{data.description}";
    }

    public void ClosePanel() 
    {
        detailPanel.SetActive(false);
    }
}