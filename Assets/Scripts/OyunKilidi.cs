using UnityEngine;
using UnityEngine.InputSystem;

public class OyunKilidi : MonoBehaviour
{
    // Bakýþ kodunu buraya sürükleyeceðiz (Inspector'dan)
    public MonoBehaviour bakisKodu;

    void Update()
    {
        // 1. ESC'ye basýnca: Dünyayý durdur ve bakýþ kodunu kapat
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (bakisKodu != null) bakisKodu.enabled = false;
        }

        // 2. Sol Týka basýnca: Dünyayý baþlat ve bakýþ kodunu geri aç
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (bakisKodu != null) bakisKodu.enabled = true;
        }
    }
}