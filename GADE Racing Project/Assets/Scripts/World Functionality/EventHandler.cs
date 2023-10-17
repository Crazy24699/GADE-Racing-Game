using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public static EventHandler EventHandlerInstance;

    public UnityEvent CheckpointTriggered;
    public UnityEvent StartRace;

    public GameObject StartRacePoint;
    public GameObject PlayerObject;

    public string CurrentScene;

    public string[] TotalScenes;

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

    public void PopulateSceneArray()
    {
        
        
    }

    public void AddPlayer()
    {
        if (!CurrentScene.Contains("Main Menu"))
        {
            if(PlayerObject==null)
            {
                PlayerObject = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }

}
