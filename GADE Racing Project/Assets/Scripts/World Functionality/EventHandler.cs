using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public static EventHandler EventHandlerInstance;

    public UnityEvent CheckpointTriggered;
    public UnityEvent StartRace;

    public GameObject StartRacePoint;
    public GameObject PlayerObject;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayerLocation()
    {
        PlayerObject.transform.position = StartRacePoint.transform.position;
        PlayerMovement PlayerMoveScript= PlayerObject.GetComponent<PlayerMovement>();
        PlayerMoveScript.enabled = true;
        Debug.Log(StartRacePoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
