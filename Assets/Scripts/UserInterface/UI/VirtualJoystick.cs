using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform _handle;

    [SerializeField] private float _radius = 100f;

    private Vector2 _direction;

    public Vector2 Direction => _direction;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform,
            eventData.position,
            eventData.pressEventCamera,
            out pos);

        pos = Vector2.ClampMagnitude(pos, _radius);

        _handle.anchoredPosition = pos;
        _direction = pos / _radius;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _handle.anchoredPosition = Vector2.zero;
        _direction = Vector2.zero;
    }
}
