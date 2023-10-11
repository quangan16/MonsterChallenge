using UnityEngine;


public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

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

    public int LoadBatteryData()
    {
        return PlayerPrefs.GetInt("NumberOfBatteries", 0);
    }
  
    public void SaveBatteryData()
    {
        PlayerPrefs.SetInt("NumberOfBatteries", PlayerManager.Instance.batteryCollectedList.Count);
        SaveData();
    }

    public MissionData LoadCurrentMissionData()
    {
        
        int currentMissionID =  PlayerPrefs.GetInt("CurrentMission", 0);
        return GameManager.Instance.objectivesSO.GetMissionByIndex(currentMissionID);
    }

    public void SaveCurrentMissionID()
    {
        PlayerPrefs.SetInt("CurrentMission", GameManager.Instance.currentMission.missionID);
    }

    public void SaveData()
    {
        PlayerPrefs.Save();
    }
}
