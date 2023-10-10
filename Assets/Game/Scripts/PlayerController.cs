using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public bool isMoving = false;
    private float moveSpeed = 3.0f;
    private float sprintSpeed = 5.0f;
    public Vector3 moveDirection;
    private float rotationSpeed = 5.0f;
    private float upRotationBound = 70.0f;
    private float downRotationBound = -70.0f;
    private bool isSprinting = false;
    private float jumpHeight = 2.0f; // Jump height in meters
    private float gravity = -9.81f;

    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private Rigidbody playerRb;

    [SerializeField] private Button jumpButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private CharacterController characterCtl;
    public float verticalVelocity;

    private float maxInteractDistance = 1.0f;
    private LayerMask interactableLayer = 1 << 7;
    
    
    public void Awake()
    {
        jumpButton.onClick.AddListener(Jump);
    }

    public void Start()
    {
        characterCtl = GetComponent<CharacterController>();
    }
    
    public void Update()
    {
        Move();
        LookAround();
        ApplyGravity();
        Debug.Log(characterCtl.isGrounded);
        if (Input.GetKeyDown(KeyCode.A))
        {
            Interact();
        }
    }

  


    public void Move()
    {
        float currentSpeed = (isSprinting) ? moveSpeed : sprintSpeed;
        moveDirection = new Vector3(InputManager.Instance.GetNormalizedMoveDirection().x, 0.0f,
            InputManager.Instance.GetNormalizedMoveDirection().y);
            isMoving = true;
            Vector3 newMoveDirection = cameraHolder.transform.TransformDirection(moveDirection * moveSpeed) ;
           
            // playerRb.velocity = new Vector3(newMoveDirection.x, playerRb.velocity.y, newMoveDirection.z) ;
            characterCtl.Move((newMoveDirection * moveSpeed * Time.deltaTime) + (new Vector3(0, verticalVelocity, 0) * Time.deltaTime) );
    }
    
    public void LookAround()
    {
        if (InputManager.Instance.GetNormalizedRotationDirection() != Vector2.zero)
        {
            float newRotationY = cameraHolder.transform.eulerAngles.y +
                                 InputManager.Instance.GetNormalizedRotationDirection().x * rotationSpeed * Time.deltaTime;

            float newRotationX = cameraHolder.transform.eulerAngles.x -
                                 InputManager.Instance.GetNormalizedRotationDirection().y * rotationSpeed *
                                 Time.deltaTime;

            newRotationX = (newRotationX % 360 + 360) % 360;
            if (newRotationX > 180)
            {
                newRotationX -= 360;
            }

            newRotationX = Mathf.Clamp(newRotationX, downRotationBound, upRotationBound);
            Quaternion targetRotation = Quaternion.Euler(newRotationX, newRotationY, 0);
            
            // cameraHolder.transform.rotation =
            //     Quaternion.Slerp(cameraHolder.transform.rotation, targetRotation,
            //         0.5f * rotationSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up * (InputManager.Instance.GetNormalizedRotationDirection().x * rotationSpeed * Time.deltaTime), Space.World);

            cameraHolder.transform.localRotation = Quaternion.Euler(newRotationX, cameraHolder.transform.rotation.y, 0);
        }
     
       
    }

    public void ApplyGravity()
    {
        if (verticalVelocity <= 0 && characterCtl.isGrounded)
        {
            verticalVelocity = -0.1f;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }
    

    public void Jump()
    {
       
        if (characterCtl.isGrounded == true)
        {
            // Calculate the jump velocity to achieve the desired jump height
            float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * -gravity);
    
            // Set the vertical velocity to the jump velocity
            verticalVelocity = jumpVelocity;
            Debug.Log(verticalVelocity);
        }
    }
    
    public void Interact()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        if(Physics.Raycast(ray, out RaycastHit hit, maxInteractDistance, interactableLayer))
        {
            IInteractable interactableObj = hit.collider.GetComponent(typeof(IInteractable)) as IInteractable;
            if (interactableObj != null)
            {
                interactableObj.Interact();
            }
        }
       
    }

    public void OnInteract(IInteractable obj)
    {
        obj.Interact();
    }
   
    
      
}  
    

