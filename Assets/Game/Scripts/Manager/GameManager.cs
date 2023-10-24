using UnityEngine;
using UnityEngine.Serialization;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [FormerlySerializedAs("objectivesSo")] public ObjectivesSO objectivesSO;
    public MissionData currentMission;

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

        Init();

    }

    public void Init()
    {
        objectivesSO.SetMissionIDsByIndex();
        currentMission = DataManager.Instance.LoadCurrentMissionData();
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    public void NextMission()
    {
        currentMission.missionID++;
        objectivesSO.SetActiveMissionByIndex(currentMission.missionID++);
        DataManager.Instance.SaveCurrentMissionID(currentMission.missionID);
    }
        
}