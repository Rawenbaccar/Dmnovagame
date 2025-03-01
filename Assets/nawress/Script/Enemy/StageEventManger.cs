using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManger : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemySpawner enemiesManager;
    StageTime stageTime;
    int eventindex;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }
    private void Update()
    {
        if (eventindex >= stageData.stageEvents.Count) { return; }
        if (stageTime.time > stageData.stageEvents[eventindex].time)
        {
            for (int i = 0 ; i <= stageData.stageEvents[eventindex].Count;i++ ){
                enemiesManager.SpawnEnemy();
            }
            
            eventindex += 1;
        }
        
    }

}
