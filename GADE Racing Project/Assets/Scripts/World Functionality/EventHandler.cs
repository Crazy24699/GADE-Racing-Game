using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public static EventHandler EventHandlerInstance;
    public UnityEvent RaceFailed;
    public UnityEvent CheckpointTriggered;
    public UnityEvent FinishedRace;
    

    public GameObject PlayerWinPanel;
    public GameObject PlayerLossPanel;
    public GameObject TimePanel;


    void Start()
    {
        EventHandlerInstance = this;

        TimePanel.SetActive(true);
        PlayerLossPanel.SetActive(false);
        PlayerWinPanel.SetActive(false);

        RaceFailed.AddListener(PauseGame);
        RaceFailed.AddListener(() => PlayerLossPanel.SetActive(true));

        FinishedRace.AddListener(PauseGame);
        FinishedRace.AddListener(() => PlayerWinPanel.SetActive(true));

    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        TimePanel.SetActive(false);
    }



}
