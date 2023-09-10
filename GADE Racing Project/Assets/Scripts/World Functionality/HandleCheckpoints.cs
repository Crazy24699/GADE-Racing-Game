
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HandleCheckpoints : MonoBehaviour
{
    public GameObject CheckpointRef;

    public GameObject[] Checkpoints;
    public Stack<GameObject> CheckPointStack = new Stack<GameObject>();

    public GameObject ActiveCheckpoint;

    void Start()
    {
        PopulateCheckpointLocations();
        StartCoroutine(AssignDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator AssignDelay()
    {
        yield return new WaitForSeconds(1);
        EventHandler.EventHandlerInstance.CheckpointTriggered.AddListener(SetActiveCheckpoint);
    }

            //NOTE: Make this a await method and use a while loop to spawn everyting while the game loads
    public void PopulateCheckpointLocations()
    {
        List<GameObject> OrderedCheckpoints = new List<GameObject>();
        int TaggedCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        for (int i = TaggedCheckpoints; i > 0; i--)
        {
            string CheckpointName = string.Format("Checkpoint ({0})", i-1);
            OrderedCheckpoints.Add(GameObject.Find(CheckpointName));
        }
        Checkpoints = OrderedCheckpoints.ToArray();
        Checkpoints.Reverse();

        Debug.Log(Checkpoints.Length);
        foreach (var PointRef in Checkpoints)
        {
            Debug.Log(PointRef.name);
            CheckPointStack.Push(PointRef);
        }
        ActiveCheckpoint.transform.position = CheckPointStack.Peek().transform.position;
        Debug.Log(CheckPointStack.Count);
    }

    //the DisableCheckpoint will find and delete the checkpoint that was last hit, while serving as a reference point to enable the next checkpoint 
    public void SetActiveCheckpoint()
    {
        Debug.Log(CheckPointStack.Peek());
        CheckPointStack.Pop();

        if (CheckPointStack.Count>=1)
        {
            ActiveCheckpoint.transform.position = CheckPointStack.Peek().transform.position;
        }
        else if(CheckPointStack.Count==0)
        {
            EventHandler.EventHandlerInstance.FinishedRace.Invoke();
        }
    }


}
