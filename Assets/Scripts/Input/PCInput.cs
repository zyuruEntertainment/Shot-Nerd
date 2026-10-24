using UnityEngine;
using UnityEngine.InputSystem;

public class PCInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    private Vector2 lookInput;


    private void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current == null ||
            Mouse.current == null)
        {
            MoveInput = Vector2.zero;
            return;
        }

        if (Keyboard.current.wKey.isPressed)
            input.y += 1f;

        if (Keyboard.current.sKey.isPressed)
            input.y -= 1f;

        if (Keyboard.current.dKey.isPressed)
            input.x += 1f;

        if (Keyboard.current.aKey.isPressed)
            input.x -= 1f;

        MoveInput = Vector2.ClampMagnitude(input, 1f);

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            lookInput += Mouse.current.delta.ReadValue();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public Vector2 ConsumeLookInput()
    {
        Vector2 result = lookInput;
        lookInput = Vector2.zero;
        return result;
    }
}
