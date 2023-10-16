
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline;
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
        //EventHandler.EventHandlerInstance.CheckpointTriggered.AddListener(SetActiveCheckpoint);
        PopulateCheckpointLocations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

            //NOTE: Make this a await method and use a while loop to spawn everyting while the game loads
    public void PopulateCheckpointLocations()
    {
        List<GameObject> OrderedCheckpoints = new List<GameObject>();
        int TaggedCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        for (int i = TaggedCheckpoints; i > 0; i--)
        {
            string CheckpointName;
            if (i == TaggedCheckpoints)
            {
                CheckpointName = ("Start Of Race");
            }
            else
            {
                CheckpointName = string.Format("Checkpoint ({0})", i-1);
            }

            
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
        

        ActiveCheckpoint.transform.position = CheckPointStack.Peek().transform.position;

    }


}
