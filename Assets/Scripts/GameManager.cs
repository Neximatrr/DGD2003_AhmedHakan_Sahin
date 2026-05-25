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

    public TextMeshProUGUI timerText;     

    public TextMeshProUGUI bestTimeText;  

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

        RekorUIYazdir(); 

    }



    void Update()

    {

        

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



    

    void ZamanUIYazdir()

    {

        if (timerText != null)

        {

            float dakika = Mathf.FloorToInt(gecenSure / 60);

            float saniye = Mathf.FloorToInt(gecenSure % 60);

            timerText.text = string.Format("Time: {0:00}:{1:00}", dakika, saniye);

        }

    }



    

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

                bestTimeText.text = "Best Time: --:--"; 

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