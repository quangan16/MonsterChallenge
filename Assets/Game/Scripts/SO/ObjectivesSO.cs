using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "ScriptableObjects/MissionData", order = 1)]
public class ObjectivesSO :ScriptableObject
{
    public MissionData[] missions;

    public void SetMissionIDsByIndex()
    {
        for (int i = 0; i < missions.Length; i++)
        {
            missions[i].missionID = i;
        }
    }

    public MissionData GetMissionByIndex(int index)
    {
        return missions[index];
    }

    public void SetActiveMissionByIndex(int index)
    {
        foreach (var mission in missions)
        {
            mission.isActive = false;
        }

        missions[index].isActive = true;
    }

    public MissionData GetCurrentActiveMision()
    {
        foreach (var mission in missions)
        {
            if (mission.isActive == true)
            {
                return mission;
            }
        }

        return null;
    }
}

[Serializable]
public class MissionData
{
    public int missionID;
    public MissionType missionType;
    public int maxItemRequire;
    public string missionDescription;
    public bool isActive;
}

public enum MissionType
{
    FILL_BATTERY,
    
}