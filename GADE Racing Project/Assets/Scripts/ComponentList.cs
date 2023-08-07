using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentList : MonoBehaviour
{


    public Rigidbody EntityRigidbody;
    public Collider EntityCollider;

    


    //will check if the player has all the components needed for functionality 
    public void CheckPlayerComponents(GameObject CheckingObject, string SkipComponentCheck)
    {
        if(SkipComponentCheck=="" || SkipComponentCheck == null)
        {

        }
        else
        {
            switch (SkipComponentCheck.ToLower())
            {
                default:
                case "entityrigidbody":
                    break;
            }
        }
    }

}
