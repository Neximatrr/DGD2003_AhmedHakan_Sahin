using UnityEngine;

public class ProjeDosyasi : MonoBehaviour
{
    public void Topla()
    {
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DosyaToplandi();
        }

        
        Destroy(gameObject); 
    }
}