using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryItem : MonoBehaviour,IInteractable, ICollectable
{
    [SerializeField] private BatteryHolder batteryHolder;

    public void Interact()
    {
        Collect(this);
        Destroy(gameObject);


    }

    public void Collect(ICollectable item)
    {
        PlayerManager.Instance.AddItem((BatteryItem)item);
    }
}
