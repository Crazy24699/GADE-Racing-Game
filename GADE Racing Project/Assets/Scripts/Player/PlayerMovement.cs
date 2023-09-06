using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Bools"), Space(5)]
    public bool Grounded;

    protected int RotationAngle;

    [Header("Floats"),Space(5)]
    protected float BaseMoveSpeed;
    public float SpeedMultiplier;
    public float CurrentMoveSpeed;
    protected float ConstantSpeed = 10f;
    public float RotationSpeed = 105f;
    private float TurnVelocity;

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

        //Debug.Log(transform.rotation.eulerAngles);
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

        if (Mathf.Abs(HorizontalBankDirection) != 0 || Mathf.Abs(VerticalBankDiredction) != 0)
        {
            

        }

        if (Mathf.Abs(VerticalBankDiredction) != 0)
        {
            RotatePlayer(0, VerticalBankDiredction, Mathf.RoundToInt(this.transform.rotation.y), 35);
        }
        else if (Mathf.Abs(VerticalBankDiredction) <= 0.2 && transform.rotation.x != 0)
        {

            //repurpos this to fix the rotation of the 3d model.
            //float RotationIncriment = RotationSpeed;

            //this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, this.transform.rotation.y, this.transform.rotation.z), RotationIncriment);

            StablizePlayer();
            //Debug.Log("Fai  ");
        }

        if (Mathf.Abs(HorizontalBankDirection) != 0)
        {
            RotatePlayer(HorizontalBankDirection,0, 45,Mathf.RoundToInt(this.transform.rotation.eulerAngles.y));
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
        //Debug.Log(Values);


    }

    public void StablizePlayer()
    {
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z), RotationSpeed);
    }

    public void RotatePlayer(float HoriDirection,float VertDirection, int MaxHoriRoation, int MaxVertRoation)
    {
        float RotationIncriment = RotationSpeed * Time.deltaTime;


        transform.Rotate(Mathf.RoundToInt(VertDirection) * MaxVertRoation * RotationIncriment, Mathf.RoundToInt(HoriDirection) * MaxHoriRoation * RotationIncriment, this.transform.rotation.z);
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


