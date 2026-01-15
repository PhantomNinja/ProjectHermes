using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;


// using some aspects of sethpoly celeste-character-controller
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [HideInInspector]
    [Header("Player")]
    public Rigidbody rb;

    [HideInInspector]
    [Header("Player Input Actions")]
    private InputAction moveAction;
    private InputAction jumpAction;



    [Space]
    [Header("Stats")]
    public float SpeedChangeRate = 5.0f;
    public float maxSpeed = 4;
    public float jumpHeight = 7;
    public float fallSpeedMax = 10;
    public float slideSpeed = 1;
    public float wallJumpLerp = 5;
    public float dashSpeed = 8;
    public float defaultGravity = 8;
    [HideInInspector]
    public float gravityScale = 1;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;


    Coroutine currentAction;
    [Space]
    public bool isGrounded;
    private bool hasDoubleJumped;

    private float lastDir;
    private AirDash airDash;

    // planning on updating animation enum in this script and having an animator script read from it
    public enum animationEnum
    {
        idle,
        running,
        jumping,
        falling,
        dashing,
    }
    public animationEnum currentAnimation;
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        airDash = GetComponent<AirDash>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

    }

    private void Update()
    {

        // set gravity for frame calculations
        defaultGravity = defaultGravity * gravityScale;
        
        groundCheck();
        float playerInput = moveAction.ReadValue<float>();
        run(playerInput);
        jump();
        
    }
    private void run(float currentDir)
    {
        if (currentDir != 0)
            lastDir = currentDir;
        // transforms player to face towards direction of movement
        if (currentDir != 0) {
            transform.localScale = new Vector3(currentDir, transform.localScale.y, transform.localScale.z); 
        }
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = maxSpeed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        float speedOffset = 0.1f;

        // accelerate or decelerate to target speed
        
        
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset ||
            airDash.isDashing)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            targetSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            targetSpeed = Mathf.Round(targetSpeed * 1000f) / 1000f;
            if(airDash.isDashing == false)
                rb.linearVelocity = new Vector3(targetSpeed * currentDir, rb.linearVelocity.y, 0);
            else
                rb.linearVelocity = new Vector3(targetSpeed * lastDir, rb.linearVelocity.y, 0);
        }
        else
        {
            // sets player velocity to 0 with no input
            rb.linearVelocity = new Vector3(targetSpeed * currentDir, rb.linearVelocity.y, 0);
        }
    }
    private void jump()
    {
        //switch this to flow smoother with enums and a switch statement. Don't need to be checking if we're grounded and the jump action was completed this frame if we are falling, different logic should be applied

        if (isGrounded && jumpAction.WasPerformedThisFrame())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, rb.linearVelocity.z);
        }
        if (rb.linearVelocity.y < fallSpeedMax)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y + (defaultGravity * Time.deltaTime), rb.linearVelocity.z);
        }
    }
    
    void groundCheck() {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Vector3.down, out hit, 1.0f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Vector3.down, Color.yellow);
    }

    public void updateCoroutine(Coroutine startRoutine)
    {
        currentAction = startRoutine;
        Debug.Log(currentAction);
        StartCoroutine("currentAction");
    }
}
