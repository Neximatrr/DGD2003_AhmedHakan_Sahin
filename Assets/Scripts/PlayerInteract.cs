using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; 

public class PlayerInteract : MonoBehaviour
{
    [Header("Etkileşim Ayarları")]
    public float interactDistance = 800f; 
    public Camera playerCamera; 

    [Header("UI Ayarları")]
    public GameObject interactUI; 

    void Start()
    {
        if (interactUI != null) interactUI.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            ProjeDosyasi dosya = hit.collider.GetComponent<ProjeDosyasi>();

            if (dosya != null)
            {
                interactUI.SetActive(true);

                
                if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
                {
                    dosya.Topla(); 
                    interactUI.SetActive(false); 
                }
            }
            else
            {
                interactUI.SetActive(false);
            }
        }
        else
        {
            interactUI.SetActive(false);
        }
    }
}