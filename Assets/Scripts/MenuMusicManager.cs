using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    private static MenuMusicManager instance;

    void Awake()
    {
        // Eğer sahnede zaten bir müzik çalar varsa, yenisini anında yok et (Üst üste çalmayı engeller)
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Yoksa, bunu ana müzik çalar yap ve sahneler arası silinmesini engelle
        instance = this;
        DontDestroyOnLoad(gameObject);

        // --- İLK AÇILIŞTA SESİ HAFIZADAN YÜKLEME ---
        // Oyun ilk açıldığında hafızadaki ses değerini çekiyoruz
        float kayitliSes = SettingsSaveManager.SesOku();
        
        // Oyunun ana ses seviyesini, logaritmik düzeltmeyle bu değere eşitleyerek müziği kısıyoruz
        AudioListener.volume = kayitliSes * kayitliSes;
    }
}