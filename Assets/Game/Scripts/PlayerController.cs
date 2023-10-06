using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isMoving = false;
    private float playerSpeed = 10.0f;
    public Vector3 moveDirection;
    private float rotationSpeed = 5000.0f;
    private float upRotationBound = 70.0f;
    private float downRotationBound = -70.0f;

    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private Rigidbody playerRb;
      
    
    public void Update()
    {
        Move();
        LookAround();
    }

    public void Move()
    {
        moveDirection = new Vector3(InputManager.Instance.GetNormalizedMoveDirection().x, 0.0f,
            InputManager.Instance.GetNormalizedMoveDirection().y);
        if (moveDirection != Vector3.zero)
        {
            isMoving = true;
            playerRb.velocity = cameraHolder.transform.TransformDirection(moveDirection);
        }
        else
        {
            isMoving = false;
        }
    }

    public void LookAround()
    {
        cameraHolder.transform.Rotate(Vector3.up * InputManager.Instance.GetNormalizedRotation().x);
        // transform.Rotate(Vector3.left * InputManager.Instance.GetNormalizedRotation().y);
        float newRotationX = (cameraHolder.transform.eulerAngles.x - InputManager.Instance.GetNormalizedRotation().y);
        newRotationX = Mathf.Clamp(newRotationX, downRotationBound, upRotationBound);
        Debug.Log(newRotationX);
        cameraHolder.transform.eulerAngles = new Vector3(newRotationX, cameraHolder.transform.eulerAngles.y, 0.0f);
      
    }


    
      
}  
    

