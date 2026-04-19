using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; // Dash için IEnumerator kullanacağız

public class PlayerStrafeMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    [Header("Movement")]
    public float moveSpeed = 80f;
    public float gravity = -20f;
    public float jumpHeight = 3f;
    public float annen = 1.1f; // Raycast uzunluğunu buraya çektim

    [Header("Dash Settings")]
    public float dashSpeed = 40f;    // Dash hızı
    public float dashTime = 0.2f;     // Dash süresi (ne kadar sürsün?)
    public float dashCooldown = 1f;  // Tekrar dash atmak için bekleme süresi
    private bool isDashing = false;  // Dash atıyor mu kontrolü
    private bool canDash = true;     // Dash atabilir mi kontrolü

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
        // Dash atarken normal hareketi durduruyoruz
        if (isDashing) return;

        float x = 0f;
        float z = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current[Key.A].isPressed) x = -1f;
            if (Keyboard.current[Key.D].isPressed) x = 1f;
            if (Keyboard.current[Key.W].isPressed) z = 1f;
            if (Keyboard.current[Key.S].isPressed) z = -1f;

            // --- DASH TETİKLEME (E TUŞU) ---
            if (Keyboard.current[Key.E].wasPressedThisFrame && canDash)
            {
                StartCoroutine(Dash(x, z));
            }
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

            bool yerdeyim = Physics.Raycast(transform.position, Vector3.down, annen);

            if (yerdeyim && velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            if (Keyboard.current[Key.Space].wasPressedThisFrame && yerdeyim)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    // --- DASH MANTIĞI ---
    private IEnumerator Dash(float x, float z)
    {
        canDash = false;
        isDashing = true;

        // Yerçekimini dash anında hissetmemek için dikey hızı sıfırlıyoruz
        velocity.y = 0;

        // Karakterin baktığı yöne veya bastığı yöne göre dash yönü belirle
        Vector3 dashDirection;
        if (x == 0 && z == 0) 
            dashDirection = transform.forward; // Tuşa basmıyorsa baktığı yöne atılsın
        else
            dashDirection = transform.right * x + transform.forward * z;

        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            controller.Move(dashDirection.normalized * dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;

        // Bekleme süresi
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}