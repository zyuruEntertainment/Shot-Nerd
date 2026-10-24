
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerPhysics : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;

    private CharacterController controller;
    private float verticalVelocity;

    public float Gravity => gravity;
    public float VerticalVelocity => verticalVelocity;

    public bool IsGrounded =>
        controller != null && controller.isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Mengatur kecepatan vertikal dari sistem fisika
    // atau PlayerJump.
    public void SetVerticalVelocity(float velocity)
    {
        verticalVelocity = velocity;
    }

    // Memperbarui gravitasi tanpa menggerakkan karakter.
    public void ApplyGravity(float deltaTime)
    {
        if (controller == null)
            return;

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        verticalVelocity += gravity * deltaTime;
    }
}
