
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Min(0f)]
    private float moveSpeed = 5f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PCInput pcInput;
    [SerializeField] private MobileInput mobileInput;

    private CharacterController controller;
    private float cameraPitch;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        if (pcInput == null)
            pcInput = GetComponent<PCInput>();
    }

    private void Update()
    {
        Vector2 moveInput = Vector2.zero;
        Vector2 lookInput = Vector2.zero;

        // Always read mobile input, including in the Unity Editor.
        if (mobileInput != null)
        {
            moveInput = mobileInput.MoveInput;
            lookInput = mobileInput.ConsumeLookInput();
        }

        // Use keyboard/mouse when mobile input is not active.
        if (pcInput != null)
        {
            if (moveInput.sqrMagnitude < 0.0001f)
                moveInput = pcInput.MoveInput;

            if (lookInput.sqrMagnitude < 0.0001f)
                lookInput = pcInput.ConsumeLookInput();
        }

        RotatePlayer(lookInput);
        MovePlayer(moveInput);
    }

    private void MovePlayer(Vector2 input)
    {
        input = Vector2.ClampMagnitude(input, 1f);

        Vector3 direction =
            transform.right * input.x +
            transform.forward * input.y;

        controller.Move(
            direction * moveSpeed * Time.deltaTime
        );
    }

    private void RotatePlayer(Vector2 lookInput)
    {
        if (cameraTransform == null)
            return;

        float sensitivity =
            SensitivitySettings.Instance != null
                ? SensitivitySettings.Instance.LookSensitivity
                : 1f;

        // Horizontal look rotates the player.
        transform.Rotate(
            Vector3.up * lookInput.x * sensitivity
        );

        // Vertical look rotates the camera.
        cameraPitch -= lookInput.y * sensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);

        cameraTransform.localRotation =
            Quaternion.Euler(cameraPitch, 0f, 0f);
    }
}
