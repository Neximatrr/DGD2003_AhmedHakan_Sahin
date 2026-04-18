using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events; // 1. BU SATIR ÞART (Event sistemi için)

public class RaycastEtkilesim : MonoBehaviour
{
    [Header("Ayarlar")]
    public float mesafe = 2500f;
    public Color lazerRengi = Color.red;

    // 2. Unity Event Tanýmlama (Inspector'da kutu olarak gözükecek)
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
                // Mevcut Renk Deðiþtirme Kodun
                Color rastgeleRenk = new Color(Random.value, Random.value, Random.value);
                carpanObje.collider.GetComponent<MeshRenderer>().material.color = rastgeleRenk;

                // 3. EVENT TETÝKLEME (Haber salýyoruz!)
                if (onKupVuruldu != null)
                {
                    onKupVuruldu.Invoke();
                }

                Debug.Log("Obje Rengi Deðiþti ve Haber Verildi!");
            }
        }
        else
        {
            Debug.DrawRay(isin.origin, isin.direction * mesafe, Color.white);
        }
    }
}