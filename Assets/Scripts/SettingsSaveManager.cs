using UnityEngine;

public static class SettingsSaveManager
{
    // Ses değerini kaydet
    public static void KaydetSes(float volume)
    {
        PlayerPrefs.SetFloat("AyarlanmisSes", volume);
        PlayerPrefs.Save();
    }

    // Ses değerini oku (Kayıt yoksa varsayılan 1f yani tam ses döner)
    public static float SesOku()
    {
        return PlayerPrefs.GetFloat("AyarlanmisSes", 1f);
    }

    // Tam ekran durumunu kaydet
    public static void KaydetTamEkran(bool isFullscreen)
    {
        PlayerPrefs.SetInt("AyarlanmisTamEkran", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Tam ekran durumunu oku (Kayıt yoksa varsayılan true yani tam ekran döner)
    public static bool TamEkranOku()
    {
        int kayit = PlayerPrefs.GetInt("AyarlanmisTamEkran", 1);
        return kayit == 1;
    }
}