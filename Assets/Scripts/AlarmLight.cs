using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    private Light alarmIsigi;
    public float minSiddet = 0f;
    public float maxSiddet = 5000f; // Buraya Inspector'daki kendi değerini girebilirsin
    public float yanipSonmeHizi = 2f; // Hızı buradan ayarlayacağız

    void Start()
    {
        // Objedeki Light bileşenini otomatik bulur
        alarmIsigi = GetComponent<Light>(); 
    }

    void Update()
    {
        // Işığın şiddetini yumuşak bir şekilde artırıp azaltır
        alarmIsigi.intensity = Mathf.Lerp(minSiddet, maxSiddet, Mathf.PingPong(Time.time * yanipSonmeHizi, 1f));
    }
}