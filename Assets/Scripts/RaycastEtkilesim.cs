using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class RaycastEtkilesim : MonoBehaviour
{
    [Header("Ayarlar")]
    public float mesafe = 2500f;
    public Color lazerRengi = Color.red;

    // Unity Event Tanýmlama (Inspector'da kutu olarak gözükecek)
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
                // Rastgele renk oluþturuluyor
                Color rastgeleRenk = new Color(Random.value, Random.value, Random.value);

                // YENÝ TESÝSAT: Shader Graph'teki "_BaseColor" kutusuna rengi yolluyoruz!
                carpanObje.collider.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", rastgeleRenk);

                // EVENT TETÝKLEME (Haber salýyoruz!)
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