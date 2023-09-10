using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public static EventHandler EventHandlerInstance;
    public UnityEvent RaceFailed;
    public UnityEvent CheckpointTriggered;


    void Start()
    {
        RaceFailed.AddListener(PlayerLostRace);
    }
    public void PlayerLostRace()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
