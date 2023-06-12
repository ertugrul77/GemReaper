using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    public static FloatingJoystick Instance;
    protected override void Start()
    {
        base.Start();
        Instance = this;
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}