
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerJump : MonoBehaviour
{
    private PlayerPhysics playerPhysics;

    private void Awake()
    {
        playerPhysics = GetComponent<PlayerPhysics>();
    }

    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }
    }

    // Dipanggil oleh keyboard maupun UI Button.
    public void Jump()
    {
        playerPhysics.RequestJump();
    }
}
