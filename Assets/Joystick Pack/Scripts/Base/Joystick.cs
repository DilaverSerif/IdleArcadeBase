using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : Singleton<Joystick>
{
    public float Horizontal { get { return (snapX) ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x; } }
    public float Vertical { get { return (snapY) ? SnapFloat(input.y, AxisOptions.Vertical) : input.y; } }
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }
    public Vector3 Direction3D { get { return new Vector3(Horizontal, 0, Vertical); } }
    public bool Touching => input != Vector2.zero;
    protected bool working = false;
    public float HandleRange
    {
        get { return handleRange; }
        set { handleRange = Mathf.Abs(value); }
    }

    public float DeadZone
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }

    public AxisOptions AxisOptions { get { return AxisOptions; } set { axisOptions = value; } }
    public bool SnapX { get { return snapX; } set { snapX = value; } }
    public bool SnapY { get { return snapY; } set { snapY = value; } }

    [SerializeField] private float handleRange = 1;
    [SerializeField] private float deadZone = 0;
    [SerializeField] private AxisOptions axisOptions = AxisOptions.Both;
    [SerializeField] private bool snapX = false;
    [SerializeField] private bool snapY = false;
    public bool stopMove = false;

    [SerializeField] protected RectTransform background = null;
    [SerializeField] private RectTransform handle = null;
    private RectTransform baseRect = null;

    private Canvas canvas;
    private Camera cam;

    private Vector2 input = Vector2.zero;

    void Awake()
    {
        cam = Camera.main;
    }

    protected virtual void Start()
    {
        HandleRange = handleRange;
        DeadZone = deadZone;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");

        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    void OnEnable()
    {
        LeanTouch.OnFingerDown += OnPointerDown;
        LeanTouch.OnFingerUpdate += OnDrag;
        LeanTouch.OnFingerUp += OnPointerUp;
    }

    void OnDisable()
    {
        LeanTouch.OnFingerDown -= OnPointerDown;
        LeanTouch.OnFingerUpdate -= OnDrag;
        LeanTouch.OnFingerUp -= OnPointerUp;
    }

    public virtual void OnPointerDown(LeanFinger leanFinger)
    {
        if(stopMove || leanFinger.IsOverGui) return;
        working = true;
        // OnDrag(leanFinger);
    }

    public void OnDrag(LeanFinger leanFinger)
    {
        if(!working) 
        {
            return;
        }
        // if(stopMove || leanFinger.IsOverGui) return;
        // cam = null;
        // if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        //     cam = canvas.worldCamera;

        var position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        var radius = background.sizeDelta / 2;
        input = (leanFinger.ScreenPosition - position) / (radius * canvas.scaleFactor);
        FormatInput();
        HandleInput(input.magnitude, input.normalized, radius, cam);
        handle.anchoredPosition = input * radius * handleRange;
    }

    protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised;
        }
        else
            input = Vector2.zero;
    }

    private void FormatInput()
    {
        if (axisOptions == AxisOptions.Horizontal)
            input = new Vector2(input.x, 0f);
        else if (axisOptions == AxisOptions.Vertical)
            input = new Vector2(0f, input.y);
    }

    private float SnapFloat(float value, AxisOptions snapAxis)
    {
        if (value == 0)
            return value;

        if (axisOptions == AxisOptions.Both)
        {
            float angle = Vector2.Angle(input, Vector2.up);
            if (snapAxis == AxisOptions.Horizontal)
            {
                if (angle < 22.5f || angle > 157.5f)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            else if (snapAxis == AxisOptions.Vertical)
            {
                if (angle > 67.5f && angle < 112.5f)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            return value;
        }
        else
        {
            if (value > 0)
                return 1;
            if (value < 0)
                return -1;
        }
        return 0;
    }

    public virtual void OnPointerUp(LeanFinger leanFinger)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        working = false;
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
            return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }
        return Vector2.zero;
    }
    public float GetAngle()
    {
        var angle = Vector2.Angle(Vector2.up, Direction);
        if (Direction.x < 0)
        {
            angle = 360 - angle;
        }
        return angle;
    }
    public void StopMove(bool stop = true, bool reset = true)
    {
        stopMove = stop;
        if(!stop) return;
        working = false;
        if(reset)
            input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        background.gameObject.SetActive(false);
    }
}

public enum AxisOptions { Both, Horizontal, Vertical }