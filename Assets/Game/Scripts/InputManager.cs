using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public void Awake()
    {
        if (Instance != null && this != Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private VirtualJoystick virtualJoystick;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
  

    public Vector2 GetNormalizedMoveDirection()
    {
        return virtualJoystick.GetHandlerLocalPos().normalized;
        
    }

    public Vector2 GetNormalizedRotationDirection()
    {
       
        if (Input.touchCount == 1  && virtualJoystick.IsDragging == false)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                touchEndPos = touch.position;
                float deltaX = (touchEndPos.x - touchStartPos.x) ;
                float deltaY = (touchEndPos.y - touchStartPos.y);
                return new Vector2(deltaX, deltaY);
            }
        }

        touchStartPos = touchEndPos;
        return Vector2.zero;
    }
    
}
