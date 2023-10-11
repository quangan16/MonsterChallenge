using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    // public List<ICollectable> toysCollectedList;
    public Stack<BatteryItem> batteryCollectedList = new Stack<BatteryItem>();

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
  

    public void AddItem(ICollectable item)
    {
        if (item is BatteryItem )
        {
            batteryCollectedList.Push(item as BatteryItem);
            Debug.Log(batteryCollectedList.Count);
        }
        else{}
    }

    public void RemoveItem()
    {

        batteryCollectedList.Pop();
        
    }

    public void RemoveAllItem()
    {
        batteryCollectedList.Clear();
    }
    
}
