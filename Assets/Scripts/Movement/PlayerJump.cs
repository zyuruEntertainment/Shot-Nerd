using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerJump : MonoBehaviour
{
    private PlayerPhysics playerPhysics;

    private void Awake()
    {
        playerPhysics = GetComponent<PlayerPhysics>();
    }

    public void Jump()
    {
        if (playerPhysics == null)
            return;

        playerPhysics.Jump();
    }
}
