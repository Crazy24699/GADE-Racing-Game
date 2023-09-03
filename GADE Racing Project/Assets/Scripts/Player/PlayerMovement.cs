using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Bools"), Space(5)]
    public bool Grounded;

    [Header("Floats"),Space(5)]
    public float BaseMoveSpeed;
    public float SpeedMultiplier;
    public float CurrentMoveSpeed;
    protected float BaseGravity;
    public float GravityMultiplier;

    [Header("Scripts"), Space(5)]
    public CharacterController PlayerController;
    [SerializeField]protected GameManager GameManagerScript;

    [Header("GameObjects, vectors and transforms"), Space(5)]
    public GameObject PlayerRef;
    protected Vector3 PlayerVelocity;
    protected Transform GroundCheck;

    [Header("Miscellaneous"), Space(5)]
    public LayerMask GroundLayer;    

    // Start is called before the first frame update
    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        PlayerController = PlayerRef.GetComponent<CharacterController>();

        PerformChecks();

        
    }

    // Update is called once per frame
    void Update()
    {
        MoveMethod();

    }

    private void PerformChecks()
    {
        if (BaseMoveSpeed <= 0)
        {
            HandleExceptions("Speed");
        }

        try
        {
            GameManagerScript = GameManager.FindObjectOfType<GameManager>();
            if (GameManagerScript == null)
            {
                HandleExceptions("Manager");
            }
        }
        catch (System.Exception Ex)
        {

            Debug.Log(Ex + "        GameManager doesnt exist");
            throw;
        }

    }

    public void HandleExceptions(string SpecificException)
    {
        switch (SpecificException)
        {
            default:
                break;

            case "Speed":
                BaseMoveSpeed = 10;
                break;

            case "Manager":
                if (BaseGravity == 0)
                {
                    BaseGravity = -9.81f;
                }
                break;
        }





    }

    public void MoveMethod()
    {
        //Gravity
        //PlayerVelocity.y += BaseGravity * Time.deltaTime;
        //PlayerController.Move(PlayerVelocity * Time.deltaTime);

        //Player Input
        float HorizontalSpeed = Input.GetAxis("Horizontal");
        float VerticalSpeed = Input.GetAxis("Vertical");

        CurrentMoveSpeed = BaseMoveSpeed * SpeedMultiplier;
        Vector3 Movement = transform.right * HorizontalSpeed + transform.forward * VerticalSpeed;

        PlayerController.Move(Movement * CurrentMoveSpeed * Time.deltaTime);


    }
}


