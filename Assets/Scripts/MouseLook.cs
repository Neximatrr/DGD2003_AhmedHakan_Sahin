using UnityEngine;
using UnityEngine.InputSystem; 

public class MouseLook : MonoBehaviour
{
  
    public float hassasiyet = 0.5f;
    private float xCubuguAcisi = 0f; 

    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
        float fareHareketiY = Mouse.current.delta.ReadValue().y;

       
        xCubuguAcisi -= fareHareketiY * hassasiyet;

      
        xCubuguAcisi = Mathf.Clamp(xCubuguAcisi, -80f, 80f);

       
        transform.localRotation = Quaternion.Euler(xCubuguAcisi, 0f, 0f);
    }
}