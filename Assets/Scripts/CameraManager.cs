using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [Header("Kameralarý Buraya Sürükle")]
    public GameObject vCam_TPS;
    public GameObject vCam_Aim;
    public GameObject vCam_TopDown;

    private bool isTopDownMode = false;

    void Start()
    {
        // Baþlangýçta sadece TPS aktif olsun
        SetCamera(vCam_TPS);
        Cursor.lockState = CursorLockMode.Locked; //sinir bozucuydu ekledim valla 
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. Sað týk BASILI TUTULDUÐUNDA: Direkt Aim kamerasýna geç
        if (Mouse.current.rightButton.isPressed)
        {
            SetCamera(vCam_Aim);
            return; // Aim aktifken diðer tuþ kontrollerini atla
        }

        // 2. Sað týk BIRAKILDIÐI AN: Hangi kamerada olursan ol TPS'e dön
        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            isTopDownMode = false; // TopDown durumunu da sýfýrla
            SetCamera(vCam_TPS);
        }

        // 3. X TUÞUNA BASILDIÐINDA: TPS ve TopDown arasý geçiþ yap (Toggle)
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            isTopDownMode = !isTopDownMode;
            SetCamera(isTopDownMode ? vCam_TopDown : vCam_TPS);
        }
    }

    // Seçilen kamerayý açýp diðerlerini kapatan yardýmcý fonksiyon
    void SetCamera(GameObject activeCam)
    {
        vCam_TPS.SetActive(activeCam == vCam_TPS);
        vCam_Aim.SetActive(activeCam == vCam_Aim);
        vCam_TopDown.SetActive(activeCam == vCam_TopDown);
    }
}