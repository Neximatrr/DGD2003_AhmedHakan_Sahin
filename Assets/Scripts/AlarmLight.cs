using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    private Light alarmIsigi;
    public float minSiddet = 0f;
    public float maxSiddet = 5000f; 
    public float yanipSonmeHizi = 2f; 

    void Start()
    {
        
        alarmIsigi = GetComponent<Light>(); 
    }

    void Update()
    {
        
        alarmIsigi.intensity = Mathf.Lerp(minSiddet, maxSiddet, Mathf.PingPong(Time.time * yanipSonmeHizi, 1f));
    }
}