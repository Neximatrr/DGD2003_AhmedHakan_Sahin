using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Skor Ayarları")]
    public int toplananDosya = 0;
    public int toplamDosya = 5;

    [Header("UI Ayarları")]
    public TextMeshProUGUI skorText; 
    public GameObject winPanel;  
    public GameObject losePanel; 
    public GameObject crosshair; 

    [Header("Karakter Ayarı")]
    public MonoBehaviour playerMovementScript; 
    public MonoBehaviour mouseLookScript; // YENİ: Kamerayı döndüren o sinsi scripti buraya bağlayacağız

    private bool isGameActive = true;

    void Awake()
    {
        Instance = this; 
    }

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
        if (crosshair != null) crosshair.SetActive(true);
        
        Time.timeScale = 1f;
        AudioListener.pause = false; 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UIGuncelle();
    }

    void LateUpdate()
    {
        if (!isGameActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void DosyaToplandi()
    {
        if (!isGameActive) return;

        toplananDosya++;
        UIGuncelle();

        if (toplananDosya >= toplamDosya)
        {
            WinGame();
        }
    }

    public void PlayerCaught()
    {
        if (!isGameActive) return;
        isGameActive = false;
        
        OyuncuyuFelcEt(); 
        
        StartCoroutine(LoseRoutine());
    }

    IEnumerator LoseRoutine()
    {
        yield return new WaitForSeconds(2.5f); 

        Time.timeScale = 0f; 
        AudioListener.pause = true; 
        if (losePanel != null) losePanel.SetActive(true);
    }

    void WinGame()
    {
        isGameActive = false;
        Time.timeScale = 0f; 
        AudioListener.pause = true; 

        OyuncuyuFelcEt();

        if (winPanel != null) winPanel.SetActive(true);
    }

    void UIGuncelle()
    {
        if (skorText != null)
        {
            skorText.text = "Collected Project File: " + toplananDosya + " / " + toplamDosya;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        AudioListener.pause = false; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OyuncuyuFelcEt()
    {
        if (crosshair != null) crosshair.SetActive(false);

        PlayerInput[] pInputs = FindObjectsOfType<PlayerInput>();
        foreach (PlayerInput p in pInputs)
        {
            Destroy(p);
        }

        if (playerMovementScript != null) playerMovementScript.enabled = false;
        
        // MOUSE LOOK SCRİPTİNİ BURADA KAPATIYORUZ
        if (mouseLookScript != null) mouseLookScript.enabled = false;
    }
}