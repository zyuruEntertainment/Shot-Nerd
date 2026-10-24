
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerPhysics : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;

    [Header("Jump Physics")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private bool allowJump = true;

    private CharacterController controller;
    private float verticalVelocity;
    private bool jumpRequested;

    public float VerticalVelocity => verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Dipanggil oleh PlayerJump, bukan menghitung fisika di sana.
    public void RequestJump()
    {
        if (allowJump)
            jumpRequested = true;
    }

    private void Update()
    {
        bool grounded = controller.isGrounded;

        if (grounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        if (jumpRequested)
        {
            if (allowJump && grounded)
            {
                verticalVelocity = Mathf.Sqrt(
                    jumpHeight * -2f * gravity
                );
            }

            // Permintaan diproses sekali.
            jumpRequested = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        controller.Move(
            Vector3.up * verticalVelocity * Time.deltaTime
        );
    }
}
