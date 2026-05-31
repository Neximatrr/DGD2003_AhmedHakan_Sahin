using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MainMenu : MonoBehaviour
{
    
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    void Start()
    {
        
        if (volumeSlider != null && fullscreenToggle != null)
        {
            AyarlariYukle();
        }
    }

    public void PlayGame()
    {
        
        GameObject musicObj = GameObject.Find("MenuMusic");
        if (musicObj != null)
        {
            Destroy(musicObj);
        }
        
        
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
        SettingsSaveManager.KaydetTamEkran(isFullscreen); 
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume * volume; 
        SettingsSaveManager.KaydetSes(volume);  
    }

    
    private void AyarlariYukle()
    {
        float kayitliSes = SettingsSaveManager.SesOku();
        bool kayitliTamEkran = SettingsSaveManager.TamEkranOku();

        
        volumeSlider.value = kayitliSes;
        fullscreenToggle.isOn = kayitliTamEkran;

        
        AudioListener.volume = kayitliSes * kayitliSes;
        Screen.fullScreen = kayitliTamEkran;
    }
}