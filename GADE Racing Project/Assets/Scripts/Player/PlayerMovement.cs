
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Net.Sockets;

public class PlayerMovement : MonoBehaviour
{
    [Header("Bools"), Space(5)]
    public bool Grounded;

    [Header("Ints"),Space(5)]
    protected int RotationAngle;
    public int VerticalAngleClamp;
    public int HorizontalAngleClamp;

    [Header("Floats"),Space(5)]
    protected float BaseMoveSpeed;
    public float SpeedMultiplier;
    public float CurrentMoveSpeed;
    protected float ConstantSpeed = 10f;
    public float RotationSpeed = 105f;

    //Time
    public float StabilizeWaitTime;
    public float CurrentStabilizeWaitTime;

    protected float BaseGravity;
    protected float GravityMultiplier;


    [Header("Scripts"), Space(5)]
    public CharacterController PlayerController;
    [SerializeField]protected GameManager GameManagerScript;

    [Header("GameObjects, vectors and transforms"), Space(5)]
    //Vectors
    protected Vector3 PlayerVelocity;
    //transforms
    protected Transform GroundCheck;
    protected Transform DefaultTransformValues;
    //GameObejcts
    public GameObject PlayerRef;
    public GameObject ThrustPosition;

    [Header("Miscellaneous"), Space(5)]
    public LayerMask GroundLayer;    
    public Rigidbody PlayerRigidbody;
    public Camera PlayerCamera;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        PlayerController = PlayerRef.GetComponent<CharacterController>();

        //Looks for rigidbody
        PlayerRigidbody = PlayerRef.GetComponent<Rigidbody>();

        PerformChecks();

        DefaultTransformValues = this.transform;
        
    }

    public void FixedUpdate()
    {
        PlayerMoveFly();
    }

    // Update is called once per frame
    void Update()
    {



        
    }

    

    private void PerformChecks()
    {
        #region Base Move Speed
        if (BaseMoveSpeed <= 0)
        {
            HandleExceptions("BaseSpeed");
        }
        #endregion 

        #region Checking for the game manager script
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
        #endregion

        #region Constant Speed
        if (ConstantSpeed <= 0)
        {
            HandleExceptions("ConstSpeed");
        }
        #endregion

        try
        {
            if (PlayerRigidbody == null)
            {
                HandleExceptions("RBMiss");
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Rigidbody does not exist");
            throw;
        }

    }

    public void HandleExceptions(string SpecificException)
    {
        switch (SpecificException)
        {
            default:
                break;

            case "BaseSpeed":
                BaseMoveSpeed = 10;
                break;

            case "Manager":
                if (BaseGravity == 0)
                {
                    BaseGravity = -9.81f;
                }
                break;

            case "ConstSpeed":
                ConstantSpeed = 10;
                break;
        }

        
    }

    public void PlayerMoveFly()
    {

        CurrentMoveSpeed = (BaseMoveSpeed * SpeedMultiplier) + ConstantSpeed;

        float HorizontalBankDirection = Input.GetAxis("Horizontal");
        float VerticalBankDiredction = Input.GetAxis("Vertical");

        //Quaternion Rotation = Quaternion.Euler(VerticalBankDiredction * SmoothTurningTime, -HorizontalBankDirection * SmoothTurningTime, 0);


        if (Mathf.Abs(VerticalBankDiredction) > 0)
        {
            Debug.Log("BankUp");
            RotatePlayer(0, VerticalBankDiredction, 0, 35);
            CurrentStabilizeWaitTime = 0;
        }
        else if (VerticalBankDiredction == 0)
        {
            StabilizePlayerRotation();
        }

        if (Mathf.Abs(VerticalBankDiredction) != 0)
        {

        }
        else if (Mathf.Abs(VerticalBankDiredction) <= 0.2 && transform.rotation.x != 0)
        {

            //repurpos this to fix the rotation of the 3d model.
            //float RotationIncriment = RotationSpeed;

            //this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, this.transform.rotation.y, this.transform.rotation.z), RotationIncriment);

            //StablizePlayer();
            //float RotationIncriment = (Mathf.Abs(HoriDirection) >= 0.5f) ? HoriDirection * Time.deltaTime : VertDirection * Time.deltaTime;
            //Debug.Log("Fai  ");
        }

        if (Mathf.Abs(HorizontalBankDirection) != 0)
        {
            //RotatePlayer(HorizontalBankDirection+(HorizontalBankDirection*1),0, 45,Mathf.RoundToInt(this.transform.rotation.eulerAngles.y));
            RotatePlayer(HorizontalBankDirection,0,45,0); ;
            CurrentStabilizeWaitTime = 0;
        }

        else if (Mathf.Abs(HorizontalBankDirection) <= 0.2 && transform.rotation.eulerAngles.y != 0)
        {

            //repurpos this to fix the rotation of the 3d model.
            //float RotationIncriment = RotationSpeed ;

            //this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,0), RotationIncriment);

            //Debug.Log("Fai  ");
        }

        transform.Translate(Vector3.forward * Time.deltaTime * CurrentMoveSpeed);

        
        string Values=string.Format("Horizontal Bank Value: {0}   "+"Vertical Bank Value: {1}",HorizontalBankDirection,VerticalBankDiredction);

    }

    
    public void StabilizePlayerRotation()
    {
        CurrentStabilizeWaitTime += Time.deltaTime;

        if (Mathf.Abs(CurrentStabilizeWaitTime) >= StabilizeWaitTime)
        {
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0), 10* Time.deltaTime);
        }

    }

    public void StablizePlayer()
    {
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0), RotationSpeed);
    }

    public void RotatePlayer(float HoriDirection,float VertDirection, int MaxHoriRoation, int MaxVertRoation)
    {
        Debug.Log(HoriDirection+"       "+VertDirection);
        float RotationIncriment;
        float InputDirection = (Mathf.Abs(HoriDirection) >= 0.2f) ?   RotationIncriment = (HoriDirection * Time.deltaTime)  : RotationIncriment = ( VertDirection * Time.deltaTime);

        float TurningDirection = (InputDirection >= 0.5) ? RotationIncriment : RotationIncriment;

       


        transform.Rotate(TurningDirection * MaxVertRoation * RotationSpeed, TurningDirection * MaxHoriRoation * RotationSpeed,0);

        //Quaternion RotationTarget = Quaternion.Euler(0, Mathf.RoundToInt(IncrimentDIrection) * MaxRoation, 0);

        //this.transform.rotation = Quaternion.RotateTowards(transform.rotation, RotationTarget, RotationIncriment);
    }

    //Test Move method
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


