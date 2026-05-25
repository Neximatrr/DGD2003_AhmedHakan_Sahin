using UnityEngine;

public class ProjeDosyasi : MonoBehaviour
{
    public void Topla()
    {
        // GameManager'a ulaşıp skoru artırıyoruz
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DosyaToplandi();
        }

        // Objeyi sahneden sil
        Destroy(gameObject); 
    }
}