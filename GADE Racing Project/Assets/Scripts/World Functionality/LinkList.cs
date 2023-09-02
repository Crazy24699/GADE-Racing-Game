using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinkList : MonoBehaviour
{

    public List<GameObject> CheckPoints;
    public Stack<GameObject> CheckPointStack = new Stack<GameObject>();
    public string RemovedCheckPoint;

    void Start()
    {
        WayPointManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && CheckPointStack.Count != 0) 
        {
            CheckPointStack.Pop();

        }

        else if(CheckPointStack.Count == 0)
        {
            Debug.Log("stack empty");
        }

    }

    public void WayPointManager()
    {
        

        foreach (GameObject Point in CheckPoints)
        {
            CheckPointStack.Push(Point);
            
        }
        
    }
}
