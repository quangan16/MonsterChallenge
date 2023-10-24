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
            DataManager.Instance.SaveBatteryData(batteryCollectedList.Count);
        }
        else{}
    }

    public void RemoveAnItem()
    {

        batteryCollectedList.Pop();
        DataManager.Instance.SaveBatteryData(batteryCollectedList.Count);
        
    }

    public void RemoveAllItems()
    {
        batteryCollectedList.Clear();
    }

    public int GetItemQuanityByType(ICollectable itemType)
    {
        int itemQuanity = -1;
        if (itemType is BatteryItem)
        {
            itemQuanity = DataManager.Instance.LoadBatteryData();
           
        }

        return itemQuanity;
    }

   
    
}
