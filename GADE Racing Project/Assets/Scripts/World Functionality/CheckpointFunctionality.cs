using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointFunctionality : MonoBehaviour
{
    private HandleCheckpoints CheckpointScript;

    //Yes i could have made this private and used a getter and setter, but i have more self resoect than that
    public int CheckpointNumber;

    public bool IsActive;

    

    public void Awake()
    {
        GetCheckpointScriptRef();
    }

    public void GetCheckpointScriptRef()
    {
        CheckpointScript = GameObject.FindObjectOfType<HandleCheckpoints>();
    }


    public void OnTriggerEnter(Collider Colliders)
    {
        if (Colliders.CompareTag("Player"))
        {
            Debug.Log("collision");
            DeleteDelay();
        }
    }

    public void DeleteDelay()
    {
        IsActive = false;

        if(!IsActive)
        {
            CheckpointScript.SetActiveCheckpoint();
        }


    }
}
