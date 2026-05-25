using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [Header("Kameralar")]
    public GameObject vCam_TPS;
    public GameObject vCam_Aim;
    public GameObject vCam_TopDown;

    private bool isTopDownMode = false;

    void Start()
    {
        
        SetCamera(vCam_TPS);
        Cursor.lockState = CursorLockMode.Locked; //sinir bozucuydu ekledim valla 
        Cursor.visible = false;
    }

    void Update()
    {
        
        if (Mouse.current.rightButton.isPressed)
        {
            SetCamera(vCam_Aim);
            return; 
        }

        
        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            isTopDownMode = false; 
            SetCamera(vCam_TPS);
        }

        
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            isTopDownMode = !isTopDownMode;
            SetCamera(isTopDownMode ? vCam_TopDown : vCam_TPS);
        }




    }

    
    void SetCamera(GameObject activeCam)
    {
        vCam_TPS.SetActive(activeCam == vCam_TPS);
        vCam_Aim.SetActive(activeCam == vCam_Aim);
        vCam_TopDown.SetActive(activeCam == vCam_TopDown);
    }
}