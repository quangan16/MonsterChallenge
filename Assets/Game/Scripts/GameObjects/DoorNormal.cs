using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class DoorNormal : Door
{
    public void Awake()
    {
        doorType = DoorType.NORMAL;
        doorState = DoorState.UNLOCKED_AND_CLOSED;
    }
    public void Start()
    {
        base.Start();
       
    }
  
  

    public override void Unlock(DoorType type)
    {
        if (type == DoorType.NORMAL)
        {
            Debug.Log("hehe");
            Open();
        }
    }
    
}
