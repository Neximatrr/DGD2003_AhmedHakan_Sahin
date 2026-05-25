using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.VFX; 

public class RaycastEtkilesim : MonoBehaviour
{
    [Header("Lazer Ayarlar�")]
    public float mesafe = 2500f;

    [Header("Efekt Ayarlar�")]
    public VisualEffect kivilcimEfekti; 
    [Range(0.1f, 1.0f)]
    public float efektDisariItme = 0.4f; 

    [Header("Olaylar")]
    public UnityEvent onKupVuruldu;

    void Update()
    {
        
        Ray isin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit carpanObje;

        if (Physics.Raycast(isin, out carpanObje, mesafe))
        {
            Debug.DrawRay(isin.origin, isin.direction * carpanObje.distance, Color.red);

            
            if (carpanObje.collider.CompareTag("Etkilesim") && Keyboard.current.fKey.wasPressedThisFrame)
            {
                
                Color rastgeleRenk = new Color(Random.value, Random.value, Random.value);
                Renderer rend = carpanObje.collider.GetComponent<Renderer>();

                if (rend != null)
                {
                    
                    rend.material.SetColor("_BaseColor", rastgeleRenk);
                }

                
                if (kivilcimEfekti != null)
                {
                    
                    kivilcimEfekti.transform.position = carpanObje.point + (carpanObje.normal * efektDisariItme);

                    
                    kivilcimEfekti.Reinit();
                    kivilcimEfekti.Play();
                }

                
                if (onKupVuruldu != null)
                {
                    onKupVuruldu.Invoke();
                }

                Debug.Log("<color=cyan>Sistem:</color> Renk de�i�ti, k�v�lc�m tam noktada patlad�!");
            }
        }
        else
        {
            Debug.DrawRay(isin.origin, isin.direction * mesafe, Color.white);
        }
    }
}