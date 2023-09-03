using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCheckpoints : MonoBehaviour
{

    public float Size;
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(this.transform.position, Size);
    }



}
