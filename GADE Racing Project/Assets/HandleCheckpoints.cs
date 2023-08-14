using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCheckpoints : MonoBehaviour
{
    public string LastTriggeredCheckpoint;

    public List<GameObject> RemainingCheckpoints;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //the DisableCheckpoint will find and delete the checkpoint that was last hit, while serving as a reference point to enable the next checkpoint 
    public void SetActiveCheckpoint(int DisableCheckpoint)
    {
        RemainingCheckpoints.Remove(RemainingCheckpoints[DisableCheckpoint]);

        if(RemainingCheckpoints.Count > 1)
        {
            GameObject NextActivePoint = RemainingCheckpoints[DisableCheckpoint + 1];
        }
    }

}
