using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class DoorBattery : Door
{
    public void Awake()
    {
        doorType = DoorType.BATTERY;
        doorState = DoorState.LOCKED;
    }
    public void Start()
    {
        base.Start();
       
    }
  
  

    public override void Unlock(DoorType type)
    {
        if (type == DoorType.BATTERY)
        {
            doorState = DoorState.UNLOCKED_AND_CLOSED;
        }
    }
    
}
