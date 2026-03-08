using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleFollowCamera : MonoBehaviour
{
    public Transform target;

    [Header("Large Scale Camera")]
    public float distance = 350f;
    public float heightOffset = 150f;

    [Header("Pitch")]
    public float mousePitchSpeed = 1.2f;
    public float minPitch = 10f;
    public float maxPitch = 35f;
    public float pitch = 20f;

    [Header("Smoothing")]
    public float positionSmooth = 10f;
    public float rotationSmooth = 10f;

    [Header("Collision")]
    public float sphereRadius = 20f;
    public float collisionBuffer = 15f;
    public LayerMask collisionLayers = ~0;
    public float minDistance = 120f;

    private float currentDistance;

    void Start()
    {
        currentDistance = distance;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (Mouse.current != null)
        {
            float mouseY = Mouse.current.delta.ReadValue().y;
            pitch -= mouseY * mousePitchSpeed * Time.deltaTime * 60f;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        Vector3 focusPoint = target.position + Vector3.up * heightOffset;

        Quaternion targetRotation = Quaternion.Euler(pitch, target.eulerAngles.y, 0f);
        Vector3 desiredDirection = targetRotation * Vector3.back;

        float targetDistance = distance;

        if (Physics.SphereCast(focusPoint, sphereRadius, desiredDirection, out RaycastHit hit, distance, collisionLayers))
        {
            targetDistance = hit.distance - collisionBuffer;
            if (targetDistance < minDistance)
                targetDistance = minDistance;
        }

        currentDistance = Mathf.Lerp(currentDistance, targetDistance, 8f * Time.deltaTime);

        Vector3 desiredPosition = focusPoint + desiredDirection * currentDistance;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            positionSmooth * Time.deltaTime
        );

        Quaternion desiredLookRotation = Quaternion.LookRotation(focusPoint - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredLookRotation,
            rotationSmooth * Time.deltaTime
        );
    }
}