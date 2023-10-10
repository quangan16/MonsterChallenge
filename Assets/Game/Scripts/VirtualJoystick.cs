using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image joystickBgImg;
    [SerializeField] private Image joystickHandlerImg;
    [SerializeField] private Vector2 localPoint;

    public bool IsDragging { get; private set; } = false;
    
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                joystickBgImg.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out  localPoint
            ))
        {
            IsDragging = true;
            localPoint = Vector2.ClampMagnitude(localPoint, joystickBgImg.rectTransform.rect.width * 0.5f);
            joystickHandlerImg.rectTransform.anchoredPosition = localPoint;
            // Debug.Log(localPoint);
        }
        else
        {
          
            // joystickHandlerImg.rectTransform.anchoredPosition = Vector2.zero;
        }
        
        
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDragging = false;
        localPoint = Vector2.zero;
        joystickHandlerImg.rectTransform.anchoredPosition = localPoint;
    }

    public Vector2 GetHandlerLocalPos()
    {
        return localPoint;
    }
    
    
}
