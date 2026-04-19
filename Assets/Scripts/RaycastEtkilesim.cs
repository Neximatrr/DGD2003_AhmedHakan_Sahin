using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.VFX; // VFX sistemi için þart

public class RaycastEtkilesim : MonoBehaviour
{
    [Header("Lazer Ayarlarý")]
    public float mesafe = 2500f;

    [Header("Efekt Ayarlarý")]
    public VisualEffect kivilcimEfekti; // Sahnede duran VFX'i buraya sürükle
    [Range(0.1f, 1.0f)]
    public float efektDisariItme = 0.4f; // Kývýlcýmlar kutunun içine hapsolmasýn diye

    [Header("Olaylar")]
    public UnityEvent onKupVuruldu;

    void Update()
    {
        // Ekran ortasýndan lazer fýrlat
        Ray isin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit carpanObje;

        if (Physics.Raycast(isin, out carpanObje, mesafe))
        {
            Debug.DrawRay(isin.origin, isin.direction * carpanObje.distance, Color.red);

            // "Etkilesim" tag'li objeye bakarken F'ye basarsak
            if (carpanObje.collider.CompareTag("Etkilesim") && Keyboard.current.fKey.wasPressedThisFrame)
            {
                // 1. NEON RENK DEÐÝÞTÝRME
                Color rastgeleRenk = new Color(Random.value, Random.value, Random.value);
                Renderer rend = carpanObje.collider.GetComponent<Renderer>();

                if (rend != null)
                {
                    // Shader Graph'teki "_BaseColor" kutusuna rengi gönderiyoruz
                    rend.material.SetColor("_BaseColor", rastgeleRenk);
                }

                // 2. KIVILCIM PATLATMA (Tek seferlik ve temiz)
                if (kivilcimEfekti != null)
                {
                    // Efekti lazerin çarptýðý yere taþý ve hafifçe dýþarý (bize doðru) it
                    kivilcimEfekti.transform.position = carpanObje.point + (carpanObje.normal * efektDisariItme);

                    // Önceki patlamadan kalanlarý temizle ve yeniden ateþle
                    kivilcimEfekti.Reinit();
                    kivilcimEfekti.Play();
                }

                // 3. EVENT TETÝKLEME (Hocanýn istediði bildirim sistemi)
                if (onKupVuruldu != null)
                {
                    onKupVuruldu.Invoke();
                }

                Debug.Log("<color=cyan>Sistem:</color> Renk deðiþti, kývýlcým tam noktada patladý!");
            }
        }
        else
        {
            Debug.DrawRay(isin.origin, isin.direction * mesafe, Color.white);
        }
    }
}