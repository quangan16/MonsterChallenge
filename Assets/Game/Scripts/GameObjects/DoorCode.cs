using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class DoorCode : Door
{

    [SerializeField] private int password = 1234;
  
    public void Start()
    {
        base.Start();
        doorType = DoorType.NORMAL;
        doorState = DoorState.LOCKED;
    }
  
  

    public override void Unlock(DoorType type)
    {
        if (type == DoorType.CODE)
        {
            doorType = DoorType.CODE;
        }
    }
    
}
