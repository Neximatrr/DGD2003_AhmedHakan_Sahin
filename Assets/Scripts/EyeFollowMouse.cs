using UnityEngine;

public class EyeFollowMouse : MonoBehaviour
{
    public Camera mainCamera;
    public float targetDistance = 1000f;

    void Update()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null)
            return;

        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = mainCamera.ScreenPointToRay(screenCenter);

        Vector3 targetPoint = ray.origin + ray.direction * targetDistance;
        Vector3 direction = targetPoint - transform.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = targetRotation;
        }
    }
}