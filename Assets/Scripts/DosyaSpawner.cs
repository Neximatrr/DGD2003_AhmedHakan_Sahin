using UnityEngine;
using UnityEngine.AddressableAssets; 
using System.Collections.Generic; 

public class DosyaSpawner : MonoBehaviour
{
    [Header("Addressables Ayarları")]
    public AssetReference dosyaPrefabReferansi; 

    [Header("Spawn Noktaları")]
    public Transform[] spawnNoktalari; 
    
    [Header("Kaç Dosya Çıkacak?")]
    public int cikacakDosyaSayisi = 5; 

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
        
        List<Transform> musaitNoktalar = new List<Transform>(spawnNoktalari);
        
        
        int uretilecekSayi = Mathf.Min(cikacakDosyaSayisi, musaitNoktalar.Count);

        for (int i = 0; i < uretilecekSayi; i++)
        {
            
            int rastgeleIndex = Random.Range(0, musaitNoktalar.Count);
            Transform secilenNokta = musaitNoktalar[rastgeleIndex];

            
            Addressables.InstantiateAsync(dosyaPrefabReferansi, secilenNokta.position, secilenNokta.rotation);

            
            musaitNoktalar.RemoveAt(rastgeleIndex);
        }
        
        Debug.Log("Addressables: " + uretilecekSayi + " adet proje dosyası RASTGELE noktalara yüklendi.");
    }
}