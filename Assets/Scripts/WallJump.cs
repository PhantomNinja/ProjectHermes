using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallJump : MonoBehaviour
{

    InputAction jumpAction;
    bool m_HitDetect;
    RaycastHit m_Hit;
    PlayerController player;
    bool isWallSliding;
    [Header("Wall jump detection size")]
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Vector3 wallCheckBoxSize;
    [SerializeField] float m_MaxDistance;
    public GameObject wallCheck;

    [Space]
    [Header("Wall Jump variables")]
    [SerializeField] float wallSlidingSpeed;
    [SerializeField] float wallJumpingTime;
    private float wallJumpingCounter;
    [SerializeField] float wallJumpingDuration;
    [SerializeField] Vector3 wallJumpingPower;
    bool isWallJumping;
    float wallJumpingDirection;


    public void StartWallJump()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        player = PlayerController.instance;
    }

    public void UpdateWallJump()
    {

        WallSlide();
        wallJump();
    }
    bool isWall()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(wallCheck.transform.position, wallCheckBoxSize * 0.5f, wallCheck.transform.forward, out m_Hit, wallCheck.transform.rotation, m_MaxDistance, wallLayer);

        return m_HitDetect;
    }
    private void WallSlide()
    {

        if (isWall() && !player.isGrounded && player.direction != 0.0f)
        {
            player.currentAnimation = PlayerController.animationEnum.climbing;
            isWallSliding = true;
            player.rb.linearVelocity = new Vector3(0, -wallSlidingSpeed, player.rb.linearVelocity.z);
            player.gravityScale = 0.0f;
            player.currentAnimation = PlayerController.animationEnum.climbing;
            
        }
        else
        {
            isWallSliding = false;
            player.gravityScale = 1.0f;
        }
    }
    private void wallJump()
    {

        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -player.direction;
            wallJumpingCounter = wallJumpingTime;
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (jumpAction.WasPerformedThisFrame() && wallJumpingCounter > 0f)
        {
            Debug.Log("wallJump success");
            player.currentAnimation = PlayerController.animationEnum.jumping;
            player.canMove = false;
            isWallJumping = true;
            player.rb.linearVelocity = new Vector3(wallJumpingPower.x * wallJumpingDirection, wallJumpingPower.y, 0);
            // set player direction 
            player.transform.localScale = new Vector3(wallJumpingDirection, player.transform.localScale.y,player.transform.localScale.z);
            player.direction = player.transform.localScale.x;
        
            wallJumpingCounter = 0f;

            Invoke(nameof(stopWallJump), wallJumpingDuration);
        }

    }

    private void stopWallJump()
    {
        player.currentAnimation = PlayerController.animationEnum.idle;
        Debug.Log("Stopped walljump");
        isWallJumping = false;
        player.canMove = true;
    }
    
    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(wallCheck.transform.position, wallCheck.transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(wallCheck.transform.position + wallCheck.transform.forward * m_Hit.distance, wallCheckBoxSize);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(wallCheck.transform.position, wallCheck.transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(wallCheck.transform.position + wallCheck.transform.forward * m_MaxDistance, wallCheckBoxSize);
        }
    }
}
