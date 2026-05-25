using UnityEngine;
using UnityEngine.InputSystem;

public class OyunKilidi : MonoBehaviour
{
    
    public MonoBehaviour bakisKodu;

    void Update()
    {
        
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (bakisKodu != null) bakisKodu.enabled = false;
        }

        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (bakisKodu != null) bakisKodu.enabled = true;
        }
    }
}