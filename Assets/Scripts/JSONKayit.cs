using UnityEngine;
using System.IO;

public class JSONKayit : MonoBehaviour
{
    [System.Serializable]
    public class OyuncuVerisi
    {
        public float bitirmeSuresi;
        public string tarih;
    }

    public static void RekorKaydet(float gecenSure)
    {
        float eskiRekor = RekorOku();

        // Eğer daha önce hiç oyun kazanılmadıysa (0) VEYA yeni süre eskisinden daha kısaysa KAYDET
        if (eskiRekor == 0f || gecenSure < eskiRekor)
        {
            OyuncuVerisi veri = new OyuncuVerisi();
            veri.bitirmeSuresi = gecenSure;
            veri.tarih = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            string jsonMetni = JsonUtility.ToJson(veri, true);
            string dosyaYolu = Application.persistentDataPath + "/OyunRekoru.json";
            File.WriteAllText(dosyaYolu, jsonMetni);

            Debug.Log("YENİ REKOR JSON'A KAYDEDİLDİ: " + gecenSure);
        }
    }

    // Dosyadaki rekoru okuyup UI'a gönderecek fonksiyon
    public static float RekorOku()
    {
        string dosyaYolu = Application.persistentDataPath + "/OyunRekoru.json";
        if (File.Exists(dosyaYolu))
        {
            string jsonMetni = File.ReadAllText(dosyaYolu);
            OyuncuVerisi veri = JsonUtility.FromJson<OyuncuVerisi>(jsonMetni);
            return veri.bitirmeSuresi;
        }
        return 0f; // Eğer kayıt yoksa 0 döndür
    }
}