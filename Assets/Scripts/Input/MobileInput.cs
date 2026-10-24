
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler
{
    [Header("Joystick")]
    [SerializeField] private RectTransform joystickBackground;
    [SerializeField] private RectTransform joystickHandle;

    [Header("Look Area")]
    [SerializeField] private float lookMultiplier = 1f;

    public Vector2 MoveInput { get; private set; }

    private Vector2 lookInput;
    private RectTransform joystickArea;
    private Canvas canvas;

    private void Awake()
    {
        joystickArea = joystickBackground;
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsPointerOnJoystick(eventData))
            UpdateJoystick(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsPointerOnJoystick(eventData))
        {
            UpdateJoystick(eventData);
        }
        else
        {
            lookInput += eventData.delta * lookMultiplier;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsPointerOnJoystick(eventData))
        {
            MoveInput = Vector2.zero;

            if (joystickHandle != null)
                joystickHandle.anchoredPosition = Vector2.zero;
        }
    }

    private bool IsPointerOnJoystick(PointerEventData eventData)
    {
        return joystickArea != null &&
               RectTransformUtility.RectangleContainsScreenPoint(
                   joystickArea,
                   eventData.position,
                   eventData.pressEventCamera
               );
    }

    private void UpdateJoystick(PointerEventData eventData)
    {
        if (joystickArea == null || joystickHandle == null)
            return;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickArea,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint))
            return;

        float radiusX = joystickArea.rect.width * 0.5f;
        float radiusY = joystickArea.rect.height * 0.5f;

        Vector2 input = new Vector2(
            radiusX > 0 ? localPoint.x / radiusX : 0,
            radiusY > 0 ? localPoint.y / radiusY : 0
        );

        MoveInput = Vector2.ClampMagnitude(input, 1f);

        joystickHandle.anchoredPosition = new Vector2(
            MoveInput.x * radiusX,
            MoveInput.y * radiusY
        );
    }

    public Vector2 ConsumeLookInput()
    {
        Vector2 result = lookInput;
        lookInput = Vector2.zero;
        return result;
    }
}
