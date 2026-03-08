using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public float animatorSmoothTime = 0.08f;

    private bool isDancing = false;
    private bool isSad = false;
    private bool isHappy = false;

    void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector2 input = ReadMovementInput();
        HandleMoodKeys(input);
        UpdateAnimator(input);
    }

    private Vector2 ReadMovementInput()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current == null)
            return input;

        if (Keyboard.current.aKey.isPressed) input.x -= 1f;
        if (Keyboard.current.dKey.isPressed) input.x += 1f;
        if (Keyboard.current.sKey.isPressed) input.y -= 1f;
        if (Keyboard.current.wKey.isPressed) input.y += 1f;

        return Vector2.ClampMagnitude(input, 1f);
    }

    private void HandleMoodKeys(Vector2 input)
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            isDancing = true;
            isSad = false;
            isHappy = false;
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            isDancing = false;
            isSad = true;
            isHappy = false;
        }

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            isDancing = false;
            isSad = false;
            isHappy = true;
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            isDancing = false;
            isSad = false;
            isHappy = false;
        }

        
        if (input != Vector2.zero)
        {
            isDancing = false;
            isSad = false;
            isHappy = false;
        }
    }

    private void UpdateAnimator(Vector2 input)
    {
        if (animator == null) return;

        animator.SetBool("Dance", isDancing);
        animator.SetBool("Sad", isSad);
        animator.SetBool("Happy", isHappy);

        animator.SetFloat("YatayEksen", input.x, animatorSmoothTime, Time.deltaTime);
        animator.SetFloat("DikeyEksen", input.y, animatorSmoothTime, Time.deltaTime);
    }
}