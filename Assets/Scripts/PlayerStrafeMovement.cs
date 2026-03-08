using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStrafeMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    [Header("Movement")]
    public float moveSpeed = 80f;
    public float gravity = -20f;

    [Header("Look")]
    public float mouseTurnSpeed = 2f;

    private Vector3 velocity;

    void Awake()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float x = 0f;
        float z = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current[Key.A].isPressed) x = -1f;
            if (Keyboard.current[Key.D].isPressed) x = 1f;
            if (Keyboard.current[Key.W].isPressed) z = 1f;
            if (Keyboard.current[Key.S].isPressed) z = -1f;
        }

        if (Mouse.current != null)
        {
            float mouseX = Mouse.current.delta.ReadValue().x;
            transform.Rotate(0f, mouseX * mouseTurnSpeed * Time.deltaTime * 60f, 0f);
        }

        if (animator != null)
        {
            animator.SetFloat("YatayEksen", x);
            animator.SetFloat("DikeyEksen", z);
        }

        Vector3 move = transform.right * x + transform.forward * z;

        if (move.sqrMagnitude > 1f)
            move.Normalize();

        if (controller != null)
        {
            controller.Move(move * moveSpeed * Time.deltaTime);

            if (controller.isGrounded && velocity.y < 0f)
                velocity.y = -2f;

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}