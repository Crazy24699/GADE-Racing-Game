using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public GameObject PlayerRef;
    
    public float MoveSpeed = 50;

    public float CurrentMoveSpeed;

    public Rigidbody Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveMethodPH();
    }

    public void HandleExceptions()
    {
        if (MoveSpeed == 0)
        {
            MoveSpeed = 50;
        }



    }

    public void MoveMethodPH()
    {
        float HorizontalSpeed = Input.GetAxis("Horizontal");
        float VerticalSpeed = Input.GetAxis("Vertical");

        CurrentMoveSpeed = HorizontalSpeed;

        Vector3 MoveDirection = new Vector3(HorizontalSpeed * MoveSpeed, Rigidbody.velocity.y, VerticalSpeed * MoveSpeed);

        Rigidbody.velocity = MoveDirection;
        

    }
}


