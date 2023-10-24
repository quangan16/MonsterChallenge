using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   
    public bool isMoving = false;
    private float normalSpeed = 3.0f;
    private float sprintSpeed = 5.0f;
    public Vector3 moveDirection;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    [SerializeField] private float rotationSpeed = 100f;
    // [SerializeField] private float rotationSpeed = 500.0f;
    private float upRotationBound = 70.0f;
    private float downRotationBound = -70.0f;
    private bool isSprinting = false;
    private float jumpHeight = 2.0f; // Jump height in meters
    private float gravity = -9.8f;
   
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private Rigidbody playerRb;

    [SerializeField] private Button jumpButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private CharacterController characterCtl;
    public float verticalVelocity;

    private float maxInteractDistance = 2.0f;
    private float maxInteractRadius = 0.6f;
    private LayerMask interactableLayer = 1 << 7;

    private float sprintDuration = 3.0f;

    [SerializeField] IInteractable interactableObj;
    public List<ICollectable> itemList;
    public void Awake()
    {
        // jumpButton.onClick.AddListener(Jump);
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
        CheckInteract();
    }

  


    public void Move()
    {
        float currentSpeed = (isSprinting) ? sprintSpeed : normalSpeed;
        moveDirection = new Vector3(InputManager.Instance.GetNormalizedMoveDirection().x, 0.0f,
            InputManager.Instance.GetNormalizedMoveDirection().y);
            isMoving = true;
            Vector3 transformDirection = cameraHolder.transform.TransformDirection(moveDirection).normalized ;
            Vector3 newMoveDirection = new Vector3(transformDirection.x, 0.0f, transformDirection.z);
            Debug.Log(newMoveDirection);
            // playerRb.velocity = new Vector3(newMoveDirection.x, playerRb.velocity.y, newMoveDirection.z) ;
            characterCtl.Move((newMoveDirection * currentSpeed * Time.deltaTime) + (new Vector3(0, verticalVelocity, 0) * Time.deltaTime));
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
            // Quaternion targetRotation = Quaternion.Euler(newRotationX, newRotationY, 0);
            // cameraHolder.transform.rotation =
            //     Quaternion.Slerp(cameraHolder.transform.rotation, targetRotation,
            //         0.9f * rotationSpeed * Time.deltaTime);
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
       
        if (characterCtl.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * -gravity);
            verticalVelocity = jumpVelocity;
            Debug.Log(verticalVelocity);
        }
    }

    public void SprintOn()
    {
        if (isSprinting == false)
        {
            isSprinting = true;
        }
        Invoke(nameof(SprintOff), sprintDuration);
    }

    public void SprintOff()
    {
        if (isSprinting)
        {
            isSprinting = false;
        }
    }
    
    public void CheckInteract()
    {
     Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        if(Physics.SphereCast(ray,maxInteractRadius, out RaycastHit hit, maxInteractDistance, interactableLayer))
        {
            UIManager.Instance.OnInteractionEnter();
            interactableObj = hit.collider.GetComponent(typeof(IInteractable)) as IInteractable;
        }
        else
        {
            UIManager.Instance.OnInteractionExit();
        }
       
    }

    public void Interact()
    {
        if (interactableObj != null)
        {
            interactableObj.Interact();
        }
    }
    
    
    
    

   
    
      
}  
    

