using Lean.Touch;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;

    protected override void Start()
    {
        MoveThreshold = moveThreshold;
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(LeanFinger eventData)
    {
        if(stopMove || eventData.IsOverGui) return;
        working = true;

        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.ScreenPosition);
        background.gameObject.SetActive(true);
        // base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(LeanFinger eventData)
    {
        // if(stopMove || eventData.IsOverGui) return;
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}