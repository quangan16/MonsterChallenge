using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private Door connectedDoor;

    public void OpenDoor()
    {
        connectedDoor.Open();
    }

    public void CloseDoor()
    {
        connectedDoor.Close();
        
    }
}
