using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Slider ve Toggle referansları için şart knk

public class MainMenu : MonoBehaviour
{
    // Unity editöründen Settings sahnesindeki Slider ve Toggle'ı buraya sürükleyeceksin
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    void Start()
    {
        // Eğer Settings sahnesindeysek eski ayarları kayıt scriptinden yükle
        if (volumeSlider != null && fullscreenToggle != null)
        {
            AyarlariYukle();
        }
    }

    public void PlayGame()
    {
        // Önce menü müziğini bul ve yok et
        GameObject musicObj = GameObject.Find("MenuMusic");
        if (musicObj != null)
        {
            Destroy(musicObj);
        }
        
        // Müzik temizlendikten sonra oyun sahnesine geç (Tek satır yeterli)
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SettingsSaveManager.KaydetTamEkran(isFullscreen); // Yeni scriptine kaydeder
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume * volume; // Logaritmik düzeltmen kalıyor
        SettingsSaveManager.KaydetSes(volume);  // Yeni scriptine kaydeder
    }

    // Hafızadaki ayarları çekip oyuna ve arayüze uygulayan fonksiyon
    private void AyarlariYukle()
    {
        float kayitliSes = SettingsSaveManager.SesOku();
        bool kayitliTamEkran = SettingsSaveManager.TamEkranOku();

        // Arayüzdeki elemanları güncelle
        volumeSlider.value = kayitliSes;
        fullscreenToggle.isOn = kayitliTamEkran;

        // Oyun motoruna işlet
        AudioListener.volume = kayitliSes * kayitliSes;
        Screen.fullScreen = kayitliTamEkran;
    }
}