using UnityEngine;

public static class SettingsSaveManager
{
    
    public static void KaydetSes(float volume)
    {
        PlayerPrefs.SetFloat("AyarlanmisSes", volume);
        PlayerPrefs.Save();
    }

    
    public static float SesOku()
    {
        return PlayerPrefs.GetFloat("AyarlanmisSes", 1f);
    }

    
    public static void KaydetTamEkran(bool isFullscreen)
    {
        PlayerPrefs.SetInt("AyarlanmisTamEkran", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    
    public static bool TamEkranOku()
    {
        int kayit = PlayerPrefs.GetInt("AyarlanmisTamEkran", 1);
        return kayit == 1;
    }
}