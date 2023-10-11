using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public enum DoorType
{
    NORMAL,
    BATTERY,
    CODE,
    
}

public abstract class Door : MonoBehaviour
{
    public DoorType doorType;

    public bool canOperate = true;
 
    [SerializeField] private float doorHeight;

    public DoorState doorState;
    private Vector3 defaultPos;

   
    private void OnDisable()
    {
        transform.DOKill();
    }

    public virtual void Start()
    {
        canOperate = true;
        doorHeight = GetComponent<MeshRenderer>().localBounds.size.y;
        defaultPos = transform.localPosition;
    }
    [Sirenix.OdinInspector.Button]
    public void Open()
    {
       
        if (doorState == DoorState.UNLOCKED_AND_CLOSED && canOperate )
        {
            doorState = DoorState.OPENED;
            canOperate = false;
            transform.DOLocalMoveY(doorHeight, 1.0f).OnComplete(() => { canOperate = true;});
           

        }
        else if(doorState == DoorState.LOCKED)
        {
            Debug.Log("This door is locked");
        }
       
    }

    [Sirenix.OdinInspector.Button]
    public void Close()
    {
        if (doorState == DoorState.OPENED && canOperate)
        {
            canOperate = false;
            doorState = DoorState.UNLOCKED_AND_CLOSED;
            transform.DOLocalMoveY(defaultPos.y, 1.0f).OnComplete(() =>
            {
                canOperate = true;
            });
          
        }
    }

    public abstract void Unlock(DoorType type);
    
    


}

public enum DoorState{
   LOCKED,
   UNLOCKED_AND_CLOSED,
   OPENED,
    
}
