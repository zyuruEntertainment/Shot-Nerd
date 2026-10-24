using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PCInput pcInput;
    [SerializeField] private MobileInput mobileInput;
    [SerializeField] private PlayerPhysics playerPhysics;

    private CharacterController controller;
    private float cameraPitch;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Vector2 moveInput;
        Vector2 lookInput;

        bool useMobile = Application.isMobilePlatform;

        if (useMobile && mobileInput != null)
        {
            moveInput = mobileInput.MoveInput;
            lookInput = mobileInput.ConsumeLookInput();
        }
        else if (pcInput != null)
        {
            moveInput = pcInput.MoveInput;
            lookInput = pcInput.ConsumeLookInput();
        }
        else
        {
            moveInput = Vector2.zero;
            lookInput = Vector2.zero;
        }

        RotatePlayer(lookInput);
        MovePlayer(moveInput);
    }

    private void MovePlayer(Vector2 input)
    {
        input = Vector2.ClampMagnitude(input, 1f);

        Vector3 moveDirection =
            transform.right * input.x +
            transform.forward * input.y;

        controller.Move(
            moveDirection * moveSpeed * Time.deltaTime
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

        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);

        cameraTransform.localRotation =
            Quaternion.Euler(cameraPitch, 0f, 0f);
    }
}
