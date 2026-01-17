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
    public float groundCheckRayLength;
    public Animator animator;
    [HideInInspector]
    [Header("Player Input Actions")]
    public InputAction moveAction { private set; get; }
    public InputAction jumpAction { private set; get; }

    [Space]
    [Header("Stats")]
    public float SpeedChangeRate;
    public float maxSpeed;
    public float jumpHeight;
    public float fallSpeedMax;
    public float defaultGravity;
    [HideInInspector]
    public float gravityScale = 1;

    [Space]
    [Header("Booleans")]
    public bool canMove;



    Coroutine currentAction;
    [Space]
    public bool isGrounded;
    private bool hasDoubleJumped;

    public float direction { set; get; }
    public float lastDirection { private set; get; }
    private AirDash airDash;
    private WallJump wallJump;

    // planning on updating animation enum in this script and having an animator script read from it
    public enum animationEnum
    {
        idle,
        running,
        jumping,
        falling,
        dashing,
        climbing,
    }
    public animationEnum currentAnimation;
    public void WakeUpPlayer()
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
    public void StartPlayer()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        airDash = GetComponent<AirDash>();
        airDash.StartAirDash();
        wallJump = GetComponent<WallJump>();
        wallJump.StartWallJump();
    }

    public void UpdatePlayer()
    {
        
        // set gravity for frame calculations
        airDash.UpdateAirDash();
        wallJump.UpdateWallJump();
        groundCheck();

        if (canMove)
        {
            direction = moveAction.ReadValue<float>();
            if (direction != 0)
            {
                lastDirection = direction;
            }
            run(direction);
            jump();
        }
        playerAnimations();
    }
    private void run(float currentDir)
    {
        if (currentDir != 0)
        {
            // transforms player to face towards direction of movement
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
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            targetSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            targetSpeed = Mathf.Round(targetSpeed * 1000f) / 1000f;
            rb.linearVelocity = new Vector3(targetSpeed * currentDir, rb.linearVelocity.y, 0);
            if (isGrounded)
            {
                currentAnimation = animationEnum.running;
            }
        }
        else
        {
            // sets player velocity to 0 with no input
            rb.linearVelocity = new Vector3(targetSpeed * currentDir, rb.linearVelocity.y, 0);
            if (isGrounded)
            {
                currentAnimation = animationEnum.idle;
            }
        }
    }
    private void jump()
    {
        //switch this to flow smoother with enums and a switch statement. Don't need to be checking if we're grounded and the jump action was completed this frame if we are falling, different logic should be applied

        if (isGrounded && jumpAction.WasPerformedThisFrame())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, rb.linearVelocity.z);
            currentAnimation = animationEnum.jumping;
            isGrounded = false;
        }
        if (rb.linearVelocity.y < fallSpeedMax)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y + (defaultGravity * gravityScale * Time.deltaTime), rb.linearVelocity.z);
        }
    }
    
    void groundCheck() {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Vector3.down, out hit, groundCheckRayLength))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Vector3.down);
    }

    public void updateCoroutine(Coroutine startRoutine)
    {
        currentAction = startRoutine;
        Debug.Log(currentAction);
        StartCoroutine("currentAction");
    }

    void playerAnimations()
    {
        switch (currentAnimation)
        {
            case animationEnum.idle:
                animator.SetBool(1, true);
                break;
            case animationEnum.running:
                animator.SetBool(1, true);

                break;
            case animationEnum.climbing:
                animator.SetBool(1, true);

                break;
            case animationEnum.falling:
                animator.SetBool(1, true);

                break;
            case animationEnum.jumping:
                animator.SetBool(1, true);

                break;
            case animationEnum.dashing:
                animator.SetBool(0, true);

                break;
        }
    }


}
