using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeController_Advanced : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI timeText; 
    public RawImage rotateImage;      // รูปที่หมุน 360 องศา
    public RawImage shakeImage;       // รูปที่สั่น (โชว์ตลอด)

    [Header("Settings")]
    public float timeRemaining = 60f;
    private float totalTime;
    private bool isTimerRunning = true;

    [Header("Shake Settings")]
    public float shakeIntensity = 5f;
    public float shakeSpeed = 50f;
    private Vector3 originalShakePosition;

    [Header("Flash Settings (Last 10s)")]
    public Color normalColor = Color.white;
    public Color alertColor = Color.red;
    public float flashSpeed = 5f; // ความเร็วในการกะพริบ

    void Start()
    {
        totalTime = timeRemaining;
        if (shakeImage != null)
            originalShakePosition = shakeImage.rectTransform.localPosition;
        
        if (timeText != null)
            timeText.color = normalColor;
    }

    void Update()
    {
        if (isTimerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateUI();

            // 1. การหมุนของ rotateImage
            float progress = 1 - (timeRemaining / totalTime);
            float currentRotation = progress * 360f;
            if (rotateImage != null)
                rotateImage.rectTransform.localRotation = Quaternion.Euler(0, 0, -currentRotation);

            // 2. เอฟเฟกต์เมื่อเหลือ 10 วินาทีสุดท้าย
            if (timeRemaining <= 10f)
            {
                ApplyShake();
                ApplyTextFlash();
            }
        }
        else if (timeRemaining <= 0)
        {
            OnTimeOut();
        }
    }

    void UpdateUI()
    {
        if (timeText != null)
            timeText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }

    void ApplyShake()
    {
        if (shakeImage != null)
        {
            float offsetX = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
            float offsetY = Mathf.Cos(Time.time * shakeSpeed) * shakeIntensity;
            shakeImage.rectTransform.localPosition = originalShakePosition + new Vector3(offsetX, offsetY, 0);
        }
    }

    void ApplyTextFlash()
    {
        if (timeText != null)
        {
            // ใช้ Mathf.PingPong เพื่อสร้างค่าที่แกว่งไปมาระหว่าง 0 กับ 1
            float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
            timeText.color = Color.Lerp(normalColor, alertColor, t);
        }
    }

    void OnTimeOut()
    {
        isTimerRunning = false;
        timeRemaining = 0;
        
        if (timeText != null)
        {
            timeText.text = "0";
            timeText.color = alertColor; // หยุดที่สีแดง
        }

        if (rotateImage != null)
            rotateImage.rectTransform.localRotation = Quaternion.Euler(0, 0, -360f);

        if (shakeImage != null)
            shakeImage.rectTransform.localPosition = originalShakePosition;

        Debug.Log("Time's Up!");
    }
}