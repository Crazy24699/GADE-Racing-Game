using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class HandleCheckpoints : MonoBehaviour
{
    public string LastTriggeredCheckpoint;

    public List<GameObject> RemainingCheckpoints;

    public List<Vector3> CheckpointLocations;

    public GameObject CheckpointRef;

    
    void Start()
    {
        //EventHandler.EventHandlerInstance.CheckpointTriggered.AddListener(SetActiveCheckpoint);
        SpawnCheckpoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

            //NOTE: Make this a await method and use a while loop to spawn everyting while the game loads
    public void SpawnCheckpoints()
    {
        for (int i = 0; i < CheckpointLocations.Count; )
        {
            GameObject CheckpointParentObject = GameObject.Find("Checkpoint Parent");
            GameObject SpawningCheckpoint;
            SpawningCheckpoint = Instantiate(CheckpointRef, CheckpointLocations[i], Quaternion.identity);

            CheckpointFunctionality CheckpointScriptRef = SpawningCheckpoint.GetComponent<CheckpointFunctionality>();
            SpawningCheckpoint.gameObject.name = "Checkpoint " + i;

            SpawningCheckpoint.transform.SetParent(CheckpointParentObject.transform);
            CheckpointScriptRef.CheckpointNumber = i;

            RemainingCheckpoints.Add(SpawningCheckpoint);

            if (i >= 1)
            {
                SpawningCheckpoint.SetActive(false);
            }

            i++;
        }
    }

    //the DisableCheckpoint will find and delete the checkpoint that was last hit, while serving as a reference point to enable the next checkpoint 
    public void SetActiveCheckpoint()
    {
        
        if(RemainingCheckpoints.Count != 1)
        {
            GameObject NextActivePoint = RemainingCheckpoints[1];
            CheckpointFunctionality CheckpointScriptRef = NextActivePoint.GetComponent<CheckpointFunctionality>();

            Debug.Log("Set active checkpoint method problem ");

            CheckpointScriptRef.IsActive = true;
            NextActivePoint.SetActive(true);
        }

        RemainingCheckpoints.Remove(RemainingCheckpoints[0]);
    }

}
