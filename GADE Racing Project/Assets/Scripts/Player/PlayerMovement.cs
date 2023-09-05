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
    public float SmoothTurningTime = 0.25f;
    private float TurnVelocity;

    protected float BaseGravity;
    protected float GravityMultiplier;


    [Header("Scripts"), Space(5)]
    public CharacterController PlayerController;
    [SerializeField]protected GameManager GameManagerScript;

    [Header("GameObjects, vectors and transforms"), Space(5)]
    public GameObject PlayerRef;
    protected Vector3 PlayerVelocity;
    protected Transform GroundCheck;
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

        if (Mathf.Abs(HorizontalBankDirection) != 0 || Mathf.Abs(VerticalBankDiredction) != 0)
        {
            

        }

        this.transform.Rotate(this.transform.rotation.x - VerticalBankDiredction * -1, this.transform.rotation.y - HorizontalBankDirection * -1, 0);

        //PlayerRigidbody.velocity = Vector3.forward * (BaseMoveSpeed * SpeedMultiplier);
        transform.Translate(Vector3.forward * Time.deltaTime * BaseMoveSpeed);
        //PlayerRigidbody.MoveRotation(PlayerRigidbody.rotation * Rotation);

        //PlayerRigidbody.AddForce(transform.forward * CurrentMoveSpeed);


        string Values=string.Format("Horizontal Bank Value: {0}   "+"Vertical Bank Value: {1}",HorizontalBankDirection,VerticalBankDiredction);
        Debug.Log(Values);


        //CurrentMoveSpeed = (BaseMoveSpeed * SpeedMultiplier) + ConstantSpeed;

        //float HorizontalBankDirection = Input.GetAxisRaw("Horizontal");
        //float VerticalBankDiredction = Input.GetAxisRaw("Vertical");


        //Vector3 MoveDirection = new Vector3(HorizontalBankDirection, VerticalBankDiredction, 20).normalized;

        //if (MoveDirection.magnitude >= 0.1f)
        //{
        //    float FinalTurnAngle = Mathf.Atan2(MoveDirection.x, MoveDirection.z) * Mathf.Rad2Deg;
        //    float CurrentTurnAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, FinalTurnAngle, ref TurnVelocity, SmoothTurningTime);
        //    transform.rotation = Quaternion.Euler(0, CurrentTurnAngle, 0);
        //}

        //PlayerRigidbody.velocity = MoveDirection * (BaseMoveSpeed * SpeedMultiplier);
        //string Values = string.Format("Horizontal Bank Value: {0}   " + "Vertical Bank Value: {1}", HorizontalBankDirection, VerticalBankDiredction);
        //Debug.Log(Values);

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


