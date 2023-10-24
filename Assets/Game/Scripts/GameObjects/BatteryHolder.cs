using System.Collections.Generic;
using UnityEngine;


public class BatteryHolder : MonoBehaviour, IInteractable
{
    public DoorBattery connectedDoor;
    public int batteryRequired;
    public Transform[] batterySlots;
    public bool[] batterySlotsUsed;
    public GameObject batteryPrefab;

    public void Start()
    {
        Init();
    }
    public void Init()
    {
        if (GameManager.Instance.currentMission.missionType == MissionType.FILL_BATTERY)
        {
            batteryRequired = GameManager.Instance.currentMission.maxItemRequire;
        }

        batterySlotsUsed = new bool[5];
    }
    public void Interact()
    {
        if (GameManager.Instance.currentMission.missionType == MissionType.FILL_BATTERY)
        {
          
           // if(PlayerManager.Instance.GetItemQuanityByType(B))
           //     int i = 0;
                // if (batterySlotsUsed[i]== false)
                // {
                //     Instantiate(batteryPrefab, batterySlots[i]);
                //     batterySlotsUsed[i] = true;
                //     batteryRequired--;
                // }
                // else
                // {
                //     i++;
                // }
            
           
        }
    }
    
    
}