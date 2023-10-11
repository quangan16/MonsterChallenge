using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Door connectedDoor;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material[] materials;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        SetButtonState(connectedDoor.doorState);
    }
    public void OpenDoor(DoorType doorType)
    {
        connectedDoor.Unlock(doorType);
    }

    public void CloseDoor()
    {
        connectedDoor.Close();
        
    }

    public void Interact()
    {
        if (connectedDoor.doorState == DoorState.OPENED)
        {
            CloseDoor();
        }
        else if(connectedDoor.doorState == DoorState.UNLOCKED_AND_CLOSED)
        {
            OpenDoor(connectedDoor.doorType);
        }
        else if (connectedDoor.doorState == DoorState.LOCKED)
        {
            Debug.Log("Door is locked");
        }

        SetButtonState(connectedDoor.doorState);
    }

    public void SetButtonState(DoorState doorState)
    {
        switch (doorState)
        {
            case DoorState.LOCKED:
                meshRenderer.material = materials[0];
                return;
            case DoorState.UNLOCKED_AND_CLOSED:
                meshRenderer.material = materials[1];
                return;
            case DoorState.OPENED:
                meshRenderer.material = materials[2];
                return;
        }
       
    }
}
