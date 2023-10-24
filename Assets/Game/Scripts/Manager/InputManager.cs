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
    public int currentTouchRotateID;

    public void Update()
    {
        Debug.Log(Input.touchCount);
    }

    public Vector2 GetNormalizedMoveDirection()
    {
        return virtualJoystick.GetHandlerLocalPos().normalized;
        
    }

    public Vector2 GetNormalizedRotationDirection()
    {
        Touch touch;
        if (Input.touchCount >= 1 && Input.GetTouch(Input.touchCount - 1).position.x > Screen.width * 0.5f)

        {
            currentTouchRotateID = Input.touchCount - 1;
            touch = Input.GetTouch(Input.touchCount - 1);
            
                if (touch.phase == TouchPhase.Moved && touch.position.x > Screen.width * 0.5f)
                {
                    return (touch.deltaPosition);
                }
            
           
        }
        else if(Input.touchCount >= 1 && Input.GetTouch(Input.touchCount - 1).position.x < Screen.width * 0.5f)
        {
             touch = Input.GetTouch(currentTouchRotateID);
             if (touch.phase == TouchPhase.Moved && touch.position.x > Screen.width * 0.5f)
             {
               
                 return (touch.deltaPosition);
                 
             }
        }

       
        return Vector2.zero;
    }

    Vector2 ClampValuesToMagnitude(Vector2 position)
    {
        return Vector2.ClampMagnitude(position, 1f);
    }
}



