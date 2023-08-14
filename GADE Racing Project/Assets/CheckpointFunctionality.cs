using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointFunctionality : MonoBehaviour
{
    private HandleCheckpoints CheckpointScript;

    public void Start()
    {
        GetCheckpointScriptRef();
    }

    public void GetCheckpointScriptRef()
    {
        CheckpointScript = GameObject.FindObjectOfType<HandleCheckpoints>();
    }


    public void OnTriggerEnter(Collider PlayerCollider)
    {
        
    }
}
