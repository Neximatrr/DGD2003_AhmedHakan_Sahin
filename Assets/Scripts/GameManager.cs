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

    public TextMeshProUGUI timerText;     // YENİ: Anlık akan süre yazısı

    public TextMeshProUGUI bestTimeText;  // YENİ: Rekor süre yazısı

    public GameObject winPanel;  

    public GameObject losePanel;

    public GameObject crosshair;



    [Header("Karakter Ayarı")]

    public MonoBehaviour playerMovementScript;

    public MonoBehaviour mouseLookScript;



    [Header("Süre Ayarları")]

    public float gecenSure = 0f;



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

        RekorUIYazdir(); // Oyun başlar başlamaz JSON'daki rekoru ekrana bas

    }



    void Update()

    {

        // Zamanı anlık olarak say ve ekrana yaz

        if (isGameActive)

        {

            gecenSure += Time.deltaTime;

            ZamanUIYazdir();

        }

    }



    void LateUpdate()

    {

        if (!isGameActive)

        {

            Cursor.lockState = CursorLockMode.None;

            Cursor.visible = true;

        }

    }



    // SAYACI EKRANA YAZDIRMA KODU (00:00 FORMATINDA)

    void ZamanUIYazdir()

    {

        if (timerText != null)

        {

            float dakika = Mathf.FloorToInt(gecenSure / 60);

            float saniye = Mathf.FloorToInt(gecenSure % 60);

            timerText.text = string.Format("Time: {0:00}:{1:00}", dakika, saniye);

        }

    }



    // REKORU EKRANA YAZDIRMA KODU (JSON'DAN OKUYUP YAZAR)

    void RekorUIYazdir()

    {

        if (bestTimeText != null)

        {

            float rekor = JSONKayit.RekorOku();

            if (rekor > 0f)

            {

                float dakika = Mathf.FloorToInt(rekor / 60);

                float saniye = Mathf.FloorToInt(rekor % 60);

                bestTimeText.text = string.Format("Best Time: {0:00}:{1:00}", dakika, saniye);

            }

            else

            {

                bestTimeText.text = "Best Time: --:--"; // Hiç kazanılmadıysa böyle yazsın

            }

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



        // JSON KAYDINI TETİKLE (Yeni rekor mu diye kontrol edecek)

        JSONKayit.RekorKaydet(gecenSure);

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

        if (mouseLookScript != null) mouseLookScript.enabled = false;

    }

}