using UnityEngine;
using UnityEngine.AddressableAssets; 
using System.Collections.Generic; // Listeleri kullanabilmek için ekledik

public class DosyaSpawner : MonoBehaviour
{
    [Header("Addressables Ayarları")]
    public AssetReference dosyaPrefabReferansi; 

    [Header("Spawn Noktaları")]
    public Transform[] spawnNoktalari; 
    
    [Header("Kaç Dosya Çıkacak?")]
    public int cikacakDosyaSayisi = 5; // GameManager'daki toplamDosya sayısıyla aynı olmalı

    void Start()
    {
        if (spawnNoktalari.Length == 0)
        {
            Debug.LogError("HATA: DosyaSpawner içine hiç spawn noktası eklenmemiş!");
            return;
        }

        DosyalariRastgeleYarat();
    }

    void DosyalariRastgeleYarat()
    {
        // 24 noktanın hepsini bir listeye kopyalıyoruz
        List<Transform> musaitNoktalar = new List<Transform>(spawnNoktalari);
        
        // Eğer yanlışlıkla 5'ten az nokta eklendiyse oyun çökmesin diye güvenlik önlemi
        int uretilecekSayi = Mathf.Min(cikacakDosyaSayisi, musaitNoktalar.Count);

        for (int i = 0; i < uretilecekSayi; i++)
        {
            // Kalan boş noktalar arasından RASTGELE birini seç
            int rastgeleIndex = Random.Range(0, musaitNoktalar.Count);
            Transform secilenNokta = musaitNoktalar[rastgeleIndex];

            // Kitabı seçilen o noktaya Addressables ile çağır
            Addressables.InstantiateAsync(dosyaPrefabReferansi, secilenNokta.position, secilenNokta.rotation);

            // Aynı noktaya üst üste 2 kitap çıkmasın diye, dolu noktayı listeden sil
            musaitNoktalar.RemoveAt(rastgeleIndex);
        }
        
        Debug.Log("Addressables: " + uretilecekSayi + " adet proje dosyası RASTGELE noktalara yüklendi.");
    }
}