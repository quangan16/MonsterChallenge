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

        if (Input.touchCount >= 1)

    {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved && touch.position.x > Screen.width * 0.5f)
            {
                return touch.deltaPosition;
            }
        }
        return Vector2.zero;
    }
    
}
