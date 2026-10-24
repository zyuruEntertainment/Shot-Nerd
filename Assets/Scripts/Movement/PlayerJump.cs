using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerJump : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PCInput pcInput;
    [SerializeField] private PlayerPhysics playerPhysics;

    private void Awake()
    {
        if (pcInput == null)
            pcInput = GetComponent<PCInput>();

        if (playerPhysics == null)
            playerPhysics = GetComponent<PlayerPhysics>();
    }

    private void Update()
    {
        if (pcInput != null &&
            pcInput.ConsumeJumpInput())
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (playerPhysics == null)
        {
            Debug.LogError(
                "PlayerPhysics belum terpasang pada Player.",
                this
            );
            return;
        }

        playerPhysics.RequestJump();
    }
}