using UnityEngine;
using UnityEngine.AI;

public class DusmanYapayZeka : MonoBehaviour
{
    public Transform oyuncu;
    private NavMeshAgent ajan;
    private Animator anim;
    
    // Boyutlar devasa olduğu için güvenli bir saldırı mesafesi (Collider'lar çarpmadan az önce)
    public float saldiriMesafesi = 115f; 
    
    // Animasyonun her kare tetiklenmesini engelleyecek sihirli kilit
    private bool zatenSaldirdiMi = false; 

    void Start()
    {
        ajan = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // EĞER ADAM ZATEN SALDIRIYORSA UPDATE İÇİNDEKİ HİÇBİR KODU ÇALIŞTIRMA (KİLİT)
        if (zatenSaldirdiMi) return;

        // Hedefe doğru yürü
        ajan.SetDestination(oyuncu.position);

        // Mesafeyi ölç
        float aradakiMesafe = Vector3.Distance(transform.position, oyuncu.position);

        if (aradakiMesafe <= saldiriMesafesi)
        {
            SaldiriSisteminiTetikle();
        }
        else
        {
            ajan.isStopped = false;
            anim.SetBool("Yuruyor", true);
        }
    }

    void SaldiriSisteminiTetikle()
    {
        zatenSaldirdiMi = true; // Kilidi kapatıyoruz, artık Update burayı rahatsız edemez!
        
        ajan.isStopped = true; // Yürümeyi durdur
        anim.SetBool("Yuruyor", false);
        
        anim.SetTrigger("Saldir"); // Saldırı animasyonunu SADECE 1 KERE tetikle
        
        // NOT: Oyun bitmeyecekse, adamın 3 saniye sonra tekrar yürümesi için testi sıfırlayan kod:
        // Invoke("SaldriyiSifirla", 3f);
    }

    void SaldriyiSifirla()
    {
        zatenSaldirdiMi = false;
    }
}