using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool isMoving = false;
    private float playerSpeed = 10.0f;
    public Vector3 moveDirection;
    private float rotationSpeed = 5000.0f;
    private float upRotationBound = 70.0f;
    private float downRotationBound = -70.0f;
    private bool isJumping = false;

    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private Rigidbody playerRb;
    private float jumpForce = 100.0f;

    [SerializeField] private Button jumpButton;
    [SerializeField] private Button interactButton;

    public void Awake()
    {
        jumpButton.onClick.AddListener(Jump);
    }
    
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
            Vector3 newMoveDirection = cameraHolder.transform.TransformDirection(moveDirection * playerSpeed);
            newMoveDirection.y = 0.0f;
            playerRb.velocity = newMoveDirection;
        }
        else
        {
            isMoving = false;
            playerRb.velocity =new Vector3(0, playerRb.velocity.y, 0);
        }
    }

    public void LookAround()
    {
        cameraHolder.transform.Rotate(Vector3.up * InputManager.Instance.GetNormalizedRotationDirection().x);
        float newRotationY = cameraHolder.transform.eulerAngles.y -
                             InputManager.Instance.GetNormalizedRotationDirection().x;

        float newRotationX = cameraHolder.transform.eulerAngles.x -
                             InputManager.Instance.GetNormalizedRotationDirection().y;

        newRotationX = (newRotationX % 360 + 360) % 360;
        if (newRotationX > 180)
        {
            newRotationX -= 360;
        }
        // Calculate the clamped rotation
        newRotationX = Mathf.Clamp(newRotationX, downRotationBound, upRotationBound);
        Debug.Log(cameraHolder.transform.eulerAngles.x);

        // Apply the clamped rotation
        cameraHolder.transform.eulerAngles = new Vector3(newRotationX, cameraHolder.transform.eulerAngles.y, 0.0f);
        
    }

    public void Jump()
    {
        if (isJumping == false)
        {
            isJumping = true;
            playerRb.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * jumpForce, ForceMode.Impulse);
        }
       
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
   
    
      
}  
    

