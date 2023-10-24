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
  
    public void SaveBatteryData(int numberOfBatteries)
    {
        PlayerPrefs.SetInt("NumberOfBatteries", numberOfBatteries);
        SaveData();
    }

    public MissionData LoadCurrentMissionData()
    {
        
        int currentMissionID =  PlayerPrefs.GetInt("CurrentMission", 0);
        return GameManager.Instance.objectivesSO.GetMissionByIndex(currentMissionID);
    }

    public void SaveCurrentMissionID(int currentMissionID)
    {
        PlayerPrefs.SetInt("CurrentMission", currentMissionID);
    }

    public void SaveData()
    {
        PlayerPrefs.Save();
    }
}
